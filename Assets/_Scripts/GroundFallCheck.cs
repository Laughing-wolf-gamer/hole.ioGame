using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFallCheck : MonoBehaviour{
    
    [SerializeField] private Hole hole;
    private void OnTriggerEnter(Collider coli){
        coli.gameObject.SetActive(false);
        hole.IncreaseScore(1);
    }
}
