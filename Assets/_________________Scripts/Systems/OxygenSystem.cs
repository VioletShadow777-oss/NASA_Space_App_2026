using TMPro;
using UnityEngine;

public class OxygenSystem : MonoBehaviour
{
    [Header("Oxygen")]
    [Tooltip("Current amount of oxygen available.")]
    [SerializeField] private float currentOxygen = 80f;

    [Tooltip("Maximum amount of oxygen that can be stored.")]
    [SerializeField] private float maxOxygen = 100f;
    [Tooltip("Text to show the Excact Meter")]
    [SerializeField] private TextMeshProUGUI oxygenMeterText;

    [Header("Oxygen Generation")]
    [Tooltip("Water required by the oxygen generator every 5 seconds.")]
    [SerializeField] private float waterCost = 1f;

    [Tooltip("Power required by the oxygen generator every 5 seconds.")]
    [SerializeField] private float powerCost = 2f;

    [Tooltip("Amount of oxygen produced before efficiency is applied.")]
    [SerializeField] private float oxygenProduction = 2f;

    [Tooltip("Efficiency of oxygen generation. 1 = 100%.")]
    [Range(0f, 1f)]
    [SerializeField] private float efficiency = 0.8f;

    [Header("Time")]
    [Tooltip("How often the oxygen system updates, in seconds.")]
    [SerializeField] private float updateInterval = 5f;

    private float timer;

    private PowerSystem powerSystem;
    private WaterSystem waterSystem;


    private void Awake()
    {
        powerSystem = GetComponent<PowerSystem>();
        waterSystem = GetComponent<WaterSystem>();
    }


    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            timer -= updateInterval;

            GenerateOxygen();
            UpdateText();
        }
    }


    private void GenerateOxygen()
    {
        // Oxygen requires BOTH water and power.
        //
        // If either resource is unavailable,
        // oxygen cannot be produced.

        bool hasWater = waterSystem.ConsumeWater(waterCost);

        if (!hasWater)
        {
            return;
        }

        bool hasPower = powerSystem.ConsumePower(powerCost);

        if (!hasPower)
        {
            // Return the water because the generator
            // could not actually operate.
            //
            // For this very simple prototype we will
            // keep this behavior simple and just stop.
            return;
        }

        float oxygenProduced = oxygenProduction * efficiency;

        currentOxygen += oxygenProduced;

        currentOxygen = Mathf.Clamp(currentOxygen, 0f, maxOxygen);
    }

    private void UpdateText()
    {
        oxygenMeterText.text = currentOxygen.ToString() + " %";
    }


    /// <summary>
    /// Attempts to consume oxygen.
    /// Returns true if enough oxygen was available.
    /// </summary>
    public bool ConsumeOxygen(float amount)
    {
        if (currentOxygen < amount)
        {
            return false;
        }

        currentOxygen -= amount;

        return true;
    }


    public float GetCurrentOxygen()
    {
        return currentOxygen;
    }
}