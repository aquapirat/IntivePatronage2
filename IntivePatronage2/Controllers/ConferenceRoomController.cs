using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntivePatronage2.Model;
using IntivePatronage2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IntivePatronage2.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ConferenceRoomController : Controller
    {
        private readonly ConferenceRoomContext _conferenceRoomContext;
        private readonly ILogger _logger;

        public ConferenceRoomController(ConferenceRoomContext conferenceRoomContext, ILogger<ConferenceRoomController> logger)
        {
            _conferenceRoomContext = conferenceRoomContext;
            _logger = logger;
        }

        // GET /ConferenceRoom
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConferenceRoomItem>>> GetConferenceRooms()
        {
            return await _conferenceRoomContext.ConferenceRooms.ToListAsync();
        }

        // GET /conferenceroom/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ConferenceRoomItem>> GetConferenceRoom(int id)
        {
            var foundItem = await _conferenceRoomContext.ConferenceRooms.FindAsync(id);

            if (foundItem == null)
            {
                _logger.LogWarning("Object with id {id} not found.", id);
                return NotFound();
            }

            return foundItem;
        }

        // POST /conferenceroom
        [HttpPost]
        public async Task<ActionResult<ConferenceRoomItem>> PostConferenceRoom(ConferenceRoomItem conferenceRoom)
        {
            if (ModelState.IsValid)
            {
                _conferenceRoomContext.ConferenceRooms.Add(conferenceRoom);
                await _conferenceRoomContext.SaveChangesAsync();

                _logger.LogInformation("Object with id {id} added successfully via POST.", conferenceRoom.Id);
                return CreatedAtAction("GetConferenceRoom", new {id = conferenceRoom.Id}, conferenceRoom);
            }


            _logger.LogWarning("Model is invalid! Make sure to write every required field.");
            return null;
        }

        // PATCH /conferenceroom/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult<ConferenceRoomItem>> PatchConferenceRoom(int id, [FromBody]ConferenceRoomItem conferenceRoomItem)
        {
            var foundItem = await _conferenceRoomContext.ConferenceRooms.FindAsync(id);

            if (foundItem == null)
            {
                _logger.LogWarning("Object with id {id} not found.", id);
                return NotFound();
            }

            foundItem.RoomNumber =
                conferenceRoomItem.RoomNumber == 0 ? foundItem.RoomNumber : conferenceRoomItem.RoomNumber;

            foundItem.Capacity = 
                conferenceRoomItem.Capacity == 0 ? foundItem.Capacity : conferenceRoomItem.Capacity;

            await _conferenceRoomContext.SaveChangesAsync();

            _logger.LogWarning("Object with id {id} patched successfully.", id);

            return foundItem;
        }

        // DELETE /conferenceroom/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ConferenceRoomItem>> DeleteConferenceRoom(int id)
        {
            var itemToDelete = await _conferenceRoomContext.ConferenceRooms.FindAsync(id);

            if (itemToDelete == null)
            {
                _logger.LogWarning("Object with id {id} not found.", id);
                return NotFound();
            }

            _conferenceRoomContext.ConferenceRooms.Remove(itemToDelete);
            await _conferenceRoomContext.SaveChangesAsync();

            _logger.LogWarning("Object with id {id} deleted successfully.", id);

            return itemToDelete;
        }
    }
}
