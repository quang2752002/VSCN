using GUIs.Helper;
using GUIs.Support;
using Microsoft.AspNetCore.Mvc;
using VSCN.Models.DAO;
using VSCN.Models.EF;
using VSCN.Models.VIEW;

namespace VSCN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private const string PRODUCT_ID = "PRODUCT_ID";

        public IActionResult Index()
        {
            CategoryDAO categoryDAO = new CategoryDAO();
            ViewBag.Pagesize = DataServices.Pagesize();
            ViewBag.ListCategory = categoryDAO.getAll();
            return View();
        }
        [HttpPost]
        public JsonResult Showlist(string name = "",int categoryId=0, int index = 1, int size = 10)
        {
            try
            {
                ProductDAO productDAO = new ProductDAO();
                (int total, List<ProductVIEW> list) = productDAO.Search(name,categoryId, index, size);
                string page = Support.InTrang(total, index, size);
                return Json(new { data = list, page = page });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mess = $"Lỗi: {ex.Message}" });
            }
        }
        public IActionResult Create()
        {
            CategoryDAO categoryDAO = new CategoryDAO();

            ViewBag.ListCategory = categoryDAO.getAll();
            return View();
        }
        [HttpPost]
        public JsonResult Create(string name, string slug, int? parentId, string typeArticle, string content, IFormFile img, bool active)
        {
            try
            {
                ProductDAO productDAO = new ProductDAO();
                if (productDAO.CheckSlug(slug,0))
                {
                    return Json(new { success = false, mess = "Lỗi tên bài viết trùng nhau" });

                }
                // If parentId is 0, set it to null
                if (parentId == 0)
                {
                    parentId = null;
                }

                // Xử lý ảnh tải lên
                string imgPath = null;
                if (img != null && img.Length > 0)
                {
                    // Đường dẫn thư mục để lưu ảnh
                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }

                    // Tạo tên tệp ảnh duy nhất (có thể dùng Guid hoặc tên gốc)
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                    var filePath = Path.Combine(uploads, fileName);

                    // Lưu tệp ảnh
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        img.CopyTo(stream);
                    }

                    // Đường dẫn ảnh mà sẽ lưu vào cơ sở dữ liệu
                    imgPath = "/uploads/" + fileName;
                }

                // Tạo đối tượng Article
                Product article = new Product
                {
                    Name = name,
                    Slug = slug,
                    CategoryId = parentId,
                    TypeArticle = typeArticle.ToUpper(),
                    Content = content,
                    Avatar = imgPath, // Lưu đường dẫn ảnh vào trường Avatar
                    Active = active,                  
                    Trash = false,
                };

                // Thêm bài viết vào cơ sở dữ liệu

                productDAO.InsertOrUpdate(article); // Phương thức để thêm hoặc cập nhật bài viết

                return Json(new { success = true, mess = "Thêm bài viết thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mess = $"Lỗi: {ex.Message}" });
            }
        }
        public IActionResult Edit(string id)
        {
            CategoryDAO categoryDAO = new CategoryDAO();
            ViewBag.Pagesize = DataServices.Pagesize();

            ViewBag.ListCategory = categoryDAO.getAll();
            HttpContext.Session.SetString(PRODUCT_ID, id);

            return View();
        }
        [HttpGet]
        public JsonResult getProduct()
        {
            try
            {
                string id = HttpContext.Session.GetString(PRODUCT_ID) ?? "";

                ProductDAO productDAO = new ProductDAO();
                var query = productDAO.GetItemBySlug(id);
                return Json(new { data = query });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mess = $"Lỗi: {ex.Message}" });
            }

        }
    }
}
