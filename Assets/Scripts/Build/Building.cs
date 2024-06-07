using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int health = 10;
    int currentHealth;
    public int damage;
    public int delayTime;   
    

    void Start()
    {
        currentHealth = health;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(other.gameObject.GetComponent<Bullet>().bulletDamage);
        }
        if (other.gameObject.CompareTag("Bullet Enemy"))
        {
            TakeDamage(other.gameObject.GetComponent<Bullet_Enemy>().bulletDamage);
        }
        if (other.gameObject.CompareTag("Trap"))
        {
            TakeDamage(other.gameObject.GetComponent<Traps>().damage);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject, delayTime);
        }
    }
}
