using api_bookStore.App.Modules.Auth.DTO;
using api_bookStore.App.Modules.Auth.Entitiy;
using api_bookStore.App.Modules.Author.DTO;
using api_bookStore.App.Modules.Author.Entity;
using api_bookStore.App.Modules.Book.DTO;
using api_bookStore.App.Modules.Book.Entity;
using api_bookStore.App.Modules.Category.DTO;
using api_bookStore.App.Modules.Category.Entity;
using api_bookStore.App.Modules.Inventory.DTO;
using api_bookStore.App.Modules.Inventory.Entity;
using api_bookStore.App.Modules.Sale.DTO;
using api_bookStore.App.Modules.Sale.Entity;
using api_bookStore.App.Modules.User.DTO;
using api_bookStore.App.Modules.User.Entity;
using AutoMapper;

namespace api_bookStore.App.Services.AutoMapper
{
    /// <summary>
    /// Configura o mapeamento entre entidades e DTOs utilizando o AutoMapper.
    /// </summary>
    public class AutoMapperService : Profile
    {
        /// <summary>
        /// Construtor que define os mapeamentos entre as entidades e os DTOs.
        /// </summary>
        public AutoMapperService()
        {
            CreateMap<UserEntity, UserDTO>().ReverseMap();
            CreateMap<AuthEntity, AuthDTO>();
            CreateMap<AuthorEntity, AuthorDTO>().ReverseMap();
            CreateMap<CategoryEntity, CategoryDTO>().ReverseMap();
            CreateMap<InventoryEntity, InventoryDTO>().ReverseMap();
            CreateMap<SaleEntity, SaleDTO>()
            .ForMember(dest => dest.SaleXBooks, opt => opt.MapFrom(src => src.SaleXBooks.Select(src => src.Book)))
            .ReverseMap();
            CreateMap<BookEntity, BookDTO>()
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity.Quantity))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
            .ReverseMap();

        }
    }
}