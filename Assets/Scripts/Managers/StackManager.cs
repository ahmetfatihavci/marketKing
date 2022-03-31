using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : Singleton<StackManager>
{
    public void PickUp(GameObject _pickerGameObject, GameObject _pickedGameObject, Action<GameObject> Success)
    {
        if (_pickedGameObject == null || _pickerGameObject == null)
        {
            return;
        }
        CharacterController _characterController = _pickerGameObject.GetComponent<CharacterController>();
        _pickedGameObject.transform.parent = _characterController.stackTransform;

        Vector3 desPos = _characterController.lastStackTransform.localPosition;
        desPos = new Vector3(0, desPos.y, 0);
        desPos.y += _pickedGameObject.transform.localScale.y;

        _pickedGameObject.transform.localPosition = desPos;
        _characterController.lastStackTransform = _pickedGameObject.transform;
        Success?.Invoke(_pickedGameObject);
    }
}