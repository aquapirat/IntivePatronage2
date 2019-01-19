using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntivePatronage2.Logic;
using IntivePatronage2.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntivePatronage2.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class FizzBuzzController : Controller
    {

        private readonly FizzBuzzContext _fizzBuzzContext;

        public FizzBuzzController(FizzBuzzContext fizzBuzzContext)
        {
            _fizzBuzzContext = fizzBuzzContext;
        }

        // GET /fizzbuzz
        // GET /fizzbuzz?value=3
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FizzBuzzItem>>> GetFizzBuzzItems(int? value)
        {
            if (value == null)
            {
                return await _fizzBuzzContext.FizzBuzzItems.ToListAsync();
            }
            else
            {
                var fb = new FizzBuzzItem()
                {
                    Value = (int) value,
                    Result = FizzBuzz.Create((int) value)
                };

                _fizzBuzzContext.FizzBuzzItems.Add(fb);
                await _fizzBuzzContext.SaveChangesAsync();

                return await _fizzBuzzContext.FizzBuzzItems.ToListAsync();
            }
        }

        // GET /fizzbuzz/id
        [HttpGet("{id}")]
        public async Task<ActionResult<FizzBuzzItem>> GetFizzBuzzItem(int id)
        {
            var foundItem = await _fizzBuzzContext.FizzBuzzItems.FindAsync(id);

            if (foundItem == null)
            {
                return NotFound();
            }

            return foundItem;
        }

        // PATCH /fizzbuzz/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult<FizzBuzzItem>> PatchFizzBuzzItem(int id, [FromBody]FizzBuzzItem fizzBuzzItem)
        {
            var foundItem = await _fizzBuzzContext.FizzBuzzItems.FindAsync(id);

            if (foundItem == null)
            {
                return NotFound();
            }

            foundItem.Value = fizzBuzzItem.Value;
            foundItem.Result = FizzBuzz.Create(fizzBuzzItem.Value);

            await _fizzBuzzContext.SaveChangesAsync();

            return foundItem;
        }

        // DELETE /fizzbuzz/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<FizzBuzzItem>> DeleteFizzBuzzItem(int id)
        {
            var itemToDelete = await _fizzBuzzContext.FizzBuzzItems.FindAsync(id);

            if (itemToDelete == null)
            {
                return NotFound();
            }

            _fizzBuzzContext.FizzBuzzItems.Remove(itemToDelete);
            await _fizzBuzzContext.SaveChangesAsync();

            return itemToDelete;
        }
    }
}
