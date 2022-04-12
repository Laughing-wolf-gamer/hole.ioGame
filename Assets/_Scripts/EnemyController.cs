using UnityEngine;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {
    
    [SerializeField] private HoleController playerHole;
    [SerializeField] private List<BoxCollider> collidersArray;
    [SerializeField,Tooltip("Only Ad AI Holes to the List")] private List<AIHoles> holesList;

    private void Start(){
        playerHole.OnObjectFallInHole += () =>{
            TryRemoveColliders();
        };
    }

    public List<BoxCollider> GetColliders{
        get{
            return collidersArray;
        }
    }
    
    public void GetNewDistantion(){
        for (int i = 0; i < holesList.Count; i++){
            int Rand = UnityEngine.Random.Range(0,collidersArray.Count);
            holesList[i].SetDestination(collidersArray[Rand].transform);
            TryRemoveColliders(collidersArray[Rand]);
        }
    }
    public void TryRemoveColliders(BoxCollider coli = null){
        if(coli != null){
            collidersArray.Remove(coli);
        }
        for (int i = 0; i < collidersArray.Count; i++){
            if(!collidersArray[i].gameObject.activeSelf){
                collidersArray.Remove(collidersArray[i]);
            }
        }
    }
}
