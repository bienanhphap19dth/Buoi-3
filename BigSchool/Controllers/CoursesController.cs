using BigSchool.Models;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;   //Tương tác trực tiếp Sql
        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Courses
        [Authorize] //Đăng nhập
        public ActionResult Create()        //Biến ban đầu
        {
            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList() //Đẩy dữ liệu Sql qua Controller
            };
            return View(viewModel);
        }
        [Authorize]
        [HttpPost]  //Lấy dữ liệu từ View (CoureController) đẩy qua Form (Begin) đưa qua biến Create mới dùng [HttpPost] => update lại Sql
        public ActionResult Create (CourseViewModel viewModel)      //Create được tạo biến mới
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.ToList();
                return View("Create", viewModel);
            }
            var course = new Course
            {
                LecturerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Place = viewModel.Place
            };
            _dbContext.Courses.Add(course);     // = INSERT INTO ..... (Sql)
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Home"); //Làm xong r chuyển sang View trong Home của Controller
        }    
    }
}