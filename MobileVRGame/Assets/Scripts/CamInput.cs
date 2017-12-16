using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamInput : MonoBehaviour {

    public float speedH = 2.0f; //How fast the camera can move from left to right
    public float speedV = 2.0f; //How fast the camera can move from up to down

    private float yaw = 0.0f;   //How to move the cam
    private float pitch = 0.0f; //How to move the cam

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
