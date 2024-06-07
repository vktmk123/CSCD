using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_text : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    PlayerScore playerScore;

    void Awake()
    {
        playerScore = FindObjectOfType<PlayerScore>();
    }

    void Update()
    {
        scoreText.text = playerScore.GetPlayerScore().ToString("000");
    }
}
