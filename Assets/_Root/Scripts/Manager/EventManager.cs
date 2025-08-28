using System;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static Action<int> Event_OnPlayerCointChange;

    public static Action Event_OnPlayerDie;

}
