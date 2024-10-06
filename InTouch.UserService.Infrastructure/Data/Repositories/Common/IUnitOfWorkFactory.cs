namespace InTouch.Infrastructure;

public interface IUnitOfWorkFactory
{
    IUnitOfWork CreateUnitOfWork();
}