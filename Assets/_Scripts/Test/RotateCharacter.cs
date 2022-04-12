using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCharacter : MonoBehaviour
{


    // Maths
    public static float Vector2ToAngle(Vector2 v)
    {
        float num = Mathf.Atan2(v.y, v.x);
        return (num * 57.29578f - 90f + 360f) % 360f;
    }
    Vector3 currentMouse, lastMouse;
    public float Angle, LastAngle;
    public Vector2 startPos, direction;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    startPos = touch.position;
                    LastAngle = 0;

                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one
                    direction = touch.position - startPos;

                    Angle = Vector2ToAngle(direction);
                    transform.rotation = Quaternion.Euler(0, Angle, 0);
                    break;

                case TouchPhase.Ended:
                    // Report that the touch has ended when it ends

                    break;
            }
        }

    }


}
