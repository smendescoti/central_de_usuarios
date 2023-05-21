using CentralDeUsuarios.Infra.Messages.Consumers;
using CentralDeUsuarios.Services.Api;
using CentralDeUsuarios.Services.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

Setup.AddSwagger(builder);
Setup.AddCors(builder);
Setup.AddRegisterServices(builder);
Setup.AddEntityFrameworkServices(builder);
Setup.AddMessageServices(builder);
Setup.AddAutoMapperServices(builder);
Setup.AddMediatRServices(builder);
Setup.AddMongoDBServices(builder);
Setup.AddJwtBearerSecurity(builder);

//fix: desabilitar o consumidor da mensageria
//builder.Services.AddHostedService<MessageQueueConsumer>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionMiddleware>();

Setup.UseCors(app);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

//Declaração publica da classe
public partial class Program { }