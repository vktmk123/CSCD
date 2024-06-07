using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeePlayer : MonoBehaviour
{
     Player player; 

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = FindObjectOfType<Player>();
        }
        if (player != null)
        {
            transform.position = player.transform.position;
        }  
        
    }
        public void LoadGameOver(){
        SceneManager.LoadScene("Menu_GameOver");
    }

}
