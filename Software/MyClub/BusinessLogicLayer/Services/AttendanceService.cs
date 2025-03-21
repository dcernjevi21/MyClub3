﻿using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class AttendanceService
    {
        public List<Attendance> GetAttendances()
        {
            using (var repo = new AttendancesRepository())
            {
                return repo.GetAllAttendances().ToList();
            }
        }

        public List<Attendance> GetTrainingAttendanceById(int teamId)
        {
            using (var repo = new AttendancesRepository())
            {
                return repo.GetTrainingAttendanceById(teamId).ToList();
            }
        }

        public bool AddAttendance(Attendance attendance)
        {
            bool isSuccessful = false;
            using (var repo = new AttendancesRepository())
            {
                int affectedRows = repo.AddNewAttendance(attendance);
                isSuccessful = affectedRows > 0;
            }
            return isSuccessful;
        }

        public bool UpdateAttendance(Attendance attendance)
        {
            bool isSuccessful = false;
            using (var repo = new AttendancesRepository())
            {
                int affectedRows = repo.Update(attendance);
                isSuccessful = affectedRows > 0;
            }
            return isSuccessful;
        }
        public List<Attendance> GetAttendancesByTrainingId(int trainingId)
        {
            using (var repo = new AttendancesRepository())
            {
                return repo.GetAttendancesByTrainingId(trainingId).ToList();
            }
        }

        public List<Attendance> GetAttendancesByMatchId(int matchId)
        {
            using (var repo = new AttendancesRepository())
            {
                return repo.GetAttendancesByMatchId(matchId).ToList();
            }
        }

        public List<Attendance> GetUserAttendances(int userId)
        {
            using (var repo = new AttendancesRepository())
            {
                return repo.GetUserAttendances(userId).ToList();
            }
        }

        public List<Attendance> GetTeamAttendancesForPeriod(int teamId, DateTime startDate, DateTime endDate)
        {
            using (var repo = new AttendancesRepository())
            {
                return repo.GetTeamAttendancesForPeriod(teamId, startDate, endDate).ToList();
            }
        }

        public List<Attendance> FilterAttendancesByEventType(List<Attendance> attendances, int eventType)
        {
            if (eventType == 1)
                return attendances.Where(a => a.TrainingID != null).ToList();
            else if (eventType == 2)
                return attendances.Where(a => a.MatchId != null).ToList();
            return attendances;
        }

        public List<Attendance> GetAttendancesForUser(List<Attendance> attendances, int userId)
        {
            return attendances.Where(a => a.UserID == userId).ToList();
        }

        public int CountAttendancesByStatus(List<Attendance> attendances, int statusId)
        {
            return attendances.Count(a => a.StatusID == statusId);
        }
        public List<Attendance> GetExistingTrainingAttendances(int trainingId, int teamId)
        {
            return GetTrainingAttendanceById(teamId)
                .Where(a => a.TrainingID == trainingId)
                .ToList();
        }
    }
}
