using MusicLib.BLL.DTO;
using MusicLib.DAL.Entities;
using MusicLib.DAL.Interfaces;
using MusicLib.BLL.Infrastructure;
using MusicLib.BLL.Interfaces;
using AutoMapper;

namespace MusicLib.BLL.Services
{
    public class RoleService : IRoleService
    {
        IUnitOfWork Database { get; set; }
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public async Task CreateRole(RoleDTO roleDto)
        {
            var role = new Role
            {
                Id = roleDto.Id,
                Title = roleDto.Title
            };
            await Database.Roles.Create(role);

            await Database.Save();
        }

        public async Task UpdateRole(RoleDTO roleDto)
        {
            var role = new Role
            {
                Id = roleDto.Id,
                Title = roleDto.Title
            };
            Database.Roles.Update(role);

            await Database.Save();
        }

        public async Task DeleteRole(int id)
        {
            try 
            { 
                await Database.Roles.Delete(id);

                await Database.Save();            
            } 
            catch (Exception ex)
            {

            }
}

        public async Task<RoleDTO> GetRole(int id)
        {
            var role = await Database.Roles.Get(id);

            if (role == null)
                throw new ValidationException("Wrong role!", "");

            return new RoleDTO
            {
                Id = role.Id,
                Title = role.Title
            };
        }

        // Automapper позволяет проецировать одну модель на другую, что позволяет сократить объемы кода и упростить программу.
        public async Task<IEnumerable<RoleDTO>> GetRoles()
        {
            var roles = await Database.Roles.GetAll();
            return _mapper.Map<IEnumerable<Role>, IEnumerable<RoleDTO>>(roles);
        }

        public async Task<IEnumerable<RoleDTO>> GetSortedItemsAsync(string sortOrder)
        {
            var roles = await Database.Roles.GetSortedAsync(sortOrder);
            return _mapper.Map<IEnumerable<Role>, IEnumerable<RoleDTO>>(roles);
        }
    }
}
