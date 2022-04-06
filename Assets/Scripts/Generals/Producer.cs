using System;
using System.Collections;
using System.Collections.Generic;
using Bases;
using Models;
using UnityEngine;
using UnityEngine.Events;

public class Producer : BaseMonoBehaviour
{
    public int id;
    public float produceFrequency;
    public Action<Product> ProductCreated;
    
    protected List<GameObject> products;

    public FarmModel farmModel;

    #region ArrowFuncs
    public bool HasProduct => products.Count > 0;

    #endregion
    
    private void Awake()
    {
        products = new List<GameObject>();
        id = farmModel.id;
    }

    private void Start()
    {
        StartCoroutine(ToProduce());
    }

    private IEnumerator ToProduce()
    {
        yield return new WaitForSeconds(produceFrequency);
        StartCoroutine(ToProduce());
        Produce();
    }

    public void OnRemoveProductFromProducer(GameObject _product)
    {
        products.Remove(_product);
    }

    public virtual void Produce()
    {
        if (products.Count < farmModel.productCapacity)
        {
            GameObject createdProduct = Instantiate(farmModel.product.productPrefab, transform);
            Product product = createdProduct.GetComponent<Product>();
            product.ProductModel = new ProductModel(farmModel.product.productId, farmModel.product.productName, farmModel.product.productPrice,farmModel.product.productType);
            product.RemoveProductFromProducer = OnRemoveProductFromProducer;
            products.Add(createdProduct);
            createdProduct.name = $"{farmModel.product.productName}";
            ProductCreated?.Invoke(product);
        }
    }
}