

using BookTest.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});
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
   .ReverseMap();
    cfg.CreateMap<Author, AuthorReadDTO>()
    .ForMember(dest => dest.BookIds,
               opt => opt.MapFrom(src => src.AuthorBooks.Select(ab => ab.BookId)));

    // Map Book to BookDTO
    cfg.CreateMap<Book, BookDTO>()
    .ReverseMap();
    cfg.CreateMap<Book, BookReadDTO>()
    .ForMember(dest => dest.AuthorIds,
               opt => opt.MapFrom(src => src.AuthorBooks.Select(ab => ab.AuthorId)));

    cfg.CreateMap<AuthorBook, AuthorBookDTO>().ReverseMap();

});


    IMapper mapper = mapperConfig.CreateMapper();
    services.AddSingleton(mapper);
}
