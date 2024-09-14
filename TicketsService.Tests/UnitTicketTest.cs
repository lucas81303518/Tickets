using Moq;
using AutoMapper;
using Tickets.Data;
using Tickets.Services;
using Tickets.Data.DTO;
using Tickets.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Tickets.Profiles;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

public class TicketServiceTests
{
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<DbSet<TicketEntregue>> _mockTicketSet;
    private readonly Mock<TicketsContext> _mockContext;
    private readonly TicketService _ticketService;

    public TicketServiceTests()
    {
        _mockMapper = new Mock<IMapper>();
        _mockTicketSet = new Mock<DbSet<TicketEntregue>>();
        _mockContext = new Mock<TicketsContext>();
        _ticketService = new TicketService(_mockContext.Object, _mockMapper.Object);
 
        _mockContext.Setup(c => c.Set<TicketEntregue>()).Returns(_mockTicketSet.Object);
    }

    [Fact]
    public async Task AdicionarTicket_DeveRetornarFalseDadosInvalidos()
    {
        var createTicket = new CreateTicket 
        { 

        };                
      
        var result = await _ticketService.AdicionarTicket(createTicket);

        //Assert.True(result);        
        _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
    }
}
