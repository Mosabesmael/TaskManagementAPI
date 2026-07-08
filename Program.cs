using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// اختبار 1: Integers
var intRepo = new Repository<int>();
intRepo.Add(10);
intRepo.Add(20);
intRepo.Add(30);
intRepo.PrintAll();

// اختبار 2: Strings
var stringRepo = new Repository<string>();
stringRepo.Add("Task 1");
stringRepo.Add("Task 2");
stringRepo.PrintAll();

// اختبار 3: Tasks (الأهم)
var taskRepo = new Repository<TaskItem>();

var task1 = new TaskItem
{
    Id = 1,
    Title = "Learn Generics",
    Description = "understand Generic Classes",
    IsCompleted = true ,
    CreatedAt = DateTime.Now,
    DueDate = DateTime.Now.AddDays(2)
};

var task2 = new TaskItem
{
    Id = 2,
    Title = "build Repository",
    Description = "creat Generic Repository",
    IsCompleted = true,
    CreatedAt = DateTime.Now,
    DueDate = DateTime.Now.AddDays(3)
};

taskRepo.Add(task1);
taskRepo.Add(task2);
taskRepo.PrintAll();

Console.WriteLine($"\nnumbers of tasks: {taskRepo.Count}");