using EmailClient.Extension;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NotificationsClient.Extension;
using RM.CarteResto.Abstraction;
using RM.CarteResto.Business;
using RM.CarteResto.Data;
using RM.CarteResto.Remote;
using RM.CarteResto.Service;
using RM.DemandeCarteResto.Abstraction;
using RM.DemandeCarteResto.Business;
using RM.DemandeCarteResto.Data;
using RM.Notif.Business.Commands;
using RM.Notif.Business;
using RM.Notifications.Abstraction;
using RM.Notifications.Business;
using RM.Notifications.Data;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();

        });
});

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var cosmosDbSettings = builder.Configuration.GetSection("CosmosDb");
var accountEndpoint = cosmosDbSettings["AccountEndpoint"];
var accountKey = cosmosDbSettings["AccountKey"];
var databaseName = cosmosDbSettings["DatabaseName"];
builder.Services.AddDbContext<DemandeCarteRestoContext>(options =>
{
    options.UseCosmos(accountEndpoint, accountKey, databaseName);
});
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddDbContext<CarteRestoContext>(options =>
{
    options.UseCosmos(accountEndpoint, accountKey, databaseName);
});
builder.Services.AddDbContext<EmailNotificationContext>(options =>
{
    options.UseCosmos(accountEndpoint, accountKey, databaseName);
});
builder.Services.AddDbContext<DemandeCarteRestoContext>(options =>
{
    options.UseCosmos(accountEndpoint, accountKey, databaseName);
});
builder.Services.AddScoped<DecrementBalanceCommand>();
builder.Services.AddEmailServices();
builder.Services.AddScoped<ICarteRestoRepository, CarteRestoRepository>();
builder.Services.AddCarteRestoGrpcClient();
builder.Services.AddScoped<IDemandeCarteRestoRepository, DemandeCarteRestoRepository>();
builder.Services.AddCarteRestoGrpcClient();
builder.Services.AddScoped<ICarteRestoService, CarteRestoServiceGRPC>();
builder.Services.AddScoped<AddDemandCardCommand>();
builder.Services.AddScoped<AcceptDemandCardCommand>();
builder.Services.AddScoped<RejectDemandCardCommand>();
builder.Services.AddScoped<RemoveDemandCardCommand>();
builder.Services.AddScoped<UpdateCardRestoCommand>();
builder.Services.AddScoped<GetAllDemandesCardsQuery>();
builder.Services.AddScoped<GetAllPendingDemandsQuery>();
builder.Services.AddScoped<GetDemandCardByIdQuery>();
builder.Services.AddScoped<GetDemandeCardByUserIdQuery>();
builder.Services.AddScoped<ICarteRestoService, CarteRestoServiceGRPC>();

builder.Services.AddDbContext<NotificationContext>(options =>
{
    options.UseCosmos(accountEndpoint, accountKey, databaseName);
});
builder.Services.AddNotifClientServices();
// REPOS
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IEmailNotificationRepository, EmailNotificationRepository>();

// COMMANDS
builder.Services.AddScoped<AddNotificationCommand>();
builder.Services.AddScoped<ReadNotificationCommand>();
builder.Services.AddScoped<SendEmailCommand>();
builder.Services.AddScoped<SendSuccessDemandEmailCommand>();
builder.Services.AddScoped<ApprovedDemandEmail>();
builder.Services.AddScoped<RejectedDemandEmail>();
builder.Services.AddScoped<RechargeCardEmail>();


// QUERIES
builder.Services.AddScoped<GetAllNotificationsByReceiverIdQuery>();

var validIssuer = builder.Configuration.GetValue<string>("JwtTokenSettings:ValidIssuer");
var validAudience = builder.Configuration.GetValue<string>("JwtTokenSettings:ValidAudience");
var symmetricSecurityKey = builder.Configuration.GetValue<string>("JwtTokenSettings:SymmetricSecurityKey");


builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.IncludeErrorDetails = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = validIssuer,
            ValidAudience = validAudience,
            NameClaimType = ClaimTypes.Email,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(symmetricSecurityKey)
            ),
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Query["access_token"];
                return Task.CompletedTask;
            }
        };
    });


builder.Services.AddSwaggerGen(option =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    option.IncludeXmlComments(xmlPath);
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Test API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStatusCodePages();
app.UseRouting();

app.UseCors("AllowSpecificOrigin");
app.UseWebSockets();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
