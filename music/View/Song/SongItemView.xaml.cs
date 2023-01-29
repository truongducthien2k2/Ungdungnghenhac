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

namespace music.View.Song
{
    /// <summary>
    /// Interaction logic for SongItemView.xaml
    /// </summary>
    public partial class SongItemView : UserControl
    {
        SongViewModel songVM = new SongViewModel();
        SONG songDataItem;
        Image songImage;
        TextBlock songName;
        TextBlock singerName;
        MediaPlayer player;
        public SongItemView()
        {
            InitializeComponent();
        }

        public SongItemView( SONG songDataItem, Image songImage, TextBlock songName, TextBlock singerName, MediaPlayer player )
        {
            InitializeComponent();
            this.songDataItem = songDataItem;
            this.songImage = songImage;
            this.songName = songName;
            this.singerName = singerName;
            this.player = player;
            LoadSong();
        }

        private void LoadSong()
        {
            tbSongName.Text = songDataItem.songName;
            tbSingerName.Text = songVM.GetAllSinger().Where(singer => singer.id == songDataItem.singerId).Select(singer => singer.singerName).First();
        }

        private void Grid_MouseLeftButtonDown( object sender, MouseButtonEventArgs e )
        {
            songImage.Source = new BitmapImage(new Uri(songDataItem.songImage));
            songName.Text = tbSongName.Text;
            singerName.Text = tbSingerName.Text;
            player.Open(new Uri(songDataItem.songCode));
            BasicSong.Instance.name = songName.Text;
        }

        private void btnShare_Click( object sender, RoutedEventArgs e )
        {
            ShareSongView shareSong = new ShareSongView();
            shareSong.Show();
        }
    }
}
