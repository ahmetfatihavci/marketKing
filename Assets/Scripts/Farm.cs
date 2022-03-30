using System;
using Bases;
using Interfaces;
using Models;
using UnityEngine;

public class Farm : Producer
{
    public FarmModel farmModel;
    [SerializeField] private int _activeProductCount;

    public override void Produce()
    {
        if (_activeProductCount < farmModel.productCapacity)
        {
            _activeProductCount++;
           GameObject createdProduct = Instantiate(farmModel.product.productPrefab,transform);
           createdProduct.name = $"Product{_activeProductCount}";
        }
    }

}