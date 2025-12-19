using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MusicLib.DAL.EF;
using AutoMapper;


namespace MusicLib.BLL.Infrastructure
{
    public static class MusicLibExtensions
    {
        public static void AddMusicLibContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<MusicLibContext>(options => options.UseSqlServer(connection));
        }

        public static void AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}
