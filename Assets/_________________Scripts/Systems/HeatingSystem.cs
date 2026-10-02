using TMPro;
using UnityEngine;

public class HeatingSystem : MonoBehaviour
{
    [Header("Temperature")]
    [Tooltip("Current temperature of the Mars base in Fahrenheit.")]
    [SerializeField] private float currentTemperature = -80;

    [Tooltip("Minimum possible temperature on Mars in Fahrenheit.")]
    [SerializeField] private float minTemperature = -225;

    [Tooltip("Maximum possible temperature on Mars in Fahrenheit.")]
    [SerializeField] private float maxTemperature = 80;

    [Tooltip("Text to show the exact temperature.")]
    [SerializeField] private TextMeshProUGUI temperatureMeterText;


    [Header("Heating System")]
    [Tooltip("Power consumed by the heating system every 5 seconds.")]
    [SerializeField] private int powerCost = 2;

    [Tooltip("Amount of temperature increased before efficiency is applied.")]
    [SerializeField] private int heatProduction = 5;

    [Tooltip("Efficiency of the heating system. 1 = 100%.")]
    [Range(0f, 1f)]
    [SerializeField] private float efficiency = 0.75f;


    [Header("Time")]
    [Tooltip("How often the heating system updates, in seconds.")]
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

            ProduceHeat();
        }
    }


    private void ProduceHeat()
    {
        if (!isSystemOn)
        {
            LoseHeat(); // Decrease heat and return if the system is off
            return;
        }
        // The heating system requires power to operate.
        bool hasPower = powerSystem.ConsumePower(powerCost);

        if (!hasPower)
        {
            LoseHeat(); // Decrease heat and return of the system has no power
            // Not enough power.
            // Heating system cannot operate.
            return;
        }

        // Apply heating efficiency.
        float heatProduced = heatProduction * efficiency;
        // Convert the produced heat into an integer
        // because this system intentionally uses whole
        // Fahrenheit values.
        currentTemperature += heatProduced;

        // Keep the temperature within the defined
        // scientifically grounded Mars temperature range.
        currentTemperature = Mathf.Clamp(
            currentTemperature,
            minTemperature,
            maxTemperature
        );

        UpdateText();

        Debug.Log("Heat");
    }

    private void LoseHeat()
    {
        int heatDeduced = 1; // 1 Degree farenhite will lost per countdown

        currentTemperature -= heatDeduced;

        currentTemperature = Mathf.Clamp(
            currentTemperature,
            minTemperature,
            maxTemperature
        );
        UpdateText();
        Debug.Log("Heat lost");
    }


    private void UpdateText()
    {
        temperatureMeterText.text = currentTemperature.ToString("F1") + " °F";
    }


    public float GetCurrentTemperature()
    {
        return currentTemperature;
    }

    public void HeatingSystemToggle()
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