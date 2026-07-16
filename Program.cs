using TaskManagementAPI.Interfaces;
using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories;
using TaskManagementAPI.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// ========== Initialize Repository and Service ==========
IRepository<TaskItem> repository = new Repository<TaskItem>();
var taskService = new TaskService(repository);

Console.WriteLine("✅ Repository and Service initialized\n");

// ========== Test 1: Create Tasks ==========

Console.WriteLine("📝 Test 1: Create Tasks");
taskService.CreateTask(title : "Learn Generics", description : "Understand Generic Classes", dueDate : DateTime.Now.AddDays(2) );
taskService.CreateTask("Build Repository", "Create Generic Repository", DateTime.Now.AddDays(3));
taskService.CreateTask("Learn Generic Methods", "Understand Generic Methods", DateTime.Now.AddDays(1));
taskService.CreateTask("Build API", "Create REST API", DateTime.Now.AddDays(5));
taskService.CreateTask("Deploy Application", "Deploy to production", DateTime.Now.AddDays(-2));
Console.WriteLine("✓ Created 5 tasks\n");

// ========== Test 2: Display All Tasks ==========
Console.WriteLine("Display All Tasks");
var AllTasks = taskService.GetAllTasks();
foreach (var task in AllTasks)
{
    Console.WriteLine($"[{task.Id}] {task.Title} - {(task.IsCompleted? "Done" : "Pending")}");
}

// ========== Test 3: Get Completed Tasks ==========

Console.WriteLine("\n✓ Test 3: Get Completed Tasks ");
var completedTasks = taskService.GetCompletedTasks();
foreach (var task in completedTasks)
{
    Console.WriteLine($"Completed tasks count: {(task.IsCompleted ? completedTasks.Count : 0 + "\n" 
        + "\t (No completed tasks yet)")} ");

}

// ========== Test 4: Get Pending Tasks ==========
Console.WriteLine("\n○ Test 4:Get Pending Tasks ");
var Pending = taskService.GetPendingTasks();
Console.WriteLine($"Pending tasks count : {Pending.Count} ");
foreach (var task in Pending)
{
    Console.WriteLine($"{task}");
}

// ========== Test 5: Get Upcoming Tasks ==========
Console.WriteLine("\n📅 Test 5: Get Upcoming Tasks");
var Upcoming = taskService.GetUpcomingTasks(3);
Console.WriteLine($"Upcoming tasks count : {Upcoming.Count}");
foreach(var task in Upcoming)
{
    Console.WriteLine($"{task} - {( task.DueDate.Value - DateTime.Now.Date).Days} days left");
}


// ========== Test 6: Get Overdue Tasks ==========
Console.WriteLine("\n⚠️  Test 6: Get Overdue Tasks");
var overdue = taskService.GetOverDueTasks();
Console.WriteLine($"Overdue tasks count : {overdue.Count}");
foreach (var task in overdue)
{
    Console.WriteLine($"{task} - OVERDUE! by {(DateTime.Now.Date - task.DueDate.Value).Days} days");
}

// ========== Test 7: Search Tasks ==========
Console.WriteLine("\n\n🔎 Test 7: Search Tasks by keyword 'API'");
var searchResults = taskService.SearchByTitel("API");
foreach (var task in searchResults)
{
    Console.WriteLine(task);
}


// ========== Test 8: Complete aTask ==========
Console.WriteLine("\n✅ Test 8: Complete a Task");

var complete = taskService.CompleteTask(1);
Console.WriteLine($"task 1 completed : {complete}");

var complete2 = taskService.CompleteTask(999);
Console.WriteLine($"task 999 completed : {complete2}");

Console.WriteLine("\n***Completed Tasks : ***");
var completed = taskService.GetCompletedTasks();
foreach (var task in completed)
{
    Console.WriteLine(task);
}


// ========== Test 9: Get Statistics ==========
Console.WriteLine("\n📊 Test 9: Task Statistics");
var statiicstasks = taskService.GetStatistics();
Console.WriteLine(statiicstasks);

// ========== Test 10: Using Generic Methods ==========
Console.WriteLine("\n 🔄 Test 10: Using Generic Methods");
var convert = repository.ConvertAll(task =>task.Title);
Console.WriteLine("Task titles:");
foreach(var task in convert)
{
    Console.WriteLine(" - " + task);
}


// ========== Test 11: Using ForEach ==========
Console.WriteLine("\n📋 Test 11: Using ForEach with Repository");
Console.WriteLine("Task details:\n");
repository.Foreach(task => Console.WriteLine($"{task} - ({task.DueDate.Value.Date})"));

// ========== Test 12: Dependency Injection Pattern ==========

Console.WriteLine("\n🎯 Test 12: Dependency Injection Pattern\r\nThis shows why IRepository interface is important:" +
    "\r\n- TaskService depends on IRepository, not Repository" +
    "\r\n- We can swap implementations easily" +
    "\r\n- Makes testing easier with mock repositories" );

Console.WriteLine("\r\n\r\n✅ Day 3 completed successfully!");

