using MusicLib.BLL.DTO;
using MusicLib.DAL.Entities;
using MusicLib.DAL.Interfaces;
using MusicLib.BLL.Infrastructure;
using MusicLib.BLL.Interfaces;
using AutoMapper;

namespace MusicLib.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public async Task CreateUser(UserDTO userDto)
        {
            var user = new User
            {
                Id = userDto.Id,
                Email = userDto.Email,
                Password = userDto.Password,
                Salt = userDto.Salt,
                RoleId = userDto.RoleId
            };
            await Database.Users.Create(user);

            await Database.Save();
        }

        public async Task UpdateUser(UserDTO userDto)
        {
            var user = new User
            {
                Id = userDto.Id,
                Email = userDto.Email,
                Password = userDto.Password,
                Salt = userDto.Salt,
                RoleId = userDto.RoleId
            };
            Database.Users.Update(user);

            await Database.Save();
        }

        public async Task DeleteUser(int id)
        {
            await Database.Users.Delete(id);

            await Database.Save();
        }

        public async Task<UserDTO> GetUser(int id)
        {
            var user = await Database.Users.Get(id);

            if (user == null)
                throw new ValidationException("Wrong user!", "");

            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Salt = user.Salt,
                RoleId = user.RoleId,
                Role = user.Role?.Title
            };
        }
        public async Task<UserDTO> GetUserByEmail(string email)
        {
            var user = await Database.Users.Get(email);
            if (user == null)
            {
                UserDTO nullUser = new UserDTO();
                return nullUser;
            }

            return new UserDTO
            {
                Id = user.Id,
                Password = user.Password,
                Salt = user.Salt,
                Email = user.Email,
                RoleId = user.RoleId,
                Role = user.Role?.Title
            };
        }

        // Automapper позволяет проецировать одну модель на другую, что позволяет сократить объемы кода и упростить программу.

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var users = await Database.Users.GetAll();
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);
        }

        public async Task<IEnumerable<UserDTO>> GetSortedItemsAsync(string sortOrder)
        {
            return (IEnumerable<UserDTO>)await Database.Users.GetSortedAsync(sortOrder);
        }

    }
}
