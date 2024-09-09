using AutoMapper;
using Tickets.Data.DTO;
using Tickets.Models;

namespace Tickets.Profiles
{
    public class FuncionarioProfile: Profile
    {
        public FuncionarioProfile()
        {
            CreateMap<UpdateFuncionarioDto, Funcionario>();
            CreateMap<CreateFuncionarioDto, Funcionario>(); 
        }
    }
}
