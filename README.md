Порядок создания проекта
------------------------
#### 1. Создаем доменную модель (структура):
   ##### Проект: Core
      - Папка:SharedKernel
          - BaseEntity.cs
          - BaseEvent.cs
          - IAggregateRoot.cs
          - IEntity.cs
      - Папка: Utils
          - RegexPatterns.cs
   ##### Проект: Domain
      - Папка: Entities
         - Папка: UserAggregateRoot
            - Папка: Events
               - UserBaseEvent.cs
               - UserCreatedEvent.cs
               - UserDeletedEvent.cs
               - UserUpdatedEvent.cs
            - User.cs
      - Папка: ValueObjects
         Email.cs
