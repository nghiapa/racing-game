using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AutoGetIk : SerializedMonoBehaviour
{
    public ebike BikeType;

    public IkContaniner IkContainer;
    public Transform bikerPos;


    public Transform HipIkTarget;
    public Transform ChestIkTarget;
    public Transform RightFootIkTarget;
    public Transform LeftFootIkTarget;
    public Transform LeftFootIdleIkTarget;
    public Transform RightHandIkTarget;
    public Transform LeftHandIkTarget;
    public Transform HeadIkTarget;

    public Transform RightFootIKHint;
    public Transform LeftFootIKHint;


    [Button]
    public void GetIk()
    {


        HipIkTarget = FindDeepChild(transform, "HipIKTarget");
        ChestIkTarget = FindDeepChild(transform, "ChestIKTarget");
        RightFootIkTarget = FindDeepChild(transform, "RightFootIKTarget");
        LeftFootIkTarget = FindDeepChild(transform, "LeftFootIKTarget");
        LeftFootIdleIkTarget = FindDeepChild(transform, "LeftFootIdleIKTarget");
        RightHandIkTarget = FindDeepChild(transform, "RightHandIKTarget");
        LeftHandIkTarget = FindDeepChild(transform, "LeftHandIKTarget");
        HeadIkTarget = FindDeepChild(transform, "HeadIKTarget");

        RightFootIKHint = FindDeepChild(transform, "RightFootIKHint");
        LeftFootIKHint = FindDeepChild(transform, "LeftFootIKHint");

        IkContainer.GetIk();

        var sources = IkContainer.HipK.data.sourceObjects;
        sources.SetTransform(0, HipIkTarget);
        sources.SetWeight(0, 1f);
        IkContainer.HipK.data.sourceObjects = sources;

        var chestSources = IkContainer.ChestIK.data;
        chestSources.target = ChestIkTarget;
        IkContainer.ChestIK.data = chestSources;

        var rightFootSources = IkContainer.RightFootIK.data;
        rightFootSources.target = RightFootIkTarget;
        rightFootSources.hint = RightFootIKHint;
        IkContainer.RightFootIK.data = rightFootSources;

        var leftFootSources = IkContainer.LeftFootIK.data;
        leftFootSources.target = LeftFootIkTarget;
        leftFootSources.hint = LeftFootIKHint;
        IkContainer.LeftFootIK.data = leftFootSources;

        var leftFootIdleSources = IkContainer.LeftFootIdleIK.data;
        leftFootIdleSources.target = LeftFootIdleIkTarget;
        IkContainer.LeftFootIdleIK.data = leftFootIdleSources;

        var rightHandSources = IkContainer.RightHandIK.data;
        rightHandSources.target = RightHandIkTarget;
        IkContainer.RightHandIK.data = rightHandSources;

        var leftHandSources = IkContainer.LeftHandIK.data;
        leftHandSources.target = LeftHandIkTarget;
        IkContainer.LeftHandIK.data = leftHandSources;

        var headSources = IkContainer.HeadIk.data.sourceObjects;
        headSources.SetTransform(0, HeadIkTarget);
        headSources.SetWeight(0, 1f);
        IkContainer.HeadIk.data.sourceObjects = headSources;

        IkContainer.rigBuilder.Build();
    }
    Transform FindDeepChild(Transform parent, string name)
    {
        foreach (Transform child in parent.GetComponentsInChildren<Transform>(true))
        {
            if (child.name == name)
                return child;
        }
        return null;
    }
}
