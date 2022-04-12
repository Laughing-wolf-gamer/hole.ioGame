using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllNodes : MonoBehaviour
{

    // Use this for initialization

    public List<Transform> Nodes = new List<Transform>();
    public static AllNodes Instance;

    private void OnEnable()

    {
        Instance = this;
        startCollectingNodes();
    }

    public void startCollectingNodes()
    {
        Instance = this;
        foreach (var trans in gameObject.GetComponentsInChildren<Transform>())
        {

            if (transform != trans)
            {
                Nodes.Add(trans);
                trans.GetComponent<Renderer>().enabled = false;
            }
        }
        Debug.Log("Node child count is " + Nodes.Count);
    }

    public Transform getOneRandomNode()
    {

        return Nodes[AceHelper.GetRandomNumberFromArray(Nodes)];
    }


}
