using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DONKIConnection : MonoBehaviour
{
    public string startDate = "2026-01-01";
    public string endDate = "2026-01-10";

    private SolarFlareSystem solarFlareSystem;
    private string url
    {
        get
        {
            return $"https://ccmc.gsfc.nasa.gov/DONKI-API/get/SEP?startDate={startDate}&endDate={endDate}";
        }
    }
   

    // remembers flares we already used, so one flare doesn't start the event many times
    HashSet<string> usedFlares = new HashSet<string>();

    private void Awake()
    {
        solarFlareSystem = GetComponent<SolarFlareSystem>();
    }
    private void Start()
    {
        //StartCoroutine(CheckNasa());
        
    }
    // Call this from your own timer
    public void CheckNasaNow()
    {
        StartCoroutine(CheckNasa());
    }

    // Runs once, then stops
    IEnumerator CheckNasa()
    {

        //while (true)
        //{
        //    yield return new WaitForSeconds(1800);
            usedFlares.Clear();
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    string json = request.downloadHandler.text;

                    SEPEventList list = JsonUtility.FromJson<SEPEventList>("{\"events\":" + json + "}");

                    if (list != null && list.events != null)
                    {
                        foreach (SEPEvent sep in list.events)
                        {
                            if (sep.linkedEvents == null) continue;

                            foreach (SEPLinkedEvent linked in sep.linkedEvents)
                            {
                                string id = linked.activityID;

                                if (id.Contains("FLR"))
                                {
                                    if (usedFlares.Add(id))
                                    {
                                        StartFlareEvent(id);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("NASA request failed: " + request.error);
                }
            }

        //}
        
    }

    void StartFlareEvent(string id)
    {
        Debug.LogWarning("SOLAR FLARE EVENT STARTED: " + id);
        solarFlareSystem.StartSolarFlareEvent();
    }
}