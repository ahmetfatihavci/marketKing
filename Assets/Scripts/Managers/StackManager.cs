using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StackManager : Singleton<StackManager>
{

    public void StackProduct(GameObject _pickerGameObject, Product _pickedProduct, Action<Product> Success)
    {
       _pickedProduct.RemoveProductFromProducer?.Invoke(_pickedProduct.gameObject);
        CharacterController _characterController = _pickerGameObject.GetComponent<CharacterController>();
        
        _pickedProduct.transform.parent = _pickerGameObject.transform;
        _pickedProduct.transform.DOLocalMove(_characterController.lastStackTransform.localPosition, 0.3f);
        Vector3 desPos = _characterController.lastStackTransform.localPosition;
        desPos.y += 0.3f;
        _characterController.lastStackTransform.localPosition = desPos;
        Success?.Invoke(_pickedProduct);
    }
    
    
}