using Assignment.API;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AssignmentsContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("AssignmentsConnectionString")));

builder.Services.AddTransient<IAssignmentsService, AssignmentsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else { app.UseExceptionHandler("/Error"); }
                                                                                                                                                                            
app.UseHttpsRedirection();



app.UseRouting();

// * need to delete that, use for individual users account
//app.UseCors(x => x
//    .AllowAnyOrigin()
//    .AllowAnyMethod()
//    .AllowAnyHeader());

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



app.Run();
