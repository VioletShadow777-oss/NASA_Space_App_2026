using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class DONKIConnection : MonoBehaviour
{
    
    private string startDate = "2024-01-01";
    private string endDate = "2024-01-10";

    private int maxRetries = 5;
    private float retryDelay = 3f; // seconds

    private SEPEvent[] eventData;

    private void Start()
    {
        StartCoroutine(GetSepData());
    }

    IEnumerator GetSepData()
    {
        string url = $"https://ccmc.gsfc.nasa.gov/DONKI-API/get/SEP?startDate={startDate}&endDate={endDate}";

        for (int attempt = 1; attempt <= maxRetries; attempt++)
        {
            Debug.Log($"Sending Request (attempt {attempt}/{maxRetries})...");

            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                request.timeout = 20; // give up after 20 seconds
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    string json = request.downloadHandler.text;

                    // Wrap the JSON array
                    string wrappedJson = "{\"events\":" + json + "}";

                    // Convert JSON into C# objects
                    SEPEventList eventList = JsonUtility.FromJson<SEPEventList>(wrappedJson);

                    // Assign the array
                    eventData = eventList.events;

                    // Check whether NASA returned any events
                    if (eventData == null || eventData.Length == 0)
                    {
                        Debug.Log("No SEP events found for this date range.");
                        yield break;
                    }

                    // Loop through every SEP event
                    for (int i = 0; i < eventData.Length; i++)
                    {

                        // Seperate the event info
                        for (int j = 0; j < eventData[i].linkedEvents.Length; j++)
                        {
                            string activityID = eventData[i].linkedEvents[j].activityID;

                            // Find the last two hyphens
                            int lastDash = activityID.LastIndexOf('-');
                            int secondLastDash = activityID.LastIndexOf('-', lastDash - 1);

                            // Extract the information
                            string timestamp = activityID.Substring(0, secondLastDash);
                            string eventType = activityID.Substring(secondLastDash + 1, lastDash - secondLastDash - 1);
                            string eventNumber = activityID.Substring(lastDash + 1);

                            Debug.Log("Timestamp: " + timestamp);
                            Debug.Log("Event Type: " + eventType);
                            Debug.Log("Event Number: " + eventNumber);

                            if (eventType == "FLR")
                            {
                                Debug.LogWarning("FLARE EVENT DETECTED!"); // Logwarning used to notice the massage highllightly in the console
                            }
                        }

                        // Loop through instruments inside this SEP event
                        for (int j = 0; j < eventData[i].instruments.Length; j++)
                        {
                            Debug.Log("Instrument: " + eventData[i].instruments[j].displayName);
                        }

                        // Loop through linked events
                        if (eventData[i].linkedEvents != null)
                        {
                            for (int j = 0; j < eventData[i].linkedEvents.Length; j++)
                            {
                                Debug.Log("Linked Event: " +
                                    eventData[i].linkedEvents[j].activityID);
                            }
                        }
                    }

                    yield break;
                }

                Debug.LogWarning($"Failed: {request.responseCode} {request.error}");

                // Retry only for server-side problems (5xx) or no response (0)
                bool retryable = request.responseCode >= 500 || request.responseCode == 0;
                if (!retryable) yield break;
            }

            yield return new WaitForSeconds(retryDelay);
            retryDelay *= 2; // wait longer each time (3s, 6s, 12s...)
        }

        Debug.LogError("NASA API still unavailable. Try again later.");
    }
}