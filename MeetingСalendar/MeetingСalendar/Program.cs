using System;
using System.Collections.Generic;
using System.Threading;

namespace MeetingСalendar
{
    class Program
    {
        protected Program() { }
        static void Main(string[] args)
        {
            List<Meeting> meetings = new List<Meeting>();
            Logic logic = new Logic(meetings);
            new Thread(() => 
            {
                while (true) 
                {
                    Meeting[] array = new Meeting[meetings.Count];
                    if (meetings.Count == array.Length) {
                        meetings.CopyTo(array);
                        foreach (Meeting arr in array)
                        {
                            if (arr.GetReminder().ToUniversalTime().ToShortDateString().CompareTo(DateTime.UtcNow.ToShortDateString()) == 0
                                && arr.GetReminder().ToUniversalTime().ToShortTimeString().CompareTo(DateTime.UtcNow.ToShortTimeString()) == 0
                            )
                            {
                                Console.WriteLine("У вас запланирована встерча:\n " + arr);
                                Thread.Sleep(60000);
                            }
                        }
                    }
                }
            }).Start();
            while (true) 
            {
                Console.WriteLine("1) Посмотреть список втреч\n" +
                    "2) Добавить встречу\n" +
                    "3) Удалить встречу\n" +
                    "4) Изменить встречу\n" +
                    "5) Экспортировать расписание встреч за выбранный день\n"
                    );
                int caseSwitch;
                string input;
                int index;
                do
                {
                    input = Console.ReadLine();
                } while (!int.TryParse(input,out caseSwitch));

                switch (caseSwitch) 
                {
                    case 1:
                        logic.OutputMeeting();
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:
                        logic.OutputMeeting();
                        DateTime start;
                        DateTime finish;
                        DateTime reminder;
                        do
                        {
                            Console.WriteLine("Введите время начало вашей встречи в формате: 02/16/2008 12:15:12");
                            input = Console.ReadLine();
                            Console.Clear();
                        } while (!DateTime.TryParse(input, out start));
                        logic.OutputMeeting();
                        do
                        {
                            Console.WriteLine("Введите время окончания вашей встречи в формате: 02/16/2008 12:15:12");
                            input = Console.ReadLine();
                            Console.Clear();
                        } while (!DateTime.TryParse(input, out finish));

                        logic.AddMeeting(start, finish);
                        break;
                    case 3:
                        logic.OutputMeeting();
                        do
                        {
                            Console.WriteLine("Ведите номер встречи, которую хотите удалить");
                            input = Console.ReadLine();
                            Console.Clear();
                        } while (!int.TryParse(input, out index));
                        logic.DeleteMeeting(index);
                        Console.Clear();
                        break;
                    case 4:
                        logic.OutputMeeting();
                        do
                        {
                            Console.WriteLine("Ведите номер встречи");
                            input = Console.ReadLine();
                            Console.Clear();
                        } while (!int.TryParse(input, out index));
                        do
                        {
                            Console.WriteLine("Введите время начало вашей встречи в формате: 02/16/2008 12:15:12");
                            input = Console.ReadLine();
                            Console.Clear();
                        } while (!DateTime.TryParse(input, out start));
                        do
                        {
                            Console.WriteLine("Введите время окончания вашей встречи в формате: 02/16/2008 12:15:12");
                            input = Console.ReadLine();
                            Console.Clear();
                        } while (!DateTime.TryParse(input, out finish));
                        int flag;
                        string dateReminder;
                        Console.WriteLine("1) Задать или изменить напоминание");
                        Console.WriteLine("2) Продолжить без изменений");
                        do
                        {
                            input = Console.ReadLine();
                        } while (!int.TryParse(input, out flag));
                        switch (flag) 
                        {
                            case 1:
                                logic.OutputMeeting();
                                Console.WriteLine("Введите время напоминания о встречи в формате: 02/16/2008 12:15:12");
                                do
                                {
                                    dateReminder = Console.ReadLine();
                                }
                                while(!DateTime.TryParse(dateReminder, out reminder));
                                logic.ChangeMeeting(index, start, finish, reminder);
                                Console.Clear();
                                break;
                            case 2:
                                logic.OutputMeeting();
                                logic.ChangeMeeting(index, start, finish);
                                Console.Clear();
                                break;
                            default:
                                Console.WriteLine("wrong index");
                                break;
                        }
                        break;
                    case 5:
                        Console.Clear();
                        DateTime date;
                        do
                        {
                            Console.WriteLine("Введите дату в формате: 02/16/2008");
                            input = Console.ReadLine();
                            Console.Clear();
                        } while (!DateTime.TryParse(input, out date));
                        logic.ExportMeetings(date);
                        break;
                    default:
                    Console.WriteLine("wrong index");
                        break;
                }
            }
        }
    }
}
