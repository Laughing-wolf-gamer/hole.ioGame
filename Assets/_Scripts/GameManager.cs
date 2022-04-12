using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GameManager : MonoBehaviour
{

    public GameObject FollowerPrefab;
    public GameObject LeaderPrefab;

    public List<GroupData> data = new List<GroupData>();//leaders and player uses this data
    PlayerController playerInstance;
    public int dataIndex = 1;
    public int globalFollowersLimit = 5000;//as of 2018 Dec,im putting 500 people as limit,dear dev of future increase to 5000
    // Use this for initialization
    public int startingPeopleCount = 100;

    [Range(0, 10)]
    public int leaderCount = 10;
    public static GameManager Instance;
    public Material followerMaterial;
    public camFollower camScript;
    public static bool isGameEnded = false;
    public static bool isGameStarted = false;
    public Material fillMaterial;

    private void OnEnable()
    {
        isGameEnded = false;
        Instance = this;
        isGameStarted = false;
        playerInstance = data[0].groupLeader.GetComponent<PlayerController>();
        playerInstance.SetUp();
        camScript.enabled = true;

    }
    private void Awake()
    {


    }

    public void StartGame()
    {
        isGameEnded = false;
        //fill usedMaterial
        for (int i = 1; i < leaderCount; i++)
        {
            if (data[0].leaderMaterial == data[i].leaderMaterial)
            {
                data[i].leaderMaterial = fillMaterial;
                continue;
            }
        }
        //creating 10 Leaders 
        for (int i = 1; i < leaderCount; i++)
        {
            Transform CreateTransform = AllNodes.Instance.getOneRandomNode();//allnodes scripe is attached to cityNetwork gameObject on scene
            GameObject leader = GameObject.Instantiate(LeaderPrefab);
            leader.transform.position = CreateTransform.position;
            leader.transform.rotation = CreateTransform.rotation;
            data[i].groupLeader = leader;
            leader.GetComponent<Leader>().data = data[i];
            leader.name = "Leader " + data[i].groupId;


        }
        //Leader material Swith
        for (int i = 1; i < leaderCount; i++)
        {
            int randIndex = Random.Range(1, leaderCount);
            Material mat = data[i].leaderMaterial;
                data[i].leaderMaterial = data[randIndex].leaderMaterial;
            data[randIndex].leaderMaterial = mat;


        }
        //allocating indicators for leaders
        Indicator[] allIndicators = FindObjectsOfType<Indicator>();
        data[0].indicatorInstance = allIndicators[0];//for player
        for (int i = 1; i < leaderCount; i++)
        {
            data[i].indicatorInstance = allIndicators[i];
            data[i].indicatorInstance.setUpIndicator(data[i]);

        }
        InvokeRepeating("CreateFollowers", 2, 0.5f);
        InvokeRepeating("CreateFollowers", 2, 0.5f);

        for (int i = 0; i < startingPeopleCount; i++)
        {
            CreateFollowers();
        }

        playerInstance.startPlayer();

        isGameStarted = true;

    }

    public void StopGameByTimeOut()//called from GameTimer script
    {
        playerInstance.stopPlayer();
        isGameEnded = true;
        isGameStarted = false;
        Debug.Log("StopGameByTimeOut");
		FindObjectOfType<UIManager>().GameOver ();
		FindObjectOfType<TimeOut>().TimeOutPanelActivate ();
        //FindObjectOfType<AdManager> ().ShowAd ();
        AdmobScript.instance.showInterstitialAd();
    }
    public void StopGameByPlayerDead()//  script call from player
    {
        playerInstance.stopPlayer();
        isGameEnded = true;
        isGameStarted = false;
        Debug.Log("StopGameByPlayerDead");
		// FindObjectOfType<UIManager> ().KilledBy ();
		// FindObjectOfType<KilledBy> ().KilledByMethod ();
        //FindObjectOfType<AdManager> ().ShowAd ();
        AdmobScript.instance.showInterstitialAd();
    }
    public void stopGameByLead() //self call
    {
        playerInstance.stopPlayer();
        isGameEnded = true;
        isGameStarted = false;
        Debug.Log("stopGameByLead");
        StopGameByTimeOut();//hot fix ForNow
    }

    float lastTimeCheckAllDead;

    public List<GroupData> sortedDataForUI;
    private void FixedUpdate()
    {
        if (Time.timeSinceLevelLoad - lastTimeCheckAllDead > 1 && isGameEnded == false && isGameStarted)// using of two bool ? use gameState enum
        {
            lastTimeCheckAllDead = Time.timeSinceLevelLoad;
            List<GroupData> nonDeadList = new List<GroupData>();
            foreach (var e in data)
            {
                if (!e.isDead) nonDeadList.Add(e);
            }
            sortedDataForUI.Clear();
            sortedDataForUI = nonDeadList.OrderBy(o => o.score).ToList();//simple sort for ranking
            sortedDataForUI.Reverse();//making o to 10
            if (sortedDataForUI.Count == 1) stopGameByLead();

        }

    }

    public List<GroupData> getFinalRakings() // including death ranking
    {
        List<GroupData> sortedList = data.OrderBy(o => o.score).ToList();
        sortedList.Reverse();
        return sortedList;
    }
    void CreateFollowers()
    {
        if (Follower.TotalFollowersCount > globalFollowersLimit) return; // no more than globalFollowersLimit people   
        Transform CreateTransform = AllNodes.Instance.getOneRandomNode();
        GameObject follower = GameObject.Instantiate(FollowerPrefab);
        follower.transform.position = CreateTransform.position;
        follower.transform.rotation = CreateTransform.rotation;


    }
}
