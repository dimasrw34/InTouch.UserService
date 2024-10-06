using InTouch.UserService.Core;

namespace InTouch.UserService.Domain;

public interface IUserWriteOnlyRepository :IWriteOnlycRepository<User,Guid>
{
    /// <summary>
    /// Проверяет, существует ли пользователь с указанным адресом электронной почты.
    /// </summary>
    /// <param name="email">Адрес почты для проверки</param>
    /// <returns>Истина, если существует пользователь с такой почтой, иначе - ложь</returns>
    Task<bool> ExistsByEmailAsync(Email email);

    /// <summary>
    /// Проверяет, существует ли пользователь с указанным адресом электронной почты
    /// и текущим идентификатором.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="currentID"></param>
    /// <returns></returns>
    Task<bool> ExistsByEmailAsync(Email email, Guid currentID);
}