using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay;

using UnityEngine.AI;

public class FunctionalAdult : MonoBehaviour, IHear
{
    [SerializeField] private NavMeshAgent agent = null;

    [SerializeField, Tooltip("How far away, in meters, the agent will run from danger.")]
    private float displacementFromDanger = 10f;

    void Awake()
    {
        if (agent == null && !TryGetComponent(out agent))
            Debug.LogWarning(name + " doesn't have an agent!");
    }

    public void RespondToSound(Sound sound)
    {

        if (sound.soundType == Sound.SoundType.Interesting)
            MoveTo(sound.pos);
        else if (sound.soundType == Sound.SoundType.Dangerous) 
        {
            Vector3 dir = (sound.pos - transform.position).normalized;
            MoveTo(transform.position - (dir * displacementFromDanger));
        }
    }

    private void MoveTo(Vector3 pos)
    {
        agent.SetDestination(pos);
        agent.isStopped = false;
    }
}