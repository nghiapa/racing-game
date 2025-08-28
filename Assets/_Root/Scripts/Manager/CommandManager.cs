using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public PlayerProfile playerProfile;
    public AssetCollection assetCollection;
    public GameConfig gameConfig;

    public EventManager eventManager;


    private void Start()
    {
        playerProfile = GameManager.Instance.playerProfile;
        assetCollection = GameManager.Instance.assetCollection;
        gameConfig = GameManager.Instance.gameConfig;
    }

    public void AddMoney(int amt)
    {
        playerProfile.AddResource(EgameResource.money, amt);

        EventManager.Event_OnPlayerCointChange?.Invoke(playerProfile.bag[EgameResource.money]);
    }

    public void AddResorce(EgameResource egameResource,int amt)
    {

    }

}
