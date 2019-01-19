using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IntivePatronage2.Models
{
    public class ConferenceRoomContext : DbContext
    {
        public ConferenceRoomContext(DbContextOptions<ConferenceRoomContext> options) :
            base(options)
        {
        }

        public DbSet<ConferenceRoomItem> ConferenceRooms { get; set; }
    }
}
