﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Job_Requests.DataAccess.Data;
using Job_Requests.Models;
using Job_Requests.DataAccess.Services;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Job_Requests.Models.Enums;
using Job_Requests.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Job_Requests.Models.Consts;

namespace Job_Requests.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.ROLE_ADMIN)]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        // GET: Departments
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;

            return View(await _service.GetPaginatedDepartmentsAsync(page, pageSize));
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var department = await _service.GetDepartmentByIdAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentId,DepartmentName,DepartmentDescription")] Department department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (await _service.IsExistingDepartmentNameWithDifferentId(department.DepartmentId, department.DepartmentName))
                    {
                        ModelState.AddModelError("DepartmentName", "Existing Department Name");
                        return View(department);
                    }

                    await _service.AddDepartmentAsync(department);
                    TempData["success"] = "Department Created Successfully.";

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    TempData["error"] = $"Saving Failed. Error Encountered. {ex.Message}";

                }

            }

            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var department = await _service.GetDepartmentByIdAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (await _service.IsExistingDepartmentNameWithDifferentId(id, department.DepartmentName))
                    {
                        ModelState.AddModelError("DepartmentName", "Existing Department Name");
                        return View(department);
                    }

                    await _service.UpdateDepartmentAsync(department);
                    TempData["success"] = "Department Updated Successfully.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _service.IsExistingDepartmentId(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(department);
        }

        // GET: Departments/Manage/5
        public async Task<IActionResult> Manage(int id)
        {

            var department = await _service.GetDepartmentByIdAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(int id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (department.Status == RecordStatusEnum.Active)
                    {
                        department.Status = RecordStatusEnum.Inactive;
                        TempData["success"] = "Department Deactivated Successfully.";
                    }
                    else
                    {
                        department.Status = RecordStatusEnum.Active;
                        TempData["success"] = "Department Activated Successfully.";
                    }

                    await _service.UpdateDepartmentAsync(department);
                }
                catch (Exception ex)
                {
                    TempData["error"] = $"Update Failed. \n{ex.Message}";
                    return View(department);

                    throw;

                }

                return RedirectToAction(nameof(Index));
            }

            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var department = await _service.GetDepartmentByIdAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _service.GetDepartmentByIdAsync(id);

            if (department != null)
            {
                try
                {
                    await _service.DeleteDepartmentAsync(department);
                    TempData["success"] = "Department Deleted Successfully.";

                    //return RedirectToAction(nameof(Index));

                    return Json(new { success = true });
                }
                catch (DbUpdateException ex)
                {

                    if (ex.InnerException is SqlException sqlEx)
                    {
                        // Handle the specific SQL exception for constraint violations, if needed
                        if (sqlEx.Number == 547)  // SQL error code for foreign key constraint violation
                        {
                            ViewBag.ReferencedDepartment = $"Cannot delete department because it is referenced by other records.";
                        }
                    }
                    else
                    {
                        TempData["error"] = $"Unable to Delete.\n {ex.Message}";
                    }

                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    throw;
                }

            }

            //return View(department);

            return Json(new { success = false, message = "Department not found." });
        }

    }
}
