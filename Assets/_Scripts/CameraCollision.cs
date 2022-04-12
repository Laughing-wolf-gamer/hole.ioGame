using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraCollision : MonoBehaviour{

    [SerializeField] private float rayLength = 2f;
    [SerializeField] private LayerMask checkMask;
    [SerializeField] private Material newTranspernetMat;
    [SerializeField] private List<ObjectCollidedCheck> objectCollidedChecks;
    public static CameraCollision i{get;private set;}
	private void Awake(){
		i = this;
	}
    private void Update(){
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward,out hit , rayLength,checkMask,QueryTriggerInteraction.Ignore)){
            if(hit.collider != null){
                if(!AlreadyHasThisColliderInList(hit.collider)){
                    Renderer hitRenderer = hit.collider.GetComponent<Renderer>();
                    if(hitRenderer != null){
                        objectCollidedChecks.Add(new ObjectCollidedCheck{isColliding = true,colliededWith = hit.collider,currentMat = hitRenderer.sharedMaterial});
                        CheangeToTransparent();
                    }
                    
                }else{
                    CheangeToTransparent(hit.collider);
                }
                
            }
            
        }else{
            ChangeAllToDefult();
        }
    }
    public void AddCollider(Collider coli){
        if(!AlreadyHasThisColliderInList(coli)){
            objectCollidedChecks.Add(new ObjectCollidedCheck{isColliding = true,colliededWith = coli,currentMat = coli.GetComponent<Renderer>().sharedMaterial});
        }
    }
    
    private bool AlreadyHasThisColliderInList(Collider objectCollided){
        if(objectCollidedChecks.Count > 0){
            for (int i = 0; i < objectCollidedChecks.Count; i++){
                if(objectCollidedChecks[i].colliededWith == objectCollided){
                    objectCollidedChecks[i].isColliding = true;
                    return true;
                }
            }
        }
        return false;
    }
    private void CheangeToTransparent(){
        if(objectCollidedChecks.Count > 0){
            for (int i = 0; i < objectCollidedChecks.Count; i++){
                if(objectCollidedChecks[i].isColliding){
                    objectCollidedChecks[i].colliededWith.GetComponent<Renderer>().sharedMaterial = newTranspernetMat;
                }
                
            }
        }
    }
    private void CheangeToTransparent(Collider privousCollider){
        if(objectCollidedChecks.Count > 0){
            for (int i = 0; i < objectCollidedChecks.Count; i++){
                
                if(objectCollidedChecks[i].colliededWith == privousCollider){
                    objectCollidedChecks[i].colliededWith.GetComponent<Renderer>().sharedMaterial = newTranspernetMat;
                    objectCollidedChecks[i].isColliding = true;
                }
                // objectCollidedChecks[i].ChangeToDefultMat();
                
            }
        }
    }
    private void ChangeAllToDefult(){
        if(objectCollidedChecks.Count > 0){
            for (int i = 0; i < objectCollidedChecks.Count; i++){
                objectCollidedChecks[i].ChangeToDefultMat();
                objectCollidedChecks.Remove(objectCollidedChecks[i]);
                
            }
        }
    }
    
    
}
[System.Serializable]
public class ObjectCollidedCheck{
    public bool isColliding;
    public Collider colliededWith;
    public Material currentMat;
    public void ChangeToDefultMat(){
        isColliding = false;
        colliededWith.GetComponent<Renderer>().sharedMaterial = currentMat;
    }
}
