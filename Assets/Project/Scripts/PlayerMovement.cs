using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Handles Movment
    [Header("GetComponents")]
    private PlayerInput playerInput;
    private InputAction actionMove;

    [Header("ValoresDelPlayer")]
    [SerializeField] float speed;
    [SerializeField] float crouchForce;


    

    public void Movement()
    {
        Vector2 direction = actionMove.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x * speed, 0, direction.y  * speed) * Time.deltaTime;
    }

    void Crouch()
    {

    }
    void Slowed()
    {

    }

    #endregion


    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        actionMove = playerInput.actions.FindAction("Move");


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

    private void FixedUpdate()
    {
        Movement();
    }

    #region Handles CameraChange
    [Header("Las Dos Camaras")]
    [SerializeField] GameObject camara1;

    [Header("Saber Si la Vista Parcial esta activa")]
    [SerializeField] bool vistaArriba;
    #endregion
}


