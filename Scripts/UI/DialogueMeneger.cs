using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueMeneger : MonoBehaviour
{
    public Text dialogueText;
    public Image Image;

    public Animator boxAnim;

    private Queue<string> sentences;
    private Queue<Sprite> images;
    public bool dialogyEnded = false;
    public int dialogueCounter = 0;
    public float dialogueTime = 5f;

    private void Start()
    {
        sentences = new Queue<string>();
        images = new Queue<Sprite>();   
    }

    public void StartDialogue(Dialogue dialogue)
    {
        boxAnim.SetBool("open", true);
        sentences.Clear();
        foreach (string sentance in dialogue.sentenses)
        {
            sentences.Enqueue(sentance);
        }
        foreach(Sprite image in dialogue.image)
        {
            images.Enqueue(image);
        }
        DisplayNextSentance();

    }
    public void DisplayNextSentance() 
    {
        
        if (sentences.Count == 0) 
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        Image.sprite = images.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentance(sentence));
    }

    IEnumerator TypeSentance(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) 
        {
            dialogueText.text += letter;
            yield return null;
        }
        yield return new WaitForSeconds(dialogueTime);
        dialogueCounter++;
        DisplayNextSentance();
    }

    public void EndDialogue() 
    {
        boxAnim.SetBool("open", false);
        dialogyEnded = true;
    }
}
