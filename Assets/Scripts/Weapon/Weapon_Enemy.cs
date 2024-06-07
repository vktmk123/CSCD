using UnityEngine;

public class Weapon_Enemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject fireEffect;
    public GameObject muzzle;

    public float TimeBtwFire = 20f; // Tốc độ bắn
    public float bulletForce = 20f; // Lực bắn

    private float timeBtwFire = 0f; // Thời gian bắn tiếp theo
    private Vector3 playerPosition; // Vị trí người chơi

    [SerializeField] private RangedEnemy rangedEnemy;
    Player player;

    private void Start()
    {
        // GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        // if (playerObject != null)
        // {
        //     player = playerObject.GetComponent<Player>();
        // }
    }

    void Update()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.GetComponent<Player>();
            }
        }

        if (rangedEnemy.PlayerInSight() && !player.isDead)
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            if (timeBtwFire <= 0)
            {
                RotateGun();
                FireBullet();
            }
            else
            {
                timeBtwFire -= Time.deltaTime;
            }
        }
    }

    void RotateGun()
    {
        Vector2 lookDir = playerPosition - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;

        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            transform.localScale = new Vector3(-1, -1, 0);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 0);
        }
    }


    void FireBullet()
    {
        timeBtwFire = TimeBtwFire;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);// Tạo ra 1 viên đạn

        Instantiate(fireEffect, firePoint.position, firePoint.rotation);// Tạo hiệu ứng bắn
        Instantiate(muzzle, firePoint.position, firePoint.rotation);// Tạo hiệu ứng súng

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
}
