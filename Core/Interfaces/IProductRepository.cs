using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
Task<IReadOnlyList<Product>> GetProductsAsync();
Task<Product?> GetProductByIdAsync(int id);
void AddProduct (Product product); 
void UpdadeProduct (Product product);
void DeleteProduct (Product product);
bool ProductExists(int id);
Task<bool>SaveChangesAsync(); 

}
