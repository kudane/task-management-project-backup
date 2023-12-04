using Backend.Persistence;
using Microsoft.EntityFrameworkCore;
using Type = Backend.Persistence.Type;

namespace Backend.Bussiness;

public class ListType
{

    public class Response
    {
        public IEnumerable<Type> Items { get; set; } = new List<Type>();
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
            var query = context.Types.AsNoTracking();
            return new Response() { Items = query };
        }
    }
}
