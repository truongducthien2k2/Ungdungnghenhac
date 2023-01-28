using music.LocalStore;
using music.Model;
using music.View.History;
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
    /// Interaction logic for HistoryView.xaml
    /// </summary>
    public partial class HistoryView : Page
    {
        SongViewModel songVM = new SongViewModel();
        Image songImage;
        TextBlock songName;
        TextBlock singerName;
        MediaPlayer player;
        public List<SONG> viewedSongLocal;
        public HistoryView()
        {
            InitializeComponent();
        }
        public HistoryView( List<SONG> viewedSongLocal, Image songImage, TextBlock songName, TextBlock singerName, MediaPlayer player )
        {
            InitializeComponent();
            this.viewedSongLocal = viewedSongLocal;
            this.songImage = songImage;
            this.songName = songName;
            this.singerName = singerName;
            this.player = player;
            LoadHistoryView();
        }

        private void LoadHistoryView()
        {
            string user = Properties.Settings.Default.user;
            if ( user == "" )
            {
                if ( viewedSongLocal.Count > 0)
                {
                    foreach ( var song in viewedSongLocal )
                    {
                        plHistory.Children.Add(new HistoryItemView( song, null, songImage, songName, singerName, player));
                    }
                }
            }
            else
            {
                List<CLIENT_VIEW_SONG> songs = songVM.GetViewHistoryOfUser(user);
                if ( songs != null && songs.Count > 0)
                {
                    foreach ( var song in songs )
                    {
                        plHistory.Children.Add(new HistoryItemView( null, song, songImage, songName, singerName, player));
                    }
                }
            }
        }
    }
}
