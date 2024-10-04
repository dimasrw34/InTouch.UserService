# Порядок создания проекта

## 1. Создаем доменную и событийную модель:
### Проект: Core
    - Папка:SharedKernel
        - BaseEntity.cs
        - BaseEvent.cs
        - IAggregateRoot.cs
        - IEntity.cs
    - Папка: Utils
        - RegexPatterns.cs
### Проект: Domain
      - Папка: Entities
         - Папка: UserAggregateRoot
            - Папка: Events
                - UserBaseEvent.cs
                - UserCreatedEvent.cs
                - UserDeletedEvent.cs
                - UserUpdatedEvent.cs
            - User.cs
        - Папка: ValueObjects
            - Email.cs
## 2. Реализуем CQRS :
### 2.1 Создаем CreateCommand
#### Проект: Core
    - Папка: SharedKerne
        - IResponse.cs
#### Проект: Application
    - Папка: User
        - Папка: Commands
            - CreateUserCommand.cs
        - Папка: Response
            - CreatedUserResponse.cs
### 2.2 Создаем CreateCommandHandler
#### Проект: Application
    - Папка: User
        - Папка: Handlers
            - CreateCommandHandler.cs
#### Проект: Core
    - Папка:SharedKernel
        - IWriteOnlyRepository.cs
        - IUnitOfWork
#### Проект: Domain
      - Папка: Entities
        - Папка: UserAggregateRoot
            - IUserWriteOnlyRepository.cs
        - Папка: Entities
             - UserFactory