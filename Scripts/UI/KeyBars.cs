using System.Collections;
using UnityEngine;

public class KeyBars : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float waitTime;
    [SerializeField] private float outTime = 10f;

    private void Start()
    {
        StartCoroutine(startAnim());
       
    }

    private IEnumerator startAnim()
    {
        yield return new WaitForSeconds(waitTime);
        animator.SetTrigger("isTriggered");
        yield return new WaitForSeconds(outTime);
        animator.SetTrigger("isTriggered");
    }
}
