using EFCore;
using EFCore.Service;
using Newtonsoft.Json;
using Faker;
using TimeAndTune.Pages;

namespace TestProject
{
    public class DatabaseTaskProviderTests
    {
        [Fact]
        public void getTaskById()
        {
            var properTask = new EFCore.Task();
            properTask.Taskid = 1;
            properTask.Name = "Sint et sed nam consequatur reprehenderit quia.";
            properTask.Description = "Iste dolore cupiditate ipsum repudiandae reiciendis ut sed nemo omnis. Et aliquam quis sequi nulla minus et. Maxime nostrum esse quo. Et voluptas ratione reiciendis voluptates voluptas nam tempora voluptate. Amet sequi iure illo. Aut recusandae voluptas iure.";
            properTask.Dateofcreation = new DateOnly(2023, 10, 16);
            properTask.Expectedfinishtime = new DateOnly(2023, 10, 18);
            properTask.Finishtime = new DateOnly(2023, 10, 17);
            properTask.Priority = 2;
            properTask.Completed = true;
            properTask.Executiontime = new TimeSpan(6, 21, 1, 6, 651);
            properTask.Useridref = 29;
            var task = new EFCore.Task();
            DatabaseTaskProvider taskService = new DatabaseTaskProvider();
            task = taskService.getTaskById(1);
            var obj1Str = JsonConvert.SerializeObject(properTask);
            var obj2Str = JsonConvert.SerializeObject(task);
            Assert.Equal(obj1Str, obj2Str);
        }

        [Fact]
        public void getTaskId()
        {
            var task = new EFCore.Task();
            DatabaseTaskProvider taskService = new DatabaseTaskProvider();
            task = taskService.getTaskById(1);
            int foundID = taskService.getTaskId(task);
            Assert.Equal(task.Taskid, foundID);
        }

        [Fact]
        public void getCompleted()
        {
            var task = new EFCore.Task();
            DatabaseTaskProvider taskService = new DatabaseTaskProvider();
            task = taskService.getTaskById(1);
            bool foundCompleted = taskService.getCompleted(task);
            Assert.Equal(task.Completed, foundCompleted);
        }

        [Fact]
        public void getName()
        {
            var task = new EFCore.Task();
            DatabaseTaskProvider taskService = new DatabaseTaskProvider();
            task = taskService.getTaskById(1);
            string foundName = taskService.getName(task);
            Assert.Equal(task.Name, foundName);
        }

        [Fact]
        public void getDescription()
        {
            var task = new EFCore.Task();
            DatabaseTaskProvider taskService = new DatabaseTaskProvider();
            task = taskService.getTaskById(1);
            string foundDescription = taskService.getDescription(task);
            Assert.Equal(task.Description, foundDescription);
        }

        [Fact]
        public void getExpectedFinishTime()
        {
            var task = new EFCore.Task();
            DatabaseTaskProvider taskService = new DatabaseTaskProvider();
            task = taskService.getTaskById(1);
            DateOnly foundExpectedfinishtime = taskService.getExpectedFinishTime(task);
            Assert.Equal(task.Expectedfinishtime, foundExpectedfinishtime);
        }

        [Fact]
        public void getFinishTime()
        {
            var task = new EFCore.Task();
            DatabaseTaskProvider taskService = new DatabaseTaskProvider();
            task = taskService.getTaskById(1);
            DateOnly foundFinishtime = taskService.getFinishTime(task);
            Assert.Equal(task.Finishtime, foundFinishtime);
        }

        [Fact]
        public void getPriority()
        {
            var task = new EFCore.Task();
            DatabaseTaskProvider taskService = new DatabaseTaskProvider();
            task = taskService.getTaskById(1);
            int foundPriority = taskService.getPriority(task);
            Assert.Equal(task.Priority, foundPriority);
        }

        [Fact]
        public void getExecutionTime()
        {
            var task = new EFCore.Task();
            DatabaseTaskProvider taskService = new DatabaseTaskProvider();
            task = taskService.getTaskById(1);
            TimeSpan foundExecutiontime = taskService.getExecutionTime(task);
            Assert.Equal(task.Executiontime, foundExecutiontime);
        }

        [Fact]
        public void GetAmountOfTasksByDate()
        {
            int actuall_amount = 1;
            int foundAmount = 0;
            DatabaseTaskProvider taskService = new DatabaseTaskProvider();
            foundAmount = taskService.GetAmountOfTasksByDate(new DateOnly(2023, 10, 17), 29);
            Assert.Equal(actuall_amount, foundAmount);
        }

        [Fact]
        public void GetAmountOfCompletedTasksByDate()
        {
            int actuall_amount = 1;
            int foundAmount = 0;
            DatabaseTaskProvider taskService = new DatabaseTaskProvider();
            foundAmount = taskService.GetAmountOfCompletedTasksByDate(new DateOnly(2023, 10, 17), 29);
            Assert.Equal(actuall_amount, foundAmount);
        }

        [Fact]
        public void updateTaskById()
        {
            DatabaseTaskProvider taskService = new DatabaseTaskProvider();
            EFCore.Task properTask = taskService.getTaskById(29);

            string fakerName = Faker.Lorem.Sentence();
            string fakerDesc = Faker.Lorem.Paragraph();
            DateOnly fakerExpectedFinishTime = DateOnly.FromDateTime(Faker.DateTimeFaker.DateTime());
            int fakerPrior = Faker.RandomNumber.Next(1, 3);

            properTask.Name = fakerName;
            properTask.Description = fakerDesc;
            properTask.Expectedfinishtime = fakerExpectedFinishTime;
            properTask.Priority = fakerPrior;

            taskService.updateTaskById(29, fakerName, fakerDesc, fakerExpectedFinishTime.ToString(), fakerPrior - 1);
            EFCore.Task task = taskService.getTaskById(29);

            var obj1Str = JsonConvert.SerializeObject(properTask);
            var obj2Str = JsonConvert.SerializeObject(task);
            Assert.Equal(obj1Str, obj2Str);
        }

        [Fact]
        public void updateTaskExecutiontimeById()
        {
            DatabaseTaskProvider taskService = new DatabaseTaskProvider();
            EFCore.Task properTask = taskService.getTaskById(29);

            Random random = new Random();

            int hours = random.Next(24);
            int minutes = random.Next(60);
            int seconds = random.Next(60);
            int milliseconds = random.Next(1000);

            TimeSpan randomTimeSpan = new TimeSpan(hours, minutes, seconds, milliseconds);
            bool fakerCompleted = Faker.Boolean.Random();

            properTask.Executiontime = randomTimeSpan;
            properTask.Completed = fakerCompleted;

            taskService.updateTaskExecutiontimeById(29, randomTimeSpan, fakerCompleted);
            EFCore.Task task = taskService.getTaskById(29);

            var obj1Str = JsonConvert.SerializeObject(properTask);
            var obj2Str = JsonConvert.SerializeObject(task);
            Assert.Equal(obj1Str, obj2Str);
        }
        public class DatabaseUserProviderTests
        {
            [Fact]
            public void getUserByEmail()
            {
                var properUser = new EFCore.User();
                properUser.Userid = 1;
                properUser.Username = "maryam";
                properUser.Email = "norberto@mayert.info";
                properUser.Coinsamount = 469;
                properUser.Password = "$2a$11$fYHVdah6CUqNmHgljOch7.JvCIRDn9hjopGwDEPzVMT6TteswHG9i";
                properUser.Passwordsalt = "Aut qui necessitatibus est voluptas debitis voluptatem maiores.";
                var user = new EFCore.User();
                DatabaseUserProvider userService = new DatabaseUserProvider();
                user = userService.getUserByEmail("norberto@mayert.info");
                var obj1Str = JsonConvert.SerializeObject(properUser);
                var obj2Str = JsonConvert.SerializeObject(user);
                Assert.Equal(obj1Str, obj2Str);
            }

            [Fact]
            public void getCoinsAmount()
            {
                DatabaseUserProvider userService = new DatabaseUserProvider();
                var user = userService.getUserByEmail("norberto@mayert.info");
                int userCoinsAmount = userService.getCoinsAmount(user);
                Assert.Equal(user.Coinsamount, userCoinsAmount);
            }

            [Fact]
            public void getEmail()
            {
                DatabaseUserProvider userService = new DatabaseUserProvider();
                var user = userService.getUserByEmail("norberto@mayert.info");
                string userEmail = userService.getEmail(user);
                Assert.Equal(user.Email, userEmail);
            }

            [Fact]
            public void getUserID()
            {
                DatabaseUserProvider userService = new DatabaseUserProvider();
                var user = userService.getUserByEmail("norberto@mayert.info");
                int userID = userService.getUserID(user);
                Assert.Equal(user.Userid, userID);
            }

            [Fact]
            public void getUserName()
            {
                DatabaseUserProvider userService = new DatabaseUserProvider();
                var user = userService.getUserByEmail("norberto@mayert.info");
                string userUsername = userService.getUserName(user);
                Assert.Equal(user.Username, userUsername);
            }

            [Fact]
            public void setCoinsAmount()
            {
                DatabaseUserProvider userService = new DatabaseUserProvider();
                var user = userService.getUserByEmail("ebony@mohr.info");
                int newCoinsAmount = Faker.RandomNumber.Next(100, 1000);
                userService.setCoinsAmount(user, newCoinsAmount);
                Assert.Equal(user.Coinsamount, newCoinsAmount);
            }

            [Fact]
            public void isUserAlreadyExist()
            {
                DatabaseUserProvider userService = new DatabaseUserProvider();
                bool userAlreadyExists = userService.isUserAlreadyExist("norberto@mayert.info");
                Assert.Equal(userAlreadyExists, true);
            }

            [Fact]
            public void addCoinsForAUserById()
            {
                DatabaseUserProvider userService = new DatabaseUserProvider();
                var user = userService.getUserByEmail("laisha@hilllleffler.co.uk");
                int userCoinsAmount = userService.getCoinsAmount(user);
                int additionalCoins = Faker.RandomNumber.Next(10, 50);
                userService.addCoinsForAUserById(userService.getUserID(user), additionalCoins);
                user = userService.getUserByEmail("laisha@hilllleffler.co.uk");
                Assert.Equal(user.Coinsamount, userCoinsAmount + additionalCoins);
            }
        }
        public class DatabaseSoundProviderTests
        {
            [Fact]
            public void getSoundById()
            {
                var properSound = new Sound();
                properSound.Soundid = 1;
                properSound.Soundname = "Nulla illo perspiciatis rerum est.";
                properSound.Audiofilepath = "audio/Dolore a a recusandae voluptates officia sed aut quia.";
                properSound.Photofilepath = "photos/Sapiente aut beatae vel qui autem expedita quis debitis.";
                var sound = new Sound();
                DatabaseSoundProvider soundService = new DatabaseSoundProvider();
                sound = soundService.getSoundById(1);
                var obj1Str = JsonConvert.SerializeObject(properSound);
                var obj2Str = JsonConvert.SerializeObject(sound);
                Assert.Equal(obj1Str, obj2Str);
            }

            [Fact]
            public void getSoundName()
            {
                DatabaseSoundProvider soundService = new DatabaseSoundProvider();
                Sound sound = soundService.getSoundById(1);
                string foundName = soundService.getSoundName(sound);
                Assert.Equal(foundName, "Nulla illo perspiciatis rerum est.");
            }

            [Fact]
            public void getSoundId()
            {
                DatabaseSoundProvider soundService = new DatabaseSoundProvider();
                Sound sound = soundService.getSoundById(1);
                int foundId = soundService.getSoundId(sound);
                Assert.Equal(foundId, 1);
            }

            [Fact]
            public void getAudioFilePath()
            {
                DatabaseSoundProvider soundService = new DatabaseSoundProvider();
                Sound sound = soundService.getSoundById(1);
                string foundAFilePath = soundService.getAudioFilePath(sound);
                Assert.Equal(foundAFilePath, "audio/Dolore a a recusandae voluptates officia sed aut quia.");
            }

            [Fact]
            public void getPhotoFilePath()
            {
                DatabaseSoundProvider soundService = new DatabaseSoundProvider();
                Sound sound = soundService.getSoundById(1);
                string foundPFilePath = soundService.getPhotoFilePath(sound);
                Assert.Equal(foundPFilePath, "photos/Sapiente aut beatae vel qui autem expedita quis debitis.");
            }
        }
    }
    public class AppTests
    {
        public class RegisterTests
        {
            [Fact]
            public void isEmail()
            {
                bool isEmailTrue = RegisterPage.IsEmail("someemail@gmail.com");
                bool isEmailFalse = RegisterPage.IsEmail("someemail.gmail.com");
                Assert.True(isEmailTrue);
                Assert.False(isEmailFalse);
            }
            [Fact]
            public void comparePasswords()
            {
                bool comparePasswordsTrue = RegisterPage.comparePasswords("password","password");
                bool comparePasswordsFalse = RegisterPage.comparePasswords("password", "password WRONG");
                Assert.True(comparePasswordsTrue);
                Assert.False(comparePasswordsFalse);
            }

        }
    }
}