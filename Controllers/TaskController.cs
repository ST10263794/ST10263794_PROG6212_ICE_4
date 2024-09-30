using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace TaskManagementApp.Controllers
{
    public class TaskController : Controller
    {
        // In-memory list to store tasks (simulate a database)
        private static List<TaskModel> _tasks = new List<TaskModel>();
        private static int _nextId = 1;

        // GET: Task
        public IActionResult Index()
        {
            return View(_tasks);
        }

        // GET: Task/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                task.Id = _nextId++;
                _tasks.Add(task);
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: Task/Edit/5
        public IActionResult Edit(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: Task/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TaskModel updatedTask)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedTask);
            }

            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.Deadline = updatedTask.Deadline;

            return RedirectToAction(nameof(Index));
        }

        // GET: Task/Delete/5
        public IActionResult Delete(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            _tasks.Remove(task);
            return RedirectToAction(nameof(Index));
        }
    }
}
