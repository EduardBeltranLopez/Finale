using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

#region Handles States
public class State
{
    public enum STATE
    {
        IDDLE, PATROL, SHOOT
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
        if (Random.Range(0, 100) < 10)
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
    }

    public override void Exit()
    {
        base.Exit();
    }
}
#endregion