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
            funcionarioModel.DataAlteracao = DateTime.Now;
            funcionarioModel.Situacao = 'A';            
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
                funcionario.DataAlteracao = DateTime.Now;                
                _mapper.Map(updateFuncionarioDto, funcionario);                
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

        public async Task<ResponseClient<object>> ExisteAlgumFuncionarioCadastrado()
        {
            try
            {
                var retorno = await _context.Funcionarios.FirstOrDefaultAsync();
                if (retorno == null)
                    return new ResponseClient<object>(false, ErrorCode.NaoExisteNenhumFuncionarioCadastrado);
                return new ResponseClient<object>(true, retorno);
            }
            catch (Exception ex)
            {
                return new ResponseClient<object>(false, ErrorCode.ErroAoConsultar, ex.Message);
            }
        }

        public async Task<ResponseClient<IEnumerable<Funcionario>>> RecuperarFuncionarios(bool SomenteAtivos = false)
        {
            try
            {
                var query = _context.Funcionarios.AsQueryable();

                if (SomenteAtivos)                
                    query = query.Where(f => f.Situacao == 'A');                

                var funcionarios = await query.ToListAsync();

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
