using System;

namespace AFPMBAI.CLAIMS.DbUpdate
{
    public class ConsoleIO
    {
        public ConsoleKey Confirm(string message)
        {
            Console.Write(message);

            ConsoleKey response;
            do
            {
                response = Console.ReadKey(false).Key;
            } while (response != ConsoleKey.Y && response != ConsoleKey.N);

            return response;
        }
    }
}