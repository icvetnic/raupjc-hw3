using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1;
using Assignment2.Models;
using Assignment2.Models.TodoViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoController(ITodoRepository repository,
            UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            List<TodoItem> userActiveTodoes = await _repository.GetActive(new Guid(user.Id));
            IndexViewModel indexViewModel = new IndexViewModel(userActiveTodoes);
            return View(indexViewModel);
        }
    }
}