using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingСalendar
{
    class Meeting
    {
        private DateTime timeStart;
        private DateTime timeFinish;
        private DateTime reminder = new DateTime();
        public Meeting(DateTime timeStart, DateTime timeFinish)
        {
            this.timeStart = timeStart;
            this.timeFinish = timeFinish;
        }
        public DateTime GetTimeStart() 
        {
            return timeStart;
        }
        public DateTime SetTimeStart(DateTime date)
        {
            timeStart = date;
            return timeStart;
        }
        public DateTime GetTimeFinish() 
        {
            return timeFinish;
        }
        public DateTime SetTimeFinish(DateTime date) 
        {
            timeFinish = date;
            return timeFinish;
        }
        public DateTime GetReminder() 
        {
            return reminder;
        }
        public DateTime SetReminder(DateTime date) 
        {
            reminder = date;
            return reminder;
        }

        public override string ToString()
        {
            return "Date meeting: " + timeStart.ToShortDateString() +
                ", Start: " + timeStart.ToShortTimeString() + 
                ", Finish: " + timeFinish.ToShortTimeString();
        }
    }
}
