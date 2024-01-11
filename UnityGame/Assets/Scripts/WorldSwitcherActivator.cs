using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSwitcherActivator : MonoBehaviour
{
    [SerializeField] GameObject keyText;
    bool playerDetected;

    static bool worldSwitcherActive;
    public static bool WorldSwitcherActive() => worldSwitcherActive;

    public static void WorldSwitcherOn()
    {
        worldSwitcherActive = true;
    }

    public static void WorldSwitcherOff()
    {
        worldSwitcherActive = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerDetected = false;
        worldSwitcherActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerDetected)
        {
            keyText.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Z))
            {
                Destroy(keyText);
                playerDetected = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            worldSwitcherActive = true;
            playerDetected = true;
        }
    }
}
