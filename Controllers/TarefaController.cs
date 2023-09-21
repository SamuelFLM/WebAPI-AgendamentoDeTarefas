using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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





        




        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            return Ok(_context.Tarefas);
        }
        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            var tarefa = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));
            return Ok(tarefa);
        }
        //Data
        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data == data.Date);
            return Ok(tarefa);
        }
        //Status
        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            var tarefa = _context.Tarefas.Where(x => x.Status == status);
            return Ok(tarefa);
        }
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa == null)
                return NotFound();
            return Ok(tarefa);
        }
        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia." });
            _context.Add(tarefa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Tarefa _tarefa)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia." });

            tarefa.Titulo = _tarefa.Titulo;
            tarefa.Descricao = _tarefa.Descricao;
            tarefa.Data = _tarefa.Data;
            tarefa.Status = _tarefa.Status;
            _context.Tarefas.Update(_tarefa);
            _context.SaveChanges();
            return Ok(tarefa);
        }
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
                return NotFound();

            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();
            return NoContent();
        }
    }
}