using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Diagnostics;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelpdeskDAL;


namespace HelpDeskDAL
{
    public class EmployeeDAO
    {
        public async Task<Employee> GetByEmail(string Email)
        {
            Employee selectedEmployee = null;
            try
            {
                HelpdeskContext _db = new HelpdeskContext();
                selectedEmployee = await _db.Employee.FirstOrDefaultAsync(stu => stu.Email == Email);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                                 MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw;
            }
            return selectedEmployee;
        }
         public async Task<Employee> GetById(int id)
         {
             Employee selectedEmployee = null;

             try
             {
                 HelpdeskContext _db = new HelpdeskContext();
                 selectedEmployee = await _db.Employee.FirstOrDefaultAsync(stu => stu.Id == id);
             }
             catch (Exception ex)
             {
                 Debug.WriteLine("Problem in " + GetType().Name + " " +
                              MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                 throw;
             }

             return selectedEmployee;
         }

         public async Task<List<Employee>> GetAll()
         {
             List<Employee> allEmployee = new List<Employee>();

             try
             {
                 HelpdeskContext _db = new HelpdeskContext();
                 allEmployee = await _db.Employee.ToListAsync();
             }
             catch (Exception ex)
             {
                 Debug.WriteLine("Problem in " + GetType().Name + " " +
                              MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                 throw;
             }

             return allEmployee;
         }

         public async Task<int> Add(Employee newEmployee)
         {
             try
             {
                 HelpdeskContext _db = new HelpdeskContext();
                 await _db.Employee.AddAsync(newEmployee);
                 await _db.SaveChangesAsync();
             }
             catch (Exception ex)
             {
                 Debug.WriteLine("Problem in " + GetType().Name + " " +
                              MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                 throw;
             }

             return newEmployee.Id;
         }


         public async Task<int> Update(Employee updatedEmployee)
         {
             int studentsUpdated = -1;

             try
             {
                 HelpdeskContext _db = new HelpdeskContext();
                 Employee currentEmployee = await _db.Employee.FirstOrDefaultAsync(stu => stu.Id == updatedEmployee.Id);
                 _db.Entry(currentEmployee).CurrentValues.SetValues(updatedEmployee);
                 studentsUpdated = await _db.SaveChangesAsync();
             }
             catch (Exception ex)
             {
                 Debug.WriteLine("Problem in " + GetType().Name + " " +
                              MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                 throw;
             }

             return studentsUpdated;
         }

         public async Task<int> Delete(int id)
         {
             int studentsDeleted = -1;

             try
             {
                 HelpdeskContext _db = new HelpdeskContext();
                 Employee selectedEmployee = await _db.Employees.FirstOrDefaultAsync(stu => stu.Id == id);
                 _db.Employees.Remove(selectedEmployee);
                 studentsDeleted = await _db.SaveChangesAsync();          //returns # of rows removed
             }
             catch (Exception ex)
             {
                 Debug.WriteLine("Problem in " + GetType().Name + " " +
                              MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                 throw;
             }

             return studentsDeleted;
         }


    }

}