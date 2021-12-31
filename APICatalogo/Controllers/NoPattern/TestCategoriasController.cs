using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogo.Controllers.NoPattern
{
    /*
    [Route("api/[Controller]")]
    [ApiController]
    public class TestCategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TestCategoriasController(AppDbContext contexto)
        {
            _context = contexto;
        }

        [HttpGet("produtos")] // Não esquece de modificar o metodo pra não dar conflito de endereço com o de biaxo
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutosAsync()
        {
            // Metodo include relaciona os registros das entidades e retorna (tipo um inner join)
            return await _context.Categorias.Include(x => x.Produtos).ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetAsync()
        {
            try
            {
                //AsNoTracking para otimizar consultas (quando não há ações de modificações no banco)
                return await _context.Categorias.AsNoTracking().ToListAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar obeter as categorias do banco de dados.");
            }
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public async Task<ActionResult<Categoria>> GetAsync(int id)
        {
            try
            {
                //Localizar o produto por meio do id passado
                var categoria = await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.CategoriaId == id); //AsNoTracking para otimizar consultas
                if (categoria == null)
                {
                    return NotFound($"A categoria com id = {id} não foi encontrada.");
                }
                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar obeter as categorias do banco de dados.");
            }

        }

        [HttpPost]
        public ActionResult<Categoria> Post([FromBody] Categoria categoria)
        {
            try
            {
                _context.Categorias.Add(categoria);
                _context.SaveChanges();

                //Retorna a rota onde encontrar o produto adiocionado
                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria); // "ObterProduto" Vinculado com o HttpGet{id} especifico
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar cirar uma nova categoria.");
            }

        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult<Categoria> Put(int id, [FromBody] Categoria categoria)
        {
            try
            {
                // Validar se o id que estou passando via url é o mesmo que estou alterando
                if (id != categoria.CategoriaId)
                {
                    return BadRequest($"Não foi possivel atualizar a categoria com id = {id}.");
                }

                _context.Entry(categoria).State = EntityState.Modified; // Para alterar o estado da entidade para modified
                _context.SaveChanges();
                return Ok($"A categoria com id = {id} foi atualizada com sucesso.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar a categoria com id = {id}.");
            }

        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<Categoria> Delete(int id)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
                //var categoria = _context.Categorias.Find(id);
                if (categoria == null)
                {
                    return NotFound($"A categoria com id = {id} não foi encontrada.");
                }

                _context.Categorias.Remove(categoria);
                _context.SaveChanges();
                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar excluir a categoria de id = {id}.");
            }

        }
    }*/
}
