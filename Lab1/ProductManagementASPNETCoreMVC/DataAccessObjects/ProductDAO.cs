using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects
{
    public class ProductDAO
    {
        public static List<Product> GetProducts()
        {
            var listProducts = new List<Product>();
            try
            {
                using var db = new MyStoreContext();
                listProducts = db.Products.Include(f => f.Category).ToList();
                // Sử dụng Include để lấy dữ liệu của Category liên quan đến Product
            }
            catch (Exception ex) { }
            return listProducts;
        }
        public static void SaveProduct(Product p)
        {
            try
            {
                using var context = new MyStoreContext();
                context.Products.Add(p);    // Thêm sản phẩm mới vào DbSet
                context.SaveChanges();   // Lưu thay đổi vào cơ sở dữ liệu
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static void UpdateProduct(Product p)
        {
            try
            {
                using var context = new MyStoreContext();
                context.Entry<Product>(p).State 
                    =Microsoft.EntityFrameworkCore.EntityState.Modified;
                // Đánh dấu thực thể là đã được sửa đổi
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeleteProduct(Product p)
        {
            try
            {
                using var context = new MyStoreContext();
                var p1 = context.Products.SingleOrDefault(c => c.ProductId == p.ProductId);
                context.Products.Remove(p1);   // Xóa sản phẩm khỏi DbSet
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static Product GetProductByID(int id)
        {
            using var db = new MyStoreContext();
            return db.Products.FirstOrDefault(c => c.ProductId.Equals(id));
        }
    }
}