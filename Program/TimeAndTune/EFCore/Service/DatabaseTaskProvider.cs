using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EFCore.Service
{
    public class DatabaseTaskProvider : ITaskProvider
    {
        public void addNewTask(string name, string description, DateOnly expectedDate, int priority,int userRef)
        {
            Task newTask = new Task();
            using (var context = new TTContext())
            {
                newTask.Name = name;
                newTask.Description = description;
                newTask.Expectedfinishtime = expectedDate;
                newTask.Priority = priority;
                newTask.Dateofcreation = DateOnly.FromDateTime(DateTime.Now);
                newTask.Useridref = userRef;
                context.Tasks.Add(newTask);
                context.SaveChanges();
            }
        }
        public List<Task> GetAllTasks()
        {
            using (var context = new TTContext())
            {
                var allTasks = context.Tasks.ToList();
                return allTasks;
            }
        }
        public int GetAmountOfTasksByDate(DateOnly date, int userID)
        {
            using (var context = new TTContext())
            {
                var allTasks = context.Tasks.ToList();
                var dateTasksAmount = 0;
                foreach (var task in allTasks)
                {
                    if (task.Finishtime == date || (task.Expectedfinishtime == date && task.Completed == false))
                    {
                        if (task.Useridref == userID)
                        {
                            dateTasksAmount++;
                        }
                    }
                }
                return dateTasksAmount;
            }
        }

        public int GetAmountOfCompletedTasksByDate(DateOnly date, int userID)
        {
            using (var context = new TTContext())
            {
                var allTasks = context.Tasks.ToList();
                var dateTasksAmount = 0;
                foreach (var task in allTasks)
                {
                    if (task.Finishtime == date && getCompleted(task))
                    {
                        if (task.Useridref == userID)
                        {
                            dateTasksAmount++;
                        }
                    }
                }
                return dateTasksAmount;
            }
        }
        public List<Task> GetAllTasksByUserID(int userID)
        {
            using (var context = new TTContext())
            {
                var allTasks = context.Tasks.ToList();
                var userTasks = new List<Task>();
                foreach (var task in allTasks)
                {
                    if (task.Useridref == userID)
                    {
                        userTasks.Add(task);
                    }
                }
                return userTasks;
            }
        }

        public bool getCompleted(Task task)
        {
            bool task_completed = (bool)task.Completed;
            return task_completed;
        }

        public int getTaskId(Task task)
        {
            return task.Taskid;
        }

        public string getName(Task task)
        {
            return task.Name;
        }

        public string getDescription(Task task)
        {
            return task.Description;
        }

        public DateOnly getExpectedFinishTime(Task task)
        {
            return task.Expectedfinishtime;
        }

        public DateOnly getFinishTime(Task task)
        {
            return (DateOnly)task.Finishtime;
        }

        public int getPriority(Task task)
        {
            return task.Priority;
        }

        public EFCore.Task? getTaskById(int id)
        {
            foreach(EFCore.Task item in this.GetAllTasks())
            {
                if(item.Taskid == id)
                {
                    return item;
                }
            }
            return null;
        }

        public TimeSpan getExecutionTime(Task task)
        {
            return (TimeSpan)task.Executiontime;
        }
        public void updateTaskById(int id, string newName, string newDesc, string newDate, int newpriority)
        {
            using (var context = new TTContext())
            {
                Task task = getTaskById(id);
                task.Name = newName;
                task.Description= newDesc;
                DateOnly.TryParse(newDate, out DateOnly dateOnly);
                task.Expectedfinishtime = dateOnly;
                task.Priority = newpriority + 1;
                context.Update(task);
                context.SaveChangesAsync();
            }
        }

        public List<Task> getAllTasksByDayUsingUserId(int userId)
        {
            List<EFCore.Task> items = this.GetAllTasks();
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            List<Task> tasksWithTargetDate = items
                .Where
                (task => task.Expectedfinishtime <= currentDate && 
                task.Useridref == userId)
                .ToList();

            return tasksWithTargetDate;
        }

        public List<Task> getAllTasksByWeekUsingUserId(int userId)
        {
            List<EFCore.Task> items = this.GetAllTasks();

            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            int currentDayOfWeek = (int)currentDate.DayOfWeek;
            int daysToMonday = currentDayOfWeek - (int)DayOfWeek.Monday;
            DateOnly endOfWeekDate = currentDate.AddDays(daysToMonday);

            List<Task> tasksWithTargetDate = items
                .Where
                (task => task.Expectedfinishtime >= currentDate &&
                task.Expectedfinishtime <= endOfWeekDate &&
                task.Useridref == userId)
                .ToList();
            return tasksWithTargetDate;
        }

        public List<Task> getAllTasksByMonthUsingUserId(int userId)
        {
            List<Task> items = this.GetAllTasks();

            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            DateOnly endOfMonthDate = new DateOnly(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));

            List<Task> tasksWithTargetDate = items
                .Where
                (task => task.Expectedfinishtime >= currentDate && 
                task.Expectedfinishtime <= endOfMonthDate && 
                task.Useridref == userId)
                .ToList();

            return tasksWithTargetDate;
        }
    }
}
