using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TouchRotateSingle : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{

    // Use this for initialization
    // public void RotateMesh(Vector3 dir, bool withoutCheck = false)
    // {
       
    //    // this.meshInstance.localRotation = Quaternion.LookRotation(dir);
    // }



    public float maxDistanceInInch = 0.3f;
    private float dpi;
    private Vector2 initialPosition;
    public static bool canMove;
    public static Vector3 eulerRotation;
    private Quaternion pov;
   // public Transform PlayerTransform;
    private void Start()
    {
        if ((double)this.dpi != 0.0 && !float.IsNaN(this.dpi))
            return;
        this.dpi = 96f;
      //  PlayerTransform = GameManager.Instance.data[0].groupLeader.GetComponent<Transform>();
    }


   

    public void OnBeginDrag(PointerEventData data){
        
        if(GameHandler.i.GetisGamePlaying()){
            this.initialPosition = ( data).position;
            canMove = true;
        }
    }

    public void OnDrag(PointerEventData  data){
        if(GameHandler.i.GetisGamePlaying()){
            canMove = true;
            Vector2 vector2_1 = (((PointerEventData)data).position - this.initialPosition) / this.dpi;
            Vector2 vector2_2 = vector2_1.normalized * Mathf.Min(vector2_1.magnitude, this.maxDistanceInInch);
            Vector3 dir = this.pov * new Vector3(vector2_2.x / this.maxDistanceInInch, 0.0f, vector2_2.y / this.maxDistanceInInch);
            eulerRotation = dir;
        }
       // Debug.Log("Mouse was Dragged " + dir);
    }

    public void OnRelease(PointerEventData data){
        if(GameHandler.i.GetisGamePlaying()){
            canMove = false;
        }
        
    }

    
    public void OnEndDrag(PointerEventData eventData){
        if(GameHandler.i.GetisGamePlaying()){
            canMove = false;
        }
    }
}
