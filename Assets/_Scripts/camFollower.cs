using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class camFollower : MonoBehaviour
{
    public TextMeshProUGUI CamText;
    // Use this for initialization
    public Transform target;
    public Vector2 remapDistance;
    public Vector3 positionOffset;
    Vector3 targetPos;

    public string followingName;
    GroupData data;
    float distance;
    Transform thisTrans;
    public void Start()
    {
        thisTrans = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            data = GameManager.Instance.data[0];
            target = data.groupLeader.transform;

        };
        //    CamText.text = "ObjectCount " + OnScreenObjsCount.ObjectCount;
        if (target == null)
        {
            Debug.Log("Player Not Found");
        }

        if (data != null)
        {
            distance = data.score;

            distance = ((float)OnScreenObjsCount.ObjectCount).RemapClamped(0, 100, remapDistance.x, remapDistance.y);

            positionOffset = new Vector3(0, distance, -distance * 1.5f);
        }
        targetPos = target.position + positionOffset;
        thisTrans.position = Vector3.Lerp(thisTrans.position, targetPos, Time.deltaTime * 20);

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.C))
        {
            List<GroupData> allData = GameManager.Instance.data;

            target = allData[Random.Range(0, allData.Count - 1)].groupLeader.transform;
            followingName = target.name;
        }

#endif
    }
    public void ChangeOffset(float newZOffset){
        // change the View...
        positionOffset.z -= newZOffset;
        positionOffset.y += newZOffset;
    }
}
