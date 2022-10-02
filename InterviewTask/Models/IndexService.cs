using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewTask.Models
{
    public class IndexService
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TelephoneNumber { get; set; }
        public string Status { get; set; }
        public string StatusText { get; set; }


        public IndexService(HelperServiceModel src)
        {
            List<int> OpeningHours;

            Title = src.Title;
            Description = src.Description;
            TelephoneNumber = src.TelephoneNumber;
            Id = src.Id;
            OpeningHours = OpeningTimes(DateTime.Now.DayOfWeek, src);

            if (OpeningHours[0] == 0 && OpeningHours[1] == 0)
            {
                Status = "CLOSED";
                SetNextOpeningTimes(src);
            }
            else if(DateTime.Now.Hour < OpeningHours[0])
            {
                Status = "CLOSED";
                StatusText = $"Opens at {toAMPM(OpeningHours[0])}";
            }
            else if(DateTime.Now.Hour < OpeningHours[1])
            {
                Status = "OPEN";
                StatusText = $"Open today until {toAMPM(OpeningHours[1])}";
            }
            else
            {
                Status = "CLOSED";
                SetNextOpeningTimes(src);
            }
        }

        string toAMPM(int time)
        {
            if (time == 12) return "12pm";
            if (time < 12) return $"{time}am";
            return $"{time-12}pm";
        }

        private void SetNextOpeningTimes(HelperServiceModel src)
        {
            for(int i=1;i<7;i++)
            {
                var day = DateTime.Now.AddDays(i);
                var Times = OpeningTimes(day.DayOfWeek, src);
                if (!(Times[0] == 0 && Times[1] == 0))
                {
                    StatusText = $"Reopens {day.DayOfWeek.ToString()} at {toAMPM(Times[0])}";
                    return;
                }
                StatusText = $"Closed permanently";

            }
        }
        

        private List<int> OpeningTimes(DayOfWeek day, HelperServiceModel src)
        {
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return src.MondayOpeningHours;
                case DayOfWeek.Tuesday:
                    return src.TuesdayOpeningHours;
                case DayOfWeek.Wednesday:
                    return src.WednesdayOpeningHours;
                case DayOfWeek.Thursday:
                    return src.ThursdayOpeningHours;
                case DayOfWeek.Friday:
                    return src.FridayOpeningHours;
                case DayOfWeek.Saturday:
                    return src.SaturdayOpeningHours;
                default:
                    return src.SundayOpeningHours;
            }

        }

    }
}