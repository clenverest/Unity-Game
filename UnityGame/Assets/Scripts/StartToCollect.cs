using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartToCollect : MonoBehaviour
{
    [SerializeField] GameObject keyText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            keyText.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            keyText.SetActive(false);
    }
}
