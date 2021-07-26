using System;

namespace AwesomeSoft.Helpers
{
    public static class MyConsole
    {
        public static void WriteIntroText()
        {
            Console.WriteLine(Environment.NewLine +
                              "-----------------------------" + Environment.NewLine +
                              "Select option(end with enter)" + Environment.NewLine +
                              "-----------------------------" + Environment.NewLine +
                              "1: Create Meeting" + Environment.NewLine +
                              "2: Get Meetings for specific user" + Environment.NewLine +
                              "end: This stops the application from running");
        }

        public static void WriteInfoMessage(string text)
        {
            Console.WriteLine("____________________________________________" +
                              Environment.NewLine + text + Environment.NewLine +
                              "____________________________________________");
        }

        public static void WriteInvalidMessage(string text)
        {
            WriteInfoMessage(text + " could not be found. Please validate input and try again.");
        }

        public static string[] GetCreateMeetingParams()
        {
            Console.WriteLine("Format: appointedRoom (Room.no), organizer (user.no), participants, startTime, endTime" + Environment.NewLine +
                              "separate participants by ',' or write '-' if no participants should be added" + Environment.NewLine +
                              "1 1 0,2 25.07.2021;14:15:05 25.07.2021;14:30:05");
            return Console.ReadLine()?.Split();
        }

        public static string GetUserNo()
        {
            Console.WriteLine("Specify user(user.no), for whom to list details");
            return Console.ReadLine();
        }
    }
}
