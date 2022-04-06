using Models;
using UnityEngine;

public class Farm : Producer
{

    // public override void Produce()
    // {
    //     base.Produce();
    //
    //     // if (products.Count < farmModel.productCapacity)
    //     // {
    //     //     GameObject createdProduct = Instantiate(farmModel.product.productPrefab, transform);
    //     //     Product product = createdProduct.GetComponent<Product>();
    //     //     product.ProductModel = new ProductModel(farmModel.product.productId, farmModel.product.productName, farmModel.product.productPrice,farmModel.product.productType);
    //     //     product.RemoveProductFromProducer = OnRemoveProductFromProducer;
    //     //     products.Add(createdProduct);
    //     //     createdProduct.name = $"{farmModel.product.productName}";
    //     //     ProductCreated?.Invoke(product);
    //     // }
    // } 
}