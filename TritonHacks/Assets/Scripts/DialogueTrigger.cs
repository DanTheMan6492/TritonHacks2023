using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    public Dialogue dialogue;
    new Animator animation;

    void Awake(){
        animation = GetComponent<Animator>();
    }
    public void TriggerDialogue()
    {
        animation.SetBool("Active", true);
        FindObjectOfType<PlayerController>().Lock();
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void Update(){
        animation.SetBool("Active", FindObjectOfType<DialogueManager>().isActive);;
    }
    public void Interact(){
        TriggerDialogue();
    }

    public bool CanInteract(){
        return true;
    }
}
