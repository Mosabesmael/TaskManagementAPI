using System;
using System.Collections.Generic;
using System.Text;
using TaskManagementAPI.Interfaces;

namespace TaskManagementAPI.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private List<T> _items = new();

    public void Add(T item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item), "Item cannot be null");
        _items.Add(item);
    }

    public T GetByIndex(int index)
    {
        return index >= 0 && index < _items.Count ? _items[index] : default;
    }

    public List<T> GetAll()
    {
        return new List<T>(_items);
    }

    public int Count => _items.Count;

    public bool Remove(T item)
    {
        return _items.Remove(item);
    }

    public void PrintAll()
    {
        Console.WriteLine($"\n=== {typeof(T).Name}s ({_items.Count}) ===");
        for (int i = 0; i < _items.Count; i++)
        {
            Console.WriteLine($"{i}. {_items[i]}");
        }
    }
    public List<TResult> ConvertAll<TResult>(Func<T, TResult> converter)
    {
        var result = new List<TResult>();
        foreach (var item in _items)
        {
            result.Add(converter(item));
        }
        return result;
    }

    public T? FindByCondition(Func<T, bool> condition)
    {
        foreach (var item in _items)
        {
            if (condition(item)) return item;
        }
        return default;
    }
    /// احصل على كل العناصر اللي تطابق شرط معين
    public List<T> FilterByCondition(Func<T, bool> condition)
    {
        var result = new List<T>();
        foreach (var item in _items)
        {
            if (condition(item)) result.Add(item);
        }
        return result;
    }

    /// طبّق عملية معينة على كل عنصر
    public void Foreach (Action<T> action)
    {
        foreach(var item in _items)action(item);
    }

    /// Generic Method للبحث عن نوع معين
    public TResult? Find<TResult>(Func<T, TResult?> selector, Func<TResult, bool> condition)
     where TResult : class
    {
        foreach (var item in _items)
        {
            var result = selector(item);
            if (result != null && condition(result))
                return result;
        }

        return null;
    }
}