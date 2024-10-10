namespace InTouch.UserService.Domain;

public class UserCreatedEvent : UserBaseEvent
{
        public UserCreatedEvent(
            Guid id,
            string email,
            string password,
            string name,
            string surname,
            string phone) : base(id, email, password, name, surname, phone)
        {
            
            
        }
};