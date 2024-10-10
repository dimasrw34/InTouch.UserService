using Ardalis.Result;

namespace InTouch.UserService.Domain;

public static class UserFactory
{
    /// <summary>
    /// Используется для тестирования
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <param name="name"></param>
    /// <param name="surname"></param>
    /// <param name="phone"></param>
    /// <returns></returns>
    public static Result<User> Create(
        string email, 
        string password, 
        string name, 
        string surname,
        string phone)
    {
        var emailResult = Email.Create(email);
        return !emailResult.IsSuccess
            ? Result<User>.Error(new ErrorList(emailResult.Errors.ToArray()))
            : Result<User>.Success(new User(emailResult.Value, password, name, surname, phone));
    }

    public static User Create(Email email, string password, string name, string surname, string phone) 
        => new(email, password, name, surname, phone);
    
}