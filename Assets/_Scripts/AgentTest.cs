using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentTest : MonoBehaviour
{
    public Transform target;
    public Animator charAnimator;
    public NavMeshAgent thisAgent;
    // Use this for initialization
    void Start()
    {
        thisAgent = GetComponent<NavMeshAgent>();
        charAnimator.SetTrigger("Run");
    }

    // Update is called once per frame
    void Update()
    {
        thisAgent.SetDestination(target.position);
        if (Vector3.Distance(target.position, transform.position) < 2)
        {
            charAnimator.SetTrigger("Idle");
        }
    }
}
