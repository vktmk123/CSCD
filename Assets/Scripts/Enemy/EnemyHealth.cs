using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    int currentHealth;
    public bool isDead = false;
    public UnityEvent OnDeath;
    public int score;

    CapsuleCollider2D bodyCollider;

    void OnEnable()
    {
        OnDeath.AddListener(Die);
    }

    void OnDisable()
    {
        OnDeath.RemoveListener(Die);
    }
    
    void Start()
    {
        currentHealth = maxHealth;
        bodyCollider = GetComponent<CapsuleCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(other.gameObject.GetComponent<Bullet>().bulletDamage);
        }
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            TakeDamage(100); 
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isDead) 
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Debug.Log("Enemy is dead");
                FindObjectOfType<Score>().AddScore(score);
                OnDeath.Invoke();
            }
        }
    }

    public void Die()
    {
        isDead = true;
        Destroy(gameObject, 2);
    }
}
