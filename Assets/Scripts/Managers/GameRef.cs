using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameRef
{
    public static string IsMusicOn = "IsMusicOn";
    public static string HighScore = "HighScore";

    public static void SetMusicState(int state)
    {
        PlayerPrefs.SetInt(GameRef.IsMusicOn, state);
    }
    public static int GetMusicState()
    {
        return PlayerPrefs.GetInt(GameRef.IsMusicOn);
    }

    public static void SetHighScore(int state)
    {
        PlayerPrefs.SetInt(GameRef.HighScore, state);
    }
    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt(GameRef.HighScore);
    }

}
