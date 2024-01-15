using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSwitch : MonoBehaviour
{
    public GameObject NonActiveWorld;
    [SerializeField] GameObject Anim;


    void Update()
    {
        if(WorldSwitcherActivator.WorldSwitcherActive() && Input.GetKeyDown(KeyCode.Z) && gameObject.activeInHierarchy && !NonActiveWorld.activeInHierarchy)
        {
            StartCoroutine(Transition());
        }
    }

    IEnumerator Transition()
    {
        Anim.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        NonActiveWorld.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Anim.SetActive(false);
        gameObject.SetActive(false);
    }
}
