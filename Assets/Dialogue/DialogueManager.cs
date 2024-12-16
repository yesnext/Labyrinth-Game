using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;
    public GameObject continueButton;
    public Animator animator;
    private Queue<string> sentences;
    private Queue<string> names;
    void Start()
    {
        dialogueBox.SetActive(false);
        continueButton.SetActive(false);
        sentences = new Queue<string>();
        names = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    { dialogueBox.SetActive(true);
     continueButton.SetActive(true);
        animator.SetBool("isOpen", true);

        Debug.Log("Starting conversation with " + dialogue.names);
        sentences.Clear();
        names.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (string name in dialogue.names)
        {
            names.Enqueue(name);
        }
        DisplayNextName();
           DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
          DisplayNextName();
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
     public void DisplayNextName()
    {
        if (names.Count == 0)
        {
            EndDialogue();
            return;
        }
        string name = names.Dequeue();
        nameText.text=name;
    }
    IEnumerator TypeSentence(string sentence)
    { dialogueBox.SetActive(true);
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }
    void EndDialogue()
    {
        dialogueBox.SetActive(false);
        continueButton.SetActive(false);
           

        animator.SetBool("isOpen", false); ;
    }
}

