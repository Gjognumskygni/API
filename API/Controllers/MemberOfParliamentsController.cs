using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Model;
using Domain;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberOfParliamentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MemberOfParliamentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MemberOfParliaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberOfParliament>>> GetMemberOfParliaments()
        {
            return await _context.MemberOfParliaments.ToListAsync();
        }

        // GET: api/MemberOfParliaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MemberOfParliament>> GetMemberOfParliament(int id)
        {
            var memberOfParliament = await _context.MemberOfParliaments.FindAsync(id);

            if (memberOfParliament == null)
            {
                return NotFound();
            }

            return memberOfParliament;
        }

        // PUT: api/MemberOfParliaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMemberOfParliament(int id, MemberOfParliament memberOfParliament)
        {
            if (id != memberOfParliament.Id)
            {
                return BadRequest();
            }

            _context.Entry(memberOfParliament).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberOfParliamentExists(id))
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

        // POST: api/MemberOfParliaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MemberOfParliament>> PostMemberOfParliament(MemberOfParliament memberOfParliament)
        {
            _context.MemberOfParliaments.Add(memberOfParliament);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMemberOfParliament", new { id = memberOfParliament.Id }, memberOfParliament);
        }

        // DELETE: api/MemberOfParliaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMemberOfParliament(int id)
        {
            var memberOfParliament = await _context.MemberOfParliaments.FindAsync(id);
            if (memberOfParliament == null)
            {
                return NotFound();
            }

            _context.MemberOfParliaments.Remove(memberOfParliament);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemberOfParliamentExists(int id)
        {
            return _context.MemberOfParliaments.Any(e => e.Id == id);
        }
    }
}
