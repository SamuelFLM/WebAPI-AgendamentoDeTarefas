using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEBAPI_AgendamentoTarefa.Controllers;
using WEBAPI_AgendamentoTarefa.Models;

namespace WEBAPI_AgendamentoTarefa.Context
{
    public class OrganizadorContext : DbContext
    {
        public OrganizadorContext(DbContextOptions<OrganizadorContext> options) : base(options) { }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}