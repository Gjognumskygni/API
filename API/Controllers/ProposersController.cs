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
    public class ProposersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProposersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Proposers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proposer>>> GetProposers()
        {
            return await _context.Proposers.ToListAsync();
        }

        // GET: api/Proposers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proposer>> GetProposer(int id)
        {
            var proposer = await _context.Proposers.FindAsync(id);

            if (proposer == null)
            {
                return NotFound();
            }

            return proposer;
        }

        // PUT: api/Proposers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProposer(int id, Proposer proposer)
        {
            if (id != proposer.Id)
            {
                return BadRequest();
            }

            _context.Entry(proposer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProposerExists(id))
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

        // POST: api/Proposers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Proposer>> PostProposer(Proposer proposer)
        {
            _context.Proposers.Add(proposer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProposer", new { id = proposer.Id }, proposer);
        }

        // DELETE: api/Proposers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProposer(int id)
        {
            var proposer = await _context.Proposers.FindAsync(id);
            if (proposer == null)
            {
                return NotFound();
            }

            _context.Proposers.Remove(proposer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProposerExists(int id)
        {
            return _context.Proposers.Any(e => e.Id == id);
        }
    }
}
