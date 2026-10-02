using TMPro;
using UnityEngine;

public class FoodSystem : MonoBehaviour
{
    [Header("Food")]
    [Tooltip("Current amount of available food.")]
    [SerializeField] private float currentFood = 50f;

    [Tooltip("Maximum amount of food that can be stored.")]
    [SerializeField] private float maxFood = 100f;

    [Tooltip("Text to show the Excact Meter")]
    [SerializeField] private TextMeshProUGUI foodMeterText;

    [Header("Greenhouse")]
    [Tooltip("Power consumed by the greenhouse every 5 seconds.")]
    [SerializeField] private float powerCost = 2f;

    [Tooltip("Amount of food produced before efficiency is applied.")]
    [SerializeField] private float foodProduction = 2f;

    [Tooltip("Efficiency of the greenhouse. 1 = 100%.")]
    [Range(0f, 1f)]
    [SerializeField] private float efficiency = 0.75f;

    [Header("Time")]
    [Tooltip("How often the food system updates, in seconds.")]
    [SerializeField] private float updateInterval = 5f;

    private float timer;

    [SerializeField] private bool isSystemOn;

    private PowerSystem powerSystem;


    private void Awake()
    {
        powerSystem = GetComponent<PowerSystem>();

    }
    private void Start()
    {
        UpdateText();
        isSystemOn = true;
        
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            timer -= updateInterval;

            ProduceFood();
        }
    }


    private void ProduceFood()
    {
        if (!isSystemOn)
        {
            return;
        }
        bool hasPower = powerSystem.ConsumePower(powerCost);

        if (!hasPower)
        {
            // Greenhouse cannot operate without power.
            return;
        }

        float foodProduced = foodProduction * efficiency;

        currentFood += foodProduced;

        currentFood = Mathf.Clamp(currentFood, 0f, maxFood);

        UpdateText();

        Debug.Log("Food");

    }

    private void UpdateText()
    {
        foodMeterText.text = currentFood.ToString("F1") + " %";
    }

    /// <summary>
    /// Attempts to consume food.
    /// Returns true if enough food was available.
    /// </summary>
    public bool ConsumeFood(float amount)
    {
        if (currentFood < amount)
        {
            return false;
        }

        currentFood -= amount;

        UpdateText();

        return true;
    }


    public float GetCurrentFood()
    {
        return currentFood;
    }

    public void FoodSystemToggle()
    {
        if (isSystemOn == true)
        {
            isSystemOn = false;
        }
        else
        {
            isSystemOn = true;
        }
    }
}