using BankSystem.Models;
using System;
using System.Text;
using BankSystem.Services;
using BankSystem.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;

namespace BankSystem
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;



Console.WriteLine("=== БЛОК 1. УРОВЕНЬ 2 ===");

// Создаем обычный банковский счет
Account account1 = new Account(
    1, "Иван Иванов", 1500m, "BYN"
);

// Создаем сберегательный счет
SavingsAccount account2 = new SavingsAccount(
    2, "Анна Петрова", 2000m, "BYN", 5m
);

// Создаем кредитный счет
CreditAccount account3 = new CreditAccount(
    3, "Мария Сидорова", 500m, "BYN", 2000m
);

// Демонстрация методов
Console.WriteLine("\nПополнение обычного счета:");
account1.Deposit(500m);

Console.WriteLine("\nНачисление процентов:");
account2.AddInterest();

Console.WriteLine("\nСнятие с кредитного счета:");
account3.Withdraw(1000m);

// Полиморфизм через массив базового типа
Console.WriteLine("\n=== ПОЛИМОРФИЗМ ===");

BaseEntity[] accounts =
{
    account1,
    account2,
    account3
};

foreach (BaseEntity account in accounts)
{
    account.PrintInfo();
}

Console.WriteLine();
Console.WriteLine("=== БЛОК 1. УРОВЕНЬ 3 ===");

// Создаем сервис через интерфейсную ссылку
IItemService<Account> bankService = new AccountService();

// Создаем банковские счета
Account bankAccount1 = new Account(
    10, "Александр Иванов", 1200m, "BYN"
);

SavingsAccount bankAccount2 = new SavingsAccount(
    11, "Елена Петрова", 5000m, "BYN", 5m
);

CreditAccount bankAccount3 = new CreditAccount(
    12, "Дмитрий Сидоров", 800m, "BYN", 3000m
);

// Добавляем счета через интерфейс
bankService.Add(bankAccount1);
bankService.Add(bankAccount2);
bankService.Add(bankAccount3);

// Получаем все счета через интерфейс
Console.WriteLine("\nСписок банковских счетов:");

foreach (Account account in bankService.GetAll())
{
    account.PrintInfo();
}

// Поиск через интерфейс
Console.WriteLine("\nПоиск счета с ID = 11:");

Account? foundAccount = bankService.GetById(11);

if (foundAccount != null)
{
    foundAccount.PrintInfo();
}
else
{
    Console.WriteLine("Счет не найден.");
}

// Удаление через интерфейс
Console.WriteLine("\nУдаление счета с ID = 10:");

bool removed = bankService.Remove(10);

Console.WriteLine(
    removed ? "Счет удален." : "Счет не найден."
);

// Демонстрация перегрузки
Console.WriteLine("\n=== ПЕРЕГРУЗКА МЕТОДОВ ===");

// Для вызова дополнительных методов используем
// ссылку на конкретный класс AccountService
AccountService service = new AccountService();

service.Add(
    new Account(20, "Ольга Иванова", 1000m, "BYN")
);

service.Add(
    21, "Максим Петров", 2500m, "BYN"
);

Console.WriteLine("\nПоиск по ID:");

Account? byId = service.GetById(20);
byId?.PrintInfo();

Console.WriteLine("\nПоиск по имени:");

Account? byName = service.GetById("Максим Петров");
byName?.PrintInfo();


Console.WriteLine("\n=== БЛОК 2. УРОВЕНЬ 1 ===");

// Создаем список банковских счетов
List<Account> accountList = new List<Account>
{
    new Account(101, "Иван Иванов", 1500m, "BYN"),
    new SavingsAccount(102, "Анна Петрова", 3200m, "BYN", 5m),
    new CreditAccount(103, "Мария Сидорова", 500m, "BYN", 2000m),
    new Account(104, "Павел Смирнов", 800m, "BYN"),
    new SavingsAccount(105, "Елена Козлова", 7500m, "BYN", 4m),
    new CreditAccount(106, "Дмитрий Орлов", 1200m, "BYN", 3000m),
    new Account(107, "Ольга Васильева", 4500m, "BYN")
};

// Вывод количества счетов
Console.WriteLine($"Количество счетов: {accountList.Count}");

// Перебор коллекции через foreach
foreach (Account account in accountList)
{
    account.PrintInfo();
}


Console.WriteLine("\n=== БЛОК 2. УРОВЕНЬ 2 ===");

// Создаем словарь банковских счетов
Dictionary<int, Account> accountDictionary =
    new Dictionary<int, Account>();

// Заполняем словарь счетами из списка
foreach (Account account in accountList)
{
    accountDictionary.Add(account.Id, account);
}

Console.WriteLine(
    $"Количество счетов в словаре: {accountDictionary.Count}"
);

// Поиск банковского счета по ID
Console.WriteLine("\nПоиск счета с ID = 103:");

if (accountDictionary.TryGetValue(103, out Account? dictionaryFoundAccount))
{
    Console.WriteLine("Счет найден:");
    dictionaryFoundAccount.PrintInfo();
}
else
{
    Console.WriteLine("Счет не найден.");
}

// Поиск несуществующего счета
Console.WriteLine("\nПоиск счета с ID = 999:");

if (accountDictionary.TryGetValue(999, out Account? missingAccount))
{
    missingAccount.PrintInfo();
}
else
{
    Console.WriteLine("Счет с таким ID не найден.");
}


Console.WriteLine("\n=== БЛОК 2. УРОВЕНЬ 3 ===");

// 1. Where — фильтрация счетов
Console.WriteLine("\nСчета с балансом больше 2000 BYN:");

var richAccounts = accountList
    .Where(a => a.Balance > 2000m);

foreach (Account account in richAccounts)
{
    account.PrintInfo();
}

// 2. OrderByDescending — сортировка
Console.WriteLine("\nСчета по убыванию баланса:");

var sortedAccounts = accountList
    .OrderByDescending(a => a.Balance);

foreach (Account account in sortedAccounts)
{
    Console.WriteLine(
        $"{account.OwnerName}: {account.Balance} {account.Currency}"
    );
}

// 3. Select — выбор отдельных данных
Console.WriteLine("\nИмена владельцев счетов:");

var ownerNames = accountList
    .Select(a => a.OwnerName);

foreach (string owner in ownerNames)
{
    Console.WriteLine(owner);
}

// 4. Count — количество счетов
int totalAccounts = accountList.Count();

Console.WriteLine(
    $"\nОбщее количество счетов: {totalAccounts}"
);

// 5. Sum — сумма балансов
decimal totalBalance = accountList
    .Sum(a => a.Balance);

Console.WriteLine(
    $"Сумма балансов: {totalBalance} BYN"
);

// 6. Average — средний баланс
decimal averageBalance = accountList
    .Average(a => a.Balance);

Console.WriteLine(
    $"Средний баланс: {averageBalance:F2} BYN"
);

// 7. Min — минимальный баланс
decimal minBalance = accountList
    .Min(a => a.Balance);

Console.WriteLine(
    $"Минимальный баланс: {minBalance} BYN"
);

// 8. Max — максимальный баланс
decimal maxBalance = accountList
    .Max(a => a.Balance);

Console.WriteLine(
    $"Максимальный баланс: {maxBalance} BYN"
);

// 9. GroupBy — группировка по типам счетов
Console.WriteLine("\nГруппировка счетов по типам:");

var groupedAccounts = accountList
    .GroupBy(a => a.GetType().Name);

foreach (var group in groupedAccounts)
{
    Console.WriteLine(
        $"\nТип счета: {group.Key}"
    );

    Console.WriteLine(
        $"Количество: {group.Count()}"
    );

    foreach (Account account in group)
    {
        Console.WriteLine(
            $"  {account.OwnerName} — {account.Balance} BYN"
        );
    }
}

Console.WriteLine("\n=== БЛОК 3. УРОВЕНЬ 1 ===");

await LoadBankDataAsync();

Console.WriteLine("\n=== БЛОК 3. УРОВЕНЬ 2 ===");

try
{
    List<Account> loadedAccounts = await GetAccountsAsync();

    Console.WriteLine("Список счетов успешно загружен!");

    foreach (Account account in loadedAccounts)
    {
        account.PrintInfo();
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка загрузки: {ex.Message}");
}
finally
{
    Console.WriteLine("Операция загрузки счетов завершена.");
}


Console.WriteLine("\n=== БЛОК 3. УРОВЕНЬ 3 ===");

// Одновременный запуск нескольких задач
Console.WriteLine("\nДемонстрация Task.WhenAll:");

Task task1 = CheckAccountAsync(301, CancellationToken.None);
Task task2 = CheckAccountAsync(302, CancellationToken.None);
Task task3 = CheckAccountAsync(303, CancellationToken.None);

await Task.WhenAll(task1, task2, task3);

Console.WriteLine("Все проверки банковских счетов завершены!");

// Демонстрация отмены операции
Console.WriteLine("\nДемонстрация CancellationToken:");

using (CancellationTokenSource cts = new CancellationTokenSource())
{
    cts.CancelAfter(2500);

    try
    {
        await LongBankOperationAsync(cts.Token);
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("Банковская операция была отменена!");
    }
}

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine();
                Console.WriteLine("====================================");
                Console.WriteLine("         БАНКОВСКАЯ СИСТЕМА");
                Console.WriteLine("       Лабораторная работа № 2");
                Console.WriteLine("====================================");

                Console.WriteLine("1. Просмотреть все счета");
                Console.WriteLine("2. Создать банковский счет");
                Console.WriteLine("3. Пополнить счет");
                Console.WriteLine("4. Снять деньги");
                Console.WriteLine("5. Найти счет по ID");
                Console.WriteLine("6. Статистика счетов (LINQ)");
                Console.WriteLine("7. Асинхронные операции");
                Console.WriteLine("0. Выход");

                Console.Write("\nВыберите пункт меню: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Просмотр счетов");
                        break;

                    case "2":
                        Console.WriteLine("Создание счета");
                        break;

                    case "3":
                        Console.WriteLine("Пополнение счета");
                        break;

                    case "4":
                        Console.WriteLine("Снятие денег");
                        break;

                    case "5":
                        Console.WriteLine("Поиск счета");
                        break;

                    case "6":
                        Console.WriteLine("Статистика счетов");
                        break;

                    case "7":
                        Console.WriteLine("Асинхронные операции");
                        break;

                    case "0":
                        Console.WriteLine("Завершение работы...");
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Неверный пункт меню!");
                        break;
                }
            }
        }
        static async Task LoadBankDataAsync()
{
    Console.WriteLine("Начало загрузки банковских данных...");

    await Task.Delay(1000);

    Console.WriteLine("Банковские данные успешно загружены!");
}
static async Task<List<Account>> GetAccountsAsync()
        {
            Console.WriteLine("Начинается загрузка списка счетов...");

            await Task.Delay(1000);

            List<Account> accounts = new List<Account>
            {
                new Account(201, "Алексей Иванов", 2500m, "BYN"),
                new SavingsAccount(202, "Мария Петрова", 4000m, "BYN", 5m),
                new CreditAccount(203, "Олег Сидоров", 1000m, "BYN", 2000m)
            };

            return accounts;
        }


static async Task CheckAccountAsync(int accountId, CancellationToken token)
{
    Console.WriteLine($"Началась проверка счета №{accountId}");

    await Task.Delay(1000, token);

    Console.WriteLine($"Проверка счета №{accountId} завершена");
}

static async Task LongBankOperationAsync(CancellationToken token)
{
    for (int i = 1; i <= 5; i++)
    {
        token.ThrowIfCancellationRequested();

        Console.WriteLine($"Этап банковской операции: {i}");

        await Task.Delay(1000, token);
    }

    Console.WriteLine("Банковская операция завершена.");
}

    }
}
