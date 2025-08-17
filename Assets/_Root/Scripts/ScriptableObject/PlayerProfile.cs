using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Sirenix.Serialization;

[CreateAssetMenu(fileName = "PlayerProfile", menuName = "Game/Player Profile")]
public class PlayerProfile : SerializedScriptableObject
{
    [Title("Player Info")]
    public string playerName;

    public Dictionary<EgameResource, int> bag = new Dictionary<EgameResource, int>();

    public eRider currentRider;
    public ebike currentBike;

    public void AddResource(EgameResource resource, int amount)
    {
        if (bag == null)
        {
            bag = new Dictionary<EgameResource, int>();
        }
        if (bag.ContainsKey(resource))
        {
            bag[resource] += amount;
        }
        else
        {
            bag[resource] = amount;
        }
    }
}

