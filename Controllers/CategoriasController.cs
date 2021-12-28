using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogo.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")] // Autenticação via JWT
    [Route("api/[Controller]")]
    [ApiController]
    [EnableCors("PermitirApiRequest")]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CategoriasController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet("produtos")] // Não esquece de modificar o metodo pra não dar conflito de endereço com o de biaxo
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasProdutos()
        {
            var categoria = await _uow.CategoriaRepository.GetCategoriasProdutos();
            var categoriaDto = _mapper.Map<List<CategoriaDTO>>(categoria);
            return categoriaDto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] CategoriasParameters categoriasParameters)
        {
            var categorias = await _uow.CategoriaRepository.GetCategorias(categoriasParameters);

            var metadata = new
            {
                categorias.TotalCount,
                categorias.PageSize,
                categorias.CurrentPage,
                categorias.TotalPages,
                categorias.HasNext,
                categorias.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var categoriaDto = _mapper.Map<List<CategoriaDTO>>(categorias);
            return categoriaDto;
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {
            var categoria = await _uow.CategoriaRepository.GetById(p => p.CategoriaId == id);
            if (categoria == null)
            {
                return NotFound($"A categoria com id = {id} não foi encontrado.");
            }

            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);
            return categoriaDto;
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> Post([FromBody] CategoriaDTO categoriaDto)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDto);

            _uow.CategoriaRepository.Add(categoria);
            await _uow.Commit();

            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

            //Retorna a rota onde encontrar a categoria adiocionada
            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoriaDTO);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult<Categoria>> Put(int id, [FromBody] CategoriaDTO categoriaDto)
        {
            // Validar se o id que estou passando via url é o mesmo que estou alterando
            if (id != categoriaDto.CategoriaId)
            {
                return BadRequest($"Não foi possivel atualizar a categoria com id = {id}.");
            }

            var categoria = _mapper.Map<Categoria>(categoriaDto);

            _uow.CategoriaRepository.Update(categoria);
            await _uow.Commit();
            return Ok($"A categoria com id = {id} foi atualizado com sucesso.");

        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult<CategoriaDTO>> Delete(int id)
        {
            var categoria = await _uow.CategoriaRepository.GetById(p => p.CategoriaId == id);
            if (categoria == null)
            {
                return NotFound($"A categoria com id = {id} não foi encontrado.");
            }

            _uow.CategoriaRepository.Delete(categoria);
            await _uow.Commit();

            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

            return categoriaDTO;

        }
    }
}
