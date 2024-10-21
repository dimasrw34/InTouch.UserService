using Ardalis.Result;
using MediatR;

namespace InTouch.Application;

public class DeleteUserCommand(Guid id) :IRequest<Result>
{
    public Guid Id { get; } = id;
}