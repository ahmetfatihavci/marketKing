using System;
using System.Collections;
using System.Collections.Generic;
using Bases;
using UnityEngine;

namespace Interfaces
{
    public interface IProducer
    {
        void Produce();
    }


    public abstract class Producer : BaseMonoBehaviour, IProducer
    {
        public int id;
        public float produceFrequency;
        public float giveFrequency;
        public List<GameObject> products;
        
        
        public bool HasProduct => products.Count > 0;

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


        public abstract void Produce();
    }
}