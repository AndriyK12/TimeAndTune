using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Service
{
    internal class DatabaseTaskProvider : ITaskProvider
    {
        public List<Task> GetAllTasks()
        {
            using (var context = new TTContext())
            {
                var allTasks = context.Tasks.ToList();
                return allTasks;
            }
        }

        public int GetAmountOfTasksByDate(DateOnly date)
        {
            using (var context = new TTContext())
            {
                var allTasks = context.Tasks.ToList();
                var dateTasksAmount = 0;
                foreach (var task in allTasks)
                {
                    if (task.Finishtime == date && getCompleted(task))
                    {
                        dateTasksAmount++;
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

        public TimeSpan getExecutionTime(Task task)
        {
            return (TimeSpan)task.Executiontime;
        }
    }
}
