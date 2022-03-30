using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;


#region Notification Class

public class Notification
{
    public Object Sender;
    public Constant.NotificationType Type;
    public object Data;

    public Notification(Object sender, Constant.NotificationType type)
    {
        Sender = sender;
        Type = type;
    }

    public Notification(Object sender,Constant.NotificationType type,object data)
    {
        Sender = sender;
        Type = type;
        Data = data;
    }
}

#endregion

#region Notification Center

public class NotificationCenter : Singleton<NotificationCenter>
{
    private Dictionary<Constant.NotificationType, Dictionary<Object, Action<Notification>>> _notifications =
        new Dictionary<Constant.NotificationType, Dictionary<Object, Action<Notification>>>();

    private static readonly object lockObj = new object();

    private NotificationCenter()
    {
    }

    public void AddObserver(Object observer,
        Constant.NotificationType type,
        Action<Notification> action)
    {
        lock (lockObj)
        {
            if (!_notifications.ContainsKey(type))
            {
                _notifications[type] = new Dictionary<Object, Action<Notification>>();
            }

            var notifyList = _notifications[type];

            if (!notifyList.ContainsKey(observer))
            {
                notifyList.Add(observer, action);
            }
        }
    }

    public void RemoveObserver(Object observer,
        Constant.NotificationType type)
    {
        lock (lockObj)
        {
            if (!_notifications.ContainsKey(type))
            {
                return;
            }

            var notifyList = _notifications[type];
            if (notifyList.ContainsKey(observer))
            {
                notifyList.Remove(observer);
            }

            if (notifyList.Count == 0)
            {
                _notifications.Remove(type);
            }
        }
    }

    public void PostNotification(Object sender,
        Constant.NotificationType type)
    {
        PostNotification(sender, type, null);
    }

    public void PostNotification(Object sender,
        Constant.NotificationType type,
        object data)
    {
        PostNotification(new Notification(sender, type, data));
    }

    private void PostNotification(Notification not)
    {
        if (!_notifications.ContainsKey(not.Type))
        {
            return;
        }

        Dictionary<int, int> asd = new Dictionary<int, int>();


        var notifyList = _notifications[not.Type];
        if (notifyList == null || notifyList.Count == 0)
        {
            return;
        }

        lock (lockObj)
        {
            foreach (var item in notifyList.Keys.ToList())
            {
                var callAction = notifyList[item];
                if (not != null)
                {
                    callAction(not);
                }
            }
        }
    }

    #endregion
}