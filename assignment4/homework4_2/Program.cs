using System;
using System.Threading.Tasks.Dataflow;
using System.Timers;
namespace homework4_2
{
    internal class Program
    {
        static System.Timers.Timer timer = new System.Timers.Timer(1000);
        public delegate void MyEventHandler(object sender, Time e);
        public struct Time
        {
            public int hour, minute, second;
            public Time(int h,int m,int s) { hour = h; minute=m; second=s;}
            public void  Inc() {
                second++;
                if(second == 60) {
                    minute++;
                    second = 0;
                }
                if (minute == 60)
                {
                    minute = 0;
                    hour++;
                }
                if (hour == 24) hour = 0;
            }
        }
        public class Clock {

            Time now, alarmTime;

            public Clock() { 
                now=alarmTime= new Time(0,0,0);
            }
            public void Set(int h,int m,int s) { now = new Time(h, m, s); }

            public void SetAlarm(int h, int m, int s) { alarmTime = new Time(h, m, s); }
            public event MyEventHandler OnTick;
            public event MyEventHandler OnAlarm;
            public void Tick(object sender, ElapsedEventArgs e)
            {
                
                OnTick?.Invoke(this, now);
                now.Inc();
                
                if (now.hour == alarmTime.hour &&
                    now.minute == alarmTime.minute &&
                    now.second == alarmTime.second)
                {
                    OnAlarm?.Invoke(this, now);
                }
            }
        }
        static void Main(string[] args)
        {
            Clock clock = new Clock();
            timer.AutoReset = true;
            timer.Elapsed += clock.Tick;
            clock.OnTick += new MyEventHandler(Clock_Tick);
            clock.OnAlarm += new MyEventHandler(Clock_Alarm);
            void Clock_Tick(object sender, Time e)
            {
                Console.WriteLine($"Tick!{e.hour}:{e.minute}:{e.second}");
            }
            void Clock_Alarm(object sender, Time e)
            {
                Console.WriteLine($"Alarm!{e.hour}:{e.minute}:{e.second}");
            }
            clock.SetAlarm(0, 0, 5);
            timer.Start(); 
            Console.ReadLine(); 
        }
    }
}
