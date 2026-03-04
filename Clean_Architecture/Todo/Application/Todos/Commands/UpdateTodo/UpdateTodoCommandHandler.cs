using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Todos;
using MediatR;

namespace Application.Todos.Commands.UpdateTodo;

public class UpdateTodoCommandHandler(IAppDbContext dbContext) : IRequestHandler<UpdateTodoCommand>
{
    public async Task Handle(UpdateTodoCommand request, CancellationToken ct)
    {
        // Todo? todo = await dbContext.Todos.FirstOrDefaultAsync(x => x.Id == request.Id, ct);
        Todo? todo = await dbContext.Todos.FindAsync([request.Id], ct)  // faster
            ?? throw new NotFoundException(nameof(Todo), request.Id);

        todo.Title = request.Title;
        todo.Completed = request.Completed;

        int x = await dbContext.SaveChangesAsync(ct);
    }
}