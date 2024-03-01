using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using yazlabproje2.Data;
using yazlabproje2.Models;

namespace yazlabproje2.Controllers
{
    public class AdminsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admins/AdminLogin
        public IActionResult AdminLogin()
        {
            return View();
        }

        // POST: Admins/AdminLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin(Admin model)
        {
            if (ModelState.IsValid)
            {
                var admin = await _context.Admin.FirstOrDefaultAsync(a => a.Email == model.Email);

                if (admin != null && model.Password == admin.Password)
                {
                    // Kullanıcı giriş başarılı
                    // Örneğin, authentication işlemlerini gerçekleştirebilirsiniz.
                    // Bu örnekte giriş işlemleri basitleştirilmiştir.

                    return RedirectToAction("AdminPanel", "Admins", new { id = admin.Id }); // Yönlendirilecek sayfa
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Wrong E-mail or password.");
                    return View(model);
                }
            }

            return View(model);
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.User != null ? 
                          View(await _context.User.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.User'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Password,Name,Surname,BirthDate,Gender,PhoneNumber,ProfilePicture")] User user)
        {
            if (ModelState.IsValid)
            {
                
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("AdminPanel", "Admins");
            }
            return View(user);
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Password,Name,Surname,BirthDate,Gender,PhoneNumber,ProfilePicture")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("AdminPanel", "Admins");
            }
            return View(user);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'ApplicationDbContext.User'  is null.");
            }
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("AdminPanel", "Admins");
        }

        // GET Users/UserPanel
        public async Task<IActionResult> AdminPanel(int? id)
        {
            if (id == null || _context.Admin == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        public async Task<IActionResult> TrainerIndex()
        {
            return _context.Trainer != null ?
                        View(await _context.Trainer.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Trainer'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> TrainerDetails(int? id)
        {
            if (id == null || _context.Trainer == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainer == null)
            {
                return NotFound();
            }

            return View(trainer);
        }

        public IActionResult CreateTrainer()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTrainer([Bind("Id,Email,Password,Name,Surname,Profession,Experience")] Trainer trainer)
        {
            if (ModelState.IsValid)
            {

                trainer.Password = BCrypt.Net.BCrypt.HashPassword(trainer.Password);

                _context.Add(trainer);
                await _context.SaveChangesAsync();
                return RedirectToAction("AdminPanel", "Admins");
            }
            return View(trainer);
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> EditTrainer(int? id)
        {
            if (id == null || _context.Trainer == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainer.FindAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }
            return View(trainer);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTrainer(int id, [Bind("Id,Email,Password,Name,Surname,Profession,Experience")] Trainer trainer)
        {
            if (id != trainer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainerExists(trainer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("AdminPanel", "Admins");
            }
            return View(trainer);
        }
        public async Task<IActionResult> DeleteTrainer(int? id)
        {
            if (id == null || _context.Trainer == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainer == null)
            {
                return NotFound();
            }

            return View(trainer);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("DeleteTrainer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTrainerConfirmed(int id)
        {
            if (_context.Trainer == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Trainer'  is null.");
            }
            var trainer = await _context.Trainer.FindAsync(id);
            if (trainer != null)
            {
                _context.Trainer.Remove(trainer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("AdminPanel","Admins");
        }

        private bool UserExists(int id)
        {
          return (_context.User?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool TrainerExists(int id)
        {
          return (_context.Trainer?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
