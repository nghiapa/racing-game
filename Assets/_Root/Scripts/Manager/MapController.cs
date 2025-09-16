using SMPScripts;
using UnityEngine;

public class MapController : MonoBehaviour
{

    public MotoCamera motoCamera;
    public MotoPerfectMouseLook motoPerfectMouseLook;

    public PlayerProfile playerProfile;


    public AssetCollection assetCollection;

    public Transform spawnPos;

    public IkContaniner rider;
    public AutoGetIk bike;
    int moneyEarnedThisRun = 0;
    float timeThisRun = 0;


    private void Start()
    {
        eRider eRider = playerProfile.currentRider;
        ebike eBike = playerProfile.currentBike;

        rider = Instantiate(assetCollection.GetRiderPrefab(eRider), spawnPos.position, spawnPos.rotation);
        bike = Instantiate(assetCollection.GetBikePrefab(eBike), spawnPos.position, spawnPos.rotation);

        rider.transform.position = bike.bikerPos.position;
        rider.transform.SetParent(bike.transform);
        bike.IkContainer = rider;

        MotoProceduralIKHandler motoProceduralIKHandler = rider.GetComponent<MotoProceduralIKHandler>();
        motoProceduralIKHandler.autoGetIk = bike;

        rider.GetComponent<MotoProceduralIKHandler>().InitIK();


        motoCamera.enabled = true;
        EventManager.Event_OnPlayerDie += () =>
        {
            StopRun();
            bike.OnPlayerDead();
        };


        EventManager.Event_OnPlayerEarnRunCoin += () =>
        {
            moneyEarnedThisRun += 1;
        };
    }

    private void Update()
    {
        if(GameManager.Instance.currentGameState != EGameState.playing) return;
        timeThisRun += Time.deltaTime;
    }

    public void StopRun()
    {
        PlayerProfile playerProfile = GameManager.Instance.playerProfile;

        playerProfile.time = (int)timeThisRun;
        if (playerProfile.bestTime == 0 || playerProfile.time < playerProfile.bestTime)
        {
            playerProfile.bestTime = playerProfile.time;
        }

        playerProfile.moneyEarned += moneyEarnedThisRun;  
    }

}
