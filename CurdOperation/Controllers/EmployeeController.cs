using CurdOperation.Data;
using CurdOperation.Models;
using CurdOperation.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CurdOperation.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MVCDemoDbcontext mvcDemoDbcontext;

        public EmployeeController(MVCDemoDbcontext mvcDemoDbcontext)
        {
            this.mvcDemoDbcontext = mvcDemoDbcontext;
        }
        [HttpGet]

        public async Task<IActionResult>Index()
        {
            var employee= await mvcDemoDbcontext.employees.ToListAsync();
            return View(employee);
        }


        [HttpGet]

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Add(AddEmployeeViewModel addEmployeeRequest) {
            var employee = new Employee()
            {
                id = Guid.NewGuid(),
                name = addEmployeeRequest.name,
                age = addEmployeeRequest.age,
                department = addEmployeeRequest.department,
                email = addEmployeeRequest.email,
                salary = addEmployeeRequest.salary,
                dateOfBirth = addEmployeeRequest.dateOfBirth
            };

            await mvcDemoDbcontext.employees.AddAsync(employee);
            await mvcDemoDbcontext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]

        public async Task<IActionResult>View(Guid id)
        {
            var employee =await mvcDemoDbcontext.employees.FirstOrDefaultAsync(x => x.id == id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    id = employee.id,
                    name = employee.name,
                    age = employee.age,
                    department = employee.department,
                    email = employee.email,
                    salary = employee.salary,
                    dateOfBirth = employee.dateOfBirth
                };
                return await Task.Run(()=> View("View",viewModel)); 
            }
            return RedirectToAction("Index"); 
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbcontext.employees.FindAsync(model.id);

            if (employee != null)
            {
                employee.name = model.name;
                employee.age = model.age;
                employee.department = model.department;
                employee.email = model.email;
                employee.salary = model.salary;
                employee.dateOfBirth = model.dateOfBirth;
                await mvcDemoDbcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbcontext.employees.FindAsync(model.id);
            if (employee != null)
            {
                mvcDemoDbcontext.employees.Remove(employee);
                await mvcDemoDbcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }


}
