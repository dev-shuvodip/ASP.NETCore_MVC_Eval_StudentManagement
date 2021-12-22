using Microsoft.AspNetCore.Mvc;
using StudentManagement.Data;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET
        public IActionResult Index()
        {
            IEnumerable<Student> student = _db.Students;
            return View(student);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        public IActionResult Create(Student obj)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Add(obj);
                _db.SaveChanges();

                StudentTracker studentTracker = new()
                {
                    OperationLog = "New Student " + obj.StudentName + " created with ID: " + obj.StudentId
                };

                _db.StudentTracker.Add(studentTracker);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var studentFromDB = _db.Students.Find(id);

            if (studentFromDB == null)
            {
                return NotFound();
            }

            return View(studentFromDB);
        }

        //POST
        [HttpPost]
        public IActionResult Edit(Student obj)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Update(obj);
                _db.SaveChanges();

                StudentTracker studentTracker = new()
                {
                    OperationLog = "Student " + obj.StudentName + " ID - " + obj.StudentId + " edited with new Course " + obj.Course
                };

                _db.StudentTracker.Add(studentTracker);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var studentFromDB = _db.Students.Find(id);

            if (studentFromDB == null)
            {
                return NotFound();
            }

            return View(studentFromDB);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteStudent(int? id)
        {
            var obj = _db.Students.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            StudentTracker studentTracker = new()
            {
                OperationLog = "Student " + obj.StudentName + " ID - " + obj.StudentId + " deleted"
            };

            _db.Students.Remove(obj);
            _db.SaveChanges();

            _db.StudentTracker.Add(studentTracker);
            _db.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}
