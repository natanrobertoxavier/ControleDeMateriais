using ControleDeMateriais.Application;
using ControleDeMateriais.Infrastructure;
using ControleDeMateriais.Domain.Extension;
using ControleDeMateriais.Application.Services.AutoMapper;
using ControleDeMateriais.Api.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Controle de Materiais API",
        Version = "v1",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact() 
        { 
            Name = "Natan Roberto Xavier",
            Email = "natanroberto182@gmail.com"
        },
    });

    c.SchemaFilter<ExampleRegisterUserFilter>();
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilters)));

var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperConfiguration());
});

var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();