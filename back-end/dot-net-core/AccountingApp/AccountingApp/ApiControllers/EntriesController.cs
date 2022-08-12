using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DTO;
using Identity;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace AccountingApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ResponseCache(NoStore = true)]
    public class EntriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EntriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Entries
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EntryDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(String))]
        public async Task<ActionResult<IEnumerable<EntryDTO>>> GetEntries()
        {
            List<Entry> entries = null;
            DateTime currentDate = DateTime.Now;
            entries = await _context.Entries.Where(e => e.AppUserId == User.UserGuidId() && (e.DeletedAt > currentDate || e.DeletedAt == null)).ToListAsync();
            if (entries.Count == 0)
            {
                return Ok(new List<EntryDTO>());
            }
            List<EntryDTO> entryDTOs = new List<EntryDTO>();
            foreach (var entry in entries)
            {
                EntryDTO entryDTO = new EntryDTO
                {
                    Id = entry.Id,
                    DateOfPayment = entry.DateOfPayment.ToString("d", new CultureInfo("et-EE")),
                    Sum = entry.Sum,
                    Description = entry.Description,
                    IsExpense = entry.IsExpense
                };
                entryDTOs.Add(entryDTO);

            }
            return Ok(entryDTOs);

        }

        // GET: api/Entries/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EntryDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(String))]
        public async Task<ActionResult<EntryDTO>> GetEntry(Guid id)
        {
            DateTime currentDate = DateTime.Now;

            var entry = await _context.Entries.FirstOrDefaultAsync(e => e.Id == id && e.AppUserId == User.UserGuidId() && (e.DeletedAt > currentDate || e.DeletedAt == null));

            if (entry == null)
            {
                return NotFound("Entry not found");
            }
            EntryDTO entryDTO = new EntryDTO
            {
                Id = entry.Id,
                DateOfPayment = entry.DateOfPayment.ToString("d", new CultureInfo("et-EE")),
                Sum = entry.Sum,
                Description = entry.Description,
                IsExpense = entry.IsExpense
            };
            return Ok(entryDTO);


        }


        // PUT: api/Entries/5
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(String))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
        public async Task<IActionResult> PutEntry(Guid id, EntryDTO entryDTO)
        {
            if (id != entryDTO.Id)
            {
                return BadRequest("Id and Entry.id do not match.");
            }


            DateTime currentDate = DateTime.Now;

            var entry1 = await _context.Entries.FirstOrDefaultAsync(e => e.Id == id && e.AppUserId == User.UserGuidId() && (e.DeletedAt > currentDate || e.DeletedAt == null));

            if (entry1 == null)
            {
                return NotFound("No such entry was found.");
            }


            entry1.ModifiedAt = currentDate;
            try
            {
                entry1.DateOfPayment = DateTime.ParseExact(entryDTO.DateOfPayment, "dd.MM.yyyy", new CultureInfo("et-EE"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                entry1.Sum = entryDTO.Sum;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                entry1.Description = entryDTO.Description;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                entry1.IsExpense = (Boolean)entryDTO.IsExpense;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }



            _context.Entry(entry1).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Entries
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EntryDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(String))]
        public async Task<ActionResult<EntryDTO>> PostEntry(EntryDTO entryDTO)
        {
            DateTime currentDate = DateTime.Now;

            Entry entry = new Entry
            {
                CreatedAt = currentDate,
                ModifiedAt = currentDate,
                DeletedAt = DateTime.ParseExact("31.12.9999", "dd.MM.yyyy", new CultureInfo("et-EE")),
                AppUserId = User.UserGuidId()
            };
            try
            {
                entry.DateOfPayment = DateTime.ParseExact(entryDTO.DateOfPayment, "dd.MM.yyyy", new CultureInfo("et-EE"));
            }
            catch (Exception)
            {
                return BadRequest("Date of payment missing or incorrect. It has to be a string in the form: 21.07.2022.");
            }
            try
            {
                if (entryDTO.Sum <= 0)
                {
                    throw new Exception("Sum has to be a positive number (sum > 0).");
                }
                entry.Sum = entryDTO.Sum;
            }
            catch (Exception)
            {
                return BadRequest("Sum missing or incorrect. It has to be a positive number (sum > 0).");
            }
            try
            {
                if (Regex.Replace(entryDTO.Description, @"\s+", "") == "")
                {
                    throw new Exception("Description contains only whitespace.");
                }
                entry.Description = entryDTO.Description;
            }
            catch (Exception)
            {
                return BadRequest("Description missing or incorrect. It has to be a text.");
            }
            try
            {
                entry.IsExpense = (Boolean)entryDTO.IsExpense;
            }
            catch (Exception)
            {
                return BadRequest("IsExpense missing or incorrect. It has to be a boolean (true or false).");
            }

            _context.Entries.Add(entry);
            await _context.SaveChangesAsync();

            entryDTO.Id = entry.Id;

            return Ok(entryDTO);
        }

        // DELETE: api/Entries/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(String))]
        public async Task<ActionResult<Entry>> DeleteEntry(Guid id)
        {
            DateTime currentDate = DateTime.Now;

            var entry = await _context.Entries.FirstOrDefaultAsync(e => e.Id == id && e.AppUserId == User.UserGuidId() && (e.DeletedAt > currentDate || e.DeletedAt == null));

            if (entry == null)
            {
                return NotFound("Entry not found.");
            }

            entry.DeletedAt = currentDate;
            _context.Entry(entry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryExists(id))
                {
                    return NotFound("Entry not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }

        private bool EntryExists(Guid id)
        {
            DateTime currentDate = DateTime.Now;
            return _context.Entries.Any(e => e.Id == id && e.AppUserId == User.UserGuidId() && (e.DeletedAt > currentDate || e.DeletedAt == null));
        }
    }
}
