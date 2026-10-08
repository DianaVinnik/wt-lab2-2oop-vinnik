
using System;
using System.Collections.Generic;
using BankSystem.Interfaces;
using BankSystem.Models;

namespace BankSystem.Services
{
    public class AccountService : IItemService<Account>
    {
        // Закрытая коллекция банковских счетов
        private readonly List<Account> _accounts = new();

        // Добавление счета
        public void Add(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (GetById(account.Id) != null)
            {
                Console.WriteLine(
                    $"Счет с ID {account.Id} уже существует."
                );
                return;
            }

            _accounts.Add(account);

            Console.WriteLine(
                $"Счет №{account.Id} успешно добавлен."
            );
        }

        // Поиск счета по ID
        public Account? GetById(int id)
        {
            foreach (Account account in _accounts)
            {
                if (account.Id == id)
                {
                    return account;
                }
            }

            return null;
        }

        // Получение всех счетов
        public List<Account> GetAll()
        {
            return new List<Account>(_accounts);
        }

        // Удаление счета
        public bool Remove(int id)
        {
            Account? account = GetById(id);

            if (account == null)
            {
                return false;
            }

            return _accounts.Remove(account);
        }

        // Перегрузка метода Add
        public void Add(
            int id,
            string ownerName,
            decimal balance,
            string currency
        )
        {
            Account account = new Account(
                id, ownerName, balance, currency
            );

            Add(account);
        }

        // Перегрузка метода GetById
        public Account? GetById(string ownerName)
        {
            foreach (Account account in _accounts)
            {
                if (account.OwnerName.Equals(
                    ownerName,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return account;
                }
            }

            return null;
        }
    }
}
