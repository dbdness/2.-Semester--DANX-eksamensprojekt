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
    public class MainEmployeesController : ApiController
    {
        private DanxDbContext db = new DanxDbContext();

        // GET: api/MainEmployees
        public IQueryable<MainEmployee> GetMainEmployees()
        {
            return db.MainEmployees;
        }

        // GET: api/MainEmployees/5
        [ResponseType(typeof(MainEmployee))]
        public IHttpActionResult GetMainEmployee(int id)
        {
            MainEmployee mainEmployee = db.MainEmployees.Find(id);
            if (mainEmployee == null)
            {
                return NotFound();
            }

            return Ok(mainEmployee);
        }

        // PUT: api/MainEmployees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMainEmployee(int id, MainEmployee mainEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mainEmployee.EmployeeId)
            {
                return BadRequest();
            }

            db.Entry(mainEmployee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MainEmployeeExists(id))
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

        // POST: api/MainEmployees
        [ResponseType(typeof(MainEmployee))]
        public IHttpActionResult PostMainEmployee(MainEmployee mainEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MainEmployees.Add(mainEmployee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mainEmployee.EmployeeId }, mainEmployee);
        }

        // DELETE: api/MainEmployees/5
        [ResponseType(typeof(MainEmployee))]
        public IHttpActionResult DeleteMainEmployee(int id)
        {
            MainEmployee mainEmployee = db.MainEmployees.Find(id);
            if (mainEmployee == null)
            {
                return NotFound();
            }

            db.MainEmployees.Remove(mainEmployee);
            db.SaveChanges();

            return Ok(mainEmployee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MainEmployeeExists(int id)
        {
            return db.MainEmployees.Count(e => e.EmployeeId == id) > 0;
        }
    }
}