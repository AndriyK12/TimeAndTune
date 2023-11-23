using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Service
{
    internal interface ITaskProvider
    {
        List<Task> GetAllTasks();
        List<Task> GetAllTasksByUserID(int userID);
        int GetAmountOfTasksByDate(DateOnly date);
        int getTaskId(Task task);
        string getName(Task task);
        string getDescription(Task task);
        DateOnly getExpectedFinishTime(Task task);
        DateOnly getFinishTime(Task task);
        int getPriority(Task task);
        bool getCompleted(Task task);
        TimeSpan getExecutionTime(Task task);
        EFCore.Task getTaskById(int id);
        void addNewTask(string name, string description, DateOnly expectedDate, int priority, int userRef);
        void updateTaskById(int id, EFCore.Task task);
    }
}
