using music.LocalStore;
using music.Model;
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

namespace music.View.History
{
    /// <summary>
    /// Interaction logic for HistoryItemView.xaml
    /// </summary>
    public partial class HistoryItemView : UserControl
    {
        SongViewModel songVM = new SongViewModel();
        SONG localSong;
        CLIENT_VIEW_SONG songOfUser;
        Image songImage;
        TextBlock songName;
        TextBlock singerName;
        MediaPlayer player;
        SONG viewedSong;
        public HistoryItemView()
        {
            InitializeComponent();
        }

        public HistoryItemView( SONG localSong, CLIENT_VIEW_SONG songOfUser, Image songImage, TextBlock songName, TextBlock singerName, MediaPlayer player )
        {
            InitializeComponent();
            this.localSong = localSong;
            this.songOfUser = songOfUser;
            this.songImage = songImage;
            this.songName = songName;
            this.singerName = singerName;
            this.player = player;
            LoadSong();
        }

        private void LoadSong()
        {
            if (localSong != null)
            {
                tbSongName.Text = localSong.songName;
                tbSingerName.Text = songVM.GetAllSinger().Where(singer => singer.id == localSong.singerId).Select(singer => singer.singerName).First();
            }
            else
            {
                viewedSong = songVM.GetAllSong().Where(song => song.id == songOfUser.id).First();
                tbSongName.Text = viewedSong.songName;
                tbSingerName.Text = songVM.GetAllSinger().Where(singer => singer.id == viewedSong.singerId).Select(singer => singer.singerName).First();
            }    
        }

        private void borderHistory_MouseMove( object sender, MouseEventArgs e )
        {
            borderHistory.Background = (SolidColorBrush) (new BrushConverter().ConvertFrom("#F2F2F2"));
        }

        private void borderHistory_MouseLeave( object sender, MouseEventArgs e )
        {
            borderHistory.Background = new SolidColorBrush(Colors.White);
        }

        private void ShowSongToTaskbar(SONG song)
        {
            songImage.Source = new BitmapImage(new Uri(song.songImage));
            songName.Text = tbSongName.Text;
            singerName.Text = tbSingerName.Text;
            player.Open(new Uri(song.songCode));
            BasicSong.Instance.name = songName.Text;
        }

        private void Grid_MouseLeftButtonDown( object sender, MouseButtonEventArgs e )
        {
            if ( localSong != null )
            {
                ShowSongToTaskbar(localSong);
            }
            else
            {
                ShowSongToTaskbar(viewedSong);
            }
            
        }
    }
}
