using System;
using System.Collections.Generic;
using EF_Core.MitegatorAcademy.Entities;
using EF_Core.MitegatorAcademy.Enums;

namespace EF_Core.MitegatorAcademy.Data.Config;

public static class SeedData
{
    public static List<Course> LoadCourses() => new List<Course>()
    {
        new Course {Id = 1, CourseName = "Mathematics", Price = 2000m},
        new Course {Id = 2, CourseName = "Physics", Price = 2000m},
        new Course {Id = 3, CourseName = "Chemistry", Price = 1500m},
        new Course {Id = 4, CourseName = "Biology", Price = 1200m},
        new Course {Id = 5, CourseName = "CS-50", Price = 3000m},
    };

    public static List<Enrollment> LoadEnrollments() => new List<Enrollment>
    {
        new Enrollment() { ParticipantId = 1, SectionId = 6 },
        new Enrollment() { ParticipantId = 2, SectionId = 6 },
        new Enrollment() { ParticipantId = 3, SectionId = 7 },
        new Enrollment() { ParticipantId = 4, SectionId = 7 },
        new Enrollment() { ParticipantId = 5, SectionId = 8 },
        new Enrollment() { ParticipantId = 6, SectionId = 8 },
        new Enrollment() { ParticipantId = 7, SectionId = 9 },
        new Enrollment() { ParticipantId = 8, SectionId = 9 },
        new Enrollment() { ParticipantId = 9, SectionId = 10 },
        new Enrollment() { ParticipantId = 10, SectionId = 10 }
    };

    public static List<Instructor> LoadInstructors() => new List<Instructor>()
    {
        new Instructor(1, "Ahmed", "Abdullah", 1),
        new Instructor(2, "Yamen", "Mohammed", 2),
        new Instructor(3, "Khalid", "Hassan", 3),
        new Instructor(4, "Nadia", "Ali", 4),
        new Instructor(5, "Omar", "Ibrahim", 5),
    };

    public static List<Office> LoadOffices() => new List<Office>()
    {
        new Office(1, "Off_05", "building A"),
        new Office(2, "Off_12", "building B"),
        new Office(3, "Off_32", "Administration"),
        new Office(4, "Off_44", "IT Department"),
        new Office(5, "Off_43", "IT Department"),
    };

    public static List<Schedule> LoadSchedules() => new List<Schedule>()
    {
        new Schedule { Id = 1, Title = ScheduleTitleEnum.Daily, SUN = true, MON = true, TUE = true, WED = true, THU = true, FRI = false, SAT = false },
        new Schedule { Id = 2, Title = ScheduleTitleEnum.DayAfterDay, SUN = true, MON = false, TUE = true, WED = false, THU = true, FRI = false, SAT = false },
        new Schedule { Id = 3, Title = ScheduleTitleEnum.TwiceAWeek, SUN = false, MON = true, TUE = false, WED = true, THU = false, FRI = false, SAT = false },
        new Schedule { Id = 4, Title = ScheduleTitleEnum.Weekend, SUN = false, MON = false, TUE = false, WED = false, THU = false, FRI = true, SAT = true },
        new Schedule { Id = 5, Title = ScheduleTitleEnum.Compact, SUN = true, MON = true, TUE = true, WED = true, THU = true, FRI = true, SAT = true }
    };

    public static List<Section> LoadSections() => new List<Section>()
    {
        new Section(){Id = 1, SectionName = "S_MA1", CourseId = 1, InstructorId = 1, ScheduleId = 1, TimeSlot = new TimeSlot(){ StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00") }},
        new Section(){Id = 2, SectionName = "S_MA2", CourseId = 1, InstructorId = 2, ScheduleId = 3, TimeSlot = new TimeSlot(){ StartTime = TimeSpan.Parse("14:00:00"), EndTime = TimeSpan.Parse("18:00:00") }},
        new Section(){Id = 3, SectionName = "S_PH1", CourseId = 2, InstructorId = 1, ScheduleId = 4, TimeSlot = new TimeSlot(){ StartTime = TimeSpan.Parse("10:00:00"), EndTime = TimeSpan.Parse("15:00:00") }},
        new Section(){Id = 4, SectionName = "S_PH2", CourseId = 2, InstructorId = 3, ScheduleId = 1, TimeSlot = new TimeSlot(){ StartTime = TimeSpan.Parse("10:00:00"), EndTime = TimeSpan.Parse("12:00:00") }},
        new Section(){Id = 5, SectionName = "S_CH1", CourseId = 3, InstructorId = 2, ScheduleId = 1, TimeSlot = new TimeSlot(){ StartTime = TimeSpan.Parse("16:00:00"), EndTime = TimeSpan.Parse("18:00:00") }},
        new Section(){Id = 6, SectionName = "S_CH2", CourseId = 3, InstructorId = 3, ScheduleId = 2, TimeSlot = new TimeSlot(){ StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00") }},
        new Section(){Id = 7, SectionName = "S_BI1", CourseId = 4, InstructorId = 4, ScheduleId = 3, TimeSlot = new TimeSlot(){ StartTime = TimeSpan.Parse("11:00:00"), EndTime = TimeSpan.Parse("14:00:00") }},
        new Section(){Id = 8, SectionName = "S_BI2", CourseId = 4, InstructorId = 5, ScheduleId = 4, TimeSlot = new TimeSlot(){ StartTime = TimeSpan.Parse("10:00:00"), EndTime = TimeSpan.Parse("14:00:00") }},
        new Section(){Id = 9, SectionName = "S_CS1", CourseId = 5, InstructorId = 4, ScheduleId = 4, TimeSlot = new TimeSlot(){ StartTime = TimeSpan.Parse("16:00:00"), EndTime = TimeSpan.Parse("18:00:00") }},
        new Section(){Id = 10,SectionName = "S_CS2", CourseId = 5, InstructorId = 5, ScheduleId = 3, TimeSlot = new TimeSlot(){ StartTime = TimeSpan.Parse("12:00:00"), EndTime = TimeSpan.Parse("15:00:00") }},
        new Section(){Id = 11,SectionName = "S_CS3", CourseId = 5, InstructorId = 4, ScheduleId = 5, TimeSlot = new TimeSlot(){ StartTime = TimeSpan.Parse("09:00:00"), EndTime = TimeSpan.Parse("11:00:00") }} 
    };
}