using System;
using System.ComponentModel.DataAnnotations;

namespace HabitsTracker.Data.Entities
{
    public class UnitEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}