using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follower : MonoBehaviour
{

    // Use this for initialization

    NavMeshAgent thisAgent;
    public Transform target;
    public float distanceToTarget;
    Transform thisTrans;

    public bool isFollowingLeader = false;
    Material thisMaterial;
    public GroupData data;
    public AnimationController thisAnimator;
    Rigidbody thisRigidbody;
    public static int TotalFollowersCount = 0;

    public int walkingSpeed = 4;
    public int walkingAccel = 4;
    public int FolowingSpeed = 8;
    public int FolowingAccel = 8;

    public particleController particle;


    void Start()
    {
        isFollowingLeader = false;
        //   if (target == null) target = GameObject.FindGameObjectWithTag("Player").transform;
        thisAgent = GetComponent<NavMeshAgent>();
        thisTrans = GetComponent<Transform>();

        thisRigidbody = this.GetComponent<Rigidbody>();
        TotalFollowersCount++;
        gameObject.name = "Follower " + TotalFollowersCount;
        if (target == null)
        {

            FindOneNode();
        }


        if (thisAgent.isOnNavMesh) thisAgent.SetDestination(target.position);

        //changing follower material on Start
        foreach (var mesh in transform.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            mesh.material = GameManager.Instance.followerMaterial;
        }
    }

    void FindOneNode()
    {
        target = AllNodes.Instance.getOneRandomNode();
    }

    // Update is called once per frame

    float timeSinceGotLeader;
    public void ChangeMaterial()
    {
        if (data != null)
        {
            particle.PlayParticle(data.groupColor);
        }
       
        thisMaterial = data.leaderMaterial;
        isFollowingLeader = true;
        target = data.groupLeader.transform;
        timeSinceGotLeader = Time.timeSinceLevelLoad;
        StartCoroutine( 
        AceHelper.waitThenCallback(0.7f, () => {
            foreach (var mesh in transform.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                mesh.material = thisMaterial;
            }

        }));


    }
    float lastSetTime;
    void FixedUpdate()
    {
        if (GameManager.isGameEnded)
        {

            isFollowingLeader = false;

        }
        if (!isFollowingLeader)
        {
            float distanceDifference = Vector3.Distance(transform.position, target.position);
            if (distanceDifference < 2)
            {
                FindOneNode();
            }
            thisAnimator.currentAnimState = 1;

            if (Time.timeSinceLevelLoad - lastSetTime > 3)
            {
                thisAgent.SetDestination(target.position);
                lastSetTime = Time.timeSinceLevelLoad;
            }

            thisAgent.speed = walkingSpeed;
            thisAgent.acceleration = walkingAccel;
        }
        else
        {

            thisAnimator.currentAnimState = 2;

            thisAgent.speed = FolowingSpeed;
            thisAgent.acceleration = FolowingAccel;

            thisAgent.updateRotation = false;
            thisAgent.SetDestination(target.position);
            lastSetTime = Time.timeSinceLevelLoad;

            thisTrans.rotation = target.rotation;
        }



    }

    float lastTime;
    Vector3 lasPos;




    void OnTriggerEnter(Collider incoming)
    {
        if (GameManager.isGameEnded) return;
        GameObject IncomingObj = incoming.gameObject;

        if (IncomingObj.tag.Contains("Followers") && isFollowingLeader && Time.timeSinceLevelLoad - timeSinceGotLeader > 1)
        {
            Follower followInstance = IncomingObj.GetComponent<Follower>();

            if (followInstance.isFollowingLeader)
            {
                if (followInstance.data.groupId != data.groupId)
                {
                    if (followInstance.data.score < data.score)
                    {

                        followInstance.data.score--;

                        if (followInstance.data.indicatorInstance == null)
                        {
                            Debug.Log("follwInstnace " + followInstance.gameObject.name);
                        }

                        followInstance.data.indicatorInstance.UpdateText();

                        followInstance.target = data.groupLeader.transform;
                        followInstance.isFollowingLeader = true;
                        followInstance.data = data;
                        followInstance.ChangeMaterial();

                        data.score++;

                        data.indicatorInstance.UpdateText();
                        IncomingObj.layer = data.PhysicsLayerID;
                    }

                }

            }
            else
            {
                followInstance.target = data.groupLeader.transform;
                followInstance.isFollowingLeader = true;
                followInstance.data = data;
                followInstance.ChangeMaterial();

                data.score++;

                data.indicatorInstance.UpdateText();
                IncomingObj.layer = data.PhysicsLayerID;
            }




        }


    }

    void OnBecameInvisible()
    {
        thisAnimator.enabled = false;
        Debug.Log("Changed Animator to False    ");
    }
    void OnBecameVisible()
    {
        thisAnimator.enabled = true;
        Debug.Log("Changed Animator to True");
    }

}
