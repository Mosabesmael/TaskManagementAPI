//using TaskManagementAPI.Models;
//using TaskManagementAPI.Repositories;

//Console.OutputEncoding = System.Text.Encoding.UTF8;

//// اختبار 1: Integers
//var intRepo = new Repository<int>();
//intRepo.Add(10);
//intRepo.Add(20);
//intRepo.Add(30);
//intRepo.PrintAll();

//// اختبار 2: Strings
//var stringRepo = new Repository<string>();
//stringRepo.Add("Task 1");
//stringRepo.Add("Task 2");
//stringRepo.PrintAll();

//// اختبار 3: Tasks (الأهم)
//var taskRepo = new Repository<TaskItem>();

//var task1 = new TaskItem
//{
//    Id = 1,
//    Title = "Learn Generics",
//    Description = "understand Generic Classes",
//    IsCompleted = true ,
//    CreatedAt = DateTime.Now,
//    DueDate = DateTime.Now.AddDays(2)
//};

//var task2 = new TaskItem
//{
//    Id = 2,
//    Title = "build Repository",
//    Description = "creat Generic Repository",
//    IsCompleted = true,
//    CreatedAt = DateTime.Now,
//    DueDate = DateTime.Now.AddDays(3)
//};

//taskRepo.Add(task1);
//taskRepo.Add(task2);
//taskRepo.PrintAll();

//Console.WriteLine($"\nnumbers of tasks: {taskRepo.Count}");





using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// ========== إنشاء البيانات ==========

var taskRepository = new Repository<TaskItem>();

var task1 = new TaskItem
{
    Id = 1,
    Title = "Learn Generics",
    Description = "Understand Generic Classes",
    IsCompleted = false,
    CreatedAt = DateTime.Now,
    DueDate = DateTime.Now.AddDays(2)
};

var task2 = new TaskItem
{
    Id = 2,
    Title = "Build Repository",
    Description = "Create Generic Repository",
    IsCompleted = false,
    CreatedAt = DateTime.Now,
    DueDate = DateTime.Now.AddDays(3)
};

var task3 = new TaskItem
{
    Id = 3,
    Title = "Learn Generic Methods",
    Description = "Understand Generic Methods",
    IsCompleted = true,
    CreatedAt = DateTime.Now.AddDays(-1),
    DueDate = DateTime.Now.AddDays(-1)
};

var task4 = new TaskItem
{
    Id = 4,
    Title = "Build API",
    Description = "Create REST API",
    IsCompleted = false,
    CreatedAt = DateTime.Now,
    DueDate = DateTime.Now.AddDays(5)
};

taskRepository.Add(task1);
taskRepository.Add(task2);
taskRepository.Add(task3);
taskRepository.Add(task4);

Console.WriteLine("✅ 4 tasks have been added\n");

// ========== الاختبار 1: PrintAll ==========
Console.WriteLine("🔍 Test 1: Show all tasks");
taskRepository.PrintAll();

// ========== الاختبار 2: ConvertAll (Generic Method) ==========
Console.WriteLine("\n📝 Test 2: Convert tasks to Titles only");
var titles = taskRepository.ConvertAll(task => task.Title);
Console.WriteLine("=== Task Titles ===");
titles.ForEach(title => Console.WriteLine($"- {title}"));

// ========== الاختبار 3: FilterByCondition (Generic Method) ==========
Console.WriteLine("\n✓ Test 3: Get only completed tasks");
var completedTasks = taskRepository.FilterByCondition(task => task.IsCompleted);
Console.WriteLine($"Number of completed tasks: {completedTasks.Count}");
completedTasks.ForEach(task => Console.WriteLine($"  {task}"));

// ========== الاختبار 4: FindByCondition (Generic Method) ==========
Console.WriteLine("\n🔎 Test 4: Find a task with a specific ID");
var foundTask = taskRepository.FindByCondition(task => task.Id == 2);
if (foundTask != null)
{
    Console.WriteLine($"We found: {foundTask.Title}");
}

// ========== الاختبار 5: ForEach (Generic Method) ==========
Console.WriteLine("\n📋 Test 5: Print the information for each task");
taskRepository.Foreach(task =>
{
    var daysLeft = task.DaysUntilDue();
    var status = task.IsCompleted ? "✓ Done" : $"○ {daysLeft} days left";
    Console.WriteLine($"  [{task.Id}] {task.Title} → {status}");
});

// ========== الاختبار 6: البحث المتقدم ==========
Console.WriteLine("\n🎯 Test 6: Advanced search for near-deadline tasks");
var urgentTasks = taskRepository.FilterByCondition(task =>
    !task.IsCompleted && task.DaysUntilDue() <= 3 && task.DaysUntilDue() > 0
);
Console.WriteLine($"Number of urgent tasks (≤3 days)): {urgentTasks.Count}");
urgentTasks.ForEach(task =>
{
    Console.WriteLine($"  ⚠️  {task.Title} - {task.DaysUntilDue()} days left");
});

// ========== الاختبار 7: تحويل البيانات ==========
Console.WriteLine("\n🔄 Test 7: Turn tasks into simplified information");
var taskInfo = taskRepository.ConvertAll(task =>
    $"{task.Title} ({(task.IsCompleted ? "Done" : "Pending")})"
);
Console.WriteLine("=== Task Info ===");
taskInfo.ForEach(info => Console.WriteLine($"• {info}"));

// ========== الاختبار 8: إحصائيات ==========
Console.WriteLine("\n📊 Test 8: Statistics");
var totalTasks = taskRepository.Count;
var completedCount = taskRepository.FilterByCondition(t => t.IsCompleted).Count;
var pendingCount = totalTasks - completedCount;
var completionRate = totalTasks > 0 ? (completedCount * 100) / totalTasks : 0;

Console.WriteLine($"Total tasks: {totalTasks}");
Console.WriteLine($"Completed tasks: {completedCount}");
Console.WriteLine($"For pending tasks: {pendingCount}");
Console.WriteLine($"Completion rate: {completionRate}%");

Console.WriteLine("\n✅ We finished day 2!");


