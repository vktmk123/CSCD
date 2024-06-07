using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Star : MonoBehaviour
{
    public int score = 1;
    public static int TotalStars { get; private set; } 

     [SerializeField] AudioClip coinPickupSFX;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<Score>().AddScore(score);
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            AddStar();
            Destroy(gameObject);
        }
    }
    public void AddStar()
    {
        TotalStars += 1;
        Debug.Log("Total number of stars: " + TotalStars);
    }

    public static void ResetStars()
    {
        TotalStars = 0;
    }
}
