using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    [Header("Las Dos Camaras")]
    [SerializeField] GameObject camara1;

    [Header("Saber Si la Vista Parcial esta activa")]
    [SerializeField] bool vistaArriba;

    private void Start()
    {
        //Desactiva La Vista Alta
        camara1.SetActive(false);
        //Desactiva El Bool que comprueba si se puede usar la Vista Alta
        vistaArriba = false;
    }

    void Update()
    {
        //Si le das a la tecla K usas Vista Alta
        if (Input.GetKeyDown(KeyCode.K) && vistaArriba == false)
        {
            camara1.SetActive(true);
            Time.timeScale = 0;
            vistaArriba = true;
        }

        //Si le das al ESCAPE te vas de la vista alta
        if (Input.GetKeyDown(KeyCode.Escape) && vistaArriba == true)
        {
            camara1.SetActive(false);
            Time.timeScale = 1;
            vistaArriba = false;
        }
    }
}
