using DevQuestions.Application;

namespace DevQuestions.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddProgrammDependencies(this IServiceCollection services)
    {
        return services.AddWebDependencies().AddApplication();
    }
    
    private static IServiceCollection AddWebDependencies(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
}