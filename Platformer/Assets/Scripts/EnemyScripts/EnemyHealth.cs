using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;  // Maximum health of the enemy
    private float currentHealth;

    public Slider healthBar;  // Optional health bar

    private void Awake()
    {
        currentHealth = maxHealth;

        // Initialize health bar if available
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Remaining health: {currentHealth}");

        // Update health bar
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        // Check if the enemy is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died!");
        BarEventManager.OnSliderReset();
        ScoreEventManager.OnScoreIncrement();
        Destroy(gameObject);
    }
}
