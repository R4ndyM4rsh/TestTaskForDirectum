using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MeetingСalendar
{
    class Logic
    {
        readonly List<Meeting> meetings;
        public Logic(List<Meeting> meetings) 
        {
            this.meetings = meetings;
        }
        public void OutputMeeting()
        {
            Console.Clear();
            foreach (Meeting meeting in meetings)
            {
                meetings.IndexOf(meeting);
                Console.WriteLine(meetings.IndexOf(meeting) +" :    "+ meeting);
            }
        }
        public void AddMeeting(DateTime timeStart, DateTime timeFinish)
        {
            if (timeFinish.CompareTo(timeStart) > 0 && timeStart.ToUniversalTime() > DateTime.UtcNow)
            {
                foreach (Meeting obj in meetings)
                {
                    if (!(obj.GetTimeStart().CompareTo(timeFinish) > 0 || timeStart.CompareTo(obj.GetTimeFinish()) > 0))
                    {
                        throw new ArgumentException("invalid date parameters");
                    }
                }
            }
            else throw new ArgumentException("invalid date parameters");
            Meeting date = new Meeting(timeStart, timeFinish);
            meetings.Add(date);
        }
        public void DeleteMeeting(int index)
        {
            try
            {
                meetings.RemoveAt(index);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("{0} wrong index", e);
            }
        }
        public void ChangeMeeting(int index, DateTime timeStart, DateTime timeFinish, DateTime reminder = new DateTime()) 
        {
            if (timeFinish.CompareTo(timeStart) > 0 && timeStart.ToUniversalTime().CompareTo(DateTime.UtcNow) > 0
                && (reminder.ToUniversalTime().CompareTo(DateTime.UtcNow) > 0 || reminder.CompareTo(new DateTime()) == 0)
                && timeStart.CompareTo(reminder) > 0 && index >= 0 && index < meetings.Count)
            {
                foreach (Meeting meeting in meetings)
                {
                    if (timeFinish.CompareTo(meeting.GetTimeStart()) > 0 && timeFinish.CompareTo(meeting.GetTimeFinish()) < 0 ||
                        timeStart.CompareTo(meeting.GetTimeStart()) > 0 && timeStart.CompareTo(meeting.GetTimeFinish()) < 0 ||
                        timeStart.CompareTo(meeting.GetTimeStart()) < 0 && timeFinish.CompareTo(meeting.GetTimeFinish()) > 0
                        ) throw new ArgumentException("invalid date parameters");
                }
            }
            else throw new ArgumentException("invalid date parameters");
            meetings[index].SetTimeStart(timeStart);
            meetings[index].SetTimeFinish(timeFinish);
            meetings[index].SetReminder(reminder);
        }

        public void ExportMeetings(DateTime date) 
        {
            var a = new StreamWriter("MeetingsForTheDay.txt");
            foreach (Meeting meeting in meetings)
            {
                if (meeting.GetTimeStart().ToShortDateString().CompareTo(date.ToShortDateString()) == 0) 
                {
                    a.WriteLine(meetings.IndexOf(meeting) + 1 + " :    " + meeting.ToString());
                }
            }
            a.Close();
        }
    }
}
