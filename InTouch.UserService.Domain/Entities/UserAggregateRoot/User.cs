using InTouch.UserService.Core;

namespace InTouch.UserService.Domain;

public class User : BaseEntity, IAggregateRoot
{
    private bool _isDeleted;

    /// <summary>
    /// 
    /// </summary>
    public Email Email { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    public string Password { get; }
    /// <summary>
    /// 
    /// </summary>
    public string Surname { get; }
    /// <summary>
    /// 
    /// </summary>
    public string Phone { get; }
}