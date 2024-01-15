using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseAnim : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        animator.SetBool("isActive", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("isActive", false);
    }
}
