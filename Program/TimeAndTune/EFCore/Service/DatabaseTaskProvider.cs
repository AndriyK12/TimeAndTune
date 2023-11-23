﻿using Microsoft.EntityFrameworkCore;
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
        public void updateTaskById(int id, EFCore.Task task)
        {
            List<EFCore.Task> items = this.GetAllTasks();
            using (var context = new TTContext())
            {
                foreach (EFCore.Task item in items)
                {
                    if (item.Taskid == id)
                    {
                        Task newTask = new Task();
                        newTask.Taskid = id;
                        newTask.Name = task.Name;
                        newTask.Description = task.Description;
                        newTask.Dateofcreation = task.Dateofcreation;
                        newTask.Expectedfinishtime = task.Expectedfinishtime;
                        newTask.Finishtime = task.Finishtime;
                        newTask.Priority = task.Priority;
                        newTask.Completed = task.Completed;
                        newTask.Executiontime = task.Executiontime;
                        newTask.Useridref = task.Useridref;
                        newTask.UseridrefNavigation = task.UseridrefNavigation;
                        context.Update(newTask);
                        context.SaveChangesAsync();
                        break;
                    }
                }
            }
        }
    }
}
