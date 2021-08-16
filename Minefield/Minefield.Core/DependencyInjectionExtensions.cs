using Microsoft.Extensions.DependencyInjection;
using Minefield.Core.Contracts;

namespace Minefield.Core
{
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Add functionality to play a minefield hgame
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMinefieldGame(this IServiceCollection services)
        {   
            services.AddSingleton(new Configuration());
            services.AddSingleton<IBoardCreator, SimpleBoardCreator>();
            services.AddSingleton<IMineLayer, RandomMineLayer>();
            services.AddSingleton<IGame, Game>();
            return services;
        }
    }
}
