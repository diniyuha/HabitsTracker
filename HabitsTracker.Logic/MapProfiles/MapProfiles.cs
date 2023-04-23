using System.Linq;
using AutoMapper;
using HabitsTracker.Data.Entities;
using HabitsTracker.Logic.Models;

namespace HabitsTracker.Logic.MapProfiles
{
    public class MapProfiles : Profile
    {
        public MapProfiles()
        {
            CreateMap<HabitEntity, Habit>()
                .ForMember(d => d.DayNumbers, o => o.MapFrom(s => s.Frequencies.Select(f => f.DayNumber).ToList()));
            CreateMap<Habit, HabitEntity>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Frequencies, o => o.Ignore());

            CreateMap<UserEntity, User>();
            CreateMap<User, UserEntity>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Role, o => o.Ignore());

            CreateMap<HabitsDictionaryEntity, HabitsDictionary>();
            CreateMap<HabitsDictionary, HabitsDictionaryEntity>()
                .ForMember(d => d.Id, o => o.Ignore());
            
            CreateMap<HabitReminderEntity, HabitReminder>();
            CreateMap<HabitReminder, HabitReminderEntity>()
                .ForMember(d => d.Id, o => o.Ignore());


            CreateMap<FrequencyEntity, Frequency>();
            CreateMap<Frequency, FrequencyEntity>()
                .ForMember(d => d.Id, o => o.Ignore());


            CreateMap<Unit, Unit>();
            CreateMap<Frequency, UnitEntity>()
                .ForMember(d => d.Id, o => o.Ignore());

        }
    }
}