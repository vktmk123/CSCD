// PlayerHealth.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] float loadDelay = 2f;
    float currentHealth;
    HealthBar healthBar;
    public UnityEvent OnDeath;
    public float safeTime = 1f;
    float _safeTimeCooldown;
    Animator myanimator;
    Player player;


    public ShopSaveScriptable shopData;    
    private int currentIndex = 0;
    private int selectedINdex = 0;

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
        selectedINdex = shopData.selectedIndex;
        currentIndex = selectedINdex;

        maxHealth = shopData.shopItems[currentIndex].
        playerLevel[shopData.shopItems[currentIndex].unlockedLevel].Health;
        
        healthBar = FindObjectOfType<HealthBar>();
        currentHealth = maxHealth;
        healthBar.UpdateBar(currentHealth, maxHealth);
        myanimator = GetComponent<Animator>();
    }

    void Update()
    {   
        _safeTimeCooldown -= Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet Enemy")
        {
            TakeDamage(other.gameObject.GetComponent<Bullet>().bulletDamage);
        }
        if (other.gameObject.tag == "Trap")
        {
            TakeDamage(other.gameObject.GetComponent<Traps>().damage);
        }
        
    }   

    void TakeDamage(int damage)
    {
        if (_safeTimeCooldown <= 0)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Debug.Log("Player is dead");
                OnDeath.Invoke();
            }
            myanimator.SetTrigger("IsHurt");
            _safeTimeCooldown = safeTime;
            healthBar.UpdateBar(currentHealth, maxHealth);
        }
    }

    public void Die()
    {
        // Destroy(gameObject);
        GetComponent<Player>().Die();
        StartCoroutine(WaitAndLoad("Menu_GameOver", loadDelay));
    }

    IEnumerator WaitAndLoad(string sceneName, float delay){
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
        
    }
}
