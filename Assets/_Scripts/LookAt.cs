using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LookAt : MonoBehaviour {
    
    private Transform camT;

    private void Start(){
        camT = Camera.main.transform;
    }
    private void Update(){
        transform.LookAt(camT);
    }
}
