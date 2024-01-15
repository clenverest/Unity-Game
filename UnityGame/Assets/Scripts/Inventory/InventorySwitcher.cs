using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySwitcher : MonoBehaviour
{
    [SerializeField] GameObject inventory;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(inventory.activeInHierarchy)
            {
                inventory.SetActive(false);
            }
            else
            {
                inventory.SetActive(true);
            }
        }    
    }
}
