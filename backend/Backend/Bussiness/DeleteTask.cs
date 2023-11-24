using Backend.Persistence;
using Microsoft.EntityFrameworkCore.Internal;

namespace Backend.Bussiness;

public class DeleteTask
{
    public class Command
    {
        public int Id { get; set; }
    }

    public class Handler
    {
        private readonly TaskContext context;

        public Handler(TaskContext context)
        {
            this.context = context;
        }

        public void Handle(Command command)
        {
            var task = context.Tasks.First(a => a.Id == command.Id);

            if (task == null)
            {
                throw new Exception($"task not found: {command.Id}");
            }

            context.Remove(task);
            context.SaveChanges();
        }
    }
}
