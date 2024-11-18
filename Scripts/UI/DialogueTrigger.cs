using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    bool isActive = false;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueMeneger>().StartDialogue(dialogue);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isActive)
        {
            TriggerDialogue();
            isActive = true;
        }
    }



}
