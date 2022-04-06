using Models;
using UnityEngine;

public class Chicken : Producer
{
    public FarmModel farmModel;

    public override void Produce()
    {
        if (products.Count < farmModel.productCapacity)
        {
            GameObject createdProduct = Instantiate(farmModel.product.productPrefab, transform);
            Product product = createdProduct.GetComponent<Product>();
            product.ProductModel = new ProductModel(farmModel.product.productId, farmModel.product.productName, 
                farmModel.product.productPrice,farmModel.product.productType);

            products.Add(createdProduct);
            createdProduct.name = $"{farmModel.product.productName}";
            ProductCreated?.Invoke();
        }
    }
}
