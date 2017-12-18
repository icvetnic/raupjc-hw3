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
using Serilog;

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

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTodoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                TodoItem todoItem = new TodoItem(model.Text, new Guid(user.Id));
                todoItem.DateDue = model.DateDue;
                String[] labels = model.separateLabels();
                if (labels != null)
                {
                    foreach (string label in labels)
                    {
                        TodoItemLabel todoItemLabel = _repository.AddLabel(label);
                        todoItem.Labels.Add(todoItemLabel);
                    }
                }
                try
                {
                    _repository.Add(todoItem);
                } catch (DuplicateTodoItemException ex)
                {
                    Log.Information(ex.Message);
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> MarkAsCompleted(Guid Id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            await _repository.MarkAsCompleted(Id, new Guid(user.Id));             
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Completed()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            List<TodoItem> userCompletedTodoes = await _repository.GetCompleted(new Guid(user.Id));
            CompletedViewModel indexViewModel = new CompletedViewModel(userCompletedTodoes);
            return View(indexViewModel);
        }

        public async Task<IActionResult> Remove(Guid Id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            bool isRemoved = await _repository.Remove(Id, new Guid(user.Id));
            if (!isRemoved)
            {
                Log.Information(
                    $"TodoItem with id {Id} is not removed from database.");
            }
            return RedirectToAction("Completed");
        }
    }
}