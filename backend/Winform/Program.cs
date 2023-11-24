using Backend.Bussiness;
using Backend.Persistence;

using (var context = new TaskContext())
{
    context.Database.EnsureCreated();

    var command = new ListTask.Command() { PriorityId = null, TypeId = null };
    var service = new ListTask.Handler(context);
    var response = service.Handle(command);

    if (response != null)
    {
        foreach (var item in response.Items)
        {
            Console.WriteLine(item.Name);
        }
    }
}
