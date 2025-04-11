using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerMovement : MonoBehaviour
{
    #region Handles Movment
    [Header("GetComponents")]
    public Rigidbody rb;
    public GameObject camHolder;

    public float speed;
    public float saveSpeed;
    public float sensitivity;
    public float maxForce;
    private Vector2 move, look, crouch;

    public float lookRotation;
    public float lookTop = 45;
    public float lookBottom = -45;

    public bool isCrouching;
    public GameObject playerCamera;
    public GameObject playerHeadRayOrigin;
    public float playerRayDistance;
    public bool hasCeilingUp;
    public LayerMask ceilingMask;
    #endregion

    #region Handles CameraChange
    [Header("Las Dos Camaras")]
    [SerializeField] GameObject camaraArriba;
    [SerializeField] GameObject camaraNormal;

    [Header("Saber Si la Vista Parcial esta activa")]
    [SerializeField] bool vistaArriba;
    #endregion

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        //Desactiva La Vista Alta
        camaraArriba.SetActive(false);
        //Desactiva El Bool que comprueba si se puede usar la Vista Alta
        vistaArriba = false;

        saveSpeed = speed;
    }
    void Update()
    {

        //Si le das a la tecla K usas Vista Alta
        if (Input.GetKeyDown(KeyCode.K) && vistaArriba == false)
        {
            camaraArriba.SetActive(true);
            camaraNormal.SetActive(false);
            speed = 0;
            vistaArriba = true;
        }

        //Si le das al ESCAPE te vas de la vista alta
        if (Input.GetKeyDown(KeyCode.Escape) && vistaArriba == true)
        {
            camaraArriba.SetActive(false);
            camaraNormal.SetActive(true);
            speed = saveSpeed;
            vistaArriba = false;
        }

        CheckCeilingAbove();

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
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            isCrouching = true;
            speed = speed /  2;
            transform.localScale = new Vector3(1, 0.5f, 1);
            playerCamera.transform.localPosition = new Vector3(0, -0.9f, 0);
        }
        else if (context.canceled && hasCeilingUp == false)
        {
            isCrouching = false;
            speed = saveSpeed;
            transform.localScale = new Vector3(1, 1, 1);
            playerCamera.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    void CheckCeilingAbove()
    {

        Ray ray = new Ray(playerHeadRayOrigin.transform.position, Vector3.up);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, playerRayDistance, ceilingMask))
        {
            hasCeilingUp = true;
            Debug.DrawRay(playerHeadRayOrigin.transform.position, Vector3.up * playerRayDistance, Color.red);
        }
        else
        {
            hasCeilingUp = false;
            Debug.DrawRay(playerHeadRayOrigin.transform.position, Vector3.up * playerRayDistance, Color.green);

        }

    }
    void Move()
    {
        Vector3 currentVelocity = rb.velocity;
        Vector3 targetVelocity = new Vector3(move.x, 0, move.y) * speed;

        targetVelocity = transform.TransformDirection(targetVelocity);

        Vector3 velocityChange = new Vector3(targetVelocity.x - currentVelocity.x, 0, targetVelocity.z - currentVelocity.z);

        velocityChange = Vector3.ClampMagnitude(velocityChange, maxForce);
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }



}


