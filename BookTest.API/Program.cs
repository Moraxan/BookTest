



using BookTest.Data.Authentication;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = null;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BookContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookConnection")));
builder.Services.AddScoped<IDbService, DbService>();


// Configure AutoMapper
ConfigureAutomapper(builder.Services);



// Configure JwtSettings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));


// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var serviceProvider = builder.Services.BuildServiceProvider();
    var jwtSettings = serviceProvider.GetService<IOptions<JwtSettings>>().Value;

    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = false,
        ValidateLifetime = true
    };
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

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

    cfg.CreateMap<UserDTO, User>().ReverseMap();
    cfg.CreateMap<User, UserReadDTO>().ReverseMap();

});


    IMapper mapper = mapperConfig.CreateMapper();
    services.AddSingleton(mapper);
}
