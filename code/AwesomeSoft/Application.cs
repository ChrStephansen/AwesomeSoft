using System;
using System.Collections.Generic;
using System.Linq;
using AwesomeSoft.Helpers;
using AwesomeSoft.Models;

namespace AwesomeSoft
{
    public class Application
    {
        private const int ExpectedMeetingParameters = 5;

        private List<User> _applicationUsers;
        private List<Room> _applicationRooms;
        private List<Meeting> _applicationMeetings;

        public Application()
        {
            InitializeApplication();
        }

        #region Initialization

        private void InitializeApplication()
        {
            _applicationUsers = new List<User>();
            _applicationRooms = new List<Room>();
            _applicationMeetings = new List<Meeting>();

            //Create initial users
            CreateUser((uint) _applicationUsers.Count, "Anton");
            CreateUser((uint) _applicationUsers.Count, "Benny");
            CreateUser((uint) _applicationUsers.Count, "Charlie");
            CreateUser((uint) _applicationUsers.Count, "David");

            //Create initial rooms
            CreateRoom((uint) _applicationRooms.Count, "Basement");
            CreateRoom((uint) _applicationRooms.Count, "1st floor, Small Meeting Room");
            CreateRoom((uint) _applicationRooms.Count, "1st floor, Large Meeting Room");

            CreateMeeting(_applicationRooms.FirstOrDefault(r => r.No == 0),
                _applicationUsers.FirstOrDefault(u => u.No == 0),
                _applicationUsers.Where(u => u.No == 1 || u.No == 2).ToList(),
                DateTime.ParseExact("28.07.2021;14:15:00", "dd.MM.yyyy;HH:mm:ss", null),
                DateTime.ParseExact("28.07.2021;14:45:00", "dd.MM.yyyy;HH:mm:ss", null));

            CreateMeeting(_applicationRooms.FirstOrDefault(r => r.No == 1),
                _applicationUsers.FirstOrDefault(u => u.No == 1), _applicationUsers.Where(u => u.No == 2).ToList(),
                DateTime.ParseExact("29.07.2021;08:15:00", "dd.MM.yyyy;HH:mm:ss", null),
                DateTime.ParseExact("29.07.2021;08:45:00", "dd.MM.yyyy;HH:mm:ss", null));

            CreateMeeting(_applicationRooms.FirstOrDefault(r => r.No == 1),
                _applicationUsers.FirstOrDefault(u => u.No == 1), _applicationUsers.Where(u => u.No == 0).ToList(),
                DateTime.ParseExact("28.07.2021;14:15:00", "dd.MM.yyyy;HH:mm:ss", null),
                DateTime.ParseExact("28.07.2021;14:45:00", "dd.MM.yyyy;HH:mm:ss", null));
        }

        #endregion

        #region Object creation

        private void CreateUser(uint no, string userName)
        {
            _applicationUsers.Add(new User(no, userName));
        }

        private void CreateRoom(uint no, string description)
        {
            _applicationRooms.Add(new Room(no, description));
        }

        private void CreateMeeting(Room appointedRoom, User organizer, List<User> participants, DateTime startTime,
            DateTime endTime)
        {
            _applicationMeetings.Add(new Meeting(appointedRoom, organizer, participants, startTime, endTime));
        }

        #endregion

        #region Application methods

        private void ValidateAndCreateMeeting(string[] parms)
        {
            try
            {
                if (parms.Length == ExpectedMeetingParameters)
                {
                    Room appointedRoom = _applicationRooms.FirstOrDefault(r => r.No == uint.Parse(parms[0]));
                    User organizer = _applicationUsers.FirstOrDefault(u => u.No == uint.Parse(parms[1]));
                    List<User> participants = parms[2].Equals("-") ? new List<User>() : _applicationUsers
                        .Where(x => parms[2].Split(",").Select(uint.Parse).Contains(x.No)).ToList();
                    DateTime startTime = DateTime.ParseExact(parms[3], "dd.MM.yyyy;HH:mm:ss", null);
                    DateTime endTime = DateTime.ParseExact(parms[4], "dd.MM.yyyy;HH:mm:ss", null);

                    if (startTime > endTime)
                    {
                        Console.WriteLine(
                            "Specified start time is after end time. Swapping the two values before creating the meeting");
                        var tmp = endTime;
                        endTime = startTime;
                        startTime = tmp;
                    }

                    if (appointedRoom == null)
                    {
                        MyConsole.WriteInvalidMessage("Room");
                    }
                    else if (organizer is null)
                    {
                        MyConsole.WriteInvalidMessage("Organizer");
                    }
                    else
                    {
                        CreateMeeting(appointedRoom, organizer, participants, startTime, endTime);
                        Console.WriteLine("Meeting created for {0}, with {1} participant(s)", organizer.UserName, participants.Count);
                    }
                }
                else
                {
                    throw new InvalidOperationException("Unexpected number of parameters found." + Environment.NewLine +
                                                        "Found " + parms.Length + " parameters in \"" +
                                                        string.Join(" ", parms) + "\" Expected " +
                                                        ExpectedMeetingParameters);
                }
            }
            catch (Exception e)
            {
                MyConsole.WriteInfoMessage("There was an error, creating the meeting:" + Environment.NewLine + e.Message);
            }
        }

        private void GetMeetings(string userNo)
        {
            var userName = _applicationUsers.FirstOrDefault(u => u.No == int.Parse(userNo))?.UserName;
            if (userName != null)
            {
                var meetings = _applicationMeetings.Where(m =>
                    m.Organizer.No == int.Parse(userNo) || m.Participants.Any(p => p.No == int.Parse(userNo)));

                var meetingList = meetings as Meeting[] ?? meetings.ToArray();
                if (meetingList.Any())
                {

                    MyConsole.WriteInfoMessage(meetingList.Length + " meeting(s) includes " + userName);

                    foreach (var meeting in meetingList)
                    {
                        Console.WriteLine(meeting.ToString());
                    }
                }
                else
                {
                    MyConsole.WriteInfoMessage("No meetings scheduled for " + userName);
                }
            }
            else
            {
                MyConsole.WriteInfoMessage("No user found at with specified number.");
            }
        }

        public void Run()
        {
            var doExit = false;

            while (!doExit)
            {
                MyConsole.WriteIntroText();
                switch (Console.ReadLine())
                {
                    case "1":
                        ValidateAndCreateMeeting(MyConsole.GetCreateMeetingParams());
                        break;
                    case "2":
                        GetMeetings(MyConsole.GetUserNo());
                        break;
                    case "end":
                        doExit = true;
                        break;
                    default:
                        Console.WriteLine("Option not recognized, please use one of the listed options");
                        break;
                }
            }
        }
        #endregion
    }
}
