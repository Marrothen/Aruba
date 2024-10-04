using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Traccia5.DB;

namespace Traccia5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttivitaController : ControllerBase
    {

        private readonly IRepository<Attivita> _attivitaRepository;
        public AttivitaController(IRepository<Attivita> attivitaRepository)
        {
            _attivitaRepository = attivitaRepository;
        }

        [Route("getAllItem")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllItem()
        {
            return Ok(await _attivitaRepository.getAll());
        }

        [Route("getItemById/{id}")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetItemById(int id)
        {
            return await _attivitaRepository.GetById(id) is Attivita item
                            ? Ok(item)
                            : NotFound($"Elemento con id:{id} non trovato");
        }

        [Route("createItem")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateItem([FromBody] Attivita item)
        {
            var tempItem = await _attivitaRepository.GetById(item.Id);

            if (tempItem is null) return NotFound($"Elemento con Id:{item.Id} non trovato");

            tempItem.Nome = item.Nome;
            tempItem.Descrizione = item.Descrizione;
            tempItem.IsComplete = item.IsComplete;
            tempItem.Priority = item.Priority;

            await _attivitaRepository.Update(tempItem);
            return NoContent();
        }
        [Route("deleteItem")]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
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

        [Route("login")]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            string tokenString = await _attivitaRepository.Login();
            Response.Cookies.Append("AuthToken", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });
            return Ok();
        }
    }
}
