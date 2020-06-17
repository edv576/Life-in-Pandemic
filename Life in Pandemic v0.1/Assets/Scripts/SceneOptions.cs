using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneOptions : MonoBehaviour
{

    public GameObject inventory = null;


    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Inventory");
        int numberItems = inventory.GetComponent<InventoryBehavior>().inventoryItemNames.Count;
        List<string> listItemNames = inventory.GetComponent<InventoryBehavior>().inventoryItemNames;

        for (int i = 0; i < numberItems; i++)
        {
            GameObject.Find(listItemNames[i]).SetActive(false);
            GameObject.Find(listItemNames[i] + " block").SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
