using Backend.Persistence;
using Microsoft.EntityFrameworkCore;
using Task = Backend.Persistence.Task;

namespace Backend.Bussiness;

public class ByIdTask
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

        public Task Handle(Command command)
        {
            var task = context.Tasks.AsNoTracking().First(a => a.Id == command.Id);

            if (task == null)
            {
                throw new Exception($"task not found: {command.Id}");
            }

            return task;
        }
    }
}
