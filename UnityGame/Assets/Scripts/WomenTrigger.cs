using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomenTrigger : MonoBehaviour
{
    private Animator animator;
    [SerializeField] GameObject pentagram;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        animator.SetBool("isActive", true);
        pentagram.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        animator.SetBool("isActive", false);

    }
}
