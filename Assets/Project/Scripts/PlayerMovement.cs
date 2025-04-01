using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Handles Movment
    [Header("GetComponents")]
    public Rigidbody rb;
    public GameObject camHolder;

    public float speed;
    public float sensitivity;
    public float maxForce;
    private Vector2 move, look;

    public float lookRotation;
    public float lookTop = 45;
    public float lookBottom = -45;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    void Move()
    {

        Vector3 currentVeloity = rb.velocity;
        Vector3 targetVelocity = new Vector3(move.x, 0, move.y);
        targetVelocity *= speed;

        targetVelocity = transform.TransformDirection(targetVelocity);

        Vector3 velocityChange = (targetVelocity - currentVeloity);

        Vector3.ClampMagnitude(velocityChange, maxForce);
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    #endregion

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        //Desactiva La Vista Alta
        camaraArriba.SetActive(false);
        //Desactiva El Bool que comprueba si se puede usar la Vista Alta
        vistaArriba = false;
    }
    void Update()
    {

        //Si le das a la tecla K usas Vista Alta
        if (Input.GetKeyDown(KeyCode.K) && vistaArriba == false)
        {
            camaraArriba.SetActive(true);
            camaraNormal.SetActive(false);
            Time.timeScale = 0;
            vistaArriba = true;
        }

        //Si le das al ESCAPE te vas de la vista alta
        if (Input.GetKeyDown(KeyCode.Escape) && vistaArriba == true)
        {
            camaraArriba.SetActive(false);
            camaraNormal.SetActive(true);
            Time.timeScale = 1;
            vistaArriba = false;
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void LateUpdate()
    {
        transform.Rotate(Vector3.up * look.x * sensitivity);

        lookRotation += (-look.y * sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, lookBottom, lookTop);
        camHolder.transform.eulerAngles = new Vector3(lookRotation, camHolder.transform.eulerAngles.y, camHolder.transform.eulerAngles.z);
    }

    #region Handles CameraChange
    [Header("Las Dos Camaras")]
    [SerializeField] GameObject camaraArriba;
    [SerializeField] GameObject camaraNormal;

    [Header("Saber Si la Vista Parcial esta activa")]
    [SerializeField] bool vistaArriba;
    #endregion
}


