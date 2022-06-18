﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AuthenticationService.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AuthenticationService.Data
{
    public class UserServiceContext : IdentityDbContext<UserModel>
    {
        public UserServiceContext(DbContextOptions options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Group>()
                .HasOne(c => c.Year)
                .WithMany(e=>e.YearGroups)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.Entity<StudentPassedExam>().HasKey(c => new { c.StudentId, c.ExamId });
            builder.Entity<ExamGroup>().HasKey(c => new { c.GroupId, c.ExamId });

            builder.Entity<Group>()
                .HasOne(c => c.Section)
                .WithMany(e=>e.Groups)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<Year> Years { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<StudyModule> Modules { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<QuestionSolution> QuestionSolutions { get; set; }
        public DbSet<StudentPassedExam> StudentPassedExams { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        
    }
}
