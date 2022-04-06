using System;
using System.Collections;
using System.Collections.Generic;
using Bases;
using UnityEngine;

public class CharacterController : BaseMonoBehaviour
{
    private Producer _activeProducer;
    private Vector3 _moveVector;

    [SerializeField] private VariableJoystick _joystick;
    [SerializeField] private float _currentSpeed;
    [SerializeField] List<Product> products;
    public Transform stackTransform;
    public Transform lastStackTransform;
    public int maxCapacity;

    private void Awake()
    {
        lastStackTransform = stackTransform;
    }

    private void Update()
    {
        _moveVector = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).normalized * (3 * Time.deltaTime);
        _moveVector = Quaternion.Euler(0, 30, 0) * _moveVector;
        transform.LookAt(transform.position + _moveVector);
        transform.Translate(_moveVector * _currentSpeed * Time.deltaTime, Space.World);
    }

    private void OnSuccess(Product obj)
    {
        products.Add(obj);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Product>() != null)
        {
            if (maxCapacity > products.Count)
            {
                StackManager.instance.StackProduct(this.gameObject, other.GetComponent<Product>(), OnSuccess);
            }
        }
    }
}