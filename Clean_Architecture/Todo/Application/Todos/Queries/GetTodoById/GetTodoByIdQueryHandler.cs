using Application.Common.Interfaces;
using Domain.Todos;
using MediatR;

namespace Application.Todos.Queries.GetTodos;

public class GetTodoByIdQueryHandler(IAppDbContext dbContext) : IRequestHandler<GetTodoByIdQuery, Todo?>
{
    async Task<Todo?> IRequestHandler<GetTodoByIdQuery, Todo?>.Handle(GetTodoByIdQuery request, CancellationToken ct)
    {
        return await dbContext.Todos.FindAsync([request.Id], ct);
    }
}