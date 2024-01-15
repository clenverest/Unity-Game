using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryActivator : MonoBehaviour
{
    [SerializeField] GameObject dialogue;
    [SerializeField] GameObject keyText;
    [SerializeField] GameObject artifact;
    [SerializeField] GameObject inventory;

    bool playerDetected = false;

    void Update()
    {
        if(playerDetected)
        {
            DialogueCheck();
            if (!isDialogueActive)
            {
                inventory.SetActive(true);
                if(!artifact.IsUnityNull())
                    artifact.GetComponent<BoxCollider2D>().enabled = true;
            }
            else
            {
                if (!artifact.IsUnityNull())
                    artifact.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        
        if(inventoryActive)
        {
            keyText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Destroy(keyText);
                Destroy(gameObject);
            }
        }
        else
        {
            keyText.SetActive(false);
        }
    }

    bool isDialogueActive;
    bool inventoryActive;

    void DialogueCheck()
    {
        if (dialogue.activeInHierarchy)
        {
            isDialogueActive = true;
        }
        else
        {
            isDialogueActive = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
            inventoryActive = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = false;
            inventoryActive = true;
        }
    }
}
