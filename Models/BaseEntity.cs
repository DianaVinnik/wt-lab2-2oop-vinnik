
using System;

namespace BankSystem.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; private set; }

        public DateTime CreatedAt { get; private set; }

        protected BaseEntity(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "ID должен быть положительным числом."
                );
            }

            Id = id;
            CreatedAt = DateTime.Now;
        }

        public abstract void PrintInfo();
    }
}
