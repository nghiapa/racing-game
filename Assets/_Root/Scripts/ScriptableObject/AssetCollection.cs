using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Asset Collection", menuName = "Game/Asset Collection")]


public class AssetCollection : SerializedScriptableObject
{
    public Dictionary<eRider, IkContaniner> riders = new Dictionary<eRider, IkContaniner>();


    public Dictionary<ebike, AutoGetIk> bikes = new Dictionary<ebike, AutoGetIk>();

    public IkContaniner GetRiderPrefab(eRider rider)
    {
        return riders.TryGetValue(rider, out IkContaniner ikContainer) ? ikContainer : null;
    }

    public AutoGetIk GetBikePrefab(ebike bike)
    {
        return bikes.TryGetValue(bike, out AutoGetIk autoGetIk) ? autoGetIk : null;
    }
}
