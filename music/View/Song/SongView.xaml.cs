using music.Model;
using music.View.Song;
using music.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace music.View
{
    /// <summary>
    /// Interaction logic for SongView.xaml
    /// </summary>
    public partial class SongView : Page
    {
        SongViewModel songVM = new SongViewModel();
        Image songImage;
        TextBlock songName;
        TextBlock singerName;
        MediaPlayer player;
        public SongView()
        {
            InitializeComponent();
        }

        public SongView(Image songImage, TextBlock songName, TextBlock singerName, MediaPlayer player)
        {
            InitializeComponent();
            this.songImage = songImage;
            this.songName = songName;
            this.singerName = singerName;
            this.player = player;
            LoadUI();
        }

        private void LoadUI()
        {
            List<SONG> songs = songVM.GetAllSong();
            foreach (SONG songDataItem in songs)
            {
                plSongs.Children.Add(new SongItemView(songDataItem, songImage, songName, singerName, player));
            }
        }
    }
}
