using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using InTouch.Infrastructure.Data;
using InTouch.UserService.Core;
using MediatR;

using InTouch.UserService.Domain;



namespace InTouch.Application;

public class CreateUserCommandHandler(
    IValidator<CreateUserCommand> validator,
    UnitOfWork unitOfWork,
    IDbContext dbContext
    ) : IRequestHandler<CreateUserCommand, Result<CreatedUserResponse>>
{
    
    public async Task<Result<CreatedUserResponse>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        //Валидация request.
        var _validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!_validationResult.IsValid)
        {
            //возвращаем result с ошибкой валидации.
            return Result<CreatedUserResponse>.Invalid(_validationResult.AsErrors());
        }
        
        // Создаем email value object.
        var email = Email.Create(request.Email).Value;
        
        // Проверяем, что пользователь с такой почтой создан. 
        /*if (await repository.ExistsByEmailAsync(email))
        {
            return Result<CreatedUserResponse>.Error("Указанный адрес электронной почты уже существует.");
        }*/
        
        // Создание экземпляра сущности пользователя.
        // При создании экземпляра будет создано событие «UsrCreatedEvente».
        var _user = UserFactory.Create(
            email,
            request.Password,
            request.Name,
            request.Surname,
            request.Phone);
        
       var eventStore = new EventStore(
           _user.Id,
           "CreateUserEntity",
           _user.ToJson());

        // Сохранение изменений в БД и срабатывание событий.
              await unitOfWork.SaveChanges(_user, eventStore,cancellationToken);

        // Возвращаем ИД нового пользователя и сообщение об успехе.
        return Result<CreatedUserResponse>.Success(
            new CreatedUserResponse(_user.Id), "Пользователь успешно зарегистрирован!");
    }
}