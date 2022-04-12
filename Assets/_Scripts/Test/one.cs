using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class one : MonoBehaviour {
  public  two instance;
	// Use this for initialization
	void Start () {

        instance= GetComponent<three>().obj.GetComponent<two>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
