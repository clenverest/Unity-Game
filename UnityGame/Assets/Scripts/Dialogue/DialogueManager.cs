using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject Dialogue;

    private Queue<string> speeches;
    bool isSentenceActive;

    private void Start()
    {
        speeches = new Queue<string>();
        isSentenceActive = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && !isSentenceActive)
        {
            DisplayNextSpeech();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Dialogue.SetActive(true);
        WorldSwitcherActivator.WorldSwitcherOff();
        speeches.Clear();

        foreach(var speech in dialogue.speeches)
        {
            speeches.Enqueue(speech);
        }
        DisplayNextSpeech();
    }

    public void DisplayNextSpeech()
    {
        if(speeches.Count == 0 )
        {
            EndDialogue();
            return;
        }

        var nameAndSentence = speeches.Dequeue().Split(new[] {':'}, 2);
        nameText.text = nameAndSentence[0];
        StopAllCoroutines();
        StartCoroutine(TypeSentence(nameAndSentence[1]));

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        isSentenceActive = false;
        foreach (var letter in sentence)
        {
            isSentenceActive = true;
            dialogueText.text += letter;
            yield return null;
        }
        isSentenceActive = false;
    }

    public void EndDialogue()
    {
        Dialogue.SetActive(false);
        WorldSwitcherActivator.WorldSwitcherOn();
    }
}
