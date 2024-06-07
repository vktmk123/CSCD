using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.instance == null)
        {
            SceneManager.LoadScene(0);
            Debug.Log("GameManager instance is null!");
            return;
        }
        if (GameManager.instance.currentCharacter == null)
        {
            SceneManager.LoadScene(0);
        }

        Instantiate(GameManager.instance.currentCharacter.prefab, transform.position, Quaternion.identity);
    }

}
