using System;
using System.Collections.Generic;
using System.Linq;
using Bases;
using Interfaces;
using Models;
using UnityEngine;

public class Farm : Producer
{
    public FarmModel farmModel;


    public override void Produce()
    {
        if (products.Count < farmModel.productCapacity)
        {
            GameObject createdProduct = Instantiate(farmModel.product.productPrefab, transform);
            products.Add(createdProduct);
            createdProduct.name = $"{farmModel.product.productName}";
            NotificationCenter.instance.PostNotification(this,Constant.NotificationType.ProductCreated);
        }
    }

    public GameObject GetLastProductAndRemove()
    {
        return products.RemoveLast();
    }
    
}