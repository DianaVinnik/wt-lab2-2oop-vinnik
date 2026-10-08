
using System.Collections.Generic;

namespace BankSystem.Interfaces
{
    public interface IItemService<T>
    {
        // Добавление объекта
        void Add(T item);

        // Получение объекта по ID
        T? GetById(int id);

        // Получение всех объектов
        List<T> GetAll();

        // Удаление объекта по ID
        bool Remove(int id);
    }
}