using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerProfile", menuName = "Game/Player Profile")]
public class PlayerProfile : SerializedScriptableObject
{
    [Title("Player Info")]
    public string playerName;

    [Range(1, 100)]
    public int level = 1;

    [ProgressBar(0, 10000)]
    public int experience;

    [ReadOnly]
    public int highScore;


    public Dictionary<EgameResource, int> bag;

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

