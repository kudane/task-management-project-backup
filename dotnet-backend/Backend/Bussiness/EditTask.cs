using Backend.Persistence;

namespace Backend.Bussiness;

public class EditTask
{
    public class Command
    {
        public int Id { get; set; }

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
            var task = context.Tasks.First(a => a.Id == command.Id);

            if (task == null)
            {
                throw new Exception($"task not found: {command.Id}");
            }

            task.Name = command.Name;
            task.Description = command.Description;
            task.FkPriorityId = command.Priority;
            task.FkTypeId = command.Type;
            task.StartDate = command.StartDate;
            task.DueDate = command.DueDate;

            context.Update(task);
            context.SaveChanges();
        }
    }
}
