using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlayerProfile playerProfile;
    public GameConfig gameConfig;
    public AssetCollection assetCollection;
    public CommandManager commandManager;
    public MapController mapController;
    public VehicleController vehicleController;

    public EGameState currentGameState = EGameState.start;
    public EGameMode currentGameMode = EGameMode.menu;



    private void Start()
    {
        EventManager.Event_OnPlayerDie += () =>
        {
            currentGameState = EGameState.GameOver;
            StartCoroutine(IeLoseGame());
        };
    }


    IEnumerator IeLoseGame()
    {
        yield return new WaitForSeconds(gameConfig.DelayLoseTime);
        UIManager.Instance.ShowLosePanel();
    }
}
