using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score_text : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    Score scoreKeeper;

    // Start is called before the first frame update
    void Awake()
    {
        scoreKeeper = FindObjectOfType<Score>();
    }

    void Update()
    {
        scoreText.text = scoreKeeper.GetScore().ToString("000");
    }
}
