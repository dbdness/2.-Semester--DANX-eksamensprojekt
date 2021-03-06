﻿using System;
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
    public class AdminEmployeesController : ApiController
    {
        private DanxDbContext db = new DanxDbContext();

        // GET: api/AdminEmployees
        public IQueryable<AdminEmployee> GetAdminEmployee()
        {
            return db.AdminEmployee;
        }

        // GET: api/AdminEmployees/5
        [ResponseType(typeof(AdminEmployee))]
        public IHttpActionResult GetAdminEmployee(int id)
        {
            AdminEmployee adminEmployee = db.AdminEmployee.Find(id);
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

            db.AdminEmployee.Add(adminEmployee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = adminEmployee.EmployeeId }, adminEmployee);
        }

        // DELETE: api/AdminEmployees/5
        [ResponseType(typeof(AdminEmployee))]
        public IHttpActionResult DeleteAdminEmployee(int id)
        {
            AdminEmployee adminEmployee = db.AdminEmployee.Find(id);
            if (adminEmployee == null)
            {
                return NotFound();
            }

            db.AdminEmployee.Remove(adminEmployee);
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
            return db.AdminEmployee.Count(e => e.EmployeeId == id) > 0;
        }
    }
}