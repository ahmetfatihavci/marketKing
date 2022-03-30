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

    }   
 
}


