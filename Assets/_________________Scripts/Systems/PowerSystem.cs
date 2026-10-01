using TMPro;
using UnityEngine;

public class PowerSystem : MonoBehaviour
{
    [Header("Power")]
    [Tooltip("Current amount of stored power.")]
    [SerializeField] private float currentPower = 50f;

    [Tooltip("Maximum amount of power that can be stored.")]
    [SerializeField] private float maxPower = 100f;
    [Tooltip("Text to show the Excact Meter")]
    [SerializeField] private TextMeshProUGUI powerMeterText;

    [Header("Solar Panels")]
    [Tooltip("Number of solar panels currently built.")]
    [SerializeField] private int solarPanelCount = 2;

    [Tooltip("Power produced by ONE solar panel every 5 seconds.")]
    [SerializeField] private float powerPerSolarPanel = 5f;

    [Tooltip("Cost to build a single solar panel")]
    [SerializeField] private float solarPanelCost;

    [Tooltip("Efficiency of the solar panels. 1 = 100%, 0.8 = 80%.")]
    [Range(0f, 1f)]
    [SerializeField] private float solarEfficiency = 0.8f;

    [Header("Time")]
    [Tooltip("How often the power system updates, in seconds.")]
    [SerializeField] private float updateInterval = 5f;

    private float timer;
    private bool canBuild; // Bool to check if can build solar panel or not

    private ScrapMaterialSystem scrapMaterialSystem;

    private void Awake()
    {
        scrapMaterialSystem = GetComponent<ScrapMaterialSystem>();
    }

    private void Start()
    {
        UpdateText();
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            timer -= updateInterval;

            GeneratePower();
        }
    }


    private void GeneratePower()
    {
        // POWER SUPPLY:
        // One solar panel produces "powerPerSolarPanel"
        // power every 5 seconds.
        //
        // Example:
        // 2 panels × 5 power × 80% efficiency = 8 power
        //
        // You can change the values above to balance
        // the actual power production of your game.

        float powerProduced =
            solarPanelCount *
            powerPerSolarPanel *
            solarEfficiency;

        currentPower += (int)powerProduced;

        currentPower = Mathf.Clamp(currentPower, 0f, maxPower);

        UpdateText();

    }

    private void UpdateText()
    {
        powerMeterText.text = currentPower.ToString() + " %";
    }

    /// <summary>
    /// Builds one additional solar panel.
    /// </summary>
    public void BuildSolarPanel()
    {
        canBuild = scrapMaterialSystem.ConsumeMaterial(solarPanelCost);

        if (!canBuild)
        {
            // can not build solar panel if there is not enough scrap materials
            return;
        }

        solarPanelCount++;
    }


    /// <summary>
    /// Attempts to consume power.
    /// Returns true if enough power was available.
    /// </summary>
    public bool ConsumePower(float amount)
    {
        if (currentPower < amount)
        {
            return false;
        }

        currentPower -= amount;

        UpdateText();

        return true;
    }


    public float GetCurrentPower()
    {
        return currentPower;
    }
}