using SMPScripts;
using System.Collections.Generic;
using UnityEngine;

public class AiControler : MonoBehaviour
{
    Transform target;
    MotoController motoController;
    Transform wayspoint;
    int indexTarget = 0;

    public void SetupAI(MotoController _motoController, Transform _wayspoint)
    {
        motoController = _motoController;
        wayspoint = _wayspoint;
        motoController.engineSettings.numOfGears = 3;
        motoController.aiActive = true;
        NextTarget();


    }


    public float steerSmooth = 5f; // tốc độ mượt
    private float previousSteer = 0f;
    float customSteerAxis;
    float rateGas;

    private void Update()
    {
        UpdateAiSteering();
    }
    void UpdateAiSteering()
    {
        if (target == null) return;

        Vector3 toTarget = target.position - motoController.transform.position;
        toTarget.y = 0;

        //if (toTarget.sqrMagnitude < 0.01f) return;

        float angle = Vector3.SignedAngle(motoController.transform.forward, toTarget, Vector3.up);
        float maxSteer = 45f;
        float clamped = Mathf.Clamp(angle, -maxSteer, maxSteer);
        float normalized = clamped / maxSteer;

        float distance = toTarget.magnitude;
        //float speedFactor = Mathf.Clamp01(distance / 10f); // giảm lái khi gần
        //normalized *= speedFactor;

        customSteerAxis = Mathf.Lerp(customSteerAxis, normalized, Time.deltaTime * 5f);
        Debug.Log("Axis"+customSteerAxis);
        Debug.Log("Distance"+distance);
        Debug.Log("velocity"+motoController.rb.linearVelocity.magnitude);
        if (customSteerAxis>1 || customSteerAxis < -1)
        {
            rateGas = -1;
        }else if (distance < 40 )
        {
            if(motoController.rb.linearVelocity.magnitude > 10)
            {
                rateGas = -1;
            }
            else
            {
                rateGas = 1f;
            }
        }
        else
        {
            rateGas = 1f;
        }
        Debug.Log("rateGas" + rateGas);

            motoController.SetAiInput(customSteerAxis, rateGas, motoController.rb.linearVelocity.magnitude > 30); // throttle nhẹ
        if (distance <= 10)
        {
            NextTarget();
        }
    }

    void NextTarget()
    {
        if (target == null)
        {
            target = wayspoint.GetChild(indexTarget);
        }
        else
        {
            target = wayspoint.GetChild(indexTarget);
        }
        indexTarget++;
        if(indexTarget == wayspoint.childCount)
        {
            wayspoint = GameManager.Instance.mapController.aiManager.GetWaypoint(false);
            indexTarget = 0;
        }
    }
}
