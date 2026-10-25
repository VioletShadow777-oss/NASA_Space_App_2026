using UnityEngine;

public class BaseInteriorZone : MonoBehaviour
{

    public Transform player;

    private SuitOxygen suitOxygen;
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
            Debug.Log("Player Entered the Base");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Left the Base");
        }
    }
}
