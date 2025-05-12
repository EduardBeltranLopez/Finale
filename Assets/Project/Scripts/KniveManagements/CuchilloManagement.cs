using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuchilloManagement : MonoBehaviour
{


    private void Start()
    {
        carcasa.SetActive(false);
    }
    private void Update()
    {
        OpenCase();
        ShootKnive();
        NewPickUpKnive();


    }


    #region Handles CasilleraOpen
    [Header("Cuchillos Count")]
    [SerializeField] GameObject cuchillo;
    [SerializeField] GameObject carcasa;
    [SerializeField] bool abierto;

    void OpenCase()
    {
        if (Input.GetKeyDown(KeyCode.I) && abierto == false)
        {
            abierto = true;
            //Time.timeScale = 0;
            carcasa.SetActive(true);


            CuchilloEsta();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && abierto == true)
        {
            abierto = false;
            //Time.timeScale = 1;
            carcasa.SetActive(false);
        }
    }

    void CuchilloEsta()
    {
        if (cuchilloFuera == false) { cuchillo.SetActive(true); }
        else { cuchillo.SetActive(false); }
    }
    #endregion

    #region Handles Shoot
    [Header("BulletInstantiate")]
    public bool cuchilloFuera = false;
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int bulletSpeed;




    void ShootKnive()
    {
        if (Input.GetKeyDown(KeyCode.Q) && cuchilloFuera == false)
        {
            cuchilloFuera = true;
            GameObject bulletInstant = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

            bulletInstant.GetComponent<Rigidbody>().AddForce(shootPoint.forward * bulletSpeed);
        }
    }



    #endregion


    #region Handles PlayerRefillKnife
    [Header("RaycastCosas")]
    [SerializeField] float interactionDistance;
    [SerializeField] public LayerMask canMask;
    [SerializeField] Transform pointer;

    void NewPickUpKnive()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(pointer.position, pointer.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance, canMask))
            {
                if (cuchilloFuera == true)
                {
                    Destroy(hit.collider.gameObject);

                    cuchilloFuera = false;
                }
                else if (cuchilloFuera == false)
                {
                    Debug.Log("YaEstasLleno");
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(pointer.transform.position, pointer.forward * interactionDistance);
    }
    #endregion
}