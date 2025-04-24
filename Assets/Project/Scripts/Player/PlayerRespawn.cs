using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] float Ynumber;
    [SerializeField] Transform respawner;




    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < Ynumber)
        {
            transform.position = respawner.position;
        }
    }
}
