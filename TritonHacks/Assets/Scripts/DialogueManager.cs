using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;
    public Texture2D portrait;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        dialogueText.text = "";
        nameText.text = "";
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
        StartCoroutine(waitFor(dialogue));
    }

    IEnumerator waitFor(Dialogue dialogue)
    {
        yield return new WaitForSeconds(1);
        begin(dialogue);
    }

    private void begin(Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;

        }
    }

    private void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        dialogueText.text = "";
        nameText.text = "";
        Debug.Log("End of Dialogue");
    }

    
}
