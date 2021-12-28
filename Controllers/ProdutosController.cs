using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogo.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ProdutosController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery] ProdutosParameters produtosParameters)
        {
            //var produtos = _uow.ProdutoRepository.Get().ToList(); // Sem Paginação
            var produtos = await _uow.ProdutoRepository.GetProdutos(produtosParameters); // Com Paginação

            var metadata = new
            {
                produtos.TotalCount,
                produtos.PageSize,
                produtos.CurrentPage,
                produtos.TotalPages,
                produtos.HasNext,
                produtos.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);
            return produtosDto;
        }

        [HttpGet("menorpreco")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosPrecos()
        {
            var produtos = await _uow.ProdutoRepository.GetProdutosPorPreco();
            var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);
            return produtosDto;
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public async Task<ActionResult<ProdutoDTO>> Get(int id)
        {
            //Localizar o produto por meio do id passado
            var produto = await _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);
            if (produto == null)
            {
                return NotFound($"O produto com id = {id} não foi encontrado.");
            }
            var produtosDto = _mapper.Map<ProdutoDTO>(produto);
            return produtosDto;
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Post([FromBody]ProdutoDTO produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);

            _uow.ProdutoRepository.Add(produto);
            await _uow.Commit();

            var produtosDTO = _mapper.Map<ProdutoDTO>(produto);

            //Retorna a rota onde encontrar o produto adiocionado
            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produtosDTO); // "ObterProduto" Vinculado com o HttpGet{id} especifico 
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult<Produto>> Put(int id, [FromBody] ProdutoDTO produtoDto)
        {
            // Validar se o id que estou passando via url é o mesmo que estou alterando
            if (id != produtoDto.ProdutoId)
            {
                return BadRequest($"Não foi possivel atualizar o produto com id = {id}.");
            }

            var produto = _mapper.Map<Produto>(produtoDto);

            _uow.ProdutoRepository.Update(produto);
            await _uow.Commit();
            return Ok($"O produto com id = {id} foi atualizado com sucesso.");
            
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult<ProdutoDTO>> Delete(int id)
        {
            var produto = await _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);
            if (produto == null)
            {
                return NotFound($"O produto com id = {id} não foi encontrado.");
            }

            _uow.ProdutoRepository.Delete(produto);
            await _uow.Commit();

            var produtoDto = _mapper.Map<ProdutoDTO>(produto);

            return produtoDto;
       
        }
    }
}
