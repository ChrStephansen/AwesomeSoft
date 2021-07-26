using System;
using System.Collections.Generic;
using System.Linq;

namespace AwesomeSoft.Models
{
    class Meeting
    {
        public Meeting(Room appointedRoom, User organizer, List<User> participants, DateTime startTime,
            DateTime endTime)
        {
            AppointedRoom = appointedRoom;
            EndTime = endTime;
            StartTime = startTime;
            Participants = participants;
            Organizer = organizer;
        }

        public Room AppointedRoom { get; }

        public User Organizer { get; }

        public List<User> Participants { get; }

        public DateTime StartTime { get; }

        public DateTime EndTime { get; }

        public override string ToString()
        {
            return "Meeting scheduled in " + AppointedRoom?.Description + Environment.NewLine +
                   "By " + Organizer.UserName + Environment.NewLine +
                   (!Participants.Any()
                       ? "No participants"
                       : "Participants are " + string.Join(",", Participants.Select(x => x.UserName))) +
                   Environment.NewLine +
                   "Start time is " + StartTime + Environment.NewLine +
                   "End time is   " + EndTime + Environment.NewLine;
        }
    }
}