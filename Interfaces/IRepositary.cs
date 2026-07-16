using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagementAPI.Interfaces
{
    public interface IRepository <T> where T : class
    {
        void Add (T item);

        T ? GetByIndex (int index);

        List<T> GetAll ();

        int Count { get; }

        List<TResult> ConvertAll <TResult> (Func<T , TResult> converter);

        T ? FindByCondition (Func<T ,bool> condition );

        List<T>  FilterByCondition (Func<T ,bool> condition );

        void Foreach (Action<T>  action);

        bool Remove (T item);
    }
}
