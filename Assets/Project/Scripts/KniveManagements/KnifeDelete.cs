using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeDelete : MonoBehaviour
{

    CuchilloManagement cuchilloManagement;

    private void Start()
    {   
        cuchilloManagement = FindObjectOfType<CuchilloManagement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && cuchilloManagement.cuchilloFuera == true)
        {
            Destroy(gameObject);
            cuchilloManagement.cuchilloFuera = false;
        }
    }
}
