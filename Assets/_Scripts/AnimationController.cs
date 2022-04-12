using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
    public int currentAnimState = 0;
    int lastAnimState;
    public Animator thisAnimator;
    private void Start()
    {
        thisAnimator.applyRootMotion = false;



    }
    private void FixedUpdate()
    {
        AnimUpdate();
    }
    void AnimUpdate()
    {
        if (lastAnimState != currentAnimState)
        {
            switch (currentAnimState)
            {
                case 0:
                    thisAnimator.SetTrigger("idle");
                    break;
                case 1:
                    thisAnimator.SetTrigger("walk");
                    break;
                case 2:
                    thisAnimator.SetTrigger("run");
                    break;

            }


            lastAnimState = currentAnimState;
        }
    }
   
}
