using AutoMapper;
using WebApiKor;
using WebApiKor.Models;


namespace WebApiKor.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
      
     
        protected override void Configure()
        {
            Mapper.CreateMap<UsuarioViewModel, usuario>();

          
        }


    }
}