using JWTAuthentication.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JWTAuthentication.Controllers;

[ApiController]
[Route("api/projects")]
[Authorize] // Apply Authorize at the controller level to ensure all actions require authentication by default
public class ProjectsController : ControllerBase
{

    [HttpGet] // Requires 'project:read' permission
    [Authorize(Policy = Permission.Project.Read)]
    public IActionResult GetProjects()
    {
        return Ok(new
        {
            Message = "Read List of project",
            UserInfo = GetUserInfo(),
            Permission = Permission.Project.Read
        });
    }


    [HttpGet("{id}")]
    [Authorize(Policy = Permission.Project.Read)]
    public IActionResult GetProjectById(string id)
    {
        return Ok(new
        {
            Message = $"Read a single project with id = `{id}`",
            UserInfo = GetUserInfo(),
            Permission = Permission.Project.Read
        });
    }

    [HttpPost]
    [Authorize(Policy = Permission.Project.Create)]
    public IActionResult CreateProject()
    {
        return CreatedAtAction(
            nameof(GetProjectById),
            new { id = Guid.NewGuid() },
            new
            {
                Message = "Project created successfully",
                UserInfo = GetUserInfo(),
                Permission = Permission.Project.Create
            });
    }

    [HttpPut("{id}")]
    [Authorize(Policy = Permission.Project.Update)]
    public IActionResult UpdateProject(string id)
    {
        return Ok(new
        {
            Message = $"Project with Id = '{id}' was updated successfully",
            UserInfo = GetUserInfo(),
            Permission = Permission.Project.Update
        });
    }


    [HttpDelete("{id}")]
    [Authorize(Policy = Permission.Project.Delete)]
    public IActionResult DeleteProject(string id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return Ok(new
        {
            Message = $"Project with Id = '{id}' was Deleted successfully",
            UserInfo = GetUserInfo(),
            Permission = Permission.Project.Delete
        });
    }

    [HttpPost("{id}/members")]
    [Authorize(Policy = Permission.Project.AssignMember)]
    public IActionResult AssignMember(string id)
    {
        return CreatedAtAction(
            nameof(GetProjectById),
            new { id },
            new
            {
                Message = $"A member has been assigned successfully to project '{id}'",
                UserInfo = GetUserInfo(),
                Permission = Permission.Project.AssignMember
            }
        );
    }

    [HttpGet("{id}/budget")]
    [Authorize(Policy = Permission.Project.ManageBudget)]
    public IActionResult GetProjectBudget(string id)
    {
        return Ok(new
        {
            Message = $"Successfully accessed the budget for project '{id}'",
            UserInfo = GetUserInfo(),
            Permission = Permission.Project.ManageBudget
        });
    }

    [HttpGet("tasks/{taskId}")]
    [Authorize(Policy = Permission.Task.Read)]
    public IActionResult GetTask(string taskId)
    {
        return Ok(new
        {
            Message = $"Task '{taskId}' details retrieved",
            UserInfo = GetUserInfo(),
            Permission = Permission.Task.Read
        });
    }

    [HttpPost("tasks")]
    [Authorize(Policy = Permission.Task.Create)]
    public IActionResult CreateTask()
    {
        var taskId = Guid.NewGuid().ToString();

        return Created($"/api/projects/tasks/{taskId}", new
        {
            Message = $"Task '{taskId}' created successfully",
            UserInfo = GetUserInfo(),
            Permission = Permission.Task.Create
        });
    }

    [HttpPost("tasks/{taskId}/assign")]
    [Authorize(Policy = Permission.Task.AssignUser)]
    public IActionResult AssignUserToTask(string taskId)
    {
        return Ok(new
        {
            Message = $"User assigned to task '{taskId}' successfully",
            UserInfo = GetUserInfo(),
            Permission = Permission.Task.AssignUser
        });
    }

    [HttpPut("tasks/{taskId}")]
    [Authorize(Policy = Permission.Task.Update)]
    public IActionResult UpdateTask(string taskId)
    {
        return Ok(new
        {
            Message = $"Task '{taskId}' updated successfully",
            UserInfo = GetUserInfo(),
            Permission = Permission.Task.Update
        });
    }

    [HttpDelete("tasks/{taskId}")]
    [Authorize(Policy = Permission.Task.Delete)]
    public IActionResult DeleteTask(string taskId)
    {
        return Ok(new
        {
            Message = $"Task '{taskId}' deleted successfully",
            UserInfo = GetUserInfo(),
            Permission = Permission.Task.Delete
        });
    }


    [HttpPut("tasks/{taskId}/status")]
    [Authorize(Policy = Permission.Task.Update)]
    public IActionResult UpdateTaskStatus(string taskId)
    {
        return Ok(new
        {
            Message = $"Successfully updated status for task '{taskId}'",
            UserInfo = GetUserInfo(),
            Permission = Permission.Task.Update
        });
    }

    // Helper method to extract user information from the claims principal
    private string GetUserInfo()
    {
        if (User.Identity is { IsAuthenticated: false })
            return "Anonymous"; // Corrected typo: Anonymous -> Anonymous

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var firstName = User.FindFirstValue(ClaimTypes.GivenName);
        var lastName = User.FindFirstValue(ClaimTypes.Surname);

        return $"[{userId}] {firstName} {lastName}";
    }
}