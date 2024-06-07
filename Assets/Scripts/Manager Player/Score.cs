using UnityEngine;

public class Score : MonoBehaviour
{
    int currentScore = 0;
    static Score instance;   
    PlayerScore playerScore; // Reference to PlayerScore class

    void Start()
    {
        ManageSingleton();
        playerScore = FindObjectOfType<PlayerScore>();
    }

    void ManageSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Method to add score
    public void AddScore(int scoreToAdd)
    {   
        currentScore += scoreToAdd;
        // Debug.Log("Score: " + currentScore);
        Mathf.Clamp(currentScore, 0, int.MaxValue);
        
        // Notify PlayerScore class when a new score is added
        playerScore.AddPlayerScore(scoreToAdd);
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }
}
