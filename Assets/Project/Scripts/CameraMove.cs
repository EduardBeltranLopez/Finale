using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    #region Handles CameraMovementTry 1
    [Header("CameraValues")]
    public float mouseX;
    public float mouseY;
    public float cameraSensity;

    public Transform body;

    public float angle;
    public float angleTop = 45;
    public float angleBottom = -30;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * cameraSensity * Time.deltaTime;
        body.Rotate(Vector3.up, mouseX);

        mouseY = Input.GetAxis("Mouse Y") * cameraSensity * Time.deltaTime;

        angle -= mouseY;
        angle = Mathf.Clamp(angle, angleBottom, angleTop);
        body.localRotation = Quaternion.Euler(angle, 0, 0);
    }
}
    #endregion
