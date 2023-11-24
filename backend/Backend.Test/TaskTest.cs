using Backend.Bussiness;
using Backend.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Reflection.Metadata;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Task = Backend.Persistence.Task;

namespace Backend.Test
{
    public class TaskTest
    {
        private static DbContextOptions<TaskContext> options = new DbContextOptionsBuilder<TaskContext>().UseSqlite($"Data Source=taskdb.db").Options;

        [Fact]
        public void Test_Task_Get_All_Count_Not_Equal_0()
        {
            using (var context = new TaskContext(options))
            {
                // Arrange
                context.Database.EnsureCreated();
                context.Tasks.RemoveRange(context.Tasks.ToList());
                context.SaveChanges();
    
                context.Tasks.Add(new Task() { Name = "Mock", Description = "Mock", FkPriorityId = 1, FkTypeId = 1 });
                context.SaveChanges();
                var command = new ListTask.Command() { PriorityId = null, TypeId = null };
                var service = new ListTask.Handler(context);
                var response = service.Handle(command);

                // Act
                Assert.True(response.Items.Count() > 0);

                // Clean-Up
                var clean = context.Tasks.First(a => a.Name == "Mock");
                context.Tasks.Remove(clean);
                context.SaveChanges();
            }
        }

        [Fact]
        public void Test_Task_New_Success()
        {
            using (var context = new TaskContext(options))
            {
                // Arrange
                context.Database.EnsureCreated();
                context.Tasks.RemoveRange(context.Tasks.ToList());
                context.SaveChanges();

                context.Tasks.Add(new Task() { Name = "Mock", Description = "Mock", FkPriorityId = 1, FkTypeId = 1 });
                context.SaveChanges();
                var mockId = context.Tasks.First(a => a.Name == "Mock").Id;

                var command = new DeleteTask.Command() { Id = mockId };
                var service = new DeleteTask.Handler(context);
                service.Handle(command);

                // Act
                var act = context.Tasks.FirstOrDefault(a => a.Id == mockId);
                Assert.True(act == null);
            }
        }

        [Fact]
        public void Test_Task_Edit_Success()
        {
            using (var context = new TaskContext(options))
            {
                // Arrange
                context.Database.EnsureCreated();
                context.Tasks.RemoveRange(context.Tasks.ToList());
                context.SaveChanges();

                context.Tasks.Add(new Task() { Name = "Mock", Description = "Mock", FkPriorityId = 1, FkTypeId = 1 });
                context.SaveChanges();
                var mockId = context.Tasks.First(a => a.Name == "Mock").Id;

                var command = new EditTask.Command() { Id = mockId, Name = "Mock Edit", Description = "Mock Edit", Priority = 1, Type = 1 };
                var service = new EditTask.Handler(context);
                service.Handle(command);

                // Act
                var act = context.Tasks.FirstOrDefault(a => a.Name == "Mock Edit");
                Assert.True(act != null);

                // Clean-Up
                if (act != null)
                {
                    context.Tasks.Remove(act);
                    context.SaveChanges();
                }
            }
        }

        [Fact]
        public void Test_Task_Delete_Success()
        {
            using (var context = new TaskContext(options))
            {
                // Arrange
                context.Database.EnsureCreated();
                context.Tasks.RemoveRange(context.Tasks.ToList());
                context.SaveChanges();

                var command = new NewTask.Command() { Name = "Mock", Description = "Mock", Priority = 1, Type = 1 };
                var service = new NewTask.Handler(context);
                service.Handle(command);

                // Act
                var act = context.Tasks.First(a => a.Name == "Mock");
                Assert.True(act.Name == "Mock");

                // Clean-Up
                if (act != null)
                {
                    context.Tasks.Remove(act);
                    context.SaveChanges();
                }
            }
        }
    }
}