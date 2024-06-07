using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_Game : MonoBehaviour
{
    public float loadDelay;
    private bool isGameOver = false;

    private void Start()
    {
        Debug.Log("Level: " + SceneManager.GetActiveScene().buildIndex);
        Star.ResetStars();
    }

    void Update()
    {
        if (!isGameOver)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0)
            {
                isGameOver = true;
                UpdateStarCount();
                UnlockNewLevel();
                StartCoroutine(WaitAndLoad(loadDelay));

            }
        }
    }

    IEnumerator WaitAndLoad(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Menu_GameOver");
    }

    void UnlockNewLevel()
    {
        if ((SceneManager.GetActiveScene().buildIndex - 1) >= PlayerPrefs.GetInt("ReachedIndex")) // nếu scene hiện tại lớn hơn scene đã đạt được thì lưu lại scene hiện tại
        {
            PlayerPrefs.SetInt("ReachedIndex", (SceneManager.GetActiveScene().buildIndex - 1) + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel") + 1);
            PlayerPrefs.Save();
        }
    }

    void UpdateStarCount()
    {
        int currentStarCount = PlayerPrefs.GetInt("starCount" + (SceneManager.GetActiveScene().buildIndex - 1).ToString());
        if (currentStarCount < Star.TotalStars)
        {
            PlayerPrefs.SetInt("starCount" + (SceneManager.GetActiveScene().buildIndex - 1).ToString(), Star.TotalStars);
            PlayerPrefs.Save();
        }
    }

}
