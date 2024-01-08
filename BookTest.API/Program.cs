



using BookTest.Data.Authentication;

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

// Configure JwtSettings
var jwtSettings = new JwtSettings();
builder.Configuration.Bind(nameof(JwtSettings), jwtSettings);
builder.Services.AddSingleton(jwtSettings);

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
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

});


    IMapper mapper = mapperConfig.CreateMapper();
    services.AddSingleton(mapper);
}
