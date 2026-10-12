
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Health")]
    [SerializeField, Min(0f)] private float maxHealth = 100f;
    [SerializeField, Min(0f)] private float currentHealth = 100f;

    [Header("Stamina")]
    [SerializeField, Min(0f)] private float maxStamina = 100f;
    [SerializeField, Min(0f)] private float currentStamina = 100f;

    [SerializeField, Min(0f)] private float sprintStaminaCost = 20f;
    [SerializeField, Min(0f)] private float staminaRecoveryRate = 15f;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    public float CurrentStamina => currentStamina;
    public float MaxStamina => maxStamina;

    private void Awake()
    {
        maxHealth = Mathf.Max(0f, maxHealth);
        maxStamina = Mathf.Max(0f, maxStamina);

        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
    }

    // Updates stamina and returns whether sprinting is allowed.
    public bool UpdateStamina(bool sprintRequested, float deltaTime)
    {
        bool canSprint =
            sprintRequested && currentStamina > 0f;

        if (canSprint)
        {
            currentStamina -= sprintStaminaCost * deltaTime;
        }
        else
        {
            currentStamina += staminaRecoveryRate * deltaTime;
        }

        currentStamina = Mathf.Clamp(
            currentStamina, 0f, maxStamina);

        return canSprint;
    }

    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Clamp(
            currentHealth - Mathf.Max(0f, amount),
            0f,
            maxHealth);
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(
            currentHealth + Mathf.Max(0f, amount),
            0f,
            maxHealth);
    }

    public void RestoreStamina(float amount)
    {
        currentStamina = Mathf.Clamp(
            currentStamina + Mathf.Max(0f, amount),
            0f,
            maxStamina);
    }
}