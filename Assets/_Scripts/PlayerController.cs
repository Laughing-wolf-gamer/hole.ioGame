using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Use this for initialization

    public AnimationController thisAnim;

    public GroupData data;
    bool isPlayer = true;

    Transform thisTrans;
    Indicator indicator;
    public PlayerMovement movementScript;

    void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }

    private void OnEnable()
    {
        movementScript.enabled = false;
    }
    void Start()
    {
        thisTrans = transform;
        killedBy ="";


    }
    public void SetUp()
    {
        data = GameManager.Instance.data[0];//get data object
        data.groupLeader = this.gameObject;

        ChangeMaterial(data.leaderMaterial);
    }
    public void startPlayer()
    {
        data.indicatorInstance.setUpIndicator(data);
        movementScript.enabled = true;
        thisAnim.currentAnimState = 2;

    }
    public void stopPlayer()
    {
        movementScript.enabled = false;
        thisAnim.currentAnimState = 0;
    }
    public void ChangeMaterial(Material mat){
        foreach (var mesh in transform.GetComponentsInChildren<SkinnedMeshRenderer>()){
            mesh.material = mat;
        }
    }


    void OnTriggerEnter(Collider incoming)
    {
        if (GameManager.isGameEnded) return;
        GameObject IncomingObj = incoming.gameObject;

        if (IncomingObj.tag.Contains("Followers"))
        {
            Follower followInstance = IncomingObj.GetComponent<Follower>();


            if (!followInstance.isFollowingLeader)
            {
                data.score++;
                followInstance.data = data;
                followInstance.ChangeMaterial();
                data.indicatorInstance.UpdateText();
                IncomingObj.layer = data.PhysicsLayerID;

            }
            else
            {

                if (followInstance.data.groupId != data.groupId)
                {
                    if (followInstance.data.score < data.score)
                    {

                        followInstance.data.score--;
                        followInstance.data.indicatorInstance.UpdateText();

                        followInstance.data = data;
                        data.score++;
                        data.indicatorInstance.UpdateText();


                        IncomingObj.layer = data.PhysicsLayerID;
                        followInstance.ChangeMaterial();
                    }
                    else if( data.score == 0)
                    {
                        data.isDead = true;
						killedBy = followInstance.data.LeaderName;
                        GameManager.Instance.StopGameByPlayerDead();

                    }
                }
            }



        }



    }
    bool isMouseDown = false;
    int directional = -1;
	public static string killedBy;
}
