using TMPro;
using UnityEngine;

public class WaterSystem : MonoBehaviour
{
    [Header("Water")]
    [Tooltip("Current amount of available water.")]
    [SerializeField] private float currentWater = 50f;

    [Tooltip("Maximum amount of water that can be stored.")]
    [SerializeField] private float maxWater = 100f;
    [Tooltip("Text to show the Excact Meter")]
    [SerializeField] private TextMeshProUGUI waterMeterText;

    [Header("Water Recycling")]
    [Tooltip("Power consumed by the water recycling system every 5 seconds.")]
    [SerializeField] private float powerCost = 2f;

    [Tooltip("Amount of water recovered before efficiency is applied.")]
    [SerializeField] private float waterProduction = 2f;

    [Tooltip("Efficiency of the water recycling system. 1 = 100%.")]
    [Range(0f, 1f)]
    [SerializeField] private float efficiency = 0.75f;

    [Header("Time")]
    [Tooltip("How often the water system updates, in seconds.")]
    [SerializeField] private float updateInterval = 5f;

    private float timer;

    private PowerSystem powerSystem;


    private void Awake()
    {
        powerSystem = GetComponent<PowerSystem>();
    }


    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            timer -= updateInterval;

            RecycleWater();
            UpdateText();
        }
    }


    private void RecycleWater()
    {
        // First check whether the base has enough power.
        bool hasPower = powerSystem.ConsumePower(powerCost);

        if (!hasPower)
        {
            // The recycler cannot operate without power.
            return;
        }

        // Apply efficiency to the amount of recovered water.
        float waterProduced = waterProduction * efficiency;

        currentWater += waterProduced;

        currentWater = Mathf.Clamp(currentWater, 0f, maxWater);
    }

    private void UpdateText()
    {
        waterMeterText.text = currentWater.ToString() + " %";
    }
    /// <summary>
    /// Attempts to consume water.
    /// Returns true if enough water was available.
    /// </summary>
    public bool ConsumeWater(float amount)
    {
        if (currentWater < amount)
        {
            return false;
        }

        currentWater -= amount;

        return true;
    }


    public float GetCurrentWater()
    {
        return currentWater;
    }
}