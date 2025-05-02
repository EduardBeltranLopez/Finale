using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] float Ynumber;
    [SerializeField] Transform respawner;
    public PlayerStats playerStats;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < Ynumber)
        {
            transform.position = respawner.position;
        }

        if (playerStats.currentHp == -1)
        {
            transform.position = respawner.position;
            
        }
    }
}
