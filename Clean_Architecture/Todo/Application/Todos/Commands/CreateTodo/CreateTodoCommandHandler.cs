using Application.Common.Interfaces;
using Domain.Todos;
using MediatR;

namespace Application.Todos.Commands.CreateTodo;

public class CreateTodoCommandHandler(IAppDbContext dbContext) : IRequestHandler<CreateTodoCommand, Guid>
{
    public async Task<Guid> Handle(CreateTodoCommand request, CancellationToken ct)
    {
        Todo todo = new()
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
        };

        await dbContext.Todos.AddAsync(todo, ct);

        int x = await dbContext.SaveChangesAsync(ct);

        return todo.Id;
    }
}