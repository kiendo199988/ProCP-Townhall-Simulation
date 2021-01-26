using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppTownHall
{
   public class Clock
    {
        private int hour;
        private int minute;
        private int second;

        public int Hour { get { return this.hour; } set { this.hour = value; } }
        public int Minute { get { return this.minute; } set { this.minute = value; } }
        public int Second { get { return this.second; } set { this.second = value; } }
        public Clock(int hh, int mm, int ss)
        {
            this.hour = hh;
            this.minute = mm;
            this.second = ss;
        }
        public void Tick()
        {
            this.second++;
            if (this.second == 60)
            {
                minute++;
                second = 00;
            }
            if (this.minute == 60)
            {
                this.hour++;
                this.minute = 00;
            }
            if (this.hour == 24)
            {
                this.hour = 00;
            }
        }
        public string displayTime()
        {
            string time = this.hour.ToString("D2") + ":" + this.minute.ToString("D2") + ":" + this.second.ToString("D2");
            return time;
        }
    }
}
