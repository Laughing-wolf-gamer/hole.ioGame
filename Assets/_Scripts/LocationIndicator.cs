using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationIndicator : MonoBehaviour
{

    public GameObject TargetToIndicate;
    Transform targetTransform;
    [Range(0, 1)]
    public float offScreenPositionOffset = 1;


    public Vector3 onScreenPositionOffset = new Vector3(0, 100, 0);
    public Vector3 onScreenPositionOffsetArrow = new Vector3(0, 100, 0);

    public Transform UiRotational;
    public Transform UiStational;
    private Camera cam;
    
    void Start()
    {
        cam = Camera.main;
        targetTransform = TargetToIndicate.transform;
    }

    // Update is called once per frame

    public bool isOnScreen = false;
    float lastUpdateTime;
    private void FixedUpdate()
    {

        if(targetTransform != null){
            isOnScreen = OnScreen(WorldToScreen(targetTransform));
            if (isOnScreen)
            {
                UiRotational.localEulerAngles = new Vector3(0, 0, 180);
                Vector3 pos = (WorldToScreen(targetTransform)) + onScreenPositionOffset;
                UiRotational.position = pos + onScreenPositionOffsetArrow;
                UiStational.position = pos;
            }
            else
            {

                UpdateOffScreen(WorldToScreen(targetTransform));
            }
        }

    }
    private void UpdateOffScreen(Vector3 targetPosOnScreen)
    {

        Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;


        Vector3 newIndicatorPos = targetPosOnScreen - screenCenter;


        if (newIndicatorPos.z < 0)
            newIndicatorPos *= -1;


        float angle = Mathf.Atan2(newIndicatorPos.y, newIndicatorPos.x);
        angle -= 90 * Mathf.Deg2Rad;

        //  y = mx + b (intercept forumla)
        float cos = Mathf.Cos(angle);
        float sin = Mathf.Sin(angle);
        float m = cos / sin;


        Vector3 screenBounds = new Vector3(screenCenter.x * offScreenPositionOffset, screenCenter.y * offScreenPositionOffset);


        if (cos > 0)
            newIndicatorPos = new Vector2(-screenBounds.y / m, screenBounds.y);
        else{

            newIndicatorPos = new Vector2(screenBounds.y / m, -screenBounds.y);
        }


        if (newIndicatorPos.x > screenBounds.x)
            newIndicatorPos = new Vector2(screenBounds.x, -screenBounds.x * m);
        else if (newIndicatorPos.x < -screenBounds.x)
            newIndicatorPos = new Vector2(-screenBounds.x, screenBounds.x * m);


        newIndicatorPos += screenCenter;
        UiStational.position = newIndicatorPos;
        UiRotational.position = newIndicatorPos;
        UiRotational.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);

    }
    private bool OnScreen(Vector3 pos){
        
        if (pos.x < (Screen.width) && pos.x > (Screen.width - Screen.width) && pos.y < (Screen.height) && pos.y > (Screen.height - Screen.height) && pos.z > cam.nearClipPlane && pos.z < cam.farClipPlane){
            
            return true;
        }
        return false;
    }
    Vector3 WorldToScreen(Transform obj)
    {
        if (obj == null)
        {
            Debug.Log("Attached to " + gameObject.name);
        }
        return cam.WorldToScreenPoint(obj.position+new Vector3(0,4,0));
    }

}
