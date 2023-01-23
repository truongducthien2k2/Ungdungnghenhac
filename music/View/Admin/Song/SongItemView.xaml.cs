using music.Component;
using music.Model;
using music.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace music.View.Admin.Song
{
    /// <summary>
    /// Interaction logic for SongItemView.xaml
    /// </summary>
    public partial class SongItemView : UserControl
    {
        Frame MainContent;
        SONG song;
        SongViewModel songVM = new SongViewModel();
        public SongItemView()
        {
            InitializeComponent();
        }
        public SongItemView(SONG song, Frame MainContent)
        {
            InitializeComponent();
            this.song = song;
            this.MainContent = MainContent;
            LoadSongItem();
        }

        private void LoadSongItem()
        {
            tbTopicName.Text = songVM.GetAllTopic().Where(topic => topic.id == song.topicId).Select(topic => topic.topicName).First();
            tbSongName.Text = song.songName;
            tbSingerName.Text = songVM.GetAllSinger().Where(singer => singer.id == song.singerId).Select(singer => singer.singerName).First();
        }

        private void btnAdjustSong_Click( object sender, RoutedEventArgs e )
        {
            NewSongView newSong = new NewSongView(song, "adjust", MainContent);
        }

        private void btnRemoveSong_Click( object sender, RoutedEventArgs e )
        {
            if ( songVM.RemoveSong(song.id) == 1 )
            {
                bool? result = new CustomMessageBox("Xóa bài hát thành công!!!", "Success").ShowDialog();
                if ( result.Value )
                {
                    MainContent.Navigate(new SongAdminView(MainContent));
                }
            }
            else
            {
                bool? result = new CustomMessageBox("Bạn không thể xóa bài hát này!!!", "Error").ShowDialog();
                if ( result.Value )
                {
                    MainContent.Navigate(new SongAdminView(MainContent));
                }
            }
        }
    }
}
