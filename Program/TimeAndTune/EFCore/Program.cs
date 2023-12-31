﻿using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EFCore.Service;

namespace EFCore
{
    internal class Program
    {

        static void Main(string[] args)
        {
            using (var context = new TTContext())
            {
                var allUsers = context.Users.ToList();
                var allTasks = context.Tasks.ToList();
                var allSounds = context.Sounds.ToList();

                DatabaseUserProvider userService = new DatabaseUserProvider();
                Console.WriteLine(userService.getEmail(allUsers[0]));

                var firstTaskUsername = allTasks[0].UseridrefNavigation?.Username;
                var secondTaskUsername = allTasks[1].UseridrefNavigation?.Username;
                Console.WriteLine("Username of the user with the first task: " + firstTaskUsername);
                Console.WriteLine("Username of the user with the second task: " + secondTaskUsername);

                foreach (var user in allUsers)
                {
                    Console.WriteLine("===================================================================================");
                    Console.WriteLine($" User ID: {user.Userid},\n Username: {user.Username},\n Email {user.Email},\n Coins amout: {user.Coinsamount},\n Password: {user.Password}");
                    Console.WriteLine("===================================================================================");
                }
                foreach (var task in allTasks)
                {
                    Console.WriteLine("===================================================================================");
                    Console.WriteLine($" Task ID: {task.Taskid},\n UserRef: {task.Useridref},\n Name {task.Name},\n Description: {task.Description},\n Priority: {task.Priority},\n Completed status: {task.Completed},\n Date of creation: {task.Dateofcreation},\n Expected finish time{task.Expectedfinishtime},\n Finish time {task.Finishtime},\n Execution time: {task.Executiontime}");
                    Console.WriteLine("===================================================================================");
                }
                foreach (var sound in allSounds)
                {
                    Console.WriteLine("===================================================================================");
                    Console.WriteLine($" Sound ID: {sound.Soundid},\n Sound Name: {sound.Soundname}");
                    Console.WriteLine("===================================================================================");
                }
            }
        }
    }
}