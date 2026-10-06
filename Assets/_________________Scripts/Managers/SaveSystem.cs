using UnityEngine;
using System.IO;
public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }

    private string saveFilePath;

    private GameSaveData saveData;

    public bool HasSave { get; private set; }

    private void Awake()
    {
        // Prevent Duplicate SaveSystems
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Keep the save system alive between scenes.
        DontDestroyOnLoad(gameObject);

        saveFilePath = Path.Combine(
            Application.persistentDataPath,
            "red_frontier_save.json"
        );

        LoadGame();
    }

    // Loads the previous Save
    private void LoadGame()
    {
        if (!File.Exists(saveFilePath))
        {
            // means no previous save
            saveData = new GameSaveData();

            HasSave = false;

            Debug.Log("No Previous File Found");

            return;
        }

        try
        {
            string json = File.ReadAllText(saveFilePath);

            saveData = JsonUtility.FromJson<GameSaveData>(json);

            if(saveData == null)
            {
                saveData = new GameSaveData();

                HasSave = false;

                Debug.LogWarning("Save data was invalid. Starting with defaults");

                return;
            }
            HasSave = true;

            Debug.Log("Red Frontier save loaded successfully.");

        }

        catch(System.Exception exception)
        {
            saveData = new GameSaveData();

            HasSave = false;

            Debug.LogError("Failed to load save files" + exception.Message);

        }
    }

    // Writes the current save data
    public void SaveGame()
    {
        if(saveData == null)
        {
            saveData = new GameSaveData();
        }

        try
        {
            string json = JsonUtility.ToJson(saveData, true);

            File.WriteAllText(saveFilePath, json);

            HasSave = true;
            Debug.Log("Game saved successfully.");

        }
        catch(System.Exception exception)
        {
            Debug.LogError("Failed to save the game" + exception.Message);
        }
    }

    // Oxygen
    public void SetOxygen(float value)
    {
        saveData.currentOxygen = value;
    }
    public float GetOxygen(float defaultValue)
    {
        return saveData.currentOxygen;
    }

    // ==================================================
    // WATER
    // ==================================================

    public void SetWater(float value)
    {
        saveData.currentWater = value;
    }

    public float GetWater(float defaultValue)
    {
        return saveData.currentWater;
    }

    // ==================================================
    // FOOD
    // ==================================================

    public void SetFood(float value)
    {
        saveData.currentFood = value;
    }

    public float GetFood(float defaultValue)
    {
        return saveData.currentFood;
    }


    // ==================================================
    // POWER
    // ==================================================

    public void SetPower(float value)
    {
        saveData.currentPower = value;
    }

    public float GetPower(float defaultValue)
    {
        return saveData.currentPower;
    }

    // ==================================================
    // HEALTH
    // ==================================================

    public void SetHealth(float value)
    {
        saveData.currentHealth = value;
    }

    public float GetHealth(float defaultValue)
    {
        return saveData.currentHealth;
    }

    // ==================================================
    // TEMPERATURE
    // ==================================================

    public void SetTemperature(float value)
    {
        saveData.currentTemperature = value;
    }

    public float GetTemperature(float defaultValue)
    {
        return saveData.currentTemperature;
    }

    // ==================================================
    // SYSTEM STATES
    // ==================================================

    public void SetOxygenSystemState(bool value)
    {
        saveData.oxygenSystemOn = value;
    }

    public bool GetOxygenSystemState(bool defaultValue)
    {
        return saveData.oxygenSystemOn;
    }


    public void SetWaterSystemState(bool value)
    {
        saveData.waterSystemOn = value;
    }

    public bool GetWaterSystemState(bool defaultValue)
    {
        return saveData.waterSystemOn;
    }


    public void SetFoodSystemState(bool value)
    {
        saveData.foodSystemOn = value;
    }

    public bool GetFoodSystemState(bool defaultValue)
    {
        return saveData.foodSystemOn;
    }


    public void SetHeatingSystemState(bool value)
    {
        saveData.heatingSystemOn = value;
    }

    public bool GetHeatingSystemState(bool defaultValue)
    {
        return saveData.heatingSystemOn;
    }


    public void SetPowerSystemState(bool value)
    {
        saveData.powerSystemOn = value;
    }

    public bool GetPowerSystemState(bool defaultValue)
    {
        return saveData.powerSystemOn;
    }

    // ==================================================
    // APPLICATION EVENTS
    // ==================================================

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveGame();
        }
    }


    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
