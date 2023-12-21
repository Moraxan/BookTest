

using BookTest.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BookContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookConnection")));

builder.Services.AddScoped<IDbService, DbService>();


// Configure AutoMapper
ConfigureAutomapper(builder.Services);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void ConfigureAutomapper(IServiceCollection services)
{
    var mapperConfig = new MapperConfiguration(cfg =>
    {
        // Map Author to AuthorDTO
        cfg.CreateMap<Author, AuthorDTO>()
           .ForMember(dest => dest.BookIds, act => act.MapFrom(src => src.AuthorBooks.Select(ab => ab.Book.Id)));

        // Map Book to BookDTO
        cfg.CreateMap<Book, BookDTO>()
           .ForMember(dest => dest.AuthorIds, act => act.MapFrom(src => src.AuthorBooks.Select(ab => ab.Author.Id)));
    });


    IMapper mapper = mapperConfig.CreateMapper();
    services.AddSingleton(mapper);
}
