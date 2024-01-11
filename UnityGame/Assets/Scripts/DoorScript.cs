using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    bool playerDetected;
    [SerializeField] Transform PosToGo;
    [SerializeField] GameObject Text;
    [SerializeField] GameObject AnimObject;
    [SerializeField] GameObject FadeObject;
    GameObject playerGO;
    bool isSceneActive = false;

    void Start()
    {
        playerDetected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(Transition());
            }
        }
    }

    IEnumerator Transition()
    {
        if (!isSceneActive)
        {
            AnimObject.SetActive(true);
            isSceneActive = true;
        }
        FadeObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        playerGO.transform.position = PosToGo.position;
        playerDetected = false;

        yield return new WaitForSeconds(1.5f);
        FadeObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
            playerGO = collision.gameObject;
            Text.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = false;
            Text.SetActive(false);
        }
    }
}
