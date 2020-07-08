using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class InventoryBehavior : MonoBehaviour
{
    public List<string> inventoryItemNames;
    public GameObject inventoryCanvas;
    public GameObject playerCamera = null;
    public GameObject[] inventoryItemSlots;
    bool statusInfected = false;
    int statusDaysInfected = -1;
    int statusMentalState = 5;
    public float probabilityInfectionOutside = 0.3f;
    public float probabilityMaskProtection = -0.6f;
    public float probabilityInfectionStairs = 0.35f;
    public float probabilityInfectionEmptyElevator = 0.45f;
    public float probabilityInfectionNonEmptyElevator = 0.6f;
    public bool isMaskEquipped = false;
    public float generalProbabilityInfection = 0.0f;
    public TextMeshProUGUI textInfected;
    public TextMeshProUGUI textDaysInfected;
    public TextMeshProUGUI textMentalState;
    public TextMeshProUGUI textMaskEquipped;
    
    
        
   private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    

    // Start is called before the first frame update
    void Start()
    {
        textInfected.text = "No";
        textDaysInfected.text = "Not applicable";
        textMentalState.text = "Healthy";
        textMaskEquipped.text = "No";
        inventoryItemNames = new List<string>(); 
    }

    public void EquipMask()
    {
        if (!isMaskEquipped)
        {
            textMaskEquipped.text = "Yes";

            if (generalProbabilityInfection < 0.0f)
            {
                generalProbabilityInfection = 0.0f;
            }

            isMaskEquipped = true;
        }


    }

    public bool GotInfected()
    {
        int sceneID = SceneManager.GetActiveScene().buildIndex;

        if (!statusInfected)
        {

            if (sceneID < 3)
            {
                textInfected.text = "No";
                textDaysInfected.text = "Not applicable";
                textMentalState.text = "Healthy";

                statusInfected = false;
                statusDaysInfected = -1;
                statusMentalState = 5;



                return false;

            }
            else
            {

                System.Random random = new System.Random();

                bool result = random.NextDouble() < generalProbabilityInfection;

                statusInfected = result;

                if (statusInfected)
                {
                    statusDaysInfected = 0;
                    statusMentalState = 4;

                    textInfected.text = "Yes";
                    textDaysInfected.text = "0";
                    textMentalState.text = "Worried";
                }
                else
                {
                    textInfected.text = "No";
                    textDaysInfected.text = "Not applicable";
                    textMentalState.text = "Healthy";

                    statusDaysInfected = -1;
                    statusMentalState = 5;

                }

                

                return statusInfected;
            }



        }

        return true;

    }

    public void AddProbabilityInfectionOutside()
    {
        generalProbabilityInfection += probabilityInfectionOutside;
    }

    public void AddProbabilityMaskProtection()
    {
        generalProbabilityInfection += probabilityMaskProtection;
    }

    public void AddProbabilityInfectionStairs()
    {
        generalProbabilityInfection += probabilityInfectionStairs;
    }

    public void AddProbabilityInfectionEmptyElevator()
    {
        generalProbabilityInfection += probabilityInfectionEmptyElevator;
    }

    public void AddProbabilityInfectionNonEmptyElevator()
    {
        generalProbabilityInfection += probabilityInfectionNonEmptyElevator;
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
