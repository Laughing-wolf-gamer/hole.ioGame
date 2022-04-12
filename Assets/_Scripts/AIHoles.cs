using UnityEngine;
using System.Collections;

public class AIHoles : HoleController {
    
    [SerializeField] private EnemyController enemyController;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 pos; 
    private Transform thisTrans;
    
    private Transform movePoint;


    private float steeringAngle;
    protected override void Start(){
        thisTrans = transform;
        enemyController.GetNewDistantion();
        StartCoroutine(MoveRoutine());
        StartCoroutine(StartCollection());
    }
    public void SetDestination(Transform point){
        movePoint = point;
    }
    private IEnumerator MoveRoutine(){
        while(!groupData.isDead){
            if(isGameStart){

                if(movePoint != null){
                    if(Vector3.Distance(transform.position, new Vector3(movePoint.position.x,transform.position.y,movePoint.position.z)) >= 0.01f){

                        if(movePoint.gameObject.activeSelf){
                            transform.position = Vector3.MoveTowards(transform.position,new Vector3(movePoint.position.x,transform.position.y,movePoint.position.z),holeMoveSpeed * Time.deltaTime);
                            transform.LookAt(new Vector3(movePoint.position.x,transform.position.y,movePoint.position.z));
                        }else{
                            movePoint = null;
                        }
                    }else{
                        yield return StartCoroutine(SteeringRoutine());
                        yield break;
                    }
                }
                else{
                    yield return StartCoroutine(SteeringRoutine());
                    yield break;
                }
            }
            yield return null;

        }
    }
    private IEnumerator StartCollection(){
        while(!groupData.isDead){
            if(isGameStart){
                if(movePoint != null){
                    IncreaseScore(2);
                }
            }
            float rand = Random.Range(7,10f);
            yield return new WaitForSeconds(rand);
        }
    }

    private IEnumerator SteeringRoutine(){
        float x = 0.1f;
        int[] radomAngle = new int[]{90,0,-90};
        while(x >= 0f){
            x -= Time.deltaTime;
            // Debug.Log(x);
            int Rand = UnityEngine.Random.Range(0,radomAngle.Length);
            transform.localEulerAngles = new Vector3(0f,radomAngle[Rand],0f);
            yield return new WaitForSeconds(1f);
        }
        enemyController.GetNewDistantion();
        yield return StartCoroutine(MoveRoutine());
    }  
    public void SetDeth(){
        groupData.isDead = true;
        groupData.indicatorInstance.OnDeath();
    }
}
// Checking for Closest Object........
// public class DistanceCompare : IComparer{
//     public Transform comparingTransform;
//     public DistanceCompare(Transform compTransform){
//         comparingTransform = compTransform;
//     }
//     public int Compare(object x, object y){
//         Collider xCollider = x as Collider;
//         Collider yCollider = y as Collider;
//         Vector3 offset = xCollider.transform.position - comparingTransform.position;
//         float xDistance = offset.sqrMagnitude;
//         offset = yCollider.transform.position - comparingTransform.position;
//         float ydistance = offset.sqrMagnitude;
//         return xDistance.CompareTo(ydistance);
//     }
// }
