using AutoMapper;
using Similar_products.Application;
using Similar_products.Web.Extensions;
using Similar_products.Application.Requests.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Добавляем поддержку контроллеров (если они нужны)
builder.Services.AddControllers();

// Конфигурируем CORS
builder.Services.ConfigureCors();

// Конфигурируем контекст базы данных
builder.Services.ConfigureDbContext(builder.Configuration);

// Регистрируем дополнительные сервисы
builder.Services.ConfigureServices();

// Конфигурация для AutoMapper
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper autoMapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(autoMapper);
// Конфигурация JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();
// Добавляем MediatR для обработки запросов и команд
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetProductsQuery).Assembly));

// Добавление Swagger для документации
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Настройка Razor Pages
builder.Services.AddRazorPages().AddRazorOptions(options =>
{
    options.PageViewLocationFormats.Add("/Pages/Shared/{0}.cshtml");
});
builder.Services.AddScoped<DatabaseSeeder>();  // Если нужно использовать для сеедера данных

var app = builder.Build();

// Обслуживание статических файлов из папки wwwroot
app.UseStaticFiles();  // Эта строка подключает wwwroot

// Вызываем middleware для заполнения базы данных
await app.UseDatabaseSeeder();  // Если необходимо для инициализации данных

// Конфигурация HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Применяем политику CORS
app.UseCors("CorsPolicy");

// Включаем маршрутизацию
app.UseRouting();

// Подключаем контроллеры (если они есть)
app.MapControllers();

// Используем Razor Pages
app.MapRazorPages();

app.MapGet("/", () => Results.Redirect("/Home/Index"));
app.MapFallbackToPage("/Home/Index"); // Указывает на страницу Home.cshtml как начальную
app.UseAuthentication(); // Если используется
app.UseAuthorization();  // Вызов авторизации
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

// Запуск приложения
app.Run();