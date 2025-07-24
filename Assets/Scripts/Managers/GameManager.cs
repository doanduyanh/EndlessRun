using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }
    void Start()
    {
        InitVar();
    }


    void InitVar()
    {
        if (!PlayerPrefs.HasKey("Inited"))
        {
            GameRef.SetMusicState(0);
            GameRef.SetHighScore(0);

            PlayerPrefs.SetInt("Inited", 1);
        }
    }
    void OnLevelWasLoaded(int level)
    {
        if(SceneManager.GetActiveScene().buildIndex == ConstantsValue.ARENA)
        {
            GameplayController.instance.NewGame();
        }
    }
    public void CalculateHightScore(int score)
    {
        int highScore = GameRef.GetHighScore();
        if (highScore < score)
        {
            GameRef.SetHighScore(score);
        }
    }
}
