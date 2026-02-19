using Microsoft.EntityFrameworkCore;
using BackMakerHub.Models;
using BackMakerHub.DbConnection;
using BackMakerHub.DTO_s;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;

namespace BackMakerHub.Services
{
    public class ProductsService
    {
        private readonly DbLink _context;
        public ProductsService(DbLink context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Products>> GetAllProducts()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Products> AddProductsAsync(ProductsCreateDTO addProductDTO)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == addProductDTO.CategoryName);
            if(category == null)
            {
                throw new Exception("Aucune catégorie correspondante");
            }
            var newProduct = new Products
            {
                Name = addProductDTO.Name,
                Quantity = addProductDTO.Quantity,
                Price = addProductDTO.Price,
                Type = addProductDTO.Type,
                CategoryId = category.Id,
            };
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
            return newProduct;
        }
        public async Task<bool> RemoveProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Products> ModifyProductAsync(Products product)
        {
            var Product = await _context.Products.AnyAsync(p => p.ProductId == product.ProductId);
            if (!Product)
            {
                return null;
            }
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<Products> FilterProductAsync(Products product)
        {
           var filteredProduct = await _context.Products.FirstOrDefaultAsync(p => p.Name == product.Name);
            return filteredProduct;
        }

        public async Task<Products> UpdateProductQuantity(int id, int newQuantity)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            product.Quantity = newQuantity;
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
