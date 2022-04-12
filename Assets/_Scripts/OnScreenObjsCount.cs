using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenObjsCount : MonoBehaviour
{

    // Use this for initialization
    public static int ObjectCount;
    void OnBecameVisible()
    {
        ObjectCount++;
    }

    // Update is called once per frame
    void OnBecameInvisible()
    {
        ObjectCount--;
    }
}
