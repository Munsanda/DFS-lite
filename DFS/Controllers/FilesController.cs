using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DFS.Data;
using DFS.Models;
using File = DFS.Models.File;
using DFS.DTOs;

namespace DFS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly DFSContext _context;

        public FilesController(DFSContext context)
        {
            _context = context;
        }

        // GET: api/Files
        [HttpGet]
        public async Task<ActionResult<IEnumerable<File>>> GetFile()
        {
          if (_context.File == null)
          {
              return NotFound();
          }
            return await _context.File.ToListAsync();
        }

        // GET: api/Files/5
        [HttpGet("{id}")]
        public async Task<ActionResult<File>> GetFile(Guid id)
        {
          if (_context.File == null)
          {
              return NotFound();
          }
            var file = await _context.File.FindAsync(id);

            if (file == null)
            {
                return NotFound();
            }

            return file;
        }

        // PUT: api/Files/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFile(Guid id, CreateFileDTO file)
        {
            //if (id != file.ID)
            //{
            //    return BadRequest();
            //}



            var UpdateFile = new File()
            {
                ID = id,
                file_name = file.file_name,
                file_content = file.file_content
            };

            _context.Entry(UpdateFile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FileExists(id))
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

        // POST: api/Files
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<File>> PostFile(CreateFileDTO file)
        {
          if (_context.File == null)
          {
              return Problem("Entity set 'DFSContext.File'  is null.");
          }

            var NewFile = new File()
            {
                ID = Guid.NewGuid(),
                file_name = file.file_name,
                file_content = file.file_content
            };

            _context.File.Add(NewFile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFile", new { id = NewFile.ID }, file);
        }

        // DELETE: api/Files/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(Guid id)
        {
            if (_context.File == null)
            {
                return NotFound();
            }
            var file = await _context.File.FindAsync(id);
            if (file == null)
            {
                return NotFound();
            }

            _context.File.Remove(file);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FileExists(Guid id)
        {
            return (_context.File?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
