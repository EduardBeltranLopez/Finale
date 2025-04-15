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

    public float speed;
    public float saveSpeed;
    public float maxForce;
    private Vector2 move, look, crouch;


    public bool isCrouching;
    public GameObject playerHeadRayOrigin;
    public float playerRayDistance;
    public bool hasCeilingUp;
    public LayerMask ceilingMask;

    #endregion

    #region Handles Camera
    [Header("Las Dos Vistas")]
    [SerializeField] GameObject camaraArriba;
    [SerializeField] GameObject camaraNormal;
    [SerializeField] bool vistaArriba;


    [Header("Cinemachine")]
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public Transform cameraTransform;

    public float lookUp;
    public float lookDown;
    float pitch = 0f;
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
        Camera();
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
            cameraTransform.transform.localPosition = new Vector3(0, -0.4f, 0);
        }
        else if (context.canceled && hasCeilingUp == false)
        {
            isCrouching = false;
            speed = saveSpeed;
            transform.localScale = new Vector3(1, 1, 1);
            cameraTransform.localPosition = new Vector3(0, 0.6f, 0);
        }
    }

    void CheckCeilingAbove()
    {

        Ray ray = new Ray(playerHeadRayOrigin.transform.position, Vector3.up);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, playerRayDistance, ceilingMask))
        {
            hasCeilingUp = true;
            //Debug.DrawRay(playerHeadRayOrigin.transform.position, Vector3.up * playerRayDistance, Color.red);
        }
        else
        {
            hasCeilingUp = false;
            //Debug.DrawRay(playerHeadRayOrigin.transform.position, Vector3.up * playerRayDistance, Color.green);

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

    void Camera()
    {
        transform.Rotate(Vector3.up * look.x * mouseSensitivity);
        pitch -= look.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, lookDown, lookUp);
        cameraTransform.localEulerAngles = new Vector3(pitch, 0f, 0f);
    }



}


