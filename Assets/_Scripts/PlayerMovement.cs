using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    // Use this for initialization
    public float speed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    Vector3 pos; Transform thisTrans;

    private void Start(){
        controller = GetComponent<CharacterController>();
        // let the gameObject fall down
        thisTrans = transform;
    }

    private void FixedUpdate(){
        // We are grounded, so recalculate
        // move direction directly from axes
        moveDirection = new Vector3(0.0f, 0.0f, 1);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * speed;

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
        Vector3 pos = thisTrans.position;
        if (pos.y != 0.0f)

        {
            thisTrans.position = new Vector3(pos.x, 0, pos.z);
        }
        //limiting Y
        if (Application.isEditor)
        {
           // thisTrans.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * 300, 0, Space.World);
        }
         
        {
            thisTrans.localRotation = Quaternion.LookRotation(   TouchRotateSingle.eulerRotation);
        }
    }
}