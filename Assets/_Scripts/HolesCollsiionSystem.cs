using UnityEngine;
using System.Collections;

public class HolesCollsiionSystem : MonoBehaviour{
    
    public enum HoleType{
        Player,AI
    }
    [SerializeField] private HoleType holeType;
    [SerializeField] private float checkSize;
    [SerializeField] private Vector3 offset;
    [SerializeField] private LayerMask checkMask;
    private float currentSteerAngle;
    private HoleController holeController;
    private Collider myCollider;
    private bool isCheckingBourdary;
    
    
    private void Start(){
        holeController = GetComponent<HoleController>();
        myCollider = GetComponent<Collider>();
    }
    

    private void Update(){
        float newSize = transform.localScale.x * checkSize;
        Collider[] holesColider = Physics.OverlapSphere(transform.position + offset,newSize,checkMask,QueryTriggerInteraction.Collide);
        if(holesColider.Length > 0){
            for (int h = 0; h < holesColider.Length; h++){
                if(holesColider[h].gameObject.CompareTag("Bourndary")){
                    holeController.onOutSideBoudary?.Invoke();
                }
                if(holesColider[h] != myCollider){
                    HoleController holes = holesColider[h].GetComponent<HoleController>();
                    if(holes != null){
                        if(transform.localScale.x > holes.transform.localScale.x){
                            holeController.OnObjectFallInHole?.Invoke();
                            holes.SetDeath();
                            if(holeType == HoleType.Player){
                                holeController.SetKillCount();
                            }
                            if(holes.GetComponent<Hole>() != null){
                                GameHandler.i.SetKilledByName(holeController.GetHolesGroupData().LeaderName);
                                GameHandler.i.EndGame(true);
                            }
                            holes.gameObject.SetActive(false);
                        }else if(transform.localScale.x < holes.transform.localScale.x){
                            holeController.SetDeath();
                            if(holeType == HoleType.Player){
                                GameHandler.i.SetKilledByName(holes.GetHolesGroupData().LeaderName);
                                GameHandler.i.EndGame(true);
                            }else{
                                gameObject.SetActive(false);
                            }
                        }
                    }
                    
                }
                if(holeType == HoleType.AI){
                    if(holesColider[h].CompareTag("Bourndary") && !isCheckingBourdary){
                        isCheckingBourdary = true;
                        StopCoroutine(nameof(ChangeSteerAngleRoutine));
                        StartCoroutine(nameof(ChangeSteerAngleRoutine));
                    }

                }
                if(holesColider[h].TryGetComponent<BoxCollider>(out BoxCollider box)){
                    if(box.size.x > 10f){
                        CameraCollision.i.AddCollider(holesColider[h]);
                    }
                }
            }

        }
        
    }
    private void OnCollisionEnter(Collision coli){
        
    }
    private IEnumerator ChangeSteerAngleRoutine(){
        float randTime = UnityEngine.Random.Range(1f,5f);
        yield return new WaitForSeconds(randTime);
        float[] steerAngle = new float[]{-1f,0f,1f};
        int rand = UnityEngine.Random.Range(0,steerAngle.Length);
        currentSteerAngle = rand;
        yield return new WaitForSeconds(0.2f);
        currentSteerAngle = 0f;
        yield return StartCoroutine(ChangeSteerAngleRoutine());
        isCheckingBourdary = false;
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        float newSize = transform.localScale.x * checkSize;
        Gizmos.DrawWireSphere(transform.position + offset,newSize);
    }
}
