using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class InventoryBehavior : MonoBehaviour
{
    //Names of the inventory items in text
    public List<string> inventoryItemNames;
    //Canvas where the items in the inventory are listed
    public GameObject inventoryCanvas;
    //Camera of the player
    public GameObject playerCamera = null;
    //List with the maximum number of items for the inventory. This can be changed later to make this number dinamic since right now is fixed.
    public GameObject[] inventoryItemSlots;
    //Infected status of the player. It starts with false at the beginning since the user is not infected
    bool statusInfected = false;
    //Unused variable that stores the number of days the user has been infected. Starts with -1 (for uninfected) but changes to 0 once the
    //player gets infected.
    int statusDaysInfected = -1;
    //Unused variable that represents the mental status of the player. It starts as 5 for maximum mental health but the number can go down 
    //if the player gets infected.
    int statusMentalState = 5;
    //Probability of infection added if the player goes out of the apartment
    public float probabilityInfectionOutside = 0.3f;
    //Probability of infection added if the player is wearing a mask. The negative number means that it actually makes less probable to get 
    //infected when wearing the mask.
    public float probabilityMaskProtection = -0.6f;
    //Probability of infection added if the player goes down the stairs and doesn't use the elevator. 
    public float probabilityInfectionStairs = 0.35f;
    //Probability of infection added if the  player uses an empty elevator.
    public float probabilityInfectionEmptyElevator = 0.45f;
    //Probability of infection added if the  player uses a non empty elevator.
    public float probabilityInfectionNonEmptyElevator = 0.6f;
    //Flag telling if the player is wearing a mask.
    public bool isMaskEquipped = false;
    //Initial probability of infection. It starts at 0 but will increase/decrease depending of the actions taken (use elevator, wear mask, etc.)..
    public float generalProbabilityInfection = 0.0f;
    //Text object that shops if the player is infected or not. It should be hidden.
    public TextMeshProUGUI textInfected;
    //Text object that shows the number of days infected. If the player is not infected, it will just show "Not applicable".
    public TextMeshProUGUI textDaysInfected;
    //Text object that shows the mental status of the player.
    public TextMeshProUGUI textMentalState;
    //Text that shows if the player is wearing a mask or not.
    public TextMeshProUGUI textMaskEquipped;
    
    
   //Definining the game object as constant between scenes
   private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    

    // Start is called before the first frame update
    void Start()
    {
        //Initialize all the text objects and the inventory names. The later one will be empty at the beginning.
        textInfected.text = "No";
        textDaysInfected.text = "Not applicable";
        textMentalState.text = "Healthy";
        textMaskEquipped.text = "No";
        inventoryItemNames = new List<string>(); 
    }

    //Function that equips the mask and changes the wearing mask flag to true. If the general probability of infection is less than 0
    //it won't decrease it even further an keep it a zero.
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

    //Defines what happens when the player gets infected depending to the scene.
    public bool GotInfected()
    {
        int sceneID = SceneManager.GetActiveScene().buildIndex;

        if (!statusInfected)
        {

            //In the scenes inside the apartment it just defines the player as non infected since the probability of that happening is zero.
            //This can be changed depending of the game design.
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

            //In any other case it uses the general probability of infection to determine if the player gets infected or not. The final result
            //can have the user get infected or not.
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


    //The following 6 functions add probability of infection depending of the player's actions.
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
        //Press F to show or hide the inventory UI.
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
