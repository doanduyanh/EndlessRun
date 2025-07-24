using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public int difficultyLevel = 0;
    private int currentScore = 0;
    [SerializeField]
    private Text scoreText, gameoverScoreText;
    [SerializeField]
    private GameObject pausePanel, gameoverPanel;
    // Start is called before the first frame update
    void Awake()
    {
        MakeInstance();
    }

    private void Start()
    {
        Time.timeScale = 1;
    }
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void DifficultyLevelUp()
    {
        difficultyLevel++;
        Notify("DifficultyLevelUpNotify", difficultyLevel);
    }
    IEnumerator BackToMainMenu()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(ConstantsValue.MAINMENU);
    }
    public void  MainMenu()
    {
        SceneManager.LoadScene(ConstantsValue.MAINMENU);
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(ConstantsValue.ARENA);
    }
    public void NewGame()
    {
        difficultyLevel = 0;
        SetScore(0);
    }
    public void StartGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }
    public void SetScore(int score)
    {
        scoreText.text = "Score: "+score;
        currentScore = score;
    }

    public void GameOver()
    {
        GameManager.instance.CalculateHightScore(currentScore);
        gameoverPanel.SetActive(true);
        gameoverScoreText.text = currentScore.ToString();
        difficultyLevel = 0;
        StartCoroutine(BackToMainMenu());
    }
    private List<IObserver> observers = new List<IObserver>();

    public void RegisterObserver(IObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void UnregisterObserver(IObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    public void Notify(string eventType, object parameter = null)
    {
        foreach (var observer in observers)
        {
            observer.OnNotify(eventType, parameter);
        }
    }
}
