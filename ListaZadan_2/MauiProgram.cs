using ListaZadan_2.ModelView;
using ListaZadan_2.Services;
using Microsoft.Extensions.Logging;

namespace ListaZadan_2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<SQLiteDB>();
            builder.Services.AddSingleton<UserTasksModelView>();
            builder.Services.AddSingleton<Tasks>();
            builder.Services.AddSingleton<UserModelView>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<ForgotPage>();
            return builder.Build();
        }
    }
}
