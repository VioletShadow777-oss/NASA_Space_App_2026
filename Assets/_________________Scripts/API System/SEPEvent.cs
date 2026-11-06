using UnityEngine;

[System.Serializable]
public class SEPEvent
{
    public string sepID;
    public string eventTime;
    public string link;
    public int versionId;
    public SEPInstrument[] instruments;
    public SEPLinkedEvent[] linkedEvents;
}
