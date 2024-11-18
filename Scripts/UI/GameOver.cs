using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private int levelToLoad;

    [SerializeField] Vector3 position;
    [SerializeField] VectorValue playerStorage;
    [SerializeField] GameObject gameOver;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FadeToGameOver()
    {
        anim.SetTrigger("fade");
    }

    private void RestartLevel()
    {
        playerStorage.initialValue = position;
        SceneManager.LoadScene(levelToLoad);
    }
}
