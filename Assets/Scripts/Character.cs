using System;
using System.Collections;
using System.Collections.Generic;
using Bases;
using UnityEngine;

public class Character : BaseMonoBehaviour
{
    private void Awake()
    {
        SubscribeToNotificationWithSelector(Constant.NotificationType.FarmReady,FarmReady);
    }

    private void FarmReady(Notification obj)
    {
        string call = (string) obj.Data;
        Debug.Log(call);
    }

}
