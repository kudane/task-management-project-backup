using Backend.Persistence;
using Task = Backend.Persistence.Task;

namespace Backend.Bussiness;

public class NewTask
{
    public class Command
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? DueDate { get; set; }

        public int Type { get; set; }

        public int Priority { get; set; }
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
            var task = new Task()
            {
               Name = command.Name,
               Description = command.Description,
               FkPriorityId = command.Priority,
               FkTypeId = command.Type,
               StartDate = command.StartDate,
               DueDate = command.DueDate
            };

            context.Add(task);
            context.SaveChanges();
        }
    }
}
