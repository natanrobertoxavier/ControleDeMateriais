using ControleDeMateriais.Api.Filters;
using ControleDeMateriais.Api.Filters.LoggedUser;
using ControleDeMateriais.Application;
using ControleDeMateriais.Application.Services.AutoMapper;
using ControleDeMateriais.Infrastructure;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(option => option.LowercaseUrls = true);

builder.Services.AddHttpContextAccessor();

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

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models
        .OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header utilizando o Bearer sheme. Exemple: \"Authorization: Bearer {token}\"",
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilters)));

var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperConfiguration());
});

builder.Services.AddScoped<IAuthorizationHandler, LoggedUserHandler>();

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("LoggedUser", policy => policy.Requirements.Add(new LoggedUserRequirement()));
});

builder.Services.AddScoped<AuthenticatedUserAttribute>();

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