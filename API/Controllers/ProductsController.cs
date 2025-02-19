using System;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository repo): ControllerBase

{
[HttpGet]
public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
{
   return Ok(await repo.GetProductsAsync());

}

[HttpGet("{id:int}")]

public async Task<ActionResult<Product>> GetProduct(int id)
{
var product = await repo.GetProductByIdAsync(id); 

   if(product==null) return NotFound();

   return product;
}

[HttpPost]

public async Task<ActionResult<Product>> CreateProduct(Product product)
{   
   repo.AddProduct(product);

   if (await repo.SaveChangesAsync())

   {
   return CreatedAtAction("GetProduct", new{id=product.Id}, product);
   }
   await repo.SaveChangesAsync(); 
   return product;
}

[HttpPut("{id:int}")]

public async Task<ActionResult> UpdateProduct(int id , Product product)

{
if (product.Id != id || !ProductExists(id))
    return BadRequest("Cannot update this Product");
    repo.UpdadeProduct(product); 

    if(await repo.SaveChangesAsync())
    {
      return NoContent();
    }    
    
    return BadRequest("Problems Updating the Products");
}
[HttpDelete("{id:int}")]

public async Task<ActionResult> DeleteProduct(int id)
{
 var product = await repo.GetProductByIdAsync(id);

 if (product==null) return NotFound();

 repo.DeleteProduct(product);
 if(await repo.SaveChangesAsync())
    {
      return NoContent();
    }    
    
    return BadRequest("Problems Deleting the Products");
}
private bool ProductExists(int id)
{
    return repo.ProductExists(id);

}
}
