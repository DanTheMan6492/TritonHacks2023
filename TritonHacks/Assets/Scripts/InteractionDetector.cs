using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    private List<IInteractable> _interactablesInRange = new List<IInteractable>();
    public PlayerController player;

    // Update is called once per frame
    void Update()
    {
        if(!player.lockControls && Input.GetButtonDown("Interact") && _interactablesInRange.Count > 0){
            Debug.Log("Entered");
            var interactable = _interactablesInRange[0];
            interactable.Interact();
            if(!interactable.CanInteract())
                _interactablesInRange.Remove(interactable);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){

        var interactable = other.GetComponent<IInteractable>();
        if(interactable != null && interactable.CanInteract()){
            //Debug.Log("Entered");
            _interactablesInRange.Add(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        var interactable = other.GetComponent<IInteractable>();
        if(_interactablesInRange.Contains(interactable)){
            _interactablesInRange.Remove(interactable);
        }
    }
}
