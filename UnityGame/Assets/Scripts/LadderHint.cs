using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderHint : MonoBehaviour
{
    Dialogue dialogueForHint;
    [SerializeField] GameObject hint;

    void Start()
    {
        dialogueForHint = new Dialogue();
        dialogueForHint.speeches = new[]
        {
            new string (":I think there are more artifacts on the floor" ),
            new string (":I need to collect them all!" ),
        };
    }

    bool isKeyDown;
    bool playerDetected;

    private void Update()
    {
        if (playerDetected)
        {
            isKeyDown = (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow));
            if (!CheckInventory() && isKeyDown)
            {
                FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueForHint);
            }

            isLadderBlocked = !CheckInventory();
        }
    }

    static bool isLadderBlocked;

    public static bool IsLadderBlocked()
    {
        return isLadderBlocked;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
            if (Candle.CandleCount() == 1)
            {
                hint.SetActive(true);
            }
        }
    }

    bool CheckInventory()
    {
        return Candle.CandleCount() > 0 && Pentagram.PentagramCount() > 0 && Parchment.ParchmentCount() > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = false;
            if (hint.activeInHierarchy)
            {
                hint.SetActive(false);
            }
        }
    }
}
