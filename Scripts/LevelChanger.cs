using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private Animator anim;
     public int levelToLoad;

    [SerializeField] Vector3 position;
    [SerializeField] VectorValue playerStorage;

    public void FadeToLevel()
    {
        anim.SetTrigger("fade");
    }

    private void OnFadeToLevel()
    {
        playerStorage.initialValue = position;
        SceneManager.LoadScene(levelToLoad);
    }

}
