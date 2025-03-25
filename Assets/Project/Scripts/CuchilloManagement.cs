using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuchilloManagement : MonoBehaviour
{
    [Header("Cuchillos Count")]
    [SerializeField] GameObject[] cuchillo;
    [SerializeField] GameObject carcasa;
    [SerializeField] bool abierto;
    [SerializeField] int numero;


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
        CountThrow();
    }


    void CountThrow()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //cuchillos[numero--].SetActive(false);


        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //cuchillos[numero].SetActive(true);

        }



    }


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

            //Activo los cuchillos
            cuchillo[numero].SetActive(true);
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

            //Desactivo los cuchillos
            cuchillo[numero ].SetActive(false);

        }
    }



}
