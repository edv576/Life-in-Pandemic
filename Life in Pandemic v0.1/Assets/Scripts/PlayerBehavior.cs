using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    float speed = 4.5f;
    float rotSpeed = 80;
    float rot = 0.0f;
    float gravity = 8;
    Camera cam;

    Vector3 moveDir = Vector3.zero;

    CharacterController controller;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        Movement();
        

        
    }

    void Movement()
    {
        if (controller.isGrounded)
        {

            if (Input.GetKey(KeyCode.W))
            {
                anim.SetInteger("condition", 1);
                moveDir = new Vector3(cam.gameObject.transform.forward.x, 0, cam.gameObject.transform.forward.z);
                moveDir *= speed;
                //transform.eulerAngles = new Vector3(0, cam.gameObject.transform.eulerAngles.y, 0);
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 0);
            }

            if (Input.GetKey(KeyCode.S))
            {
                anim.SetInteger("condition", 2);
                moveDir = new Vector3(cam.gameObject.transform.forward.x, 0, cam.gameObject.transform.forward.z);
                moveDir *= -speed;
                //transform.eulerAngles = new Vector3(0, cam.gameObject.transform.eulerAngles.y, 0);
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 0);

            }

            if (Input.GetKey(KeyCode.A))
            {
                moveDir = new Vector3(cam.gameObject.transform.right.x, 0, cam.gameObject.transform.right.z);
                moveDir *= -speed;
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                moveDir = new Vector3(0, 0, 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                moveDir = new Vector3(cam.gameObject.transform.right.x, 0, cam.gameObject.transform.right.z);
                moveDir *= speed;
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                moveDir = new Vector3(0, 0, 0);

            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                anim.SetInteger("condition", 3);
            }
        }

        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        if(anim.GetInteger("condition") != 3)
        {
            transform.eulerAngles = new Vector3(0, cam.gameObject.transform.eulerAngles.y, 0);
        }
        
        //transform.eulerAngles = new Vector3(0, rot, 0);

        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }
}
