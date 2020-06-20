using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{

    float speed = 4.5f;
    float rotSpeed = 80;
    float rot = 0.0f;
    float gravity = 8;
    Camera cam;
    bool isDeciding = false;
    public GameObject decisionCanvas;
    public GameObject itemCanvas;
    public GameObject exitCanvas;
    public GameObject txtItem;
    public GameObject inventory = null;
    GameObject itemToTake = null;
    string originalItemText;


    Vector3 moveDir = Vector3.zero;

    CharacterController controller;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        cam = GetComponentInChildren<Camera>();
        inventory = GameObject.Find("Inventory");
        if(inventory != null)
        {
            inventory.GetComponent<InventoryBehavior>().playerCamera = GetComponentInChildren<MouseCameraMovement>().gameObject;
        }
        
        //txtItem = GameObject.Find("TxtPickItem");
        originalItemText = txtItem.GetComponent<Text>().text;


    }

    // Update is called once per frame
    void Update()
    {
        //if (!isDeciding)
        //{
        //    Movement();
        //}

        Movement();

        if (Input.GetKeyDown(KeyCode.E) && decisionCanvas.activeSelf)
        {
            //lastPlayerPosition = transform.position;
            if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 0)
            {
                
                SceneManager.LoadScene(2);
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }


        if (Input.GetKeyDown(KeyCode.E) && !decisionCanvas.activeSelf && itemCanvas.activeSelf)
        {
            GameObject newItem = new GameObject();
            newItem.name = itemToTake.name + " item";
            inventory.GetComponent<InventoryBehavior>().inventoryItemNames.Add(itemToTake.name);
            inventory.GetComponent<InventoryBehavior>().inventoryItemSlots[inventory.GetComponent<InventoryBehavior>().inventoryItemNames.Count - 1].SetActive(true);
            inventory.GetComponent<InventoryBehavior>().inventoryItemSlots[inventory.GetComponent<InventoryBehavior>().inventoryItemNames.Count - 1].GetComponentInChildren<Text>().text = itemToTake.name;
            itemToTake.SetActive(false);
            newItem.transform.parent = GameObject.Find("Items").transform;
            itemCanvas.SetActive(false);

        }




    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Stairs")
    //    {
    //        print("Touched stairs");
    //        isDeciding = true;
    //        decisionCanvas.SetActive(true);

    //    }
    //}

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Stairs")
        {
            //print("Touched stairs");
            //isDeciding = true;
            //decisionCanvas.SetActive(true);

        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Stairs")
        {
            print("Touched stairs");
            isDeciding = true;
            decisionCanvas.SetActive(true);

        }

        if(other.gameObject.tag == "Item")
        {
            itemCanvas.SetActive(true);
            txtItem.GetComponent<Text>().text = originalItemText + other.gameObject.name;
            itemToTake = other.gameObject;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Stairs")
        {
            //print("Touched stairs");
            //isDeciding = true;
            decisionCanvas.SetActive(false);

        }

        if (other.gameObject.tag == "Item")
        {
            itemCanvas.SetActive(false);
        }


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

            if (Input.GetKeyDown(KeyCode.C))
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
