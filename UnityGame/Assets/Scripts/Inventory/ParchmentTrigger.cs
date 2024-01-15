using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParchmentTrigger : MonoBehaviour
{
    bool playerDetected;

    void Start()
    {
        playerDetected = false;
    }

    void Update()
    {
        if (playerDetected)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Parchment.AddParchment();
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }
}
