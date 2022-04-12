using Cinemachine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hole : HoleController {
    
    [SerializeField] private camFollower camFollower;
    private Vector3 moveDirection = Vector3.zero;

    private int currentCameraIndex;
    Vector3 pos; 
    Transform thisTrans;
    
    protected override void Start(){
        // ChangeVirualCamera();
        AudioSource audio = GetComponent<AudioSource>();
        thisTrans = transform;
        onSizeChange += ()=>{
            currentCameraIndex ++;
            camFollower.ChangeOffset(5);
            audio.Play();
        };
        base.OnObjectFallInHole += ()=>{
            Handheld.Vibrate();
            UIManager.i.SetKillAmountText(groupData.KillCount);
        };
    }
    private void Update(){
        if(groupData.isDead || !GameHandler.i.GetisGamePlaying()){
            // GameHandler.i.EndGame();
            SetIsGameStart(false);
        }
        if(!GameHandler.i.GetisGamePlaying()){
            moveDirection = Vector3.zero;
            // holeMoveSpeed = 0f;
        }
    }
    
    private void FixedUpdate(){
        if(!groupData.isDead){
        
            if(isGameStart){
                
                // Get the Amount the Objects fell in the hole..............
                groupData.score = currentScore;
                groupData.indicatorInstance.UpdateText();
                // Movment.....
                if(TouchRotateSingle.canMove){
                    moveDirection = new Vector3(0.0f, 0.0f, 1);
                    moveDirection = moveDirection * holeMoveSpeed;
                    Vector3 pos = thisTrans.position;
                    if (pos.y != 0.0f)
                    {
                        thisTrans.position = new Vector3(pos.x, 0.21f, pos.z);
                    }
                    // Rotation And Movment... ofPlyaer Hole......
                }else{
                    moveDirection = new Vector3(0.0f, 0.0f, 0f);
                }
                thisTrans.localRotation = Quaternion.LookRotation(TouchRotateSingle.eulerRotation);
            }else{
                moveDirection = Vector3.zero;
            }
            transform.Translate(moveDirection * Time.deltaTime);
        // thisTrans.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * 300, 0, Space.World);
        }else{
            moveDirection = Vector3.zero;
        }
        
        
    }
    
    // private void ChangeVirualCamera(){
    //     // Change Camera View on Scale.............
    //     for (int i = 0; i < virtualCamera.Length; i++){
    //         if(currentCameraIndex == i){
    //             virtualCamera[i].Priority = 20;
    //         }else{
    //             virtualCamera[i].Priority = 1;
    //         }
    //     }
    // }
    
    
    
}
