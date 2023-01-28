using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace music.LocalStore
{
    internal class BasicSong
    {
        public string name { get; set; }

        private static BasicSong instance;
        public static BasicSong Instance
        {
            get
            {
                if ( instance == null )
                    instance = new BasicSong();
                return BasicSong.instance;
            }
            set
            {
                BasicSong.Instance = value;
            }
        }
        public BasicSong() { }
    }
}
