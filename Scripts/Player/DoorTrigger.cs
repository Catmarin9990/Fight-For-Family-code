using UnityEngine;


public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject frame;
    [SerializeField] private GameObject[] otherFrames;
    [SerializeField] private Animator animator;
    [SerializeField] private LevelChanger level;
    private bool isIn = false;



    private void Update()
    {
        if(isIn && Input.GetKeyDown(KeyCode.E))
        {
            level.FadeToLevel();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isIn = true;
            animator.SetTrigger("isTriggered");
            frame.SetActive(true);
            foreach (GameObject frame in otherFrames)
            {
                frame.SetActive(false);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isIn = false;
            animator.SetTrigger("isTriggered");
        }
    }
}