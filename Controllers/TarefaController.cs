using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEBAPI_AgendamentoTarefa.Context;
using WEBAPI_AgendamentoTarefa.Models;

namespace WEBAPI_AgendamentoTarefa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }
        [HttpGet()]
        public IActionResult Obter()
        {
            return Ok(_context.Tarefas);
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            var contextBanco = _context.Tarefas.Find(tarefa.Id);
            if (contextBanco == null)
            {
                if (tarefa.Data == DateTime.MinValue)
                    return BadRequest(new { Erro = "A data da tarefa não pode ser vazia." });
                _context.Add(tarefa);
                _context.SaveChanges();
                return Ok();
                // return CreatedAtAction(nameof());
            }
            return BadRequest(new { Erro = "Úsuario já existente." });
        }
    }
}