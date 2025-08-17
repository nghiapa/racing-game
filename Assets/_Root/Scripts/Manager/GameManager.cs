using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlayerProfile playerProfile;
    public GameConfig gameConfig;
    public AssetCollection assetCollection;
}
