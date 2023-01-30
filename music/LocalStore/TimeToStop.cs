using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace music.LocalStore
{
    internal class TimeToStop
    {
        public int minute { get; set; }

        private static TimeToStop instance;
        public static TimeToStop Instance
        {
            get
            {
                if ( instance == null )
                    instance = new TimeToStop();
                return TimeToStop.instance;
            }
            set
            {
                TimeToStop.Instance = value;
            }
        }
        public TimeToStop() { minute = 0; }
    }
}
