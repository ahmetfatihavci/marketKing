using System;
using System.Collections;
using System.Collections.Generic;
using Bases;
using UnityEngine;
using UnityEngine.Events;

public class Producer : BaseMonoBehaviour
{
    public int id;
    public float produceFrequency;
    public float giveFrequency;
    [SerializeField]protected List<GameObject> products;
    public Action ProductCreated;


    public bool HasProduct => products.Count > 0;

    private void Awake()
    {
        products = new List<GameObject>();
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
    }
}