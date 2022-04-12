using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remap : MonoBehaviour {

    // Use this for initialization
    public Vector2 range1;
    public Vector2 range2;
    public float current;
    public float Val;
    public float Percentage;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        Val = current.Remap(range1.x,range1.y, range2.x,range2.y)*100;
        Percentage = 100 - Val;

    }
}
