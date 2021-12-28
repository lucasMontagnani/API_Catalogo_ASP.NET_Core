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
{/*
    [Route("api/[Controller]")]
    [ApiController]
    public class TestProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TestProdutosController(AppDbContext contexto)
        {
            _context = contexto;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))] //Aplicando Filtro Personalizado
        public async Task<ActionResult<IEnumerable<Produto>>> GetAsync()
        {
            try
            {
                //AsNoTracking para otimizar consultas (quando não há ações de modificações no banco)
                return await _context.Produtos.AsNoTracking().ToListAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar obeter os produtos do banco de dados.");
            }

        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public async Task<ActionResult<Produto>> GetAsync(int id)
        {
            try
            {
                //Localizar o produto por meio do id passado
                var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.ProdutoId == id); //AsNoTracking para otimizar consultas
                if (produto == null)
                {
                    return NotFound($"O produto com id = {id} não foi encontrado.");
                }
                return produto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar obeter o produto com id = {id}.");
            }

        }

        [HttpPost]
        public ActionResult<Produto> Post([FromBody] Produto produto)
        {
            try
            {
                _context.Produtos.Add(produto);
                _context.SaveChanges();

                //Retorna a rota onde encontrar o produto adiocionado
                return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto); // "ObterProduto" Vinculado com o HttpGet{id} especifico
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar adicionar um novo produto.");
            }

        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult<Produto> Put(int id, [FromBody] Produto produto)
        {
            try
            {
                // Validar se o id que estou passando via url é o mesmo que estou alterando
                if (id != produto.ProdutoId)
                {
                    return BadRequest($"Não foi possivel atualizar o produto com id = {id}.");
                }

                _context.Entry(produto).State = EntityState.Modified; // Para alterar o estado da entidade para modified
                _context.SaveChanges();
                return Ok($"O produto com id = {id} foi atualizado com sucesso.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar o produto com id = {id}.");
            }

        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<Produto> Delete(int id)
        {
            try
            {
                var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
                //var produto = _context.Produtos.Find(id);
                if (produto == null)
                {
                    return NotFound($"O produto com id = {id} não foi encontrado.");
                }

                _context.Produtos.Remove(produto);
                _context.SaveChanges();
                return produto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar excluir o produto de id = {id}.");
            }

        }
    }*/
}
