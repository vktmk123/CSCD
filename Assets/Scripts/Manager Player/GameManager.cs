using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Character[] characters;
    public Character currentCharacter;

    [Header("Mobile Game")]
    public bool mobileGame = true;
    public bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject joystick;
    public GameObject settingGame;


    private void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
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

    private void Start()
    {
        if (characters.Length > 0 )
        {
            currentCharacter = characters[0];
        }
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Menu_GameOver" 
        || SceneManager.GetActiveScene().name == "Menu_Main" 
        || SceneManager.GetActiveScene().name == "Menu_Start" )
        {
            settingGame.SetActive(false);
        }
        else{
            settingGame.SetActive(true);
            if(mobileGame)
            {
                joystick.SetActive(true);
            }
            else
            {
                joystick.SetActive(false);
            }
        }
    }

    public void SetCurrentCharacter(Character character)
    {
        currentCharacter = character;
    }

     public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(true);
            }
        }
        else
        {
            Time.timeScale = 1;
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(false);
            }
        }
    }
    
    public void LoadGameOver(){
        SceneManager.LoadScene("Menu_GameOver");
        TogglePause();
    }

    public void ToggleJoystick()
    {
        if (mobileGame)
        {
            joystick.SetActive(true);
        }
        else
        {
            joystick.SetActive(false);
        }
    }
     public void ToggleMobileGame()
    {
        mobileGame = !mobileGame;
        Debug.Log("Mobile Game: " + mobileGame);
    }





}
