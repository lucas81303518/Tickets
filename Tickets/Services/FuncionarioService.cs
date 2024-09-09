using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Tickets.Data;
using Tickets.Data.DTO;
using Tickets.Models;

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

        public async Task AdicionarFuncionario(CreateFuncionarioDto createFuncionarioDto)
        {
            var funcionarioModel = _mapper.Map<Funcionario>(createFuncionarioDto);
            funcionarioModel.Situacao = 'A';
            funcionarioModel.DataAlteracao = DateTime.UtcNow;
            try
            {
                await _context.Funcionarios.AddAsync(funcionarioModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_Funcionario_Cpf"))
                {
                    throw new ArgumentException("CPF já existe no sistema.");
                }

                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao salvar as alterações {ex.Message}");
            }
        }

        public async Task EditarFuncionario(int id, UpdateFuncionarioDto updateFuncionarioDto)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
                throw new KeyNotFoundException($"Funcionário id: {id} não existe!");

            try
            {
                _mapper.Map(updateFuncionarioDto, funcionario);
                funcionario.DataAlteracao = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_Funcionario_Cpf"))
                {
                    throw new ArgumentException("CPF já existe no sistema.");
                }

                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao salvar as alterações {ex.Message}");
            }
        }

        public async Task<IEnumerable<Funcionario>> RecuperarFuncionarios()
        {
            return await _context.Funcionarios.ToListAsync();            
        }

        public async Task<Funcionario> RecuperarFuncionario(int id)
        {
            var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == id);
            if (funcionario == null)
                throw new KeyNotFoundException($"Funcionário id: {id} não existe!");
            return funcionario;
        }
    }
}
