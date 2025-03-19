using System;
using System.Collections.Generic;

namespace DuplicateCode
{
    // Represents a single task
    class TaskItem
    {
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsImportant { get; set; }

        public TaskItem(string description, DateTime dueDate, bool isImportant = false)
        {
            Description = description;
            DueDate = dueDate;
            IsImportant = isImportant;
        }

        public override string ToString()
        {
            return $"{Description} (Due: {DueDate.ToShortDateString()})";
        }
    }

    // Represents a category with a list of tasks
    class Category
    {
        public string Name { get; set; }
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();

        public Category(string name)
        {
            Name = name;
        }

        public void AddTask(TaskItem task)
        {
            Tasks.Add(task);
        }

        public void RemoveTask(int index)
        {
            if (index >= 0 && index < Tasks.Count)
            {
                Tasks.RemoveAt(index);
            }
            else
            {
                Console.WriteLine("Invalid task number.");
            }
        }

        public void MoveTask(int fromIndex, int toIndex)
        {
            if (fromIndex >= 0 && fromIndex < Tasks.Count && toIndex >= 0 && toIndex < Tasks.Count)
            {
                var task = Tasks[fromIndex];
                Tasks.RemoveAt(fromIndex);
                Tasks.Insert(toIndex, task);
            }
            else
            {
                Console.WriteLine("Invalid task number or position.");
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    // Manages categories and overall operations
    class TaskManager
    {
        public Dictionary<string, Category> Categories { get; private set; } = new Dictionary<string, Category>();

        public void AddCategory(string name)
        {
            if (!Categories.ContainsKey(name))
            {
                Categories[name] = new Category(name);
                Console.WriteLine($"Category '{name}' added successfully.");
            }
            else
            {
                Console.WriteLine($"Category '{name}' already exists.");
            }
        }

        public void DeleteCategory(string name)
        {
            if (Categories.ContainsKey(name))
            {
                Categories.Remove(name);
                Console.WriteLine($"Category '{name}' deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Category '{name}' does not exist.");
            }
        }

        public void DisplayCategories()
        {
            Console.WriteLine("\nCategories:");
            foreach (var category in Categories.Keys)
            {
                Console.WriteLine($"- {category}");
            }
            Console.WriteLine();
        }

        public void DisplayTasks()
        {
            foreach (var category in Categories.Values)
            {
                Console.WriteLine($"\nCategory: {category.Name}");
                for (int i = 0; i < category.Tasks.Count; i++)
                {
                    var task = category.Tasks[i];
                    if (task.IsImportant)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine($"{i + 1}. {task}");
                    Console.ResetColor();
                }
            }
        }

        public void AddTask(string categoryName, TaskItem task)
        {
            if (Categories.ContainsKey(categoryName))
            {
                Categories[categoryName].AddTask(task);
                Console.WriteLine("Task added successfully.");
            }
            else
            {
                Console.WriteLine($"Category '{categoryName}' does not exist.");
            }
        }

        public void DeleteTask(string categoryName, int taskNumber)
        {
            if (Categories.ContainsKey(categoryName))
            {
                Categories[categoryName].RemoveTask(taskNumber);
                Console.WriteLine("Task deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Category '{categoryName}' does not exist.");
            }
        }

        public void MoveTaskWithinCategory(string categoryName, int fromIndex, int toIndex)
        {
            if (Categories.ContainsKey(categoryName))
            {
                Categories[categoryName].MoveTask(fromIndex, toIndex);
                Console.WriteLine("Task moved successfully.");
            }
            else
            {
                Console.WriteLine($"Category '{categoryName}' does not exist.");
            }
        }

        public void MoveTaskBetweenCategories(string sourceCategory, int taskIndex, string destinationCategory)
        {
            if (Categories.ContainsKey(sourceCategory) && Categories.ContainsKey(destinationCategory))
            {
                var task = Categories[sourceCategory].Tasks[taskIndex];
                Categories[sourceCategory].RemoveTask(taskIndex);
                Categories[destinationCategory].AddTask(task);
                Console.WriteLine("Task moved successfully.");
            }
            else
            {
                Console.WriteLine($"One or both categories do not exist.");
            }
        }

        public void HighlightTask(string categoryName, int taskIndex)
        {
            if (Categories.ContainsKey(categoryName))
            {
                var task = Categories[categoryName].Tasks[taskIndex];
                task.IsImportant = true;
                Console.WriteLine("Task highlighted successfully.");
            }
            else
            {
                Console.WriteLine($"Category '{categoryName}' does not exist.");
            }
        }
    }

    class DuplicateCode
    {
        static void Main(string[] args)
        {
            TaskManager taskManager = new TaskManager();
            bool quit = false;

            while (!quit)
            {
                ClearConsole();
                taskManager.DisplayCategories();
                var userChoice = DisplayMainMenu();

                switch (userChoice)
                {
                    case "1":
                        AddCategory(taskManager);
                        break;
                    case "2":
                        DeleteCategory(taskManager);
                        break;
                    case "3":
                        taskManager.DisplayTasks();
                        break;
                    case "4":
                        AddTask(taskManager);
                        break;
                    case "5":
                        DeleteTask(taskManager);
                        break;
                    case "6":
                        MoveTaskWithinCategory(taskManager);
                        break;
                    case "7":
                        MoveTaskBetweenCategories(taskManager);
                        break;
                    case "8":
                        HighlightTask(taskManager);
                        break;
                    case "q":
                        quit = true;
                        break;
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static void ClearConsole()
        {
            Console.Clear();
        }

        private static string DisplayMainMenu()
        {
            Console.WriteLine("1. Add Category");
            Console.WriteLine("2. Delete Category");
            Console.WriteLine("3. Display Tasks");
            Console.WriteLine("4. Add Task");
            Console.WriteLine("5. Delete Task");
            Console.WriteLine("6. Move Task Within Category");
            Console.WriteLine("7. Move Task Between Categories");
            Console.WriteLine("8. Highlight Task");
            Console.WriteLine("q. Quit");
            Console.Write("Choose an option: ");
            return Console.ReadLine();
        }

        private static void AddCategory(TaskManager taskManager)
        {
            Console.Write("Enter the new category name: ");
            string category = Console.ReadLine();
            taskManager.AddCategory(category);
        }

        private static void DeleteCategory(TaskManager taskManager)
        {
            Console.Write("Enter the category name to delete: ");
            string category = Console.ReadLine();
            taskManager.DeleteCategory(category);
        }

        private static void AddTask(TaskManager taskManager)
        {
            Console.Write("Enter the category name: ");
            string category = Console.ReadLine();

            Console.Write("Describe your task: ");
            string description = Console.ReadLine();

            Console.Write("Enter the due date (yyyy-mm-dd): ");
            DateTime dueDate = DateTime.Parse(Console.ReadLine());

            taskManager.AddTask(category, new TaskItem(description, dueDate));
        }

        private static void DeleteTask(TaskManager taskManager)
        {
            Console.Write("Enter the category name: ");
            string category = Console.ReadLine();

            Console.Write("Enter the task number to delete: ");
            int taskNumber = int.Parse(Console.ReadLine()) - 1;

            taskManager.DeleteTask(category, taskNumber);
        }

        private static void MoveTaskWithinCategory(TaskManager taskManager)
        {
            Console.Write("Enter the category name: ");
            string category = Console.ReadLine();

            Console.Write("Enter the task number to move: ");
            int taskNumber = int.Parse(Console.ReadLine()) - 1;

            Console.Write("Enter the new position: ");
            int newPosition = int.Parse(Console.ReadLine()) - 1;

            taskManager.MoveTaskWithinCategory(category, taskNumber, newPosition);
        }

        private static void MoveTaskBetweenCategories(TaskManager taskManager)
        {
            Console.Write("Enter the source category name: ");
            string sourceCategory = Console.ReadLine();

            Console.Write("Enter the task number to move: ");
            int taskNumber = int.Parse(Console.ReadLine()) - 1;

            Console.Write("Enter the destination category name: ");
            string destinationCategory = Console.ReadLine();

            taskManager.MoveTaskBetweenCategories(sourceCategory, taskNumber, destinationCategory);
        }

        private static void HighlightTask(TaskManager taskManager)
        {
            Console.Write("Enter the category name: ");
            string category = Console.ReadLine();

            Console.Write("Enter the task number to highlight: ");
            int taskNumber = int.Parse(Console.ReadLine()) - 1;

            taskManager.HighlightTask(category, taskNumber);
        }
    }
}
