using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTrigger : MonoBehaviour
{
    Dialogue dialogueForNotFullInventory;
    Dialogue dialogueForFullInventory;
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject ritual;

    private bool isFullInventory;

    private void Start()
    {
        isFullInventory = false;
        dialogueForNotFullInventory = new Dialogue();
        dialogueForNotFullInventory.speeches = new[] { ":I need to collect more artifacts." };
        dialogueForFullInventory = new Dialogue();
        dialogueForFullInventory.speeches = new[]
        {
            new string (":I collect all artifacts." ),
            new string (":it's time to start the ritual" ),
        };
    }

    private void Update()
    {
        IsFullInventory();
    }

    public void TriggerNotFinalDialogue()
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueForNotFullInventory);
    }

    public void TriggerFinalDialogue()
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueForFullInventory);
    }

    public void IsFullInventory()
    {
        isFullInventory = (Candle.CandleCount() == 3 && Parchment.ParchmentCount() == 7 && Pentagram.PentagramCount() == 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            WorldSwitcherActivator.WorldSwitcherOff();
            if (isFullInventory)
            {
                collision.transform.position = new Vector3(32.09396f, -42.21772f);
                collision.transform.localScale = Vector3.one;
                inventory.SetActive(false);
                ritual.SetActive(true);
                TriggerFinalDialogue();
                Final.FinalGame();
            }
            else
            {
                TriggerNotFinalDialogue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isFullInventory)
        {
            WorldSwitcherActivator.WorldSwitcherOn();
        }
    }
}
