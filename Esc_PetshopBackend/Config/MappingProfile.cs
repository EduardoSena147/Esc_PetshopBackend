using AutoMapper;
using Esc_PetshopBackend.Data.Entities;
using Esc_PetshopBackend.DTOs;

namespace Esc_PetshopBackend
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UsuarioCreateDto, Usuario>()
                .ForMember(dest => dest.DataCriacao, opt => opt.Ignore()) // Será definido pelo banco
                .ForMember(dest => dest.Ativo, opt => opt.Ignore()); // Usará o valor padrão

            CreateMap<UsuarioUpdateDto, Usuario>()
                .ForMember(dest => dest.DataCriacao, opt => opt.Ignore()) // Não atualizamos a data de criação
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // Atualiza apenas campos não nulos

            CreateMap<Usuario, UsuarioDto>();
            CreateMap<Cargo, CargoDto>();
            CreateMap<CargoCreateDto, Cargo>();
            CreateMap<CargoUpdateDto, Cargo>();
        }
    }
}