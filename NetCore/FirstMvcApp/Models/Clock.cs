using System;
namespace FirstMvcApp.Models
{
    public class Clock : IClock
    {
        public DateTime GetTime()
        {
            return DateTime.Now;
        }
    }
}
