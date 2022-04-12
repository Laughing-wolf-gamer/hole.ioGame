using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AICollectorTrigger : MonoBehaviour{
    [SerializeField] private AIHoles hole;
    private void OnTriggerEnter(Collider coli){
        coli.gameObject.SetActive(false);
        hole.IncreaseScore(2);
    }
}
