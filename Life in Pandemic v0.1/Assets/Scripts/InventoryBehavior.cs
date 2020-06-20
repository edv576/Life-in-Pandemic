using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryBehavior : MonoBehaviour
{
    public List<string> inventoryItemNames;
    public GameObject inventoryCanvas;
    public GameObject playerCamera = null;
    public GameObject[] inventoryItemSlots;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    

    // Start is called before the first frame update
    void Start()
    {

        inventoryItemNames = new List<string>(); 
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (inventoryCanvas.activeSelf)
            {
                inventoryCanvas.SetActive(false);
                playerCamera.GetComponent<MouseCameraMovement>().canRotateHead = true;
            }
            else
            {
                inventoryCanvas.SetActive(true);
                playerCamera.GetComponent<MouseCameraMovement>().canRotateHead = false;
            }
        }
    }
}
