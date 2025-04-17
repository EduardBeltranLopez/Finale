using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA : MonoBehaviour
{

    NavMeshAgent agent;
    Animator animator;
    public Transform player;
    State currentState;



    public GameObject[] check;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        currentState = new Iddle(this.gameObject, agent, animator, player, check);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
    }

    void OnDrawGizmos()
    {
        if (currentState != null)
        {
        currentState.OnDrawGizmos();
        }
    }
}
