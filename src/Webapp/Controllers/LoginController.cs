using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Webapp.Models;

namespace Webapp.Controllers
{
    public class LoginController : Controller
    {
        private readonly d7tvdkqig62n6dContext _context;

        public LoginController(d7tvdkqig62n6dContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                throw;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("UserName,Password")] LoginModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            try
            {
                if (ModelState.IsValid)
                {
                     var employee = await _context.Employee
                                                 .FirstOrDefaultAsync(m => m.Gid == model.UserName);
                    if (employee.Password == model.Password)
                        {
                            String Uid = employee.Gid;
                            HttpContext.Session.SetString("username", employee.Gid);
                            return RedirectToAction("Index", "EmsEmployees");
                        }
                    else
                        {
                            ModelState.AddModelError("Error", "Login Failed - Please try again with valid credentials.");

                        }  
                }
                return View(model);
            }
            catch (Exception)
            {
                // _logger.LogError("[Error in LoginController.FormLogonAsync - Error: " + e.Message + "]");
                /// add redirect link here 
                throw;
            }
        }
    }
}