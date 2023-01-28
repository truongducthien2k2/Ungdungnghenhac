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

namespace music.View
{
    /// <summary>
    /// Interaction logic for PlaySongView.xaml
    /// </summary>
    public partial class PlaySongView : Page
    {
        SongViewModel songVM = new SongViewModel();
        SONG song;
        Frame MainContent;

        public PlaySongView()
        {
            InitializeComponent();
            LoadLyrics();
        }

        public PlaySongView( Frame MainContent )
        {
            InitializeComponent();
            this.MainContent = MainContent;
            AssignSong();
            DisplaySongUI();
            LoadLyrics();
        }

        private void AssignSong()
        {
            this.song = songVM.GetAllSong().Where(song => song.songName == BasicSong.Instance.name).First();
        }

        private void DisplaySongUI()
        {
            if (song != null)
            {
                tbSongName.Text = song.songName;
                tbSingerName.Text = songVM.GetAllSinger().Where(singer => singer.id == song.singerId).Select(singer => singer.singerName).First();
                ImageViewer.Source = new BitmapImage(new Uri(song.songImage));
            }
        }

        private void LoadLyrics()
        {
            if (song != null)
            {
                prLyrics.AppendText(song.lyrics);
            }
        }

        private void btnBack_Click( object sender, RoutedEventArgs e )
        {
            MainContent.Navigate(new HomeView());
        }

        private void ImageViewer_Loaded( object sender, RoutedEventArgs e )
        {

        }
    }
}
