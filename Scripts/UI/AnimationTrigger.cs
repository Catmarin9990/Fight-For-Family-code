using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AnimationTrigger : MonoBehaviour
{
    [SerializeField] private DialogueMeneger meneger;
    [SerializeField] private Animator[] cerectorsAnim;
    [SerializeField] PlayableDirector[] director;
    [SerializeField] PlayableDirector[] trueDirectors;
    [SerializeField] PlayableDirector[] falseDirectors;
    [SerializeField] private Skills sheald;

    [SerializeField] private AudioSource umbrella;
    [SerializeField] private AudioClip fire;



    private bool isPlayed = false;
    private bool isPlayed2 = false;
    private bool isPlayed3 = false;
    public bool ispressed = false;
    public char key;

    private void Update()
    {
       switch(meneger.dialogueCounter)
       {
            case 6:
                cerectorsAnim[0].SetTrigger("gunUp");
                break;
            case 10:
                if (!isPlayed)
                {
                    meneger.dialogueTime = 8f;
                    director[0].Play();
                    isPlayed = true;
                }
                break;
            case 11:
                if (!isPlayed2)
                {
                    meneger.dialogueTime = 6f;
                    umbrella.PlayOneShot(fire);
                    director[1].Play();
                    StartCoroutine(IsPressed());
                    isPlayed2 = true;
                }
                break;
            case 12:
                meneger.dialogueTime = 3f;
                if (GameObject.Find("QTEX") != null)
                {
                    GameObject.Find("QTEX").SetActive(false);
                }
                break;
            case 13:
                if (!isPlayed3)
                {
                    meneger.dialogueTime = 6f;
                    umbrella.PlayOneShot(fire);
                    director[2].Play();
                    StartCoroutine(IsPressed());
                    isPlayed3 = true;
                }
                break;
            case 14:
                if (GameObject.Find("QTEL") != null)
                {
                    GameObject.Find("QTEL").SetActive(false);
                }
                meneger.dialogueTime = 5;
                break;
       }
    }
    private IEnumerator IsPressed()
    {
        yield return new WaitForSeconds(4f);
        if (!ispressed && meneger.dialogueCounter == 11)
        {
            falseDirectors[0].Play();
            yield return null;
        }
        else if(!ispressed && meneger.dialogueCounter == 13)
        {
            falseDirectors[1].Play();
            yield return null;

        }
        else if (meneger.dialogueCounter == 11 && ispressed && key == 'x')
        {
            StartCoroutine(sheald.ShieldSkill());
            trueDirectors[0].Play();
            yield return null;
        }
        else if (meneger.dialogueCounter == 13 && ispressed && key == 'l')
        {
            StartCoroutine(sheald.ShieldSkill());
            trueDirectors[1].Play();
            yield return new WaitForSeconds(0.5f);
            cerectorsAnim[0].SetTrigger("fall");
            yield return null;
        }
        else
        {
            falseDirectors[0].Play();
            yield return null;
        }

    }
}
