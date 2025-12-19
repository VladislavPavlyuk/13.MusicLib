using AutoMapper;
using MusicLib.BLL.DTO;
using MusicLib.DAL.Entities;

namespace MusicLib.BLL.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Song, SongDTO>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre != null ? src.Genre.Title : null))
                .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Artist != null ? src.Artist.Name : null))
                .ForMember(dest => dest.Audio, opt => opt.MapFrom(src => src.Audio != null ? src.Audio.FileName : null));

            CreateMap<SongDTO, Song>();

            CreateMap<Artist, ArtistDTO>();
            CreateMap<ArtistDTO, Artist>();

            CreateMap<Genre, GenreDTO>();
            CreateMap<GenreDTO, Genre>();

            CreateMap<Audio, AudioDTO>();
            CreateMap<AudioDTO, Audio>();

            CreateMap<Role, RoleDTO>();
            CreateMap<RoleDTO, Role>();

            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role != null ? src.Role.Title : null));
            CreateMap<UserDTO, User>();
        }
    }
}
