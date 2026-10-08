
using System;

namespace BankSystem.Models
{
    public class CreditAccount : Account
    {
        public decimal CreditLimit { get; private set; }

        public CreditAccount(
            int id,
            string ownerName,
            decimal balance,
            string currency,
            decimal creditLimit
        ) : base(id, ownerName, balance, currency)
        {
            if (creditLimit < 0)
            {
                throw new ArgumentException(
                    "Кредитный лимит не может быть отрицательным."
                );
            }

            CreditLimit = creditLimit;
            AccountType = "Кредитный счет";
        }

        // Переопределение снятия денег
        public override void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Некорректная сумма.");
                return;
            }

            if (amount > Balance + CreditLimit)
            {
                Console.WriteLine(
                    "Превышен кредитный лимит."
                );
                return;
            }

            ChangeBalance(-amount);

            Console.WriteLine(
                $"С кредитного счета снято {amount:F2} {Currency}"
            );
        }

        // Переопределение вывода
        public override void PrintInfo()
        {
            base.PrintInfo();

            Console.WriteLine(
                $"Кредитный лимит: {CreditLimit:F2} {Currency}"
            );
        }
    }
}
