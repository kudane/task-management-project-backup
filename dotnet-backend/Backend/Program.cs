using Backend.Bussiness;
using Backend.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(i => i.FullName);
});
builder.Services.AddDbContext<TaskContext>();
builder.Services.AddScoped<ByIdTask.Handler>();
builder.Services.AddScoped<ListTask.Handler>();
builder.Services.AddScoped<NewTask.Handler>();
builder.Services.AddScoped<EditTask.Handler>();
builder.Services.AddScoped<DeleteTask.Handler>();
builder.Services.AddScoped<ListType.Handler>();
builder.Services.AddScoped<ListPriority.Handler>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(p =>
        p.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors();
app.MapControllers();
app.Run();
