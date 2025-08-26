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
        //motoPerfectMouseLook.enabled = true;


    }

}
