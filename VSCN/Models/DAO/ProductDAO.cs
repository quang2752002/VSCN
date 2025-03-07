﻿using VSCN.Helper;
using VSCN.Models.EF;
using VSCN.Models.VIEW;

namespace VSCN.Models.DAO
{
    public class ProductDAO
    {
        private VSCNContext context = new VSCNContext();
        public void InsertOrUpdate(Product item)
        {
            if (item.Id == 0)
            {
                context.Products.Add(item);
            }

            context.SaveChanges();
        }

        public Product GetItem(int id)
        {
            var query = context.Products.Where(x => x.Id == id && x.Trash == false).FirstOrDefault();
            return query;
        }
        public ProductVIEW GetItemVIEW(int id)
        {
            var query = (from a in context.Products
                         where a.Trash == false &&                      
                         a.Trash == false &&
                        a.Id==id
                         select new ProductVIEW
                         {
                             Id = a.Id,
                             CategoryId = a.CategoryId ?? 0, // Nếu có ParentId tương ứng với danh mục cha
                             Name = a.Name,
                             Slug = a.Slug, // Nếu có trường Slug trong bảng Categories
                             TypeArticle = a.TypeArticle, // Nếu có trường TypeArticle
                             Content = a.Content, // Nếu có trường Content
                             Avatar = a.Avatar,
                             Trash = a.Trash ?? false,
                             Active = a.Active ?? true


                         }).FirstOrDefault();
            return query;
        }
        public Product GetItemBySlug(string slug)
        {
            var query = context.Products.Where(x => x.Slug == slug && x.Trash == false).FirstOrDefault();
            return query;
        }
        public (int, List<ProductVIEW>) ShowList()//int index = 1, int size = 10
        {
           

            var query = (from a in context.Products
                         where a.Trash == false && 
                         a.Active==true &&
                        
                         a.TypeArticle==Common.SERVICE
                         select new ProductVIEW
                         {
                             Id = a.Id,
                             CategoryId = a.CategoryId??0, // Nếu có ParentId tương ứng với danh mục cha
                             Name = a.Name,
                             Slug = a.Slug, // Nếu có trường Slug trong bảng Categories
                             TypeArticle = a.TypeArticle, // Nếu có trường TypeArticle
                             Content = a.Content, // Nếu có trường Content
                             Avatar = a.Avatar, 
                             Trash = a.Trash??false,
                             Active = a.Active??true


                         }).ToList();

            //// Tổng số bản ghi
            int total = query.Count();

            //// Áp dụng phân trang nếu cần
            //if (size > 0 && index > 0)
            //{
            //    query = query.Skip((index - 1) * size).Take(size).ToList();
            //}

            return (total, query);
            
        }
        public (int, List<ProductVIEW>) Search(string name = "", int categoryId = 0, int index = 1, int size = 10)
        {
            if (string.IsNullOrEmpty(name)) name = "";

            var query = (from a in context.Products
                         join b in context.Categories on a.CategoryId equals b.Id
                         where a.Name.Contains(name) && a.Trash == false
                         select new ProductVIEW
                         {
                             Id = a.Id,
                             CategoryId = a.CategoryId ?? 0, // Nếu có ParentId tương ứng với danh mục cha
                             Name = a.Name,
                             Slug = a.Slug, // Nếu có trường Slug trong bảng Categories
                             TypeArticle = a.TypeArticle, // Nếu có trường TypeArticle
                             Content = a.Content, // Nếu có trường Content
                             Avatar = a.Avatar,
                             Trash = a.Trash ?? false,
                             Active = a.Active ?? true
                         });
            if (categoryId != 0)
            {
                query = query.Where(x => x.CategoryId == categoryId);

            }
            int total = query.Count();

            if (size > 0 && index > 0)
            {
                query = query.Skip((index - 1) * size).Take(size);
            }

            return (total, query.ToList());
        }
        public bool CheckSlug(string slug, int id)
        {
            if (id != 0)
            {
                var query = context.Products.FirstOrDefault(x => x.Slug == slug && x.Id != id);
                return query != null;
            }
            else
            {
                var query = context.Products.Where(x => x.Slug == slug).FirstOrDefault();
                return query != null;
            }
        }
        public List<ProductVIEW> GetList(string service)
        {
            var query = (from a in context.Products
                         where a.Trash == false && a.TypeArticle==service
                         select new ProductVIEW
                         {
                             Id = a.Id,
                             CategoryId = a.CategoryId ?? 0, // Nếu có ParentId tương ứng với danh mục cha
                             Name = a.Name,
                             Slug = a.Slug, // Nếu có trường Slug trong bảng Categories
                             TypeArticle = a.TypeArticle, // Nếu có trường TypeArticle
                             Content = a.Content, // Nếu có trường Content
                             Avatar = a.Avatar,
                             Trash = a.Trash ?? false,
                             Active = a.Active ?? true
                         }).ToList();
            return query;
        }
    }
}
