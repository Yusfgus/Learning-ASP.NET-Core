using Domain.Todos;
using MediatR;

namespace Application.Todos.Queries.GetTodos;

public sealed record GetTodoByIdQuery(Guid Id) : IRequest<Todo?>;