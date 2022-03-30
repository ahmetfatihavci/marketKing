using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bases
{
    public class BaseMonoBehaviour : MonoBehaviour
    {
        private List<Constant.NotificationType> _notifications = new List<Constant.NotificationType>();

        public void OnDestroy () {
            this.RemoveObservers();
        }

        #region Notifications

        protected void SubscribeToNotificationWithSelector (Constant.NotificationType notificationType,
            Action<Notification> action) {
            if (_notifications.Contains(notificationType)) {
                return;
            }
            NotificationCenter.instance.AddObserver(this, notificationType, action);
            _notifications.Add(notificationType);
        }

        protected void UnSubscribe (Constant.NotificationType notificationType) {
            if (_notifications.Contains(notificationType)) {
                NotificationCenter.instance.RemoveObserver(this, notificationType);
                _notifications.Remove(notificationType);
            }
        }

        protected void RemoveObservers () {
            foreach (Constant.NotificationType notificationType in _notifications) {
                if (NotificationCenter.instance != null) {
                    NotificationCenter.instance.RemoveObserver(this, notificationType);
                }
            }
        }

        #endregion
    }
}
