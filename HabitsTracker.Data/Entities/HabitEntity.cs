﻿using System;
using System.Collections.Generic;
using HabitsTracker.Data.Enums;

namespace HabitsTracker.Data.Entities
{
    public class HabitEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        /// <summary>
        /// Цель в числовом эквиваленте
        /// </summary>
        public int Goal { get; set; }

        /// <summary>
        /// Период повтора (daily, weekly, monthly)
        /// </summary>
        public GoalPeriod GoalPeriod { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

        public Guid UnitId { get; set; }
        public UnitEntity Unit { get; set; }

        public List<HabitReminderEntity> Reminders { get; set; } = new();

        public List<FrequencyEntity> Frequencies { get; set; } = new();
        
        public List<HabitTrackingEntity> TrackRecords { get; set; } = new();
    }
}