using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObjects : MonoBehaviour
{
    public class HiddenOb
    {
        public HiddenOb(int TicksToShow, Renderer renderer, Material mat)
        {
            this.TicksToShow = TicksToShow;
            this.renderer = renderer;
            oldMat = this.renderer.material;
            this.renderer.material = mat;
        }
        private int TicksToShow;
        public Renderer renderer;
        private Material oldMat;
        public void ResetTicks(int value)
        {
            TicksToShow = value;
        }
        public bool AddTick()
        {
            TicksToShow -= 1;
            if (TicksToShow <= 0)
            {
                renderer.material = oldMat;
                return true;
            }
            return false;
        }
    }
    public Material Fade;
    public Material Opaque;
    public Transform target;
    Camera cam;
    Vector3[] ClipPoints;
    public void Start()
    {
        StartCoroutine(ShowObjects());
        cam = GetComponent<Camera>();
    }
    [Range(2, 3)]
    public float vl = 2.24f;
    public float ds = 0f;
    void UpdateClipPoints(Quaternion rot, Vector3 pos)
    {
        ClipPoints = new Vector3[5];
        float z = cam.nearClipPlane;
        float x = Mathf.Tan(cam.fieldOfView / vl) * z;
        float y = x / cam.aspect;
        ClipPoints[0] = (rot * new Vector3(-x, y, z)) + pos;
        ClipPoints[1] = (rot * new Vector3(x, y, z)) + pos;
        ClipPoints[2] = (rot * new Vector3(-x, -y, z)) + pos;
        ClipPoints[3] = (rot * new Vector3(x, -y, z)) + pos;
        ClipPoints[4] = pos - transform.forward;
        ds = Vector3.Distance(ClipPoints[4], target.position + offset);
    }
    public Dictionary<Collider, HiddenOb> Hidden = new Dictionary<Collider, HiddenOb>();
    public Vector3 offset = Vector3.up;
    void Update()
    {
        UpdateClipPoints(transform.rotation, transform.position);
        foreach (Vector3 v3 in ClipPoints)
        {
            RaycastHit[] hits;
            Vector3 p = target.position + offset;
            hits = Physics.RaycastAll(p, v3 - p, ds);
            foreach (var hit in hits)
            {
                if (hit.collider.tag == "buildings")
                {
                    HiddenOb current;
                    if (Hidden.TryGetValue(hit.collider, out current))
                    {
                        current.ResetTicks(10);
                    }
                    else
                    {
                        current = new HiddenOb(10, hit.collider.GetComponent<Renderer>(), Fade);
                        Hidden.Add(hit.collider, current);
                    }
                }
            }
            Debug.DrawRay(p, (v3 - p).normalized * ds, Color.white);
        }

        Debug.Log(Hidden.Count);
    }
    List<Collider> Remove = new List<Collider>();
    IEnumerator ShowObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.10f);
            foreach (var item in Hidden.Keys)
            {
                if (Hidden[item].AddTick())
                    Remove.Add(item);
            }
            lock (Hidden)
            {
                foreach (var item in Remove)
                {
                    Hidden.Remove(item);
                }
            }
            Remove = new List<Collider>();
        }
    }
}
