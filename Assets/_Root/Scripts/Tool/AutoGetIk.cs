using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AutoGetIk : MonoBehaviour
{
    public IkContaniner IkContainer;

    public Transform HipIkTarget;
    public Transform ChestIkTarget;
    public Transform RightFootIkTarget;
    public Transform LeftFootIkTarget;
    public Transform LeftFootIdleIkTarget;
    public Transform RightHandIkTarget;
    public Transform LeftHandIkTarget;
    public Transform HeadIkTarget;


    public MultiParentConstraint hipIk;
    public RigBuilder rigBuilder;
    [Button]
    public void GetIk()
    {
        //HipIkTarget = FindDeepChild(transform,"HipIKTarget");
        //ChestIkTarget = FindDeepChild(transform, "ChestIKTarget");
        //RightFootIkTarget = FindDeepChild(transform, "RightFootIKTarget");
        //LeftFootIkTarget = FindDeepChild(transform, "LeftFootIKTarget");
        //LeftFootIdleIkTarget = FindDeepChild(transform, "LeftFootIdleIKTarget");
        //RightHandIkTarget = FindDeepChild(transform, "RightHandIKTarget");
        //LeftHandIkTarget = FindDeepChild(transform, "LeftHandIKTarget");
        //HeadIkTarget = FindDeepChild(transform, "HeadIKTarget");


        var sources = hipIk.data.sourceObjects;

        sources.SetTransform(0, HipIkTarget);
        sources.SetWeight(0, 1f);

        hipIk.data.sourceObjects = sources;

        rigBuilder.Build();

#if UNITY_EDITOR
        EditorUtility.SetDirty(IkContainer.HipK);
        EditorUtility.SetDirty(gameObject);
#endif
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
