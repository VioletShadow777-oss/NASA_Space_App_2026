using System;

[Serializable]
public class GameSaveData
{
    // Resources
    public float currentOxygen;
    public float currentWater;
    public float currentFood;
    public float currentPower;
    public float currentHealth;
    public float currentTemperature;

    // System States
    public bool oxygenSystemOn;
    public bool waterSystemOn;
    public bool foodSystemOn;
    public bool heatingSystemOn;
    public bool powerSystemOn;
}
