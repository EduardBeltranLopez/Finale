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

        NewKnive();
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

    #region Handles PlayerRefillKnife

    [Header("NeedToNewKnive")]
    [SerializeField] CuchilloManagement cuchilloManagement;
    [SerializeField] GameObject newKnife;
    [SerializeField] bool knifeOnTop = true;


    void NewKnive()
    {
        if (Input.GetKeyDown(KeyCode.E) && knifeOnTop == true) 
        {
            if (cuchilloManagement.cuchilloFuera == false)
            {
                Debug.Log("YaEstasLleno");
            }
            else if(cuchilloManagement.cuchilloFuera == true && knifeOnTop == true)
            {
                cuchilloManagement.cuchilloFuera = false;
                newKnife.SetActive(false);
                knifeOnTop = false;
            }
        }
            
    }

    #endregion


}
