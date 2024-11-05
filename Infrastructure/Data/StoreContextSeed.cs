using System;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContextSeed
{
public static async Task SeedAsync(StoreContext context)
{
if (!context.Products.Any())
{
var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/procucts.jcon");

var procucts = JsonSerializer.Deserialize<List<Product>>(productsData);

if (procucts==null) return;

context.Products.AddRange(procucts);
await context.SaveChangesAsync();

}
}
}

