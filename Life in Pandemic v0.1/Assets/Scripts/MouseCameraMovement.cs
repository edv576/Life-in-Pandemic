using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseCameraMovement : MonoBehaviour
{
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    public Vector3 forward;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public bool canRotateHead = true;



    // Use this for initialization
    void Start()
    {

    }

    

    // Update is called once per frame
    void Update()
    {
        if (canRotateHead)
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

            forward = transform.forward;
        }

        

    }
}
