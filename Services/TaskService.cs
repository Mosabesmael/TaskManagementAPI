using System;
using System.Collections.Generic;
using System.Text;
using TaskManagementAPI.Interfaces;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services

{
    public class TaskService
    {
        private readonly IRepository<TaskItem> _repository;

        public TaskService(IRepository<TaskItem> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public List<TaskItem> GetCompletedTasks()
        {
            return _repository.FilterByCondition(task => task.IsCompleted);
        }

        public List<TaskItem> GetPendingTasks()
        {
            return _repository.FilterByCondition(task => !task.IsCompleted);
        }

        public List <TaskItem> GetUpcomingTasks(int daysUntelDue)
        {
            return _repository.FilterByCondition(task =>
            !task.IsCompleted &&
            task.DueDate.HasValue &&
            task.DueDate.Value.Date > DateTime.Now.Date &&
            task.DueDate.Value.Date <= DateTime.Now.AddDays(daysUntelDue).Date );
        }
        public List <TaskItem> GetOverDueTasks()
        {
            return _repository.FilterByCondition(task => task.IsOverdue());
        } 

        public List <TaskItem> SearchByTitel (string Keyword)
        {
            return _repository.FilterByCondition(task => 
            task.Title.Contains(Keyword, StringComparison.OrdinalIgnoreCase));
        }

        public void CreateTask(string title, string description, DateTime? dueDate)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be empty", nameof(title));
            }

            var NewTask = new TaskItem
            {
                Id = _repository.Count + 1,
                Title = title ,
                Description = description ?? string.Empty ,
                IsCompleted =false ,
                CreatedAt = DateTime.Now ,
                DueDate = dueDate  
            };

            _repository.Add(NewTask);
        }

        public bool CompleteTask (int taskID)
        {
            var task = _repository.FindByCondition(t => t.Id == taskID);
            if (task != null)
            {
                task.IsCompleted = true;
                return true;
            }
             return false;
        }

        public class TaskStatistics()
        {
            public int TotalTasks { get; set; }
            public int CompletedTasks { get; set; }
            public int PendingTasks { get; set; }
            public int OverDueTasks { get; set; }
            public int CompletionRate { get; set; }
            public override string ToString()
            {
                return $@"
=== Task Statistics ===
Total Tasks:      {TotalTasks}
Completed:        {CompletedTasks}
Pending:          {PendingTasks}
Overdue:          {OverDueTasks}
Completion Rate:  {CompletionRate}%";
            }

        }

        public TaskStatistics GetStatistics()
        {
            var AllTasks = _repository.GetAll();
            //var Completed = _repository.FilterByCondition(t => t.IsCompleted);
            var Completed = GetCompletedTasks();
            //var Overdue = _repository.FilterByCondition(t => t.IsOverdue());
            var Overdue = GetOverDueTasks();
            //var Pinding = AllTasks.Count - Completed.Count;
            var Pinding = GetPendingTasks();
            return new TaskStatistics
            {
                TotalTasks = AllTasks.Count,
                CompletedTasks = Completed.Count,
                OverDueTasks = Overdue.Count,
                PendingTasks = Pinding.Count,
                CompletionRate = AllTasks.Count > 0 ? (Completed.Count * 100) / AllTasks.Count : 0
            };
        }

        public List<TaskItem> GetAllTasks()
        {
            return _repository.GetAll();
        }



    }
}
