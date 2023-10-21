using System;
using HelpDeskDAL;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelpdeskDAL;

namespace HelpdeskViewModels
{
    public class EmployeeViewModel
    {
        private readonly EmployeeDAO _dao;

        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Timer { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public bool IsTech { get; set; }
        public int Id { get; set; }
        public string StaffPicture64 { get; set; }

        public EmployeeViewModel()
        {
            _dao = new EmployeeDAO();
        }

        public async Task GetByEmail()
        {
            try
            {
                Employee stu = await _dao.GetByEmail(Email);
                Title = stu.Title;
                FirstName = stu.FirstName;
                LastName = stu.LastName;
                PhoneNo = stu.PhoneNo;
                Id = stu.Id;
                DepartmentId = stu.DepartmentId;
                if (stu.StaffPicture != null)
                {
                    StaffPicture64 = Convert.ToBase64String(stu.StaffPicture);
                }
                Timer = Convert.ToBase64String(stu.Timer);
            }
            catch (NullReferenceException nex)
            {
                LastName = "not found" + nex.Message;
            }
            catch (Exception ex)
            {
                LastName = "not found";
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw;
            }
        }

        public async Task GetById()
        {
            try
            {
                Employee stu = await _dao.GetById(Id);
                Title = stu.Title;
                FirstName = stu.FirstName;
                LastName = stu.LastName;
                PhoneNo = stu.PhoneNo;
                Email = stu.Email;
                Id = stu.Id;
                DepartmentId = stu.DepartmentId;
                if (stu.StaffPicture != null)
                {
                    StaffPicture64 = Convert.ToBase64String(stu.StaffPicture);
                }
                Timer = Convert.ToBase64String(stu.Timer);
            }
            catch (NullReferenceException nex)
            {
                LastName = "not found" + nex.Message;
            }
            catch (Exception ex)
            {
                LastName = "not found";
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw;
            }
        }

        //
        // Retrieve all the students
        //
        public async Task<List<EmployeeViewModel>> GetAll()
        {
            List<EmployeeViewModel> allVms = new List<EmployeeViewModel>();
            try
            {
                List<Employee> allEmployees = await _dao.GetAll();
                foreach (Employee stu in allEmployees)
                {
                    EmployeeViewModel stuVm = new EmployeeViewModel
                    {
                        Title = stu.Title,
                        FirstName = stu.FirstName,
                        LastName = stu.LastName,
                        PhoneNo = stu.PhoneNo,
                        Email = stu.Email,
                        Id = stu.Id,
                        DepartmentId = stu.DepartmentId,
                        Timer = Convert.ToBase64String(stu.Timer)
                    };
                    allVms.Add(stuVm);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                   MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw;
            }
            return allVms;
        }

        public async Task Add()
        {
            Id = -1;
            try
            {
                Employee stu = new Employee
                {
                    Title = Title,
                    FirstName = FirstName,
                    LastName = LastName,
                    PhoneNo = PhoneNo,
                    Email = Email,
                    DepartmentId = DepartmentId
                };
                Id = await _dao.Add(stu);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                   MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw;
            }
        }

        public async Task<int> Update()
        {
            int studentUpdated = -1;
            try
            {
                Employee stu = new Employee
                {
                    Title = Title,
                    FirstName = FirstName,
                    LastName = LastName,
                    PhoneNo = PhoneNo,
                    Email = Email,
                    Id = Id,
                    DepartmentId = DepartmentId
                };
                if (StaffPicture64 != null)
                {
                    stu.StaffPicture = Convert.FromBase64String(StaffPicture64);
                }

                stu.Timer = Convert.FromBase64String(Timer);
                studentUpdated = await _dao.Update(stu);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                   MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw;
            }
            return studentUpdated;
        }

        public async Task<int> Delete()
        {
            int studentDeleted = -1;
            try
            {
                studentDeleted = await _dao.Delete(Id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                   MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw;
            }
            return studentDeleted;
        }

    }
}