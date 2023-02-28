using Microsoft.EntityFrameworkCore;
using WebApplicationAPI.Data;
using WebApplicationAPI.Repository.IRepository;
using WebApplicationAPI.shared;

var builder = WebApplication.CreateBuilder(args);


//add cross origin

#region Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("custompolicy", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod() );
});
#endregion 


#region Configure mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
#endregion

#region Configure IRepositories
 builder.Services.AddTransient<ICountryRepository , CountryRepository>();
#endregion

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region configure dbcontext with table
builder.Services.AddDbContext<PortalDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("webapplicationapiConnectionString"))
);
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("custompolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
