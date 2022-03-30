using System;
using System.Collections;
using System.Collections.Generic;
using Bases;
using Interfaces;
using UnityEngine;

public class CharacterController : BaseMonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private IProducer _activeProducer;
    
    [SerializeField] private VariableJoystick _joystick;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private Vector3 _moveVector;
    [SerializeField] List<GameObject> products;
    public bool onFarm;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        SubscribeToNotificationWithSelector(Constant.NotificationType.FarmReady, FarmReady);
    }

    private void Update()
    {
        _moveVector = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).normalized * (3 * Time.deltaTime);
        _moveVector = Quaternion.Euler(0, 30, 0) * _moveVector;
        transform.LookAt(transform.position + _moveVector);
        transform.Translate(_moveVector * _currentSpeed * Time.deltaTime, Space.World);

    }

    private void FarmReady(Notification obj)
    {
        string call = (string) obj.Data;
        Debug.Log(call);
    }

    private void OnTriggerEnter(Collider other)
    {
        _activeProducer = other.GetComponent<IProducer>();
        if (_activeProducer != null)
        {
            onFarm = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IProducer Iproducer = other.GetComponent<IProducer>();
        if (Iproducer != null)
        {
            _activeProducer = null;
            onFarm = false;
        }
    }
}