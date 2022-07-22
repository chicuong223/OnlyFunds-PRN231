using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Implementations;
using OnlyFundsAPI.DataAccess.Interfaces;
using System.Text;
using System.Text.Json.Serialization;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddOData(options =>
            {
                options.Select().SetMaxTop(20).Filter().OrderBy().Count().Expand().AddRouteComponents("odata", GetEdmModel());
            }).AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;
            // .AddJsonOptions(x =>
            // {
            //     x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            // });
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            //add JWT Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = Configuration["Jwt:Audience"],
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            // services.AddScoped<IUserRepository, UserRepository>();
            services.AddDbContext<OnlyFundsDBContext>();

            services.AddScoped<IRepoWrapper, RepoWrapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Bookmark>("Bookmarks");
            builder.EntitySet<Comment>("Comments");
            builder.EntitySet<CommentLike>("CommentLikes");
            builder.EntitySet<Follow>("Follows");
            builder.EntitySet<Notification>("Notifications");
            builder.EntitySet<PostLike>("PostLikes");
            builder.EntitySet<PostTag>("Tags");
            builder.EntitySet<User>("Users");
            builder.EntitySet<Report>("Reports");
            builder.EntitySet<PostTagMap>("PostTagMaps");
            builder.EntitySet<Post>("Posts");
            builder.EntityType<Post>().HasKey(p => p.PostID);
            builder.EntityType<Follow>().HasKey(follow => new { follow.FollowerID, follow.FolloweeID });
            builder.EntityType<Bookmark>().HasKey(bookmark => new { bookmark.UserID, bookmark.PostID });
            builder.EntityType<PostLike>().HasKey(pLike => new { pLike.UserID, pLike.PostID });
            builder.EntityType<PostTag>().HasKey(tag => tag.TagID);
            builder.EntityType<CommentLike>().HasKey(like => new { like.UserID, like.CommentID });
            builder.EntityType<PostLike>().HasKey(like => new { like.PostID, like.UserID });
            builder.EntityType<PostTagMap>().HasKey(tagMap => new { tagMap.TagID, tagMap.PostID });

            builder.EnumType<FileType>();
            builder.EnumType<ReportTypes>();
            builder.EnumType<ReportStatus>();

            return builder.GetEdmModel();

            //IEdmModel model = builder.GetEdmModel();
            //model.AddElement(fileType);
        }
    }
}