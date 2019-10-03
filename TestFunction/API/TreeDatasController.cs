using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestFunction.Data;
using TestFunction.Models;

namespace TestFunction.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreeDatasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TreeDatasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TreeDatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TreeData>>> GetTreeDatas()
        {
            return await _context.TreeDatas.Where(x => x.ParentId == null).ToListAsync();
        }

        // GET: api/TreeDatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TreeData>>> GetTreeData(string id)
        {
            return await _context.TreeDatas.Where(x => x.ParentId.Equals(id)).ToListAsync();
        }

        // PUT: api/TreeDatas/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTreeData(string id, TreeData treeData)
        {
            if (id != treeData.Id)
            {
                return BadRequest();
            }

            _context.Entry(treeData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreeDataExists(id))
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

        // POST: api/TreeDatas
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TreeData>> PostTreeData(TreeData treeData)
        {
            _context.TreeDatas.Add(treeData);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TreeDataExists(treeData.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTreeData", new { id = treeData.Id }, treeData);
        }

        // DELETE: api/TreeDatas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TreeData>> DeleteTreeData(string id)
        {
            var treeData = await _context.TreeDatas.FindAsync(id);
            if (treeData == null)
            {
                return NotFound();
            }

            _context.TreeDatas.Remove(treeData);
            await _context.SaveChangesAsync();

            return treeData;
        }

        private bool TreeDataExists(string id)
        {
            return _context.TreeDatas.Any(e => e.Id == id);
        }
    }
}
