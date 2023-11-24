using Backend.Persistence;
using Microsoft.EntityFrameworkCore;
using Task = Backend.Persistence.Task;

namespace Backend.Bussiness;

public class ListTask
{
    public class Command
    {
        public int? TypeId { get; set; }
        public int? PriorityId { get; set; }
    }

    public class Response
    {
        public IEnumerable<Task> Items { get; set; } = new List<Task>();
    }

    public class Handler
    {
        private readonly TaskContext context;

        public Handler(TaskContext context)
        {
           this.context = context;
        }

        public Response Handle(Command command)
        {
            var query = context.Tasks.AsNoTracking();

            if (command.TypeId.HasValue && command.TypeId.Value != 0)
            {
                query = query.Where(a => a.FkTypeId == command.TypeId.Value);
            }

            if (command.PriorityId.HasValue && command.PriorityId.Value != 0)
            {
                query = query.Where(a => a.FkPriorityId == command.PriorityId.Value);
            }

            return new Response() { Items = query };
        }
    }
}
