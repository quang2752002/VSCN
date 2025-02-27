using GUIs.Helper;
using GUIs.Support;
using Microsoft.AspNetCore.Mvc;
using VSCN.Models.DAO;
using VSCN.Models.VIEW;

namespace VSCN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController :Controller
    {
        private const string ARTICLE_ID = "ARTICLE_ID";

        public IActionResult Index()
        {
            CategoryDAO categoryDAO = new CategoryDAO();
            ViewBag.Pagesize = DataServices.Pagesize();
            ViewBag.Year = DataServices.Year();
            ViewBag.Month = DataServices.Months();
            ViewBag.ListCategory = categoryDAO.getAll();
            return View();
        }
        [HttpPost]
        public JsonResult Showlist(string name = "", int categoryId = 0, int index = 1, int size = 10)
        {
            try
            {
                ProductDAO produtDAO = new ProductDAO();
                (int total, List<ProductVIEW> list) = produtDAO.Search(name, categoryId, index, size);
                string page = Support.InTrang(total, index, size);
                return Json(new { data = list, page = page });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mess = $"Lỗi: {ex.Message}" });
            }
        }
        //public IActionResult Create()
        //{
        //    CategoryDAO categoryDAO = new CategoryDAO();

        //    ViewBag.ListCategory = categoryDAO.getAll();
        //    return View();
        //}
        //[HttpPost]
        //public JsonResult Create(string name, string slug, int? parentId, string typeArticle, string summary, string content, IFormFile img, bool active, bool isShow)
        //{
        //    try
        //    {
        //        ProductDAO productDAO = new ProductDAO();
        //        if (productDAO.CheckSlug(slug,0))
        //        {
        //            return Json(new { success = false, mess = "Lỗi tên bài viết trùng nhau" });

        //        }
        //        // If parentId is 0, set it to null
        //        if (parentId == 0)
        //        {
        //            parentId = null;
        //        }

        //        // Xử lý ảnh tải lên
        //        string imgPath = null;
        //        if (img != null && img.Length > 0)
        //        {
        //            // Đường dẫn thư mục để lưu ảnh
        //            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        //            if (!Directory.Exists(uploads))
        //            {
        //                Directory.CreateDirectory(uploads);
        //            }

        //            // Tạo tên tệp ảnh duy nhất (có thể dùng Guid hoặc tên gốc)
        //            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
        //            var filePath = Path.Combine(uploads, fileName);

        //            // Lưu tệp ảnh
        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                img.CopyTo(stream);
        //            }

        //            // Đường dẫn ảnh mà sẽ lưu vào cơ sở dữ liệu
        //            imgPath = "/uploads/" + fileName;
        //        }

        //        // Tạo đối tượng Article
        //        Article article = new Article
        //        {
        //            Name = name,
        //            Slug = slug,
        //            CategoryId = parentId,
        //            TypeArticle = typeArticle,
        //            Summary = summary,
        //            Content = content,
        //            Avatar = imgPath, // Lưu đường dẫn ảnh vào trường Avatar
        //            Active = active,
        //            IsShow = isShow,
        //            DateCreate = DateTime.Now,
        //            CreateBy = "admin", // Hoặc lấy tên người dùng hiện tại nếu cần
        //            Trash = false,
        //        };

        //        // Thêm bài viết vào cơ sở dữ liệu

        //        articleDAO.InsertOrUpdate(article); // Phương thức để thêm hoặc cập nhật bài viết

        //        return Json(new { success = true, mess = "Thêm bài viết thành công" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, mess = $"Lỗi: {ex.Message}" });
        //    }
        //}


        //public IActionResult Edit(int id)
        //{
        //    CategoryDAO categoryDAO = new CategoryDAO();
        //    ViewBag.Pagesize = DataServices.Pagesize();

        //    ViewBag.ListCategory = categoryDAO.getAll();
        //    HttpContext.Session.SetInt32(ARTICLE_ID, id);

        //    return View();
        //}
        //[HttpPost]
        //public JsonResult Update(int id, string name, string slug, int? parentId, string img, string description, bool active, bool isShow)
        //{
        //    try
        //    {
        //        ArticleDAO articleDAO = new ArticleDAO();
        //        if (articleDAO.CheckSlug(slug))
        //        {
        //            return Json(new { success = false, mess = "Lỗi tên bài viết trùng nhau" });

        //        }
        //        if (parentId == 0)
        //        {
        //            parentId = null;
        //        }

        //        Article item = articleDAO.GetItem(id);
        //        item.Name = name;
        //        item.Slug = slug;
        //        item.CategoryId = parentId;
        //        item.Avatar = img;
        //        item.Summary = description;
        //        item.Active = active;
        //        item.IsShow = isShow;
        //        item.DateEdit = DateTime.Now;
        //        item.EditBy = "admin";

        //        articleDAO.InsertOrUpdate(item);

        //        return Json(new { success = true, mess = "Sửa bài viết thành công." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, mess = $"Lỗi: {ex.Message}" });
        //    }
        //}

        //[HttpPost]
        //public JsonResult Delete(int id)
        //{
        //    try
        //    {
        //        ArticleDAO articleDAO = new ArticleDAO();
        //        articleDAO.Delete(id);
        //        return Json(new { mess = "Xóa thành công" });
        //    }

        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, mess = $"Lỗi: {ex.Message}" });
        //    }

        //}
        //[HttpGet]
        //public JsonResult getArticle()
        //{
        //    try
        //    {
        //        int id = HttpContext.Session.GetInt32(ARTICLE_ID) ?? 0;

        //        ArticleDAO articleDAO = new ArticleDAO();
        //        var query = articleDAO.GetItemView(id);
        //        return Json(new { data = query });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, mess = $"Lỗi: {ex.Message}" });
        //    }

        //}
        //[HttpPost]
        //public JsonResult OnChange(int id, int orderBy)
        //{
        //    try
        //    {
        //        ArticleDAO articleDAO = new ArticleDAO();
        //        Article item = articleDAO.GetItem(id);
        //        if (orderBy == 0)
        //        {
        //            item.OrderBy = null;
        //            articleDAO.InsertOrUpdate(item);
        //        }
        //        if (orderBy > 0)
        //        {
        //            item.OrderBy = orderBy;
        //            articleDAO.InsertOrUpdate(item);
        //        }
        //        return Json(new { mess = "Cập nhật thành công" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, mess = $"Lỗi: {ex.Message}" });
        //    }
        //}

    }
}
