using System;
using System.Collections;
using System.Collections.Generic;
using Bases;
using UnityEngine;

[Serializable][CreateAssetMenu(fileName = "Product",menuName = "ScriptableObjects/CreateProduct")]
public class ProductSO : ScriptableObject
{
    public int productId;
    public string productName;
    public GameObject productPrefab;
    public int productPrice;
    public Constant.ProductType productType;
}
