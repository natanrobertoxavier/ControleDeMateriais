using ControleDeMateriais.Api.Filters;
using ControleDeMateriais.Application;
using ControleDeMateriais.Application.Services.AutoMapper;
using ControleDeMateriais.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Controle de Materiais API",
        Version = "v1",
        Description = "Developed by: Natan Roberto Xavier",

        Contact = new Microsoft.OpenApi.Models.OpenApiContact() 
        { 
            Name = "natanroberto182@gmail.com",
            Email = "natanroberto182@gmail.com"
        },
    });

    c.SchemaFilter<ExampleControllerFilter>();
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