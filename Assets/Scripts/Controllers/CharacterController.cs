using System.Collections.Generic;
using Bases;
using Managers;
using UnityEngine;

public class CharacterController : BaseMonoBehaviour
{
    [SerializeField] private Producer _activeProducer;
    private Vector3 _moveVector;

    [SerializeField] private VariableJoystick _joystick;
    [SerializeField] private float _currentSpeed;
    public List<Product> products;
    public Transform stackTransform;
    public Vector3 lastStackPosition;
    public int maxCapacity;
    public bool onFarm;


    #region ArrowFuncs
    public bool HasProduct => products.Count > 0;

    #endregion
    
    private void Awake()
    {
        lastStackPosition = stackTransform.localPosition;
    }

    private void Update()
    {
        _moveVector = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).normalized * (3 * Time.deltaTime);
        _moveVector = Quaternion.Euler(0, 30, 0) * _moveVector;
        transform.LookAt(transform.position + _moveVector);
        transform.Translate(_moveVector * _currentSpeed * Time.deltaTime, Space.World);
    }
    
    
    private void OnSuccessStack(Product obj)
    {
        products.Add(obj);
    }
    
    private void OnSuccessGive(Product obj)
    {
        products.Remove(obj);
    }
    
    private void OnProductCreated(Product _product)
    {
        StackManager.instance.StackProduct(this, _product, OnSuccessStack);
    }

    public Product GetProductByType(Constant.ProductType _productType)
    {
        return products.FindLast(x => x.ProductModel.productType == _productType);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Producer>()!=null)
        {
            onFarm = true;
            _activeProducer = other.GetComponent<Producer>();
            _activeProducer.ProductCreated += OnProductCreated;
        }
        
        if (other.GetComponent<Product>() != null && onFarm)
        {
            if (maxCapacity > products.Count)
            {
                StackManager.instance.StackProduct(this, other.GetComponent<Product>(), OnSuccessStack);
            }
        }
        else if (other.GetComponent<Place>() != null)
        {
            StackManager.instance.GiveProductToPlace(this,other.GetComponent<Place>(),OnSuccessGive);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Producer>() != null)
        {
            onFarm = false;
            _activeProducer.ProductCreated -= OnProductCreated;
            _activeProducer = null;

        }
    }
}