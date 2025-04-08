using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuchilloManagement : MonoBehaviour
{


    private void Start()
    {
        //Al empezar el juego desactivo la carcasa
        carcasa.SetActive(false);
    }
    private void Update()
    {
        //Llamo la funcion de abajo para que se realice
        OpenCase();

        //Llamo la funcion de abajo para que se realice
        ShootKnive();



    }


    #region Handles CasilleraOpen
    [Header("Cuchillos Count")]
    [SerializeField] GameObject cuchillo;
    [SerializeField] GameObject carcasa;
    [SerializeField] bool abierto;

    void OpenCase()
    {
        //Si la variable abierto es falsa y le das a la tecla I abres la carcasa
        if (Input.GetKeyDown(KeyCode.I) && abierto == false)
        {
            //Cambio la variable
            abierto = true;

            //Hago que el tiempo del juego no se mueva
            Time.timeScale = 0;

            //Activo la carcasa
            carcasa.SetActive(true);


            CuchilloEsta();
        }

        //Si la variable abierto es verdadera y le das al escape cierras la carcasa
        if (Input.GetKeyDown(KeyCode.Escape) && abierto == true)
        {
            //Cambio la variable
            abierto = false;

            //Hago que el tiempo del juego fluya de normal
            Time.timeScale = 1;

            //Desactivo la carcasa
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
}