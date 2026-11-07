using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class SolarFlareSystem : MonoBehaviour
{
    // References to other systems
    private HealthSystem healthSystem;
    private FoodSystem foodSystem;
    private WaterSystem waterSystem;
    private OxygenSystem oxygenSystem;
    private PowerSystem powerSystem;
    private HeatingSystem heatingSystem;


    // Store initial efficiency values
    private float healthInitialdamage;
    private float foodInitialEfficiency;
    private float waterInitialEfficiency;
    private float oxygenInitialEfficiency;
    private float powerInitialEfficiency;
    private float heatingInitialEfficiency;

    [SerializeField] private float flareEffectDuration = 120f; // 120 second
    public bool isSolarFlareHappaning;
    private void Awake()
    {
        GetOtherSystemsReferences();
    }


    private void Start()
    {
        // Store the initial efficiency of each system and damage from the health system
        healthInitialdamage = healthSystem.healthDamage;
        foodInitialEfficiency = foodSystem.efficiency;
        waterInitialEfficiency = waterSystem.efficiency;
        oxygenInitialEfficiency = oxygenSystem.efficiency;
        powerInitialEfficiency = powerSystem.solarEfficiency;
        heatingInitialEfficiency = heatingSystem.efficiency;
    }


    private void GetOtherSystemsReferences()
    {
        healthSystem = GetComponent<HealthSystem>();
        foodSystem = GetComponent<FoodSystem>();
        waterSystem = GetComponent<WaterSystem>();
        oxygenSystem = GetComponent<OxygenSystem>();
        powerSystem = GetComponent<PowerSystem>();
        heatingSystem = GetComponent<HeatingSystem>();
    }

    private void Update()
    {
        if (!isSolarFlareHappaning)
        {
            return;
        }

        DoubleHealthDamage();
        flareEffectDuration -= Time.deltaTime;
        if(flareEffectDuration <= 0f)
        {
            isSolarFlareHappaning = false;
            flareEffectDuration = 120f;
            RestoreValues();
        }
    }
    public void StartSolarFlareEvent()
    {
        if (isSolarFlareHappaning)
        {
            return;
        }
        HalfEfficiency();
        
        isSolarFlareHappaning = true;

    }



    // Half the efficiancy
    private void HalfEfficiency()
    {
        foodSystem.efficiency /= 2;
        waterSystem.efficiency /= 2;
        oxygenSystem.efficiency /= 2;
        powerSystem.solarEfficiency /= 2;
        heatingSystem.efficiency /= 2;
        
    }

    // Restore to the previous values
    private void RestoreValues()
    {
        healthSystem.healthDamage = healthInitialdamage;
        foodSystem.efficiency = foodInitialEfficiency;
        waterSystem.efficiency = waterInitialEfficiency;
        oxygenSystem.efficiency = oxygenInitialEfficiency;
        powerSystem.solarEfficiency = powerInitialEfficiency;
        heatingSystem.efficiency = heatingInitialEfficiency;
    }

    private void DoubleHealthDamage()
    {
        if (healthSystem.isInside)
        {
            healthSystem.healthDamage = healthInitialdamage;
            return; // dont need to double the damage when player is inside the base
        }

        healthSystem.healthDamage = healthInitialdamage * 2;
    }
}