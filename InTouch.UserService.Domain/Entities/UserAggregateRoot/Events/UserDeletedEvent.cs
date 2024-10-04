namespace InTouch.UserService.Domain;

public class UserDeletedEvent (
    Guid id,
    string email,
    string password,
    string name,
    string surname,
    string phone): UserBaseEvent (id, email,password, name, surname, phone);
