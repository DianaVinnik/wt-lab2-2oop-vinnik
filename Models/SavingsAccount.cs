
using System;

namespace BankSystem.Models
{
    public class SavingsAccount : Account
    {
        public decimal InterestRate { get; set; }

        public SavingsAccount(
            int id,
            string ownerName,
            decimal balance,
            string currency,
            decimal interestRate
        ) : base(id, ownerName, balance, currency)
        {
            if (interestRate < 0)
            {
                throw new ArgumentException(
                    "Процентная ставка не может быть отрицательной."
                );
            }

            InterestRate = interestRate;
            AccountType = "Сберегательный счет";
        }

        // Начисление процентов
        
public void AddInterest()
{
    decimal interest = Balance * InterestRate / 100;

    if (interest <= 0)
    {
        Console.WriteLine("Проценты для начисления отсутствуют.");
        return;
    }

    Deposit(interest);

    Console.WriteLine(
        $"Начислены проценты: {interest:F2} {Currency}"
    );
}


        // Переопределение метода
        public override void PrintInfo()
        {
            base.PrintInfo();

            Console.WriteLine(
                $"Процентная ставка: {InterestRate}%"
            );
        }
    }
}