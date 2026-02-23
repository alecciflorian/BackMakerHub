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
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            var allLogs = await _context.StockLogs.ToListAsync();
            foreach(var p in products)
            {
                var lastLogs = allLogs.Where(s => s.ProductId == p.ProductId).OrderByDescending(s => s.Id).FirstOrDefault();
                if (lastLogs != null)
                {
                    p.LastStockInfo = $"{lastLogs.date} (+{lastLogs.QuantityAdded})";
                }
                else
                {
                    p.LastStockInfo = "Aucun mouvement";
                }
            }
            return products;
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

            var logs = new StockLog
            {
                date = DateOnly.FromDateTime(DateTime.Now),
                ProductId = newProduct.ProductId
            };
            _context.StockLogs.Add(logs);
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
            int addedAmount = newQuantity - product.Quantity;

            product.Quantity = newQuantity;

            var log = new StockLog
            {
                date = DateOnly.FromDateTime(DateTime.Now),
                ProductId = id,
                QuantityAdded = addedAmount
            };


            _context.StockLogs.Add(log);
            await _context.SaveChangesAsync();
            product.LastStockInfo = $"{log.date} ({(addedAmount >= 0 ? "+" : "")}{addedAmount})";
            return product;
        }
        public async Task<Products?> GetProductDate(int id)
        {
           return await _context.Products.Include(p => p.StockLogs).FirstOrDefaultAsync(p => p.ProductId == id);
        }
    }
}
