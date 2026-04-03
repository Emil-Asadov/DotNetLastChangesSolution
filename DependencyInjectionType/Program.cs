
using DependencyInjectionType.Extensions;
using System.Text;

namespace DependencyInjectionType
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Dependency Injection
            //builder.Services.AddTransient<Test1>();
            //builder.Services.AddTransient<Test2>();

            //builder.Services.AddScoped<Test1>();
            //builder.Services.AddScoped<Test2>();

            //builder.Services.AddSingleton<Test1>();
            //builder.Services.AddSingleton<Test2>();

            builder.Services.MyDependencyInjection(); //Dependency Injection-lari Extension metodun icine yiqmisam
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapGet("test", (Test1 tst1, Test2 tst2) =>
            {
                //Test1 tst1 = new(); //DI tetbiq olunduqu ucun deaktiv edilib
                var id1 = tst1.Id;

                //Test2 tst2 = new(tst1); //DI tetbiq olunduqu ucun deaktiv edilib
                var id2 = tst2.GetGuid();

                var str = new StringBuilder($"Id1:{id1}").AppendLine().Append($"Id2:{id2}");
                return Results.Ok(str.ToString());
            });

            #region Garbage Collector-n manual cagrilmasi
            //app.MapGet("gc", () =>
            //{
            //    GC.Collect();
            //    GC.WaitForPendingFinalizers();
            //    GC.Collect();

            //    return Results.Ok("GC successfully!");
            //});
            #endregion

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }

    public class Test1
    {
        public Guid Id { get; set; }
        public Test1()
        {
            Id = Guid.NewGuid();
        }
    }

    public class Test2(Test1 test1)
    {
        //Class-a giris parameter-i olaraq verdiyime gore Construktor-u bagladim
        //private readonly Test1 _test1;

        //public Test2(Test1 test1)
        //{
        //    _test1 = test1;
        //}
        public Guid GetGuid()
        {
            //var id = _test1.Id;
            var id = test1.Id;

            return id;
        }
    }
}
