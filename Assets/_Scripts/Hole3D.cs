using UnityEngine;

public class Hole3D : MonoBehaviour {
    

    [SerializeField] private float initalScale  = 0.1f;
    [SerializeField] private float maxSize = 100;
    [SerializeField] private PolygonCollider2D hole2dcollider;
    [SerializeField] private PolygonCollider2D ground2DCollider;
    [SerializeField] private MeshCollider generatedMeshCollider;
    // [SerializeField] private Collider groundCollider;
    [SerializeField] private GameObject[] allGoArray;
    [SerializeField] private Collider[] groundColis;
    

    private Mesh generatedMesh;
    private void Start(){
        GameObject[] AllGOs = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach(GameObject gos in AllGOs){
            if(gos.layer == LayerMask.NameToLayer("Obstacles") || gos.layer == LayerMask.NameToLayer("Building")){
                if(gos.TryGetComponent<Collider>(out Collider coli)){
                    Physics.IgnoreCollision(coli,generatedMeshCollider,true);

                }
            }
        }
    }
    
    private void FixedUpdate(){
        if(transform.hasChanged){
            transform.hasChanged = false;
            hole2dcollider.transform.position = new Vector2(transform.position.x,transform.position.z);
            hole2dcollider.transform.localScale = transform.localScale * initalScale;
            
            MakeHole2D();
            Make3DMeshCollider();
        }
        if (hole2dcollider.transform.localScale.x > maxSize)
        {
            hole2dcollider.transform.localScale = new Vector3(maxSize, maxSize, maxSize);
        }
    }
    private void OnTriggerEnter(Collider other){
        foreach(Collider colis in groundColis){
            Physics.IgnoreCollision(other,colis,true);

        }
        Physics.IgnoreCollision(other,generatedMeshCollider,false);
        if(other.TryGetComponent<Hole3D>(out Hole3D hole)){
            if(hole.transform.localScale.x > this.transform.localScale.x){
                gameObject.SetActive(false);
                gameObject.AddComponent<Rigidbody>();
                this.enabled = false;
            }else{
                hole.gameObject.SetActive(false);
                hole.gameObject.GetComponent<AIHoles>().enabled = false;
                hole.gameObject.AddComponent<Rigidbody>();
                hole.enabled = false;
            }
        }
        if(other.TryGetComponent<Rigidbody>(out Rigidbody rbs)){
            // Try to suck in the Hole............
            rbs.constraints = RigidbodyConstraints.None;
            float downwardPull = 20f;
            rbs.AddForce(Vector3.down * downwardPull,ForceMode.Impulse);
            
        }
    }
    
    
    
    private void OnTriggerExit(Collider other){
        foreach(Collider colis in groundColis){
            Physics.IgnoreCollision(other,colis,false);

        }
        Physics.IgnoreCollision(other,generatedMeshCollider,true);
        if(other.TryGetComponent<Rigidbody>(out Rigidbody rb)){
            rb.constraints = RigidbodyConstraints.None;
            // coli.GetComponent<Rigidbody>().AddForce(Vector3.up * f,ForceMode.Impulse);
            
        }
    }
    private void MakeHole2D(){
        Vector2[] pointsPostion = hole2dcollider.GetPath(0);
        for (int i = 0; i < pointsPostion.Length; i++){
            pointsPostion[i] = hole2dcollider.transform.TransformPoint(pointsPostion[i]);

        }
        ground2DCollider.pathCount = 2;
        ground2DCollider.SetPath(1,pointsPostion);


    }
    private void Make3DMeshCollider(){
        if(generatedMesh != null){
            Destroy(generatedMesh);
        }
        generatedMesh = ground2DCollider.CreateMesh(true,true);
        generatedMeshCollider.sharedMesh = generatedMesh;
    }
}
