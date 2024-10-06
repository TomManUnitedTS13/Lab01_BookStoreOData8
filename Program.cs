using Lab01_BookStoreOData8.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

internal class Program 
{
    private static void Main(String[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<BookStoreContext>(opt => opt.UseInMemoryDatabase("BookLists"));
        builder.Services.AddControllers();

        builder.Services.AddControllers().AddOData(option => option.Select().Filter()
        .Count().OrderBy().Expand().SetMaxTop(100)
        .AddRouteComponents("odata", GetEdmModel()));
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
        app.UseODataBatching();

        app.UseRouting();

        app.Use(next => async context =>
        {
            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                IODataRoutingMetadata metadata = endpoint.Metadata?.GetMetadata<IODataRoutingMetadata>();
                if (metadata != null)
                {
                    IEnumerable<string> templates = metadata.Template.GetTemplates();
                    // Bạn có thể xử lý templates ở đây nếu cần
                }
            }
            await next(context);
        });
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
    private static IEdmModel GetEdmModel()
    {
        ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

        builder.EntitySet<Book>("Books");

        builder.EntitySet<Press>("Presses");

        return builder.GetEdmModel();
    }
}