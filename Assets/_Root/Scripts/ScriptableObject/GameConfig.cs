using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Game Config", menuName = "Game/Game Config")]
public class GameConfig : SerializedScriptableObject
{
    public List<BikerInfo> bikers;
    public List<BikeInfo> bikes;

    public float DelayLoseTime = 5f;
}

public class BikerInfo
{
    public EgameResource rider;
    public string riderName;
    public string riderDescription;
    public int riderPrice;
}

public class BikeInfo
{
    public EgameResource bike;
    public string bikeName;
    public string bikeDescription;
    public int bikePrice;
}
