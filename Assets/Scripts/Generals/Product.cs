using Bases;
using Models;
using UnityEngine;
using UnityEngine.Events;

public class Product : BaseMonoBehaviour
{
    public UnityAction<GameObject> RemoveProductFromProducer;
    private ProductModel _productModel;
    
    public ProductModel ProductModel
    {
        get { return _productModel;}
        set { _productModel = value; }
    }
}