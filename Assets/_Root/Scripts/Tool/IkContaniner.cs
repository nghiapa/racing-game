using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IkContaniner : MonoBehaviour
{
    public eRider RiderType;

    public MultiParentConstraint HipK;
    public TwoBoneIKConstraint ChestIK;
    public TwoBoneIKConstraint RightFootIK;
    public TwoBoneIKConstraint LeftFootIK;
    public TwoBoneIKConstraint LeftFootIdleIK;
    public TwoBoneIKConstraint RightHandIK;
    public TwoBoneIKConstraint LeftHandIK;
    public MultiAimConstraint HeadIk;

    public RigBuilder rigBuilder;


    [Button]
    public void GetIk()
    {
        HipK = GetComponentInChildren<MultiParentConstraint>();
        ChestIK = FindDeepChild(transform, "ChestIK").GetComponentInChildren<TwoBoneIKConstraint>();
        RightFootIK = FindDeepChild(transform, "RightFootIK").GetComponentInChildren<TwoBoneIKConstraint>();
        LeftFootIK = FindDeepChild(transform, "LeftFootIK").GetComponentInChildren<TwoBoneIKConstraint>();
        LeftFootIdleIK = FindDeepChild(transform, "LeftFootIdleIK").GetComponentInChildren<TwoBoneIKConstraint>();
        RightHandIK = FindDeepChild(transform, "RightHandIK").GetComponentInChildren<TwoBoneIKConstraint>();
        LeftHandIK = FindDeepChild(transform, "LeftHandIK").GetComponentInChildren<TwoBoneIKConstraint>();
        HeadIk = GetComponentInChildren<MultiAimConstraint>();
    }

    GameObject FindDeepChild(Transform parent, string name)
    {
        foreach (Transform child in parent.GetComponentsInChildren<Transform>(true))
        {
            if (child.name == name)
                return child.gameObject;
        }
        return null;
    }
}
