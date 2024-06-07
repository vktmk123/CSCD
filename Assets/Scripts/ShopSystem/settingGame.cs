// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class settingGame : MonoBehaviour
// {
//     public static settingGame instance;

//     public bool isPaused = false;
//     public bool mobileGame = true;
//     public GameObject pauseMenu;
//     public GameObject joystick;

//     // Start is called before the first frame update
//      private void Awake()
//     {
//         ManageSingleton();
//     }

//     void ManageSingleton()
//     {
//         if (instance != null)
//         {
//             gameObject.SetActive(false);
//             Destroy(gameObject);
//         }
//         else
//         {
//             instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//     }

//      public void TogglePause()
//     {
//         isPaused = !isPaused;
//         if (isPaused)
//         {
//             Time.timeScale = 0;
//             if (pauseMenu != null)
//             {
//                 pauseMenu.SetActive(true);
//             }
//         }
//         else
//         {
//             Time.timeScale = 1;
//             if (pauseMenu != null)
//             {
//                 pauseMenu.SetActive(false);
//             }
//         }
//     }

//     public void ToggleJoystick()
//     {
//         if (mobileGame)
//         {
//             joystick.SetActive(true);
//         }
//         else
//         {
//             joystick.SetActive(false);
//         }
//     }
//      public void ToggleMobileGame()
//     {
//         mobileGame = !mobileGame;
//         Debug.Log("Mobile Game: " + mobileGame);
//     }

// }
