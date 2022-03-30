using System.Collections;
using System.Collections.Generic;
using Bases;
using UnityEngine;

public class Farm : BaseMonoBehaviour
{
    public Models.FarmModel farmModel;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Call",5);
    }

    public void Call()
    {
        NotificationCenter.instance.PostNotification(this,Constant.NotificationType.FarmReady,$"{farmModel.farmName} is ready !");
        Invoke("Call",5);
    }
}
