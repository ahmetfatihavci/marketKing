using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Managers
{
    public class StackManager : Singleton<StackManager>
    {
        public void StackProduct(CharacterController _picker, Product _pickedProduct, Action<Product> Success)
        {
            _pickedProduct.RemoveProductFromProducer?.Invoke(_pickedProduct.gameObject);
            Vector3 _desPos = _picker.lastStackPosition;
            _desPos.x = 0;
            if (_picker.HasProduct)
            {
                _desPos.y += _pickedProduct.transform.localScale.y;
            }

            _desPos.z = 0;
            _pickedProduct.transform.parent = _picker.stackTransform;
            _pickedProduct.transform.DOLocalMove(_desPos, 0.3f);

            _picker.lastStackPosition = _desPos;
            Success?.Invoke(_pickedProduct);
        }

        public void GiveProductToPlace(CharacterController _giver, Place _place, Action<Product> Success)
        {
            Product willGiveProduct = _giver.GetProductByType(_place.productType);
            if (willGiveProduct != null)
            {
                Vector3 _desPos = _giver.lastStackPosition;
                _desPos.x = 0;
                _desPos.y -= willGiveProduct.transform.localScale.y;
                _desPos.z = 0;

                willGiveProduct.transform.parent = _place.transform;
                willGiveProduct.transform.DOLocalMove(new Vector3(0, 1.5f, 0), 0.3f);
                Success?.Invoke(willGiveProduct);
                _giver.lastStackPosition = _desPos;
                SortProducts(_giver);
            }
            else
            {
                LogManager.Log("willGiveProduct is null",LogType.Warning);
            }
        }

        public void SortProducts(CharacterController _characterController)
        {
            Vector3 _startPos = _characterController.stackTransform.localPosition;
            for (int i = 0; i < _characterController.products.Count; i++)
            {
                Vector3 tmp = _startPos;
                tmp.x = 0;
                tmp.y = (i * _characterController.products[i].transform.localScale.y);
                tmp.z = 0;
                _characterController.products[i].transform.localPosition = tmp;
            }
        }
    }
}