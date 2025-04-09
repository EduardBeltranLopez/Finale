using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

#region Handles States
public class State
{
    public enum STATE
    {
        IDDLE, PATROL, SHOOT, ATTACK, PURSUE
    }

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }

    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected State nextState;
    protected NavMeshAgent agent;

    public float vistDist = 20.0f;
    float visAngle = 30.0f;
    float shootDist = 3.0f;

    public State(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
    {
        npc = _npc;
        agent = _agent;
        anim = _anim;
        player = _player;
        stage = EVENT.ENTER;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { }
    public virtual void Exit() { }
    public virtual void OnDrawGizmos()
    {
        if (npc != null)
        {
            Vector3 position = npc.transform.position;
            Vector3 forward = npc.transform.forward;

            Quaternion leftRotation = Quaternion.Euler(0, -visAngle, 0);
            Quaternion rightRotation = Quaternion.Euler(0, visAngle, 0);

            Vector3 leftDirection = leftRotation * forward * vistDist;
            Vector3 rightDirection = rightRotation * forward * vistDist;

            Gizmos.color = Color.green;
            Gizmos.DrawRay(position, leftDirection);
            Gizmos.DrawRay(position, rightDirection);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(position, forward * shootDist);
        }
    }

    public State Process()
    {
        if (stage == EVENT.ENTER)
        {
            Enter();
            stage = EVENT.UPDATE;
        }
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }


    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);

        if (direction.magnitude < vistDist && angle < visAngle)
        {
            Ray ray = new Ray(npc.transform.position + Vector3.up, direction.normalized);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, vistDist, ~0))
            {
                if (hit.transform == player)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CanAttackPlayer()
    {

        Vector3 direction = player.position - npc.transform.position;
        if (direction.magnitude < shootDist)
        {

            return true;
        }

        return false;
    }


}
#endregion

#region Handles Iddle
public class Iddle : State
{
    public Iddle(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
            : base(_npc, _agent, _anim, _player)
    {
        name = STATE.IDDLE;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        if (CanSeePlayer())
        {
            nextState = new Pursue(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
        else if (Random.Range(0, 100) < 10)
        {
            nextState = new Patrol(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
#endregion

#region Handles Patrol
public class Patrol : State
{
    int currentIndex = -1;

    public Patrol(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
                : base(_npc, _agent, _anim, _player)
    {
        name = STATE.PATROL;
        agent.speed = 2;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        currentIndex = 0;

        if (EnviromentManager.Singleton.Checkpoints.Count > 0)
        {
            agent.SetDestination(EnviromentManager.Singleton.Checkpoints[currentIndex].transform.position);
        }

        base.Enter();
    }

    public override void Update()
    {

        if (agent.remainingDistance < 1)
        {
            if (currentIndex >= EnviromentManager.Singleton.Checkpoints.Count - 1)
                currentIndex = 0;
            else
                currentIndex++;

            agent.SetDestination(EnviromentManager.Singleton.Checkpoints[currentIndex].transform.position);
        }
        if (CanSeePlayer())
        {
            nextState = new Pursue(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
#endregion

#region Handles Persue
public class Pursue : State
{
    float lostPlayerTimer = 0f;
    float lostPlayerDuration = 3f;
    bool playerLost = false;

    public Pursue(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
                : base(_npc, _agent, _anim, _player)
    {
        name = STATE.PURSUE;
        agent.speed = 5;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        lostPlayerTimer = 0f;
        playerLost = false;
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(player.position);

        if (CanSeePlayer())
        {
            playerLost = false;
            lostPlayerTimer = 0f;

            if (CanAttackPlayer())
            {
                nextState = new Attack(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
        }
        else
        {
            if (!playerLost)
            {
                playerLost = true;
                lostPlayerTimer = 0f;
            }

            lostPlayerTimer += Time.deltaTime;

            if (lostPlayerTimer >= lostPlayerDuration)
            {
                nextState = new Patrol(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
        }

        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
#endregion

#region Handles Attack
public class Attack : State
{

    float rotationSpeed = 2.0f;
    AudioSource shoot;

    public Attack(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
        : base(_npc, _agent, _anim, _player)
    {

        name = STATE.ATTACK;
        shoot = _npc.GetComponent<AudioSource>();
    }

    public override void Enter()
    {
        agent.isStopped = true;
        shoot.Play();
        base.Enter();
    }

    public override void Update()
    {

        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);
        direction.y = 0.0f;

        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

        

        if (!CanAttackPlayer())
        {

            nextState = new Iddle(npc, agent, anim, player);
            shoot.Stop();
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
#endregion