using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class BaseModel
    {
         
    }
    
    [Serializable]
    public class FarmModel : BaseModel
    {
        public int id;
        public string farmName;
        public int productCapacity;
        public int farmPrice;
        public ProductSO product;

        public int GetProductID()
        {
            return product.productId;
        }
    }

    [Serializable]
    public class ProductModel : BaseModel
    {
        public int id;
        public string productName;
        public int productPrice;
        public Constant.ProductType productType;

        public ProductModel(int id,string productName,int productPrice,Constant.ProductType productType)
        {
            this.id = id;
            this.productName = productName;
            this.productPrice = productPrice;
            this.productType = productType;
        }
    }
    
}