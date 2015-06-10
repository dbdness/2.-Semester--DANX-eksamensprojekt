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
using ApiDanx;

namespace ApiDanx.Controllers
{
    public class AdminEmployeesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/AdminEmployees
        public IQueryable<AdminEmployee> GetAdminEmployees()
        {
            return db.AdminEmployees;
        }

        // GET: api/AdminEmployees/5
        [ResponseType(typeof(AdminEmployee))]
        public IHttpActionResult GetAdminEmployee(int id)
        {
            AdminEmployee adminEmployee = db.AdminEmployees.Find(id);
            if (adminEmployee == null)
            {
                return NotFound();
            }

            return Ok(adminEmployee);
        }

        // PUT: api/AdminEmployees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdminEmployee(int id, AdminEmployee adminEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != adminEmployee.EmployeeId)
            {
                return BadRequest();
            }

            db.Entry(adminEmployee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminEmployeeExists(id))
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

        // POST: api/AdminEmployees
        [ResponseType(typeof(AdminEmployee))]
        public IHttpActionResult PostAdminEmployee(AdminEmployee adminEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AdminEmployees.Add(adminEmployee);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AdminEmployeeExists(adminEmployee.EmployeeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = adminEmployee.EmployeeId }, adminEmployee);
        }

        // DELETE: api/AdminEmployees/5
        [ResponseType(typeof(AdminEmployee))]
        public IHttpActionResult DeleteAdminEmployee(int id)
        {
            AdminEmployee adminEmployee = db.AdminEmployees.Find(id);
            if (adminEmployee == null)
            {
                return NotFound();
            }

            db.AdminEmployees.Remove(adminEmployee);
            db.SaveChanges();

            return Ok(adminEmployee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdminEmployeeExists(int id)
        {
            return db.AdminEmployees.Count(e => e.EmployeeId == id) > 0;
        }
    }
}