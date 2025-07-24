using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerScore : MonoBehaviour
{

    private Vector3 previousPos;
    private int difCheck;


    public int scoreCount ;


    // Start is called before the first frame update
    void Start()
    {
        previousPos = transform.position;
        GameplayController.instance.SetScore(scoreCount);
    }

    // Update is called once per frame
    void Update()
    {
        CountScore();
        if (scoreCount - difCheck > 500 )
        {
            GameplayController.instance.DifficultyLevelUp();
            difCheck = scoreCount;
        }
    }
    void CountScore()
    {
        {
            if (transform.position.x > previousPos.x)
            {
                scoreCount++;

                GameplayController.instance.SetScore(scoreCount);
            }
            previousPos = transform.position;
        }
    }
    public void ObsticaleKilled(int bonus)
    {
        scoreCount += bonus;
        GameplayController.instance.SetScore(scoreCount);

    }

}
