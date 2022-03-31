using System;
using System.Collections;
using System.Collections.Generic;
using Bases;
using Interfaces;
using UnityEngine;

public class CharacterController : BaseMonoBehaviour
{
    private Rigidbody _rb;
    private Producer _activeProducer;
    private Vector3 _moveVector;

    [SerializeField] private VariableJoystick _joystick;
    [SerializeField] private float _currentSpeed;
    [SerializeField] List<GameObject> products;
    public bool onFarm;
    public Transform stackTransform;
    public Transform lastStackTransform;
    public int maxCapacity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        lastStackTransform = stackTransform;
        SubscribeToNotificationWithSelector(Constant.NotificationType.ProductCreated, OnProductCreated);
    }

    private void Update()
    {
        _moveVector = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).normalized * (3 * Time.deltaTime);
        _moveVector = Quaternion.Euler(0, 30, 0) * _moveVector;
        transform.LookAt(transform.position + _moveVector);
        transform.Translate(_moveVector * _currentSpeed * Time.deltaTime, Space.World);
    }

    private void OnProductCreated(Notification notify)
    {
        Producer producer = (Producer) notify.Sender;
        if (onFarm && _activeProducer != null && producer.id == _activeProducer.id)
        {
            StartCoroutine(GetProductFromActiveProducer());
        }
    }

    private IEnumerator GetProductFromActiveProducer()
    {
        if (!onFarm || !_activeProducer.HasProduct)
        {
            yield return null;
        }

        yield return new WaitForSeconds(_activeProducer.giveFrequency);
        if (onFarm && _activeProducer.HasProduct && products.Count < maxCapacity)
        {
            GameObject obj = _activeProducer.GetComponent<Farm>().GetLastProductAndRemove();
            StackManager.instance.PickUp(this.gameObject, obj, OnSuccess);
            StartCoroutine(GetProductFromActiveProducer());
        }
    }

    private void OnSuccess(GameObject obj)
    {
        products.Add(obj);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out _activeProducer))
        {
            onFarm = true;
            if (products.Count < maxCapacity)
            {
                StartCoroutine(GetProductFromActiveProducer());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out _activeProducer))
        {
            _activeProducer = null;
            onFarm = false;
        }
    }
}