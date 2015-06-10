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
    public class StandardEmployeesController : ApiController
    {
        private DanxDbContext db = new DanxDbContext();

        // GET: api/StandardEmployees
        public IQueryable<StandardEmployee> GetStandardEmployee()
        {
            return db.StandardEmployee;
        }

        // GET: api/StandardEmployees/5
        [ResponseType(typeof(StandardEmployee))]
        public IHttpActionResult GetStandardEmployee(int id)
        {
            StandardEmployee standardEmployee = db.StandardEmployee.Find(id);
            if (standardEmployee == null)
            {
                return NotFound();
            }

            return Ok(standardEmployee);
        }

        // PUT: api/StandardEmployees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStandardEmployee(int id, StandardEmployee standardEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != standardEmployee.EmployeeId)
            {
                return BadRequest();
            }

            db.Entry(standardEmployee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StandardEmployeeExists(id))
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

        // POST: api/StandardEmployees
        [ResponseType(typeof(StandardEmployee))]
        public IHttpActionResult PostStandardEmployee(StandardEmployee standardEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StandardEmployee.Add(standardEmployee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = standardEmployee.EmployeeId }, standardEmployee);
        }

        // DELETE: api/StandardEmployees/5
        [ResponseType(typeof(StandardEmployee))]
        public IHttpActionResult DeleteStandardEmployee(int id)
        {
            StandardEmployee standardEmployee = db.StandardEmployee.Find(id);
            if (standardEmployee == null)
            {
                return NotFound();
            }

            db.StandardEmployee.Remove(standardEmployee);
            db.SaveChanges();

            return Ok(standardEmployee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StandardEmployeeExists(int id)
        {
            return db.StandardEmployee.Count(e => e.EmployeeId == id) > 0;
        }
    }
}