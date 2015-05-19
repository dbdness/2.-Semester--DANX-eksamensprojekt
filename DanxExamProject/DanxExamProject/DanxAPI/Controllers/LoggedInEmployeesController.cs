using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DanxAPI;

namespace DanxAPI.Controllers
{
    public class LoggedInEmployeesController : ApiController
    {
        private DanxDbContext db = new DanxDbContext();

        // GET: api/LoggedInEmployees
        public IQueryable<LoggedInEmployee> GetLoggedInEmployees()
        {
            return db.LoggedInEmployees;
        }

        // GET: api/LoggedInEmployees/5
        [ResponseType(typeof(LoggedInEmployee))]
        public IHttpActionResult GetLoggedInEmployee(int id)
        {
            LoggedInEmployee loggedInEmployee = db.LoggedInEmployees.Find(id);
            if (loggedInEmployee == null)
            {
                return NotFound();
            }

            return Ok(loggedInEmployee);
        }

        // PUT: api/LoggedInEmployees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLoggedInEmployee(int id, LoggedInEmployee loggedInEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != loggedInEmployee.EmployeeId)
            {
                return BadRequest();
            }

            db.Entry(loggedInEmployee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoggedInEmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/LoggedInEmployees
        [ResponseType(typeof(LoggedInEmployee))]
        public IHttpActionResult PostLoggedInEmployee(LoggedInEmployee loggedInEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LoggedInEmployees.Add(loggedInEmployee);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LoggedInEmployeeExists(loggedInEmployee.EmployeeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = loggedInEmployee.EmployeeId }, loggedInEmployee);
        }

        // DELETE: api/LoggedInEmployees/5
        [ResponseType(typeof(LoggedInEmployee))]
        public IHttpActionResult DeleteLoggedInEmployee(int id)
        {
            LoggedInEmployee loggedInEmployee = db.LoggedInEmployees.Find(id);
            if (loggedInEmployee == null)
            {
                return NotFound();
            }

            db.LoggedInEmployees.Remove(loggedInEmployee);
            db.SaveChanges();

            return Ok(loggedInEmployee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LoggedInEmployeeExists(int id)
        {
            return db.LoggedInEmployees.Count(e => e.EmployeeId == id) > 0;
        }
    }
}