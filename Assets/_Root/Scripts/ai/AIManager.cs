using SMPScripts;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    [SerializeField] int numberAI;
    [SerializeField] List<Transform> wayspoints;

    private void Start()
    {
        SpawnAI();
    }

    void SpawnAI()
    {
        for (int i = 0; i < numberAI; i++)
        {
            Transform posSpawn = GetWaypoint(true, i);
            IkContaniner rider = Instantiate(GameManager.Instance.mapController.assetCollection.GetRiderPrefab(GetRandomEnum<eRider>()), posSpawn.GetChild(0).position, posSpawn.GetChild(0).rotation);
            AutoGetIk bike = Instantiate(GameManager.Instance.mapController.assetCollection.GetBikePrefab(GetRandomEnum<ebike>()), posSpawn.GetChild(0).position, posSpawn.GetChild(0).rotation);
            AiControler aiControler = bike.transform.AddComponent<AiControler>();
            aiControler.SetupAI(bike.motoController, posSpawn);
            rider.transform.position = bike.bikerPos.position;
            rider.transform.SetParent(bike.transform);
            bike.IkContainer = rider;
            MotoProceduralIKHandler motoProceduralIKHandler = rider.GetComponent<MotoProceduralIKHandler>();
            motoProceduralIKHandler.autoGetIk = bike;
        }
    }

    public static T GetRandomEnum<T>()
    {
        System.Array values = System.Enum.GetValues(typeof(T));
        return (T)values.GetValue(Random.Range(0, values.Length - 2));
    }

    public Transform GetWaypoint(bool isFirst,int indexWaypoint=0)
    {
        if (isFirst)
        {
            return wayspoints[indexWaypoint];
        }
        else
        {
            return wayspoints[Random.Range(0, wayspoints.Count)];
        }
    } 
}
