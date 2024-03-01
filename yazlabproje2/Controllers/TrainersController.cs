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
    public class TrainersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trainers/TrainerLogin
        public IActionResult TrainerLogin()
        {
            return View();
        }

        // POST: Trainers/TrainerLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TrainerLogin(Trainer model)
        {
            if (ModelState.IsValid)
            {
                var trainer = await _context.Trainer.FirstOrDefaultAsync(t => t.Email == model.Email);

                if (trainer != null && model.Password == trainer.Password)
                {
                    // Kullanıcı giriş başarılı
                    // Örneğin, authentication işlemlerini gerçekleştirebilirsiniz.
                    // Bu örnekte giriş işlemleri basitleştirilmiştir.

                    return RedirectToAction("TrainerPanel", "Trainers", new { id = trainer.Id }); // Yönlendirilecek sayfa
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Wrong E-mail or password.");
                    return View(model);
                }
            }

            return View(model);
        }

        // Get Trainers/UpdateTrainerProfile/1
        public async Task<IActionResult> UpdateTrainerProfile(int? id)
        {
            if (id == null)
            {
                Console.WriteLine("id null");
                return NotFound();
            }

            var trainer = await _context.Trainer.FindAsync(id);
            if (trainer == null)
            {    
                Console.WriteLine("Kullanıcı bulunamadı");
                return NotFound();
            }
            return View(trainer);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTrainerProfile(int id, [Bind("Id,Email,Password,Name,Surname,Profession,Experience")] Trainer trainer)
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
                return RedirectToAction("TrainerPanel", "Trainers");
            }
            return View(trainer);
        }

        // GET Trainers/TrainerPanel
        public async Task<IActionResult> TrainerPanel(int? id)
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

        private bool TrainerExists(int id)
        {
          return (_context.Trainer?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
