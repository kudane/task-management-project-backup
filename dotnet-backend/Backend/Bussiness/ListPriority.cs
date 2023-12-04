using Backend.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Backend.Bussiness;

public class ListPriority
{
    public class Response
    {
        public IEnumerable<Priority> Items { get; set; } = new List<Priority>();
    }

    public class Handler
    {
        private readonly TaskContext context;

        public Handler(TaskContext context)
        {
           this.context = context;
        }

        public Response Handle()
        {
            var query = context.Priorities.AsNoTracking();
            return new Response() { Items = query };
        }
    }
}
