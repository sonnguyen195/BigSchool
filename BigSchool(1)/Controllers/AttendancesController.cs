using BigSchool_1_.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using BigSchool_1_.DTOs;

namespace BigSchool_1_.Controllers
{
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _dbContext;
        public AttendancesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IHttpActionResult Attend(AttendanceDTOs attendanceDTOs)
        {
            var userId = User.Identity.GetUserId();
            if (_dbContext.Attendances.Any(a => a.AttendeeId == userId && a.CourseId == attendanceDTOs.CourseId))
                return BadRequest("The Attendance already exist");
            var attendance = new Attendance
            {
                CourseId = attendanceDTOs.CourseId,
                AttendeeId = userId
            };
            _dbContext.Attendances.Add(attendance);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
