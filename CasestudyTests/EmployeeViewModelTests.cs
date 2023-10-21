using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using HelpdeskDAL;
using HelpdeskViewModels;

namespace CasestudyTest
{
    public class EmployeeViewModelTests
    {
        [Fact]
        public async Task Employee_GetByEmailTest()
        {
            EmployeeViewModel vm = null;
            try
            {
                vm = new EmployeeViewModel { Email = "yp@gmail.com" };
                await vm.GetByEmail();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error - " + ex.Message);
            }
            Assert.NotNull(vm.FirstName);
        }

        /*[Fact]
        public async Task Employee_GetById()
        {
            EmployeeViewModel vm = null;
            try
            {
                vm = new EmployeeViewModel { Id = 8 };
                await vm.GetById();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error - " + ex.Message);
            }
            Assert.NotNull(vm.FirstName);
        }*/

        [Fact]
        public async Task Employee_GetAll()
        {
            try
            {
                EmployeeViewModel vm = new EmployeeViewModel { };
                List<EmployeeViewModel> allStudents = await vm.GetAll();
                Assert.True(allStudents.Count > 0);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error - " + ex.Message);
            }

        }

        [Fact]
        public async Task Employee_Add()
        {
            EmployeeViewModel vm = null;
            try
            {
                vm = new EmployeeViewModel
                {
                    Title = "Mr.",
                    FirstName = "Yaksh",
                    LastName = "Patel",
                    PhoneNo = "5488810622",
                    DepartmentId = 10,
                    IsTech = false,
                    Email = "yp@gmail.com"
                };
                await vm.Add();
                Assert.True(vm.Id > 0);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error - " + ex.Message);
            }

        }


        [Fact]
        public async Task Employee_UpdateTest()
        {
            try
            {
                EmployeeViewModel vm = new EmployeeViewModel { LastName = "Patel" };
                await vm.GetByEmail(); // Employee just added
                vm.PhoneNo = vm.PhoneNo == "5488810622" ? "5488817676" : "5488810622";
                int StudentsUpdated = await vm.Update();
                Assert.True(StudentsUpdated > 0);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error - " + ex.Message);
            }
        }

        [Fact]
        public async Task Employee_DeleteTest()
        {
            try
            {
                EmployeeViewModel vm = new EmployeeViewModel { LastName = "Patel" };
                await vm.GetByEmail();
                int studentsDeleted = await vm.Delete();
                Assert.True(studentsDeleted == 1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error - " + ex.Message);
            }
        }
    }
}
