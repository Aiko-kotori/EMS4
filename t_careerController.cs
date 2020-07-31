using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EMS.Models;
using static EMS.Models.t_career;

namespace EMS.Controllers
{
    public class t_careerController : Controller
    {
        private employeeEntities db = new employeeEntities();

        // GET: t_career
        public ActionResult Index()
        {
            var t_career = db.t_career.Include(t => t.m_customer).Include(t => t.m_employee).Include(t => t.m_position);
            return View(t_career.ToList());
        }
        public ActionResult End()
        {
            var t_career = db.t_career.Include(t => t.m_customer).Include(t => t.m_employee).Include(t => t.m_position);
            return View(t_career.ToList());
        }

        // GET: t_career/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_career t_career = db.t_career.Find(id);
            if (t_career == null)
            {
                return HttpNotFound();
            }
            return View(t_career);
        }

        // GET: t_career/Create
        public ActionResult Create()
        {
            ViewBag.customer_cd = new SelectList(db.m_customer, "customer_cd", "customer_nm");
            ViewBag.emp_cd = new SelectList(db.m_employee, "emp_cd", "user_id");
            ViewBag.position_cd = new SelectList(db.m_position, "position_cd", "position_name");
            return View();
        }

        // POST: t_career/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "career_no,emp_cd,item_nm,item_detail,work_class_sp,work_class_ra,work_class_bd,work_class_sd,work_class_dd,work_class_pg,work_class_pt,work_class_st,work_class_etc,position_cd,os_nm,tools_nm,work_start_date,work_end_date,customer_cd,work_site_nm,remarks,created,updated,deleted")] t_career t_career)
        {
            if (ModelState.IsValid)
            {
                db.t_career.Add(t_career);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customer_cd = new SelectList(db.m_customer, "customer_cd", "customer_nm", t_career.customer_cd);
            ViewBag.emp_cd = new SelectList(db.m_employee, "emp_cd", "user_id", t_career.emp_cd);
            ViewBag.position_cd = new SelectList(db.m_position, "position_cd", "position_name", t_career.position_cd);
            return View(t_career);
        }

        // GET: t_career/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_career t_career = db.t_career.Find(id);
            if (t_career == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_cd = new SelectList(db.m_customer, "customer_cd", "customer_nm", t_career.customer_cd);
            ViewBag.emp_cd = new SelectList(db.m_employee, "emp_cd", "user_id", t_career.emp_cd);
            ViewBag.position_cd = new SelectList(db.m_position, "position_cd", "position_name", t_career.position_cd);
            return View(t_career);
        }

        // POST: t_career/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "career_no,emp_cd,item_nm,item_detail,work_class_sp,work_class_ra,work_class_bd,work_class_sd,work_class_dd,work_class_pg,work_class_pt,work_class_st,work_class_etc,position_cd,os_nm,tools_nm,work_start_date,work_end_date,customer_cd,work_site_nm,remarks,created,updated,deleted")] t_career t_career)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_career).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_cd = new SelectList(db.m_customer, "customer_cd", "customer_nm", t_career.customer_cd);
            ViewBag.emp_cd = new SelectList(db.m_employee, "emp_cd", "user_id", t_career.emp_cd);
            ViewBag.position_cd = new SelectList(db.m_position, "position_cd", "position_name", t_career.position_cd);
            return View(t_career);
        }

        // GET: t_career/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t_career t_career = db.t_career.Find(id);
            if (t_career == null)
            {
                return HttpNotFound();
            }
            return View(t_career);
        }

        // POST: t_career/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            t_career t_career = db.t_career.Find(id);
            db.t_career.Remove(t_career);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //Get
        public ActionResult Maneger()
        {
            CareerTerm ct = new CareerTerm();
            ct.Maneger = "t_career.work_end_date - t_career.work_start_date;";
            return View(ct);
        }
        public ActionResult Leader()
        {
            CareerTerm ct = new CareerTerm();
            ct.Leader = "t_career.work_end_date - t_career.work_start_date;";
            return View(ct);
        }
        public ActionResult SubMane()
        {
            CareerTerm ct = new CareerTerm();
            ct.Submane = "t_career.work_end_date - t_career.work_start_date;";
            return View(ct);
        }
        public ActionResult Member()
        {
            CareerTerm ct = new CareerTerm();
            ct.Member = "t_career.work_end_date - t_career.work_start_date;";
            return View(ct);
        }
        //Post
        [HttpPost]
        public ActionResult Maneger(CareerTerm ct)
        {
            ct.Maneger = "t_career.work_end_date - t_career.work_start_date";
            return View(ct);
        }
        public ActionResult Leader(CareerTerm ct)
        {
            ct.Leader = "t_career.work_end_date - t_career.work_start_date";
            return View(ct);
        }
        public ActionResult Submane(CareerTerm ct)
        {
            ct.Submane = "t_career.work_end_date - t_career.work_start_date";
            return View(ct);
        }
        public ActionResult Member(CareerTerm ct)
        {
            ct.Member = "t_career.work_end_date - t_career.work_start_date";
            return View(ct);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
