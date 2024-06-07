using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    int playScore = 0;
    static PlayerScore instance;

    public static int savePlayerScore;

    void Awake()
    {
        ManageSingleton();
        LoadPlayerScore();
    }

    void ManageSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddPlayerScore(int scoreToAdd)
    {
        playScore += scoreToAdd;
        playScore = Mathf.Clamp(playScore, 0, int.MaxValue);
        savePlayerScore = playScore;
        SavePlayerScore();
    }

    public int GetPlayerScore()
    {
        return savePlayerScore;
    }

    public void ResetPlayerScore()
    {
        playScore = 0;
        savePlayerScore = playScore;
        SavePlayerScore();
    }

    private void SavePlayerScore()
    {
        PlayerPrefs.SetInt("savePlayerScore", savePlayerScore);
        PlayerPrefs.Save();
    }

    private void LoadPlayerScore()
    {
        savePlayerScore = PlayerPrefs.GetInt("savePlayerScore", 0);
        playScore = savePlayerScore;
        Debug.Log("Loaded player score: " + savePlayerScore);
    }
}
