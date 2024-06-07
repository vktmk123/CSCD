using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage = 10;
    public Object vunoEffect;

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag != "Player" && other.gameObject.tag != "phat_hien"){
            Instantiate(vunoEffect, transform.position, transform.rotation); 
            Destroy(gameObject);
        }  
        else if (other.gameObject.tag == "Enemy")
        { 
            other.gameObject.GetComponent<Bullet>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }

    void TakeDamage(int damage)
    {
        bulletDamage -= damage;
        if (bulletDamage <= 0)
        {
            Destroy(gameObject);
        }
        
    }   
}
