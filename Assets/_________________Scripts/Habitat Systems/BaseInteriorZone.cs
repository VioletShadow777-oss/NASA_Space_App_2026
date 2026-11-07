using UnityEngine;

public class BaseInteriorZone : MonoBehaviour
{

    public Transform player;

    private SuitOxygen suitOxygen;
    [SerializeField] private HealthSystem healthSystem;
    private void Start()
    {
        if(player == null)
        {
            return;
        }

        suitOxygen = player.GetComponent<SuitOxygen>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            healthSystem.isInside = true;
            Debug.Log("Player Entered the Base");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            healthSystem.isInside = false;
            Debug.Log("Player Left the Base");
        }
    }
}
