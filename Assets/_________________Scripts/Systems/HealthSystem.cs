using TMPro;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Health")]
    [Tooltip("Current health of the astronaut.")]
    [SerializeField] private float currentHealth = 100f;

    [Tooltip("Maximum health of the astronaut.")]
    [SerializeField] private float maxHealth = 100f;
    [Tooltip("Text to show the Excact Meter")]
    [SerializeField] private TextMeshProUGUI healthMeterText;

    [Header("Resource Consumption")]
    [Tooltip("Oxygen consumed by the astronaut every 5 seconds.")]
    [SerializeField] private float oxygenCost = 1f;

    [Tooltip("Water consumed by the astronaut every 5 seconds.")]
    [SerializeField] private float waterCost = 1f;

    [Tooltip("Food consumed by the astronaut every 5 seconds.")]
    [SerializeField] private float foodCost = 1f;

    [Header("Health Changes")]
    [Tooltip("Health recovered when oxygen, water and food are all available.")]
    [SerializeField] private float healthRecovery = 1f;

    [Tooltip("Health lost when one or more required resources are unavailable.")]
    [SerializeField] private float healthDamage = 2f;

    [Header("Time")]
    [Tooltip("How often the health system updates, in seconds.")]
    [SerializeField] private float updateInterval = 5f;

    private float timer;

    private OxygenSystem oxygenSystem;
    private WaterSystem waterSystem;
    private FoodSystem foodSystem;


    private void Awake()
    {
        oxygenSystem = GetComponent<OxygenSystem>();
        waterSystem = GetComponent<WaterSystem>();
        foodSystem = GetComponent<FoodSystem>();
    }


    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            timer -= updateInterval;

            UpdateHealth();
            UpdateText();
        }
    }


    private void UpdateHealth()
    {
        // Try to consume all three resources.

        bool hasOxygen = oxygenSystem.ConsumeOxygen(oxygenCost);
        bool hasWater = waterSystem.ConsumeWater(waterCost);
        bool hasFood = foodSystem.ConsumeFood(foodCost);


        // If ALL three resources were available,
        // the astronaut remains healthy.

        if (hasOxygen && hasWater && hasFood)
        {
            currentHealth += healthRecovery;
        }
        else
        {
            // One or more resources were unavailable.
            // The astronaut starts losing health.

            currentHealth -= healthDamage;
        }

        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

    private void UpdateText()
    {
        healthMeterText.text = currentHealth.ToString() + " %";
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}