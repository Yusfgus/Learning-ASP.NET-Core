using Application.Common.Interfaces;
using Domain.Todos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Todos.Queries.GetTodos;

public class GetTodosQueryHandler(IAppDbContext dbContext) : IRequestHandler<GetTodosQuery, List<Todo>>
{
    async Task<List<Todo>> IRequestHandler<GetTodosQuery, List<Todo>>.Handle(GetTodosQuery request, CancellationToken ct)
    {
        return await dbContext.Todos.ToListAsync(ct);
    }
}