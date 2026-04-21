using Api.Validators;
using Application.Commands;
using Application.Queries;
using FluentValidation;
using Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<RequestObjValidator>();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssemblyContaining<GetSecondLargestNumberCommand>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
