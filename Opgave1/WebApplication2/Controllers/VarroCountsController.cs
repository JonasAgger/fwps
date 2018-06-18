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
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class VarroCountsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/VarroCounts
        public IEnumerable<VarroCount> GetVarroCounts()
        {
            var items = from it in db.VarroCounts select it;
            return items.ToList();
        }

        // GET: api/VarroCounts/5
        [ResponseType(typeof(VarroCount))]
        public IHttpActionResult GetVarroCount(int id)
        {
            VarroCount varroCount = db.VarroCounts.Find(id);
            if (varroCount == null)
            {
                return NotFound();
            }

            return Ok(varroCount);
        }

        // PUT: api/VarroCounts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVarroCount(int id, VarroCount varroCount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != varroCount.ID)
            {
                return BadRequest();
            }

            db.Entry(varroCount).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VarroCountExists(id))
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

        // POST: api/VarroCounts
        [ResponseType(typeof(VarroCount))]
        public IHttpActionResult PostVarroCount(VarroCount varroCount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VarroCounts.Add(varroCount);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = varroCount.ID }, varroCount);
        }

        // DELETE: api/VarroCounts/5
        [ResponseType(typeof(VarroCount))]
        public IHttpActionResult DeleteVarroCount(int id)
        {
            VarroCount varroCount = db.VarroCounts.Find(id);
            if (varroCount == null)
            {
                return NotFound();
            }

            db.VarroCounts.Remove(varroCount);
            db.SaveChanges();

            return Ok(varroCount);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VarroCountExists(int id)
        {
            return db.VarroCounts.Count(e => e.ID == id) > 0;
        }
    }
}