using SMPScripts;
using System.Collections;
using System.Collections.Generic;
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

    public List<Transform> mapRandomPos = new List<Transform>();

    int moneyEarnedThisRun = 0;
    float timeThisRun = 0;
    Rigidbody rbBike;



    private void Awake()
    {
        GameManager.Instance.mapController = this;
    }
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
        rbBike = bike.GetComponent<Rigidbody>();

        motoCamera.enabled = true;

        bike.bikePathCreator.SetDestination(GetRandomDestiantion());
        StartCoroutine(IELoadAiPath());

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

    IEnumerator IELoadAiPath()
    {
        yield return new WaitForSeconds(2f);
        bike.bikePathCreator.CustomPath();
    }

    public Transform GetRandomDestiantion()
    {
        return mapRandomPos[Random.Range(0, mapRandomPos.Count)];
    }

    private void Update()
    {
        if(GameManager.Instance.currentGameState != EGameState.playing) return;
        timeThisRun += Time.deltaTime;
        //if (bike.motoController.engineSettings.currentGear == 1&& bike.motoController.engineSettings.gearRatio<.5f)
        //{
        //    bike.motoController.rb.constraints = RigidbodyConstraints.FreezeRotationY;
        //}
        //else
        //{
        //    bike.motoController.rb.constraints = RigidbodyConstraints.None;

        //}
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
