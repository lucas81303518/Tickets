using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Tickets.Data;
using Tickets.Data.DTO;
using Tickets.Models;
using static Tickets.Models.ErrorMessages;

namespace Tickets.Services
{
    public class FuncionarioService
    {
        private readonly TicketsContext _context;
        private readonly IMapper _mapper;

        public FuncionarioService(TicketsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        private bool IsUniqueCpfViolation(Exception ex)
        {
            return ex.InnerException != null && ex.InnerException.Message.Contains("IX_Funcionario_Cpf");
        }
        public async Task<ResponseClient<object>> AdicionarFuncionario(CreateFuncionarioDto createFuncionarioDto)
        {
            var funcionarioModel = _mapper.Map<Funcionario>(createFuncionarioDto);
            funcionarioModel.Situacao = 'A';
            funcionarioModel.DataAlteracao = DateTime.UtcNow;
            try
            {
                await _context.Funcionarios.AddAsync(funcionarioModel);
                await _context.SaveChangesAsync();
                return new ResponseClient<object>(true, ErrorCode.OperacaoBemSucedida);
            }
            catch (Exception ex)
            {
                if(ex is DbUpdateException)
                {
                    if (IsUniqueCpfViolation(ex))                   
                        return new ResponseClient<object>(false, ErrorCode.CpfJaExiste);                    
                }
                return new ResponseClient<object>(false, ErrorCode.ErroAoSalvar, ex.Message);
            }            
        }

        public async Task<ResponseClient<object>> EditarFuncionario(int id, UpdateFuncionarioDto updateFuncionarioDto)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
                return new ResponseClient<object>(false, ErrorCode.FuncionarioNaoEncontrado);

            try
            {
                _mapper.Map(updateFuncionarioDto, funcionario);
                funcionario.DataAlteracao = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return new ResponseClient<object>(true, ErrorCode.OperacaoBemSucedida);
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException)
                {
                    if (IsUniqueCpfViolation(ex))
                        return new ResponseClient<object>(false, ErrorCode.CpfJaExiste);
                }
                return new ResponseClient<object>(false, ErrorCode.ErroAoSalvar, ex.Message);
            }
        }

        public async Task<ResponseClient<IEnumerable<Funcionario>>> RecuperarFuncionarios()
        {
            try
            {
                var funcionarios = await _context.Funcionarios.ToListAsync();
                return new ResponseClient<IEnumerable<Funcionario>>(true, funcionarios);
            }
            catch (Exception ex)
            {
                return new ResponseClient<IEnumerable<Funcionario>>(false, ErrorCode.ErroAoConsultar, ex.Message);
            }
        }

        public async Task<ResponseClient<Funcionario>> RecuperarFuncionario(int id)
        {
            try
            {
                var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == id);
                if (funcionario == null)
                    return new ResponseClient<Funcionario>(false, ErrorCode.FuncionarioNaoEncontrado);

                return new ResponseClient<Funcionario>(true, funcionario);
            }
            catch (Exception ex)
            {
                return new ResponseClient<Funcionario>(false, ErrorCode.ErroAoConsultar, ex.Message);
            }
        }

    }
}
