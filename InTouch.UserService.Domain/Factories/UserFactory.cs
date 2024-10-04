using Ardalis.Result;

namespace InTouch.UserService.Domain;

public static class UserFactory
{
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