using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Net.Mime;
using System.Reflection;
using System.Text;

/* -- builder setting -- */
var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("CleanArchitechture.Application")
    );
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// Add config
AddAuthentications(builder);
AddControllers(builder);
AddSwaggers(builder);
AddServices(builder);


/* -- application setting -- */
var app = builder.Build();

// Config HTTP
app.UseSwagger().UseSwaggerUI(options =>
{
    options.DefaultModelsExpandDepth(-1);
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = "swagger";
});
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Status code
app.UseStatusCodePages(async context =>
{
    BaseResponse response = null;
    switch (context.HttpContext.Response.StatusCode)
    {
        case 401:
            response = new BaseResponse()
            {
                code = (int)HttpStatusCode.Unauthorized,
                error = (int)ErrorCodes.UNAUTHORIZED,
                message = EnumHelper<ErrorCodes>.GetDisplayValue(ErrorCodes.UNAUTHORIZED)
            };
            break;
        case 403:
            response = new BaseResponse()
            {
                code = (int)HttpStatusCode.Forbidden,
                error = (int)ErrorCodes.FORBIDDEN,
                message = EnumHelper<ErrorCodes>.GetDisplayValue(ErrorCodes.FORBIDDEN)
            };
            break;
        case 404:
            response = new BaseResponse()
            {
                code = (int)HttpStatusCode.NotFound,
                error = (int)ErrorCodes.NOT_FOUND,
                message = EnumHelper<ErrorCodes>.GetDisplayValue(ErrorCodes.NOT_FOUND)
            };
            break;
        case 405:
            response = new BaseResponse()
            {
                code = (int)HttpStatusCode.MethodNotAllowed,
                error = (int)ErrorCodes.METHOD_NOT_ALLOWED,
                message = EnumHelper<ErrorCodes>.GetDisplayValue(ErrorCodes.METHOD_NOT_ALLOWED)
            };
            break;
        case 415:
            response = new BaseResponse()
            {
                code = (int)HttpStatusCode.UnsupportedMediaType,
                error = (int)ErrorCodes.UNSUPPORTED_MEDIA_TYPE,
                message = EnumHelper<ErrorCodes>.GetDisplayValue(ErrorCodes.UNSUPPORTED_MEDIA_TYPE)
            };
            break;
    }

    context.HttpContext.Response.ContentType = "application/json";
    context.HttpContext.Response.StatusCode = 200;
    await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
});

app.UseMiddleware<ApplicationMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


/* -- config -- */
// Config authentication
static void AddAuthentications(WebApplicationBuilder builder)
{
    var tokenConfigSection = builder.Configuration.GetSection(JwtTokenConfig.ConfigName) ??
        throw new Exception("Let's check token of config in appsettings.json");

    builder.Services.Configure<JwtTokenConfig>(tokenConfigSection);
    builder.Services.AddSingleton(tokenConfigSection.Get<JwtTokenConfig>());
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddTransient<IJwtHelper, JwtHelper>();

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }
    ).AddJwtBearer(options => {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidAudience = builder.Configuration.GetValue<string>(JwtTokenKeys.JWT_VALID_AUDIENCE),
            ValidIssuer = builder.Configuration.GetValue<string>(JwtTokenKeys.JWT_VALID_ISSUER),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>(JwtTokenKeys.JWT_SECRET)))
        };
    });
}

// Config controller
static void AddControllers(WebApplicationBuilder builder)
{
    builder.Services.Configure<FormOptions>(x =>
    {
        x.ValueLengthLimit = int.MaxValue;
        x.MultipartBodyLengthLimit = int.MaxValue;
    });
    builder.Services.AddControllers();

    // Custom response
    builder.Services.AddMvc().ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressMapClientErrors = true;
        options.InvalidModelStateResponseFactory = context =>
        {
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            var errors = string.Join('\n', context.ModelState.Values.Where(v => v.Errors.Count > 0).SelectMany(v => v.Errors).Select(v => v.ErrorMessage));
            var body = new BaseResponse()
            {
                code = (int)HttpStatusCode.BadRequest,
                error = (int)ErrorCodes.BAD_REQUEST,
                message = errors
            };
            var result = new OkObjectResult(body);
            result.ContentTypes.Add(MediaTypeNames.Application.Json);
            result.ContentTypes.Add(MediaTypeNames.Application.Xml);
            return result;
        };
    });

    builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
}

// Config swagger
static void AddSwaggers(WebApplicationBuilder builder)
{
    var version = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version ?? "Test";
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = $"CleanArchitechture.API, Phiên bản {version}", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

// Register service
static void AddServices(WebApplicationBuilder builder)
{
    // Middleware
    builder.Services.AddTransient<ApplicationMiddleware>();
}