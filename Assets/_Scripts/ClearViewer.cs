using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearViewer : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    Material OldMat, CurrentMat;
    void FixedUpdate()
    {
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            GameObject hitObj = hit.collider.gameObject;

            if (hitObj.tag.Contains("buildings"))
            {

                CurrentMat = hitObj.GetComponent<Renderer>().material;
                CurrentMat.SetColor("_Color", new Color(1, 1, 1, 0.1f));
                if (OldMat != CurrentMat)
                {
                    if (OldMat != null) OldMat.SetColor("_Color", new Color(1, 1, 1, 1));
                }
                OldMat = CurrentMat;
            }

        }
        else
        {

            if (OldMat != null) OldMat.SetColor("_Color", new Color(1, 1, 1, 1));

        }
    }
}
