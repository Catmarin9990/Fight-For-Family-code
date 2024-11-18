using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnimator : MonoBehaviour
{
    bool isActive = false;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isActive)
        {
            isActive = true;
            FindObjectOfType<DialogueTrigger>().TriggerDialogue();
        }
    }
        
}
