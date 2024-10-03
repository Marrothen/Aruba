using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Net.Mime;
using Traccia3.Models.DB;
using Traccia3.Repository.Interface;

namespace Traccia3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttivitaController : ControllerBase
    {

        private readonly IAttivitaRepository _attivitaRepository;

        public AttivitaController(IAttivitaRepository attivitaRepository)
        {
            _attivitaRepository = attivitaRepository;
        }


        /// <summary>
        /// Restituisce l'attività in base all'id
        /// </summary>
        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Attivita), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]

        [Route("getAllItem")]
        [HttpGet]
        public async Task<IActionResult> GetAllItem()
        {
            return Ok(await _attivitaRepository.getAll());
        }

        [Route("getItemById/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetItemById(string id)
        {
            try {
                if (!ObjectId.TryParse(id, out ObjectId objectId))
                {
                    throw new FormatException($"L'ID '{id}' non è un formato valido per ObjectId.");
                }
                return await _attivitaRepository.GetById(id) is Attivita item
                ? Ok(item)
                : NotFound($"Elemento con id:{id} non trovato");
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [Route("createItem")]
        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] Attivita item)
        {
            var newItem = new Attivita
            {
                Nome = item.Nome,
                Descrizione = item.Descrizione,
                IsComplete = item.IsComplete,
                Priority = item.Priority,
                CreatedDate = DateTime.Now.ToString("yyyy-MM-dd")
            };
            return Ok(await _attivitaRepository.Add(newItem));
        }

        [Route("updateItem")]
        [HttpPut]
        public async Task<IActionResult> UpdateItem([FromBody] Attivita item)
        {
            try
            {
                if (!ObjectId.TryParse(item.Id, out ObjectId objectId))
                {
                    throw new FormatException($"L'ID '{item.Id}' non è un formato valido per ObjectId.");
                }
                var tempItem = await _attivitaRepository.GetById(item.Id);

                if (tempItem is null) return NotFound($"Elemento con Id:{item.Id} non trovato");

                tempItem.Nome = item.Nome;
                tempItem.Descrizione = item.Descrizione;
                tempItem.IsComplete = item.IsComplete;
                tempItem.Priority = item.Priority;

                await _attivitaRepository.Update(tempItem);
                return NoContent();
            }
            catch (FormatException ex) {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"L'elemento con Id:{item.Id} non esiste");
            }
        }
        [Route("deleteItem")]
        [HttpDelete]
        public async Task<IActionResult> DeleteItem(string id)
        {
            try
            {
                if (!ObjectId.TryParse(id, out ObjectId objectId))
                {
                    throw new FormatException($"L'ID '{id}' non è un formato valido per ObjectId."); 
                }
                await _attivitaRepository.Delete(id);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"L'elemento con Id:{id} non esiste");
            }
            catch (Exception ex)
            {
                //Evenutali log
                return StatusCode(500, "Errore generico");
            }

            return NoContent();
        }
    }
}
