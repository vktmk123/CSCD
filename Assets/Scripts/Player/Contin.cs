using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Contin : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] AudioClip collectSFX;
    int currentHealth;
    public bool isDead = false;
    public UnityEvent OnDeath;
    public int score;
    void OnEnable()
    {
        OnDeath.AddListener(Die);
    }

    void OnDisable()
    {
        OnDeath.RemoveListener(Die);
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet Enemy"))
        {
            TakeDamage(other.gameObject.GetComponent<Bullet>().bulletDamage);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<Score>().AddScore(score);
            AudioSource.PlayClipAtPoint(collectSFX, Camera.main.transform.position);
            Destroy(gameObject, 0);
        }
    }
    public void TakeDamage(int damage)
    {
        if (!isDead) // Kiểm tra xem kẻ địch đã chết chưa
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Debug.Log("contin is dead");
                OnDeath.Invoke();
            }
        }
    }
    public void Die()
    {
        isDead = true;  
        Destroy(gameObject, 0);
    }


}
