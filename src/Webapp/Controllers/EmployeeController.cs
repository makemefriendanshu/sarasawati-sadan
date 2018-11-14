// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Http;
// using System.Data.SqlClient;
// using System.Data;
// using System.Net.Mail;
// using System.Net;
// using Microsoft.Extensions.Logging;
// using Webapp.Models;

// namespace Webapp.Controllers
// {
//     public class EmployeesController : Controller
//     {
//         private readonly d7tvdkqig62n6dContext _context;

//         public string ExpenceClaimId { get; private set; }

//         public EmployeesController(d7tvdkqig62n6dContext context)
//         {
//             _context = context;
//         }

//         [Route("logout")]
//         [HttpGet]
//         public IActionResult Logout()
//         {
//             try
//             {
//                 HttpContext.Session.Remove("username");
//                 return RedirectToAction("Index", "Login");
//             }
//             catch (Exception e)
//             {
//                 _logger.LogError("[Error in EmsEmployeesController.Logout - Error: " + e.Message + "]");
//                 /// add redirect link here 
//                 throw;
//             }
//         }


//         // GET: EmsEmployees
//         public async Task<IActionResult> Index()
//         {
//             try
//             {
//                 var id = HttpContext.Session.GetString("username");
//                 var emsEmployee = await _context.EmsEmployee.FindAsync(id);
//                 if (emsEmployee == null)
//                 {
//                     return NotFound();
//                 }

//                 var items = await _context.EmsExpenceClaimData.Where(s => s.CreatedBy == HttpContext.Session.GetString("username")).ToListAsync();
//                 items.Reverse();

//                 var viewmodel = new MyViewModel
//                 {
//                     EmsEmployee = emsEmployee,
//                     EmsExpenceClaimDataList = items
//                 };
//                 ModelState.Clear();
//                 //pdf_convert();
//                 return View(viewmodel);
//             }
//             catch (Exception e)
//             {
//                 _logger.LogError("[Error in EmsEmployeesController.Index - Error: " + e.Message + "]");
//                 /// add redirect link here 
//                 throw;
//             }

//         }

//         // GET: EmsEmployees/Action
//         public async Task<IActionResult> Action()
//         {
//             try
//             {
//                 var id = HttpContext.Session.GetString("username");
//                 var emsEmployee = await _context.EmsEmployee.FindAsync(id);
//                 if (emsEmployee == null)
//                 {
//                     return NotFound();
//                 }

//                 var items = await _context.EmsExpenceClaimData.Where(s => s.ModifiedBy == HttpContext.Session.GetString("username")).ToListAsync();
//                 items.Reverse();
//                 //items = items.FindAll(s => s.StatusId == 101);

//                 var viewmodel = new MyViewModel
//                 {
//                     EmsEmployee = emsEmployee,
//                     EmsExpenceClaimDataList = items
//                 };
//                 ModelState.Clear();
//                 return View(viewmodel);
//             }
//             catch (Exception e)
//             {
//                 _logger.LogError("[Error in EmsEmployeesController.Action - Error: " + e.Message + "]");
//                 /// add redirect link here 
//                 throw;
//             }
//         }

//         // GET: EmsEmployees/Details/5
//         public async Task<IActionResult> Details(string id)
//         {
//             try
//             {
//                 if (id == null)
//                 {
//                     return NotFound();
//                 }

//                 var Id = HttpContext.Session.GetString("username");
//                 var emsEmployee = await _context.EmsEmployee.FindAsync(Id);

//                 var emsExpenceClaimData = await _context.EmsExpenceClaimData.FindAsync(id);

//                 //var emsExpenceDetails = await _context.EmsExpenceDetails
//                 //    .FirstOrDefaultAsync(m => m.ExpenceClaimId == id);
//                 var emsExpenceDetails = await _context.EmsExpenceDetails.Where(s => s.ExpenceClaimId == id).ToListAsync();

//                 var viewmodel = new MyViewModel
//                 {
//                     EmsEmployee = emsEmployee,
//                     EmsExpenceClaimData = emsExpenceClaimData,
//                     EmsExpenceDetails = emsExpenceDetails
//                 };
//                 if (viewmodel == null)
//                 {
//                     return NotFound();
//                 }

//                 return View(viewmodel);
//             }
//             catch (Exception e)
//             {
//                 _logger.LogError("[Error in EmsEmployeesController.Details - Error: " + e.Message + "]");
//                 /// add redirect link here 
//                 throw;
//             }
//         }

//         //GET: EmsEmployees/ExpenceClaim
//         public async Task<IActionResult> ExpenceClaim()
//         {
//             try
//             {
//                 var id = HttpContext.Session.GetString("username");
//                 var emsEmployee = await _context.EmsEmployee.FindAsync(id);
//                 if (emsEmployee == null)
//                 {
//                     return NotFound();
//                 }

//                 var expenceModel = Enumerable.Range(1, 10).Select(i => new EmsExpenceDetailsModel { SpentFromDate = DateTime.Now.AddMonths(-6), SpentToDate = DateTime.Now }).ToList();

//                 var viewmodel = new MyViewModel
//                 {
//                     EmsEmployee = emsEmployee,
//                     EmsExpenceDetailsModels = expenceModel
//                 };
//                 ModelState.Clear();
//                 return View(viewmodel);
//             }
//             catch (Exception e)
//             {
//                 _logger.LogError("[Error in EmsEmployeesController.ExpenceClaim - Error: " + e.Message + "]");
//                 /// add redirect link here 
//                 throw;
//             }
//         }

//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> ExpenceClaim([Bind(Prefix = "EmsExpenceDetailsModels")] List<EmsExpenceDetailsModel> emsExpenceDetailsModel)
//         {
//             try
//             {
//                 if (ModelState.IsValid)
//                 {

//                     var id = HttpContext.Session.GetString("username");
//                     var emsEmployee = await _context.EmsEmployee.FindAsync(id);
//                     var EZ = 1;
//                     var OUTPUT = new SqlParameter("@OUTPUT", SqlDbType.Int);
//                     OUTPUT.Direction = ParameterDirection.Output;
//                     var return_value = _context.Database.ExecuteSqlCommand("EMS_SequenceKeygeneration @SequenceId, @OUTPUT OUTPUT", new SqlParameter("@SequenceId", (object)EZ), OUTPUT);

//                     var new_deatils_list = new List<EmsExpenceDetails>();
//                     var new_claim_id = "EZ-" + id.ToUpper() + "-" + OUTPUT.SqlValue;

//                     var new_expense_data = new EmsExpenceClaimData();
//                     new_expense_data.ExpenceClaimId = new_claim_id;
//                     new_expense_data.FromDate = new DateTime(DateTime.Now.Year - 1, 10, 1);
//                     new_expense_data.ToDate = new DateTime(DateTime.Now.Year, 9, 30);
//                     new_expense_data.StatusId = 101;
//                     new_expense_data.CreatedBy = id;
//                     new_expense_data.CreatedOn = DateTime.Now;
//                     new_expense_data.ModifiedBy = emsEmployee.ManagerGid;
//                     new_expense_data.ModifiedOn = DateTime.Now;

//                     _context.Add(new_expense_data);
//                     await _context.SaveChangesAsync();

//                     foreach (EmsExpenceDetailsModel expenceDetailsItem in emsExpenceDetailsModel)
//                     {
//                         var new_deatils = new EmsExpenceDetails();
//                         new_deatils.ExpenceClaimId = new_claim_id;
//                         new_deatils.ExpenceDetailId = System.Guid.NewGuid().ToString();
//                         new_deatils.ExpenceTypeId = expenceDetailsItem.ExpenceTypeId;
//                         new_deatils.SpentFromDate = expenceDetailsItem.SpentFromDate;
//                         new_deatils.SpentToDate = expenceDetailsItem.SpentToDate;
//                         new_deatils.AmountSpent = expenceDetailsItem.AmountSpent;
//                         new_deatils.Remarks = expenceDetailsItem.Remarks;
//                         _context.Add(new_deatils);
//                         await _context.SaveChangesAsync();
//                     }

//                     return RedirectToAction(nameof(Index));

//                 }
//                 ModelState.AddModelError("Error", "Auto Login Failed - Please try again with valid credentials.");
//                 return RedirectToAction(nameof(ExpenceClaim));
//             }
//             catch (Exception e)
//             {
//                 _logger.LogError("[Error in EmsEmployeesController.ExpenceClaim_post - Error: " + e.Message + "]");
//                 /// add redirect link here 
//                 throw;
//             }
//         }

//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> ApproveRequest([Bind(Prefix = "EmsExpenceClaimData")] EmsExpenceClaimData data)
//         {

//             try
//             {
//                 if (ModelState.IsValid)
//                 {
//                     var emsExpenceClaimData = await _context.EmsExpenceClaimData.FindAsync(data.ExpenceClaimId);
//                     emsExpenceClaimData.StatusId = 102;
//                     emsExpenceClaimData.ModifiedBy = HttpContext.Session.GetString("username");
//                     emsExpenceClaimData.ModifiedOn = DateTime.Now;
//                     _context.Update(emsExpenceClaimData);
//                     await _context.SaveChangesAsync();
//                 }
//                 //var too = "anshumannie@gmail.com";
//                 //SendMail(too);
//                 return RedirectToAction(nameof(Index));
//             }
//             catch (Exception e)
//             {
//                 _logger.LogError("[Error in EmsEmployeesController.ApproveRequest - Error: " + e.Message + "]");
//                 /// add redirect link here 
//                 throw;
//             }
//         }

//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> RejectRequest([Bind(Prefix = "EmsExpenceClaimData")] EmsExpenceClaimData data)
//         {
//             try
//             {
//                 if (ModelState.IsValid)
//                 {
//                     var emsExpenceClaimData = await _context.EmsExpenceClaimData.FindAsync(data.ExpenceClaimId);
//                     emsExpenceClaimData.StatusId = 103;
//                     emsExpenceClaimData.ModifiedBy = HttpContext.Session.GetString("username");
//                     emsExpenceClaimData.ModifiedOn = DateTime.Now;
//                     _context.Update(emsExpenceClaimData);
//                     await _context.SaveChangesAsync();

//                 }
//                 return RedirectToAction(nameof(Index));
//             }
//             catch (Exception e)
//             {
//                 _logger.LogError("[Error in EmsEmployeesController.RejectRequest - Error: " + e.Message + "]");
//                 /// add redirect link here 
//                 throw;
//             }
//         }
        
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create([Bind("Gid,EmployeeNo,EmployeeName,EmployeeEmail,Department,ManagerGid,ManagerName,Location,CostCentre,Grade,EntityStatusId")] EmsEmployee emsEmployee)
//         {
//             if (ModelState.IsValid)
//             {
//                 _context.Add(emsEmployee);
//                 await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             return View(emsEmployee);
//         }

//         // GET: EmsEmployees/Edit/5
//         public async Task<IActionResult> Edit(string id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var emsEmployee = await _context.EmsEmployee.FindAsync(id);
//             if (emsEmployee == null)
//             {
//                 return NotFound();
//             }
//             return View(emsEmployee);
//         }

//         // POST: EmsEmployees/Edit/5
//         // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//         // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(string id, [Bind("Gid,EmployeeNo,EmployeeName,EmployeeEmail,Department,ManagerGid,ManagerName,Location,CostCentre,Grade,EntityStatusId")] EmsEmployee emsEmployee)
//         {
//             if (id != emsEmployee.Gid)
//             {
//                 return NotFound();
//             }

//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _context.Update(emsEmployee);
//                     await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!EmsEmployeeExists(emsEmployee.Gid))
//                     {
//                         return NotFound();
//                     }
//                     else
//                     {
//                         throw;
//                     }
//                 }
//                 return RedirectToAction(nameof(Index));
//             }
//             return View(emsEmployee);
//         }

//         // GET: EmsEmployees/Delete/5
//         public async Task<IActionResult> Delete(string id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var emsEmployee = await _context.EmsEmployee
//                 .FirstOrDefaultAsync(m => m.Gid == id);
//             if (emsEmployee == null)
//             {
//                 return NotFound();
//             }

//             return View(emsEmployee);
//         }

//         // POST: EmsEmployees/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(string id)
//         {
//             var emsEmployee = await _context.EmsEmployee.FindAsync(id);
//             _context.EmsEmployee.Remove(emsEmployee);
//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }

//         private bool EmsEmployeeExists(string id)
//         {
//             return _context.EmsEmployee.Any(e => e.Gid == id);
//         }

//         private bool SendMail(string too)
//         {
//             //using (var mailMessage = new MailMessage())
//             //using (var client = new SmtpClient(config.Email.SmtpServer, config.Email.SmtpPort))
//             //{
//             //    // configure the client and send the message
//             //    client.UseDefaultCredentials = false;
//             //    client.Credentials = new NetworkCredential(config.Email.SmtpUser, config.Email.SmtpPassword);
//             //    client.EnableSsl = true;

//             //    // configure the mail message
//             //    mailMessage.From = new MailAddress(config.Email.AlertEmailFrom);
//             //    mailMessage.To.Insert(0, new MailAddress(config.Email.AlertEmailTo));
//             //    mailMessage.Subject = config.Email.AlertEmailSubject;
//             //    mailMessage.Body = body;
//             //    mailMessage.IsBodyHtml = true;

//             //    client.Send(mailMessage);
//             //}

//             SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

//             client.EnableSsl = true;
//             MailAddress from = new MailAddress("anshumannie@gmail.com", "[Anshuman]");
//             MailAddress to = new MailAddress(too, too);
//             MailMessage message = new MailMessage(from, to);
//             message.Body = "This is a test e-mail message sent using gmail as a relay server ";
//             message.Subject = "Gmail test email with SSL and Credentials";
//             NetworkCredential myCreds = new NetworkCredential("anshumannie@gmail.com", "", "");
//             client.Credentials = myCreds;

//             try
//             {
//                 client.Send(message);
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine("Exception is:" + ex.ToString());
//                 return false;
//             }
           
//         }

//         public static void CreateTestMessage2(string server)
//         {
//             string to = "jane@contoso.com";
//             string from = "ben@contoso.com";
//             MailMessage message = new MailMessage(from, to);
//             message.Subject = "Using the new SMTP client.";
//             message.Body = @"Using this new feature, you can send an email message from an application very easily.";
//             SmtpClient client = new SmtpClient(server);
//             // Credentials are necessary if the server requires the client 
//             // to authenticate before it will send email on the client's behalf.
//             client.UseDefaultCredentials = true;

//             try
//             {
//                 client.Send(message);
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
//                             ex.ToString());
//             }
//         }

//         public static void pdf_convert()
//         {
            
//             using (PdfDocument document = PdfDocument.Open(@"C:\my-file.pdf"))
//             {
//                 int pageCount = document.NumberOfPages;

//                 Page page = document.GetPage(6);

//                 decimal widthInPoints = page.Width;
//                 decimal heightInPoints = page.Height;

//                 string text = page.Text;
//                 text.Count();
//                 var parseP = Parse.Char('P').AtLeastOnce();
//                 var H = parseP.Parse(text);
//                 H.Count();
//                 widthInPoints++;
//             }
//         }

//     }
// }
           

            
    