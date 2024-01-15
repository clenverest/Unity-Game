using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    bool playerDetected;
    [SerializeField] Transform PosToGo;
    GameObject playerGO;

    void Start()
    {
        playerDetected = false;
    }

    // Update is called once per frame
    void Update()
    {
       if(playerDetected)
        {
            if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                playerGO.transform.position = PosToGo.position;
                playerDetected = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
            playerGO = collision.gameObject;
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
