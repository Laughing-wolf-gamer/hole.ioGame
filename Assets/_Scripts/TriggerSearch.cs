using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSearch : MonoBehaviour
{

    public int scanArea = 5;
    // Use this for initialization
    public Transform SearchedTransform;
    Vector3 startingScale;

    public GroupData data;
    void Start()
    {
        startingScale = this.transform.localScale;
        StartScan();
    }

    // Update is called once per frame
    void Update()
    {
        if (ScanForFollowers)
        {
            this.transform.localScale = Vector3.MoveTowards(this.transform.localScale, Vector3.one * scanArea, Time.deltaTime * 50);
        }
        if (Vector3.Distance(transform.localScale, Vector3.one * scanArea) < 0.2f)
        {
            EndScan();
        }
    }
    bool ScanForFollowers = false;

    public void StartScan()
    {
        ScanForFollowers = true;
        SearchedTransform = null;
    }
    void EndScan()
    {
        if (SearchedTransform == null)
        {
            SearchedTransform = AllNodes.Instance.getOneRandomNode();
        }
        ScanForFollowers = false;
        this.transform.localScale = startingScale;
    }

    void OnTriggerEnter(Collider incoming)
    {
        GameObject IncomingObj = incoming.gameObject;
        //        Debug.Log("Triggerin With Followers");
        if (IncomingObj.tag.Contains("Followers"))
        {
            Follower followInstance = IncomingObj.GetComponent<Follower>();
            if (!followInstance.isFollowingLeader)
            {
                SearchedTransform = IncomingObj.transform;

                EndScan();
            }
            else if (followInstance.isFollowingLeader)
            {
                if (followInstance.data.groupId != data.groupId && followInstance.data.score < data.score)
                {
                    SearchedTransform = IncomingObj.transform;
                    EndScan();
                }
            }

        }

    }


}
