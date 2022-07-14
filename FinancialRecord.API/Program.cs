using FinancialRecord.API.Context;
using FinancialRecord.API.Interfaces;
using FinancialRecord.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//DbContext Registrary
builder.Services.AddDbContext<ExperianDBContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:ExperianConnString"]));
//IUpload Registraty
builder.Services.AddScoped<IUploadFile, UploadFileService>();
builder.Services.AddScoped<IFinancialRecord, FinancialRecordService>();

//Enabling the cors policy to share the resources to other domains
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

//To store the files
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});
app.UseAuthorization();

app.MapControllers();

app.Run();
