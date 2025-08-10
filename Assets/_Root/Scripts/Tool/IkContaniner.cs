using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IkContaniner : MonoBehaviour
{
    public MultiParentConstraint HipK;
    public TwoBoneIKConstraint ChestIK;
    public TwoBoneIKConstraint RightFootIK;
    public TwoBoneIKConstraint LeftFootIK;
    public TwoBoneIKConstraint LeftFootIdleIK;
    public TwoBoneIKConstraint RightHandIK;
    public TwoBoneIKConstraint LeftHandIK;
    public MultiAimConstraint HeadIk;

    [Button]
    public void GetIk()
    {
        HipK = GetComponentInChildren<MultiParentConstraint>();
        ChestIK = GetComponentInChildren<TwoBoneIKConstraint>();
        RightFootIK = GetComponentInChildren<TwoBoneIKConstraint>();
        LeftFootIK = GetComponentInChildren<TwoBoneIKConstraint>();
        LeftFootIdleIK = GetComponentInChildren<TwoBoneIKConstraint>();
        RightHandIK = GetComponentInChildren<TwoBoneIKConstraint>();
        LeftHandIK = GetComponentInChildren<TwoBoneIKConstraint>();
        HeadIk = GetComponentInChildren<MultiAimConstraint>();
    }


}
