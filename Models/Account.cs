
using System;

namespace BankSystem.Models
{
    public class Account : BaseEntity
    {
        // Закрытое поле
        private decimal _balance;

        // Свойства
        public string OwnerName { get; set; }

        public string Currency { get; set; }

        public decimal Balance
        {
            get { return _balance; }
            private set { _balance = value; }
        }

        // Защищенное свойство для наследников
        protected string AccountType { get; set; }

        // Конструктор
        public Account(
            int id,
            string ownerName,
            decimal balance,
            string currency
        ) : base(id)
        {
            if (string.IsNullOrWhiteSpace(ownerName))
            {
                throw new ArgumentException(
                    "Имя владельца не может быть пустым."
                );
            }

            if (balance < 0)
            {
                throw new ArgumentException(
                    "Начальный баланс не может быть отрицательным."
                );
            }

            OwnerName = ownerName;
            Balance = balance;
            Currency = currency;
            AccountType = "Обычный счет";
        }

        // Пополнение счета
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine(
                    "Ошибка: сумма пополнения должна быть больше нуля."
                );
                return;
            }

            Balance += amount;

            Console.WriteLine(
                $"Счет №{Id} пополнен на {amount:F2} {Currency}"
            );
        }

        // Снятие денег
        public virtual void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine(
                    "Ошибка: сумма снятия должна быть больше нуля."
                );
                return;
            }

            if (amount > Balance)
            {
                Console.WriteLine(
                    "Ошибка: недостаточно средств на счете."
                );
                return;
            }

            Balance -= amount;

            Console.WriteLine(
                $"Со счета №{Id} снято {amount:F2} {Currency}"
            );
        }

        // Защищенный метод изменения баланса
        protected void ChangeBalance(decimal amount)
        {
            Balance += amount;
        }

        // Вывод информации
        public override void PrintInfo()
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine($"Тип счета: {AccountType}");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Владелец: {OwnerName}");
            Console.WriteLine($"Баланс: {Balance:F2} {Currency}");
            Console.WriteLine($"Создан: {CreatedAt:dd.MM.yyyy}");
            Console.WriteLine("----------------------------");
        }
    }
}
