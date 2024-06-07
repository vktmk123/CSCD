using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_Menu : MonoBehaviour
{
    public GameObject levelButtons;
    public LevelObject[] levelObjects;
    public Sprite goldenStarSprite;
    public static int currLevel;
    public static int UnlockedLevel;

    Score score;
    PlayerScore playerScore;

    void Start()
    {
        score = FindObjectOfType<Score>();
        playerScore = FindObjectOfType<PlayerScore>();
        levelToArray();

        UnlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 0);
        currLevel = PlayerPrefs.GetInt("ReachedIndex", 0);

        for (int i = 0; i < levelObjects.Length; i++)
        {
            if (UnlockedLevel >= i)
            {
                levelObjects[i].levelButton.interactable = true;

                int stars = PlayerPrefs.GetInt("starCount" + i.ToString(), 0);
                Debug.Log("starCount " + i.ToString() + ": " + stars);

                for (int j = 0; j < stars && j < levelObjects[i].stars.Length; j++)
                {
                    levelObjects[i].stars[j].sprite = goldenStarSprite;
                }
            }
        }
    }

    void levelToArray()
    {
        int childCount = levelButtons.transform.childCount;
        levelObjects = new LevelObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            levelObjects[i] = levelButtons.transform.GetChild(i).gameObject.GetComponent<LevelObject>();
        }
    }

    public void openLevel(int levelId)
    {
        string levelName = "Scene_Map " + levelId;
        score.ResetScore();
        SceneManager.LoadScene(levelName);
    }

    public void resetMap()
    {
        PlayerPrefs.SetInt("ReachedIndex", 0);
        PlayerPrefs.SetInt("UnlockedLevel", 0);
        for (int i = 0; i < levelObjects.Length; i++)
        {
            PlayerPrefs.SetInt("starCount" + i.ToString(), 0);
        }
        playerScore.ResetPlayerScore();
        SceneManager.LoadScene("Menu_Main");
    }
}
