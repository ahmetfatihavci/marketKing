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
        public float offset;

        private void Start()
        {
            StartCoroutine(ToProduce());
        }

        private IEnumerator ToProduce()
        {
            yield return new WaitForSeconds(offset);
            StartCoroutine(ToProduce());
            Produce();
        }

        public abstract void Produce();
    }
}