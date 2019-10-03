using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestFunction.Data;
using TestFunction.Models;

namespace TestFunction.Controllers
{
    public class TreeDatasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private static readonly string[] Author = new[]
        {
            "Lưu", "Trọng", "Tấn", "Trần", "Khánh", "Linh", "FPT", "University", "Chevening", "Scholarship"
        };

        public TreeDatasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TreeDatas
        public async Task<IActionResult> Index()
        {
            var treeDatas = await _context.TreeDatas.Take(1000).ToListAsync();
            if (treeDatas.Count == 0)
            {
                var rng = new Random();
                for (int i = 0; i < 3; i++)
                {
                    treeDatas.Add(new TreeData
                    {
                        Id = $"{i + 1}",
                        ParentId = null,
                        Title = Summaries[i],
                        Author = Author[i],
                        Price = rng.NextDouble(),
                        Quantity = rng.Next(100, 200),
                        Year = rng.Next(1997, 2019)
                    });
                    for (int j = 0; j < 39; j++)
                    {
                        treeDatas.Add(new TreeData
                        {
                            Id = $"{i + 1}.{j + 1}",
                            ParentId = $"{i + 1}",
                            Title = Summaries[i] + $".{j}",
                            Author = Author[i] + $".{j}",
                            Price = rng.NextDouble(),
                            Quantity = rng.Next(100, 200),
                            Year = rng.Next(1997, 2019)
                        });
                        for (int k = 0; k < 149; k++)
                        {
                            treeDatas.Add(new TreeData
                            {
                                Id = $"{i + 1}.{j + 1}.{k + 1}",
                                ParentId = $"{i + 1}.{j + 1}",
                                Title = Summaries[i] + $".{j + 1}.{k + 1}",
                                Author = Author[i] + $".{j + 1}.{k + 1}",
                                Price = rng.NextDouble(),
                                Quantity = rng.Next(100, 200),
                                Year = rng.Next(1997, 2019)
                            });
                        }
                    }
                }

                await _context.AddRangeAsync(treeDatas);
                await _context.SaveChangesAsync();
            }
            return View(treeDatas);
        }

        // GET: TreeDatas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treeData = await _context.TreeDatas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (treeData == null)
            {
                return NotFound();
            }

            return View(treeData);
        }

        // GET: TreeDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TreeDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,Year,ParentId,Price,Quantity")] TreeData treeData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(treeData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(treeData);
        }

        // GET: TreeDatas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treeData = await _context.TreeDatas.FindAsync(id);
            if (treeData == null)
            {
                return NotFound();
            }
            return View(treeData);
        }

        // POST: TreeDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,Author,Year,ParentId,Price,Quantity")] TreeData treeData)
        {
            if (id != treeData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(treeData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreeDataExists(treeData.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(treeData);
        }

        // GET: TreeDatas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treeData = await _context.TreeDatas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (treeData == null)
            {
                return NotFound();
            }

            return View(treeData);
        }

        // POST: TreeDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var treeData = await _context.TreeDatas.FindAsync(id);
            _context.TreeDatas.Remove(treeData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TreeDataExists(string id)
        {
            return _context.TreeDatas.Any(e => e.Id == id);
        }
    }
}
