﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthenticationService.Data;
using AuthenticationService.Models;
using AuthenticationService.Repositories;
using AuthenticationService.DTOs.Group;

namespace AuthenticationService.Controllers
{
    [Route("api/study/[controller]")]
    [ApiController]
    public class StudyGroupsController : ControllerBase
    {
        private readonly IGroupRepository repo;

        public StudyGroupsController(IGroupRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet("/year/{id}")]
        public ActionResult<Group> GetYearGroups(Guid id)
        {
            var sections = repo.GetYearGroups(id);
            if (sections == null)
            {
                return NotFound();
            }
            return Ok(sections);
        }

        [HttpGet("/section/{id}")]
        public ActionResult<Group> GetSectionGroups(Guid id)
        {
            var sections = repo.GetSectionGroups(id);
            if (sections == null)
            {
                return NotFound();
            }
            return Ok(sections);
        }



        [HttpPost]
        public ActionResult<Group> CreateGroup(GroupDtoForCreation group)
        {
            return Ok(repo.AddGroup(group));
        }

        // DELETE: api/StudyGroups/5
        [HttpDelete("{id}")]
        public IActionResult DeleteGroup(Guid id)
        {
            repo.RemoveGroup(id);
            return NoContent();
        }
    }
}