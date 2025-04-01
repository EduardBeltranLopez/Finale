using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class EnviromentManager
{
    private static EnviromentManager instance;
    private List<GameObject> checkpoints = new List<GameObject>();
    public List<GameObject> Checkpoints {  get { return checkpoints; } }

    public static EnviromentManager Singleton
    {
        get
        {
            if (instance == null)
            {
                instance = new EnviromentManager();
                instance.Checkpoints.AddRange(
                    GameObject.FindGameObjectsWithTag("Checkpoint"));

                instance.checkpoints = instance.checkpoints.OrderBy(waypoint => waypoint.name).ToList();

            }
            return instance;
        }
    }
}
