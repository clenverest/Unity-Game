using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject ritual;
    private Animator animatorPlayer;
    private static bool isFinal;
    private static bool isTheEnd;
    private static bool isTheFinalPlayerAnimation;
    // Start is called before the first frame update
    void Start()
    {
        isFinal = false;
        isTheEnd = false;
        animatorPlayer = player.GetComponent<Animator>();
        isTheFinalPlayerAnimation = false;
    }

    public static bool IsTheFinalPlayerAnimation()
    {
        return isTheFinalPlayerAnimation;
    }

    public static void FinalGame()
    {
        isFinal = true;
    }

    public static bool IsFinalGame()
    {
        return isFinal;
    }

    public static bool IsTheEndGame()
    {
        return isTheEnd;
    }

    [SerializeField] GameObject NonActiveWorld;
    [SerializeField] GameObject ActiveWorld;
    [SerializeField] GameObject AnimTransWorld;
    [SerializeField] GameObject dialogue;

    // Update is called once per frame
    void Update()
    {
        if(isFinal)
        {
            ritual.SetActive(true);
            if (!dialogue.activeInHierarchy)
            {
                isFinal = false;
                isTheFinalPlayerAnimation = true;
                StartCoroutine(Transition());
            }
        }
    }

    IEnumerator Transition()
    {
        yield return new WaitForSeconds(0.5f);
        AnimTransWorld.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        NonActiveWorld.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        AnimTransWorld.SetActive(false);
        Destroy(ActiveWorld);
        yield return new WaitForSeconds(3f);
        animatorPlayer.SetTrigger("final");
        yield return new WaitForSeconds(8f);
        isTheEnd = true;
    }
}
