using Sirenix.OdinInspector;
using SMPScripts;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BikePathCreator : MonoBehaviour
{

    public List<Vector3> waypoints = new List<Vector3>();
    public Transform bikeFront;
    public Transform destination;
    public int NavMeshLayerBite;
    public float AIFOV = 60;
    public float distanceToNextWayPoint = 5f;
    [SerializeField] int currentWayPoint;


    [Header("Debug")]
    public bool ShowGizmos;
    public bool Debugger;

    public MotoController motoController;

    

    private void Update()
    {
        if (waypoints.Count == 0) return;

        motoController.SetAiInput(waypoints[currentWayPoint],bikeFront.forward);
        if (Vector3.Distance(bikeFront.position, waypoints[currentWayPoint]) < distanceToNextWayPoint && waypoints.Count > 1)
        {
            if (currentWayPoint < waypoints.Count - 1)
                currentWayPoint++;
            else
            {
                destination = GameManager.Instance.mapController.GetRandomDestiantion();
                currentWayPoint = 0;
                waypoints.Clear();
                CustomPath();
            }
        }
    }

    public void SetDestination(Transform target)
    {
        destination = target;
    }

    [Button]
    public void CustomPath() //Creates a path to the Custom destination
    {
        NavMeshPath path = new NavMeshPath();
        Vector3 sourcePostion;
        currentWayPoint = 0;

        if (waypoints.Count == 0)
        {
            sourcePostion = bikeFront.position;
            Calculate(destination.position, sourcePostion, bikeFront.forward, NavMeshLayerBite);
        }
        else
        {
            sourcePostion = waypoints[waypoints.Count - 1];
            Vector3 direction = (waypoints[waypoints.Count - 1] - waypoints[waypoints.Count - 2]).normalized;
            Calculate(destination.position, sourcePostion, direction, NavMeshLayerBite);
        }

        void Calculate(Vector3 destination, Vector3 sourcePostion, Vector3 direction, int NavMeshAreaBite)
        {
            if (NavMesh.SamplePosition(destination, out NavMeshHit hit, 150, NavMeshAreaBite) &&
                NavMesh.CalculatePath(sourcePostion, hit.position, NavMeshAreaBite, path))
            {
                if (path.corners.ToList().Count() > 1 && CheckForAngle(path.corners[1], sourcePostion, direction))
                {
                    waypoints.AddRange(path.corners.ToList());
                    debug("Custom Path generated successfully", false);
                }
                else
                {
                    if (path.corners.Length > 2 && CheckForAngle(path.corners[2], sourcePostion, direction))
                    {
                        waypoints.AddRange(path.corners.ToList());
                        debug("Custom Path generated successfully", false);
                    }
                    else
                    {
                        debug("Failed to generate a Custom path. Waypoints are outside the AIFOV. Generating a new one", false);
                        
                    }
                }
            }
            else
            {
                debug("Failed to generate a Custom path. Invalid Path. Generating a new one", false);
                
            }
        }
    }

    private bool CheckForAngle(Vector3 pos, Vector3 source, Vector3 direction) //calculates the angle between the car and the waypoint 
    {
        Vector3 distance = (pos - source).normalized;
        float CosAngle = Vector3.Dot(distance, direction);
        float Angle = Mathf.Acos(CosAngle) * Mathf.Rad2Deg;

        if (Angle < AIFOV)
            return true;
        else
            return false;
    }

    void debug(string text, bool IsCritical)
    {
        if (Debugger)
        {
            if (IsCritical)
                UnityEngine.Debug.LogError(text);
            else
                UnityEngine.Debug.Log(text);
        }
    }

    private void OnDrawGizmos() // shows a Gizmos representing the waypoints and AI FOV
    {
        if (ShowGizmos == true)
        {
            for (int i = 0; i < waypoints.Count; i++)
            {
                if (i == currentWayPoint)
                    Gizmos.color = Color.blue;
                else
                {
                    if (i > currentWayPoint)
                        Gizmos.color = Color.red;
                    else
                        Gizmos.color = Color.green;
                }
                Gizmos.DrawWireSphere(waypoints[i], 2f);
            }
            CalculateFOV();
        }

        void CalculateFOV()
        {
            Gizmos.color = Color.white;
            float totalFOV = AIFOV * 2;
            float rayRange = 10.0f;
            float halfFOV = totalFOV / 2.0f;
            Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
            Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
            Vector3 leftRayDirection = leftRayRotation * transform.forward;
            Vector3 rightRayDirection = rightRayRotation * transform.forward;
            Gizmos.DrawRay(bikeFront.position, leftRayDirection * rayRange);
            Gizmos.DrawRay(bikeFront.position, rightRayDirection * rayRange);
        }
    }
}
