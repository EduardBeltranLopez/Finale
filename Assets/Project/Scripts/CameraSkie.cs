using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class CameraSkie : MonoBehaviour
{

    #region Handles Camera
    [Header("Las Dos Vistas")]
    [SerializeField] GameObject canvasHolder;
    [SerializeField] GameObject[] camarasArriba;
    [SerializeField] int currentCamaraArriba;
    [SerializeField] GameObject camaraNormal;
    [SerializeField] bool vistaArriba;
    #endregion

    [Header("Components")]
    [SerializeField] PlayerMovement playerMovement;


    // Start is called before the first frame update
    void Start()
    {
        
        playerMovement = GetComponent<PlayerMovement>();


        //Desactiva El Bool que comprueba si se puede usar la Vista Alta
        vistaArriba = false;
        currentCamaraArriba = 0;
    }

    // Update is called once per frame
    void Update()
    {


        //Si le das a la tecla K usas Vista Alta
        if (Input.GetKeyDown(KeyCode.K) && vistaArriba == false)
        {
            canvasHolder.SetActive(true);
            camaraNormal.SetActive(false);
            playerMovement.speed = 0;
            vistaArriba = true;

            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }

        //Si le das al ESCAPE te vas de la vista alta
        if (Input.GetKeyDown(KeyCode.Escape) && vistaArriba == true)
        {
            canvasHolder.SetActive(false);
            camaraNormal.SetActive(true);
            playerMovement.speed = playerMovement.saveSpeed;
            vistaArriba = false;

            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
    }


    public void ToCameraPark()
    {
        camarasArriba[currentCamaraArriba].SetActive(false);
        currentCamaraArriba++;
        camarasArriba[currentCamaraArriba].SetActive(true);
    }

    public void ToCamera()
    {
        camarasArriba[currentCamaraArriba].SetActive(false);
        currentCamaraArriba--;
        camarasArriba[currentCamaraArriba].SetActive(true);
    }
}
