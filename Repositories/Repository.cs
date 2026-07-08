using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagementAPI.Repositories;

public class Repository<T>
{
    private List<T> _items = new();

    public void Add(T item)
    {
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

    public void PrintAll()
    {
        Console.WriteLine($"\n=== {typeof(T).Name}s ({_items.Count}) ===");
        for (int i = 0; i < _items.Count; i++)
        {
            Console.WriteLine($"{i}. {_items[i]}");
        }
    }
}