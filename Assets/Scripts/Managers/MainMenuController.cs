using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private Button musicBtn;
    [SerializeField]
    private Sprite[] musicSprite;
    [SerializeField]
    private Text highScore;
    public MusicManager mM;
    private void Start()
    {
        highScore.text = "High Score: " + GameRef.GetHighScore();
        CheckMusic();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(ConstantsValue.ARENA);
    }
    public void Quit()
    {
        Application.Quit();
    }

    void CheckMusic()
    {
        if (GameRef.GetMusicState() == 1)
        {
            musicBtn.image.sprite = musicSprite[1];
        }
        if (GameRef.GetMusicState() == 0)
        {
            musicBtn.image.sprite = musicSprite[0];
        }
    }
    public void Music()
    {
        if (GameRef.GetMusicState() == 1)
        {
            GameRef.SetMusicState(0);
            mM.UpdateMusicState();
            musicBtn.image.sprite = musicSprite[0];
        }
        else if (GameRef.GetMusicState() == 0)
        {

            GameRef.SetMusicState(1);
            mM.UpdateMusicState();
            musicBtn.image.sprite = musicSprite[1];
        }
    }
}
