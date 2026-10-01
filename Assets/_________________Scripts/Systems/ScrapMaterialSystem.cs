using TMPro;
using UnityEngine;

public class ScrapMaterialSystem : MonoBehaviour
{
    [SerializeField] private float currentMaterial = 0;
    [SerializeField] private TextMeshProUGUI materialMeterText;


    private void Start()
    {
        UpdateText();
    }


    

    // Will be called from a button now, later will be called from player action
    public void AddScrapMaterials()
    {
        currentMaterial += Random.Range(3, 10);

        // Update Text
        UpdateText();
    }

    public bool ConsumeMaterial(float amount)
    {
        if(currentMaterial < amount)
        {
            return false;
        }

        currentMaterial -= amount;

        // Update Text
        UpdateText();

        return true;
    }


    private void UpdateText()
    {
        materialMeterText.text = currentMaterial.ToString();
    }
}
