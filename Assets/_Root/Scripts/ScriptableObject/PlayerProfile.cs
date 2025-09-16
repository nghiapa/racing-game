using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Sirenix.Serialization;

[CreateAssetMenu(fileName = "PlayerProfile", menuName = "Game/Player Profile")]
public class PlayerProfile : SerializedScriptableObject
{
    [Title("Player Info")]
    public string playerName;

    public int bestTime;
    public int moneyEarned;
    public int time;

    public Dictionary<EgameResource, int> bag = new Dictionary<EgameResource, int>();

    public eRider currentRider;
    public ebike currentBike;


    public void SaveData()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(EgameResource)).Length; i++)
        {
            EgameResource resource = (EgameResource)i;
            int amount = bag.ContainsKey(resource) ? bag[resource] : 0;
            PlayerPrefs.SetInt(resource.ToString(), amount);
        }
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetInt("CurrentRider", (int)currentRider);
        PlayerPrefs.SetInt("CurrentBike", (int)currentBike);
    }

    public void LoadData()
    {
        playerName = PlayerPrefs.GetString("PlayerName", "Player");
        currentRider = (eRider)PlayerPrefs.GetInt("CurrentRider", 0);
        currentBike = (ebike)PlayerPrefs.GetInt("CurrentBike", 0);
        bag = new Dictionary<EgameResource, int>();
        for (int i = 0; i < System.Enum.GetValues(typeof(EgameResource)).Length; i++)
        {
            EgameResource resource = (EgameResource)i;
            int amount = PlayerPrefs.GetInt(resource.ToString(), 0);
            bag[resource] = amount;
        }
    }

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

    public int GetResourceAmount(EgameResource resource)
    {
        if (bag != null && bag.ContainsKey(resource))
        {
            return bag[resource];
        }
        return 0;
    }

}

