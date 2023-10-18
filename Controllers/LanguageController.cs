using Microsoft.AspNetCore.Mvc;
using restful_crud_dapper.Models;
using restful_crud_dapper.Services;

namespace restful_crud_dapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase{
        
        private readonly LanguageService _languageService;

        public LanguageController(LanguageService languageService){
            _languageService = languageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Language>>> SelectAll(){
            var list = await _languageService.SelectAll();

            if(list != null){
                return Ok(list);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Language>> SelectById(int id){
            
            Language language = await _languageService.SelectById(id);

            if(language != null){
                return Ok(language);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> CreateLanguage([FromBody] Language language){

            int createdId = await _languageService.CreateLanguage(language);

            if(createdId > 0){
                return CreatedAtAction("SelectById", new { id = createdId }, null);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLanguage(int id, [FromBody] Language language){
            
            Language existingLanguage = await _languageService.SelectById(id);

            if(existingLanguage == null){
                return NotFound();
            }

            existingLanguage.Name = language.Name;

            await _languageService.UpdateLanguage(existingLanguage);

            return Ok(existingLanguage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguage(int id){

            Language language = await _languageService.SelectById(id);

            if (language == null){
                return NotFound();
                }

            int result = await _languageService.DeleteLanguage(id);

            if(result > 0){
                return NoContent();
            }

            return BadRequest();
        }
    }

}