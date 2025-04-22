using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KniveRefill : MonoBehaviour
{
    private void Start()
    {

    }

    private void Update()
    {
        FollowPlayer();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            popUp.SetActive(true);
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            popUp.SetActive(false);
        }
    }

    #region Handles PlayerFollow&Apear

    [Header("LookingToThePlayer")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject popUp;
    [SerializeField] GameObject canvas;


    void FollowPlayer()
    {
        canvas.transform.rotation = player.transform.rotation;
    }

    #endregion
}



