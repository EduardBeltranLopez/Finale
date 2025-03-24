using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("GetComponents")]
    private PlayerInput playerInput;
    private InputAction action;

    [Header("ValoresDelPlayer")]
    [SerializeField] float speed;
    [SerializeField] float crouchForce;


    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        action = playerInput.actions.FindAction("Move");
    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Movement();
    }

    #region Handles Movment

    public void Movement()
    {
        Vector2 direction = action.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x * speed, 0, direction.y  * speed) * Time.deltaTime;
    }

    #endregion

    #region Handles Crouch

    void Crouch()
    {

    }

    #endregion

    #region Handles SlowDown

    void Slowed()
    {

    }

    #endregion
}
