using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Object = UnityEngine.Object;

public class Bomb : MonoBehaviour
{
    public float delayTime;
    public float fieldoImpact;
    public float force;
    public int damage = 10;
    public GameObject BombEffect; // Đổi từ Object sang GameObject
    public LayerMask[] layerMask;

    public event Action<Vector2, float> OnExplode; // Định nghĩa sự kiện khi Bomb nổ

    void Start()
    {
        Invoke("Explode", delayTime);
        Destroy(gameObject, delayTime + 0.1f); // Thêm thời gian để hiệu ứng hoàn tất
    }

    void Explode()
    {
        // Tạo hiệu ứng bom nổ
        Instantiate(BombEffect, transform.position, transform.rotation);

        // Gửi tín hiệu sự kiện Bomb nổ với các thông số cần thiết
        if (OnExplode != null)
        {
            OnExplode(transform.position, fieldoImpact);
        }

        // Tìm các đối tượng trong vùng ảnh hưởng của Bomb và xử lý sát thương
        foreach (LayerMask layerMask in layerMask)
        {
            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldoImpact, layerMask);
            foreach (Collider2D obj in objects)
            {
                EnemyHealth enemy = obj.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }

                Building building = obj.GetComponent<Building>();
                if (building != null)
                {
                    building.TakeDamage(damage);
                }
                Traps traps = obj.GetComponent<Traps>();
                if (traps != null)
                {
                    traps.TakeDamage(damage);
                }

                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 direction = obj.transform.position - transform.position;
                    rb.AddForce(direction * force);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldoImpact);
    }
}
