﻿using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria.Server.Repositorio.AndresRepositorios;
using Microsoft.AspNetCore.Mvc;
using Concesionaria2024.Shared.DTO.AndresDTO;
using Concesionaria2024.Shared.DTO.GinoDTO;
using Concesionaria2024.Shared.DTO.FacundoDTO.Concesionaria2024.Shared.DTO.FacundoDTO;

namespace Concesionaria.Server.Controllers.AndresControllers
{
    [ApiController]
    [Route("api/Cliente")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepositorio repositorio;
        private readonly IMapper mapper;


        public ClientesController(IClienteRepositorio repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;

        }

        // GET:-------------------------------------------------------------------
        [HttpGet] //api/Clientes
        public async Task<ActionResult<List<GET_ClienteDTO>>> Get()
        {
            var ListPersonaSelect = await repositorio.Select();
            var ListPersona = mapper.Map<List<GET_ClienteDTO>>(ListPersonaSelect);
            return Ok(ListPersona);
        }

        // GET: ID 
        [HttpGet("{id:int}")]//api/Clientes/2
        public async Task<ActionResult<GET_ClienteDTO>> Get(int id)
        {
            Cliente? C = await repositorio.SelectById(id);
            if (C == null)
            {
                return NotFound();
            }
            var clienteDTO = mapper.Map<GET_ClienteDTO>(C);
            return clienteDTO;
        }

        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return existe;
        }

        // POST: ----------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<int>> Post(POST_ClienteSinFechaInicio  clienteDTO)
        {
            var DTOCliente = new POST_ClienteDTO
            {
                FechaInicio = DateTime.Today,
                PersonaId = clienteDTO.PersonaId,
            };
            try
            {
                Cliente entidad = mapper.Map<Cliente>(DTOCliente);
                return await repositorio.Insert(entidad);
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    return BadRequest($"Error: {e.Message}. Inner Exception: {e.InnerException.Message}");
                }
                return BadRequest(e.Message);
            }
        }

		// PUT: ID ------------------------------------------------------------------------
		[HttpPut("CodigoAModificar/{codigo}")]
		public async Task<ActionResult> Put(string codigo, [FromBody] PUT_ClienteDTO entidadDTO)
        {
			if (!await repositorio.ExisteByCodigo(codigo))
			{
				return BadRequest($"No se encontró un vehiculo con el código {codigo}, compruebe el valor ingresado.");
			}

			var cliente = await repositorio.SelectByCod(codigo);

			if (cliente == null)
			{
				return NotFound($"No se encontró un cliente con el código {codigo}, compruebe el valor ingresado.");
			}

			mapper.Map(entidadDTO, cliente);

			// Log para verificar los valores actualizados en verde 
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"TipoDocumento actualizado: {cliente}");
			Console.ResetColor();

			try
			{
				await repositorio.Update(cliente.Id, cliente);
				var clienteDTO = mapper.Map<GET_ClienteDTO>(cliente);
				return Ok(clienteDTO);
			}
			catch (Exception e)
			{
				if (e.InnerException != null)
				{
					return BadRequest($"Error: {e.Message}. Inner Exception: {e.InnerException.Message}");
				}
				return BadRequest(e.Message);
			}
		}

        // DELETE: ID ----------------------------------------------------------------------
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"El cliente {id} no existe");
            }

            Cliente EntidadABorrar = new Cliente();
            EntidadABorrar.Id = id;

            if (await repositorio.Delete(id))
            {
                return Ok($"El cliente {id} fue eliminado");
            }
            else
            {
                return BadRequest();
            }
        }
    }





}