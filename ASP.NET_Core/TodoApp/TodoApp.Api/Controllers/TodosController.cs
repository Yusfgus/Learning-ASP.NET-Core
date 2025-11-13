using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Models;

namespace TodoApp.Api.Controllers;

[Route("api/todo")]
[ApiController]
public class TodosController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        List<Todo> todos =
        [
            new Todo { Id = 1, Description = "Finish EF Core practice project", CreatedAt = DateTime.Now.AddDays(-2), Completed = false },
            new Todo { Id = 2, Description = "Read about Clean Architecture", CreatedAt = DateTime.Now.AddDays(-1), Completed = true },
            new Todo { Id = 3, Description = "Implement repository pattern", CreatedAt = DateTime.Now, Completed = false },
            new Todo { Id = 4, Description = "Review ChangeTracker behavior", CreatedAt = DateTime.Now.AddHours(-3), Completed = true },
            new Todo { Id = 5, Description = "Push code to GitHub", CreatedAt = DateTime.Now.AddMinutes(-30), Completed = false }
        ];

        return Ok(todos);
    }
}