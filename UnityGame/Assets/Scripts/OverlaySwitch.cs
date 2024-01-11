using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlaySwitch : MonoBehaviour
{
    [SerializeField] GameObject OverlayLadder1;
    [SerializeField] GameObject OverlayLadder2;

    public bool PlayerHere;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FeetPos"))
        {
            PlayerHere = true;
            OverlayLadder1.SetActive(true);
            OverlayLadder2.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("FeetPos"))
        {
            PlayerHere = false;
            OverlayLadder1.SetActive(false);
            OverlayLadder2.SetActive(false);
        }
    }
}
