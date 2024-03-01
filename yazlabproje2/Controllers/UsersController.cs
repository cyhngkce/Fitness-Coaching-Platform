using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using yazlabproje2.Data;
using yazlabproje2.Models;

namespace yazlabproje2.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: User/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Email,Name,Surname,BirthDate,Gender,PhoneNumber,ProfilePicture,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                // Örneğin, şifreyi hashleyerek güvenli bir şekilde saklamak için uygun bir yöntem kullanabilirsiniz.
                // Ancak burada basitleştirmek için doğrudan şifreyi kullanıyoruz.
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Users"); // Yönlendirilecek sayfa
            }
            return View(user);
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.User.FirstOrDefaultAsync(u => u.Email == model.Email);

                if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                {
                    // Kullanıcı giriş başarılı
                    // Örneğin, authentication işlemlerini gerçekleştirebilirsiniz.
                    // Bu örnekte giriş işlemleri basitleştirilmiştir.

                    return RedirectToAction("UserPanel", "Users", new { id = user.Id }); // Yönlendirilecek sayfa
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Wrong E-mail or password.");
                    return View(model);
                }
            }

            return View(model);
        }

        // GET Users/UserPanel
        public async Task<IActionResult> UserPanel(int? id)
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

        // Get Users/UpdateProfile/1
        public async Task<IActionResult> UpdateProfile(int? id)
        {
            if (id == null)
            {
                // Hata ayıklama için günlükleme ekle
                Console.WriteLine("id null");
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                // Hata ayıklama için günlükleme ekle
                Console.WriteLine("Kullanıcı bulunamadı");
                return NotFound();
            }
            return View(user);
        }



        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(int id, [Bind("Id,Email,Name,Surname,BirthDate,Gender,PhoneNumber,ProfilePicture,Password")] User user)
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
                return RedirectToAction("UserPanel","Users");
            } 
            return View(user);
        }

        
        private bool UserExists(int id)
        {
            return (_context.User?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        
    }
}