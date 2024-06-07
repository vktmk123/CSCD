using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_start : MonoBehaviour
{
    [SerializeField] float loadDelay = 2f;

    public void LoadGameMenuMain(){
        Debug.Log("Menu_choice_map");
        SceneManager.LoadScene(6);
    }

    public void LoadMenuStart(){
        SceneManager.LoadScene("Menu_Start");
    }

    public void LoadGameOver(){
        StartCoroutine(WaitAndLoad("Menu_GameOver", loadDelay));
    }
    public void QuitGame(){
        Debug.Log("Quit Game...");
        Application.Quit();
    }
    IEnumerator WaitAndLoad(string sceneName, float delay){
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
        
    }
    
}
