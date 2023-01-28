using music.LocalStore;
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
    /// Interaction logic for PlaySongView.xaml
    /// </summary>
    public partial class PlaySongView : Page
    {
        SongViewModel songVM = new SongViewModel();
        SONG song;
        Frame MainContent;
        string user = Properties.Settings.Default.user;

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
            LoadComment();
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

        private void LoadComment()
        {
            List<COMMENT> comments = songVM.GetAllCommentOfSong(song.id);
            if (comments.Count > 0)
            {
                foreach (COMMENT comment in comments)
                {
                    plComments.Children.Add(new CommentItemView(comment));
                }
            }
            else
            {
                TextBlock notice = new TextBlock();
                notice.Text = "Chưa có bình luận nào";
                plComments.Children.Add(notice);
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

        private void btnComment_Click( object sender, RoutedEventArgs e )
        {
            if (String.IsNullOrEmpty(user))
            {
                MessageBox.Show("Vui lòng đăng nhập trước khi bình luận");
            }
            else
            {
                if (String.IsNullOrEmpty(tbCommentContent.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập bình luận");
                }
                else
                {
                    if (songVM.InsertComment(tbCommentContent.Text, user, song.id) == 1)
                    {
                        MainContent.Navigate(new PlaySongView(MainContent));
                    }
                }
            }
        }
    }
}
