using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{   
    public int damage;
    public int health = 10;

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            TakeDamage(other.gameObject.GetComponent<Bullet>().bulletDamage);
        }
        if(other.gameObject.tag == "Building_1")
        {
            TakeDamage(other.gameObject.GetComponent<Building>().damage);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
