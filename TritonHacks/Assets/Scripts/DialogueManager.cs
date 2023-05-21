using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    private Queue<int> portraits;
    public Text nameText;
    public Text dialogueText;

    public GameObject Portrait;
    public GameObject Static;

    public Animator animator;
    public Animator Portraits;
    public Animator PortraitWindow;

    public PlayerController player;

    public bool isActive = false;

    public void Update(){
        if(Input.GetButtonDown("Continue") && isActive){
            DisplayNextSentence();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        dialogueText.text = "";
        
        nameText.text = "";
        Portrait.SetActive(false);
        Static.SetActive(false);
        sentences = new Queue<string>();
        portraits = new Queue<int>();

    }

    public void StartDialogue (Dialogue dialogue)
    {
        
        //Portrait.GetComponent<Image>().sprite = Unknown.sprite;
        isActive = true;
        player.Lock();
        animator.SetBool("isOpen", true);
        Portrait.SetActive(false);
        StartCoroutine(waitFor(dialogue));
    }

    IEnumerator waitFor(Dialogue dialogue)
    {
        yield return new WaitForSeconds(0.7f);
        Static.SetActive(true);
        PortraitWindow.Play("Portrait_Open");
        yield return new WaitForSeconds(0.3f);
        Portrait.SetActive(true);
        setPortrait(dialogue.portraits[0]);
        Portraits.Play("Portrait_Flicker");
        yield return new WaitForSeconds(0.8f);

        begin(dialogue);
    }

    private void begin(Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentences.Clear();

        for(int i = 0; i < dialogue.sentences.Length; i++)
        {
            sentences.Enqueue(dialogue.sentences[i]);
            portraits.Enqueue(dialogue.portraits[i]);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        if(sentences.Count == 0)
        {
            StopAllCoroutines();
            EndDialogue();
            return;
        }

        setPortrait(portraits.Dequeue());
        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    public void setPortrait(int num)
    {
        switch (num)
        {
            case 0:
                Portraits.Play("Unknown");
                break;
            case 1:
                Portraits.Play("Albert_Normal");
                break;
            case 2:
                Portraits.Play("Albert_Angry");
                break;
            case 3:
                Portraits.Play("Albert_Concern");
                break;
            case 4:
                Portraits.Play("Albert_Sad");
                break;
            case 5:
                Portraits.Play("Jones");
                break;
        }
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
        PortraitWindow.Play("Portrait_Close");
        dialogueText.text = "";
        nameText.text = "";
        Portrait.SetActive(false);
        Static.SetActive(false);
        isActive = false;
        player.Unlock();
        Debug.Log("End of Dialogue");
    }

    
}
