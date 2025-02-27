using GUIs.Helper;
using GUIs.Support;
using Microsoft.AspNetCore.Mvc;
using VSCN.Models.DAO;
using VSCN.Models.EF;
using VSCN.Models.VIEW;

namespace VSCN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController :Controller
    {
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
        public JsonResult Showlist(string name = "", int index = 1, int size = 10)
        {
            try
            {
                CategoryDAO categoryDAO = new CategoryDAO();
                (int total, List<CategoryVIEW> list) = categoryDAO.Search(name, index, size);
                string page = Support.InTrang(total, index, size);
                return Json(new { data = list, page = page });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mess = $"Lỗi: {ex.Message}" });
            }
        }
        [HttpPost]
        public JsonResult Create(string name, string slug, int? parentId, string img, string description, bool active)
        {
            try
            {
                CategoryDAO categoryDAO = new CategoryDAO();
                if (categoryDAO.CheckSlug(slug, 0))
                {
                    return Json(new { success = false, mess = "Lỗi tên danh mục  trùng nhau" });

                }
                // If parentId is 0, set it to null
                if (parentId == 0)
                {
                    parentId = null;
                }

                Category item = new Category
                {
                    Name = name,
                    Slug = slug,
                    ParentId = parentId,
                    Avatar = img,
                    Summary = description,
                    Active = active,                
                    Trash = false,
                };

                categoryDAO.InsertOrUpdate(item);

                return Json(new { success = true, mess = "Thêm danh mục thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mess = $"Lỗi: {ex.Message}" });
            }
        }

        [HttpPost]
        public JsonResult Update(int id, string name, string slug, int? parentId, string img, string description, bool active, bool isShow)
        {
            try
            {
                CategoryDAO categoryDAO = new CategoryDAO();
                if (categoryDAO.CheckSlug(slug, id))
                {
                    return Json(new { success = false, mess = "Lỗi tên danh mục  trùng nhau" });

                }
                if (id == parentId)
                {
                    return Json(new { success = false, mess = "Sửa danh mục thất bại." });
                }
                if (parentId == 0)
                {
                    parentId = null;
                }


                Category item = categoryDAO.GetItem(id);
                item.Name = name;
                item.Slug = slug;
                item.ParentId = parentId;
                item.Avatar = img;
                item.Summary = description;
                item.Active = active;
               

                categoryDAO.InsertOrUpdate(item);

                return Json(new { success = true, mess = "Sửa danh mục thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mess = $"Lỗi: {ex.Message}" });
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                CategoryDAO categoryDAO = new CategoryDAO();
                categoryDAO.Delete(id);
                return Json(new { mess = "Xóa thành công" });
            }

            catch (Exception ex)
            {
                return Json(new { success = false, mess = $"Lỗi: {ex.Message}" });
            }
        }
        [HttpGet]
        public JsonResult getDanhmuc(int id)
        {
            try
            {
                CategoryDAO categoryDAO = new CategoryDAO();
                var query = categoryDAO.GetItemView(id);
                return Json(new { data = query });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mess = $"Lỗi: {ex.Message}" });
            }
        }
        //[HttpPost]
        //public JsonResult OnChange(int id, int orderBy)
        //{
        //    try
        //    {
        //        CategoryDAO categoryDAO = new CategoryDAO();
        //        Category item = categoryDAO.GetItem(id);
        //        if (orderBy == 0)
        //        {
        //            item.OrderBy = null;
        //            categoryDAO.InsertOrUpdate(item);
        //        }
        //        if (orderBy > 0)
        //        {
        //            item.OrderBy = orderBy;
        //            categoryDAO.InsertOrUpdate(item);
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
