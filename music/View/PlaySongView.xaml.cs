using music.LocalStore;
using music.Model;
using music.View.Song;
using music.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

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
        Button btnPlay;
        Button btnPause;
        MediaPlayer player;
        DispatcherTimer timer;
        private int totalTime = 0;

        public PlaySongView( Frame MainContent, Button btnPlay, Button btnPause, MediaPlayer player )
        {
            InitializeComponent();
            this.MainContent = MainContent;
            this.btnPlay = btnPlay;
            this.btnPause = btnPause;
            this.player = player;
            this.timer = new DispatcherTimer();
            AssignSong();
            DisplaySongUI();
            LoadLikeSong();
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

        private void LoadLikeSong()
        {
            if (songVM.IsLikedSong(song.id, user))
            {
                btnLike.Foreground = new SolidColorBrush(Colors.Gray);
            }
            else
            {
                btnLike.Foreground = new SolidColorBrush(Colors.LightGray);
            }

        }

        private void LoadComment()
        {
            List<COMMENT> comments = songVM.GetAllCommentOfSong(song.id);
            if (comments.Count > 0)
            {
                foreach (COMMENT comment in comments)
                {
                    plComments.Children.Add(new CommentItemView(comment, MainContent, btnPlay, btnPause, player));
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
                        MainContent.Navigate(new PlaySongView(MainContent, btnPlay, btnPause, player));
                    }
                }
            }
        }

        private void btnLike_Click( object sender, RoutedEventArgs e )
        {
            if (songVM.LikeSong(song.id, user) == 1)
            {
                MainContent.Navigate(new PlaySongView(MainContent, btnPlay, btnPause, player));
            }
        }

        private void btnSetTime_Click( object sender, RoutedEventArgs e )
        {
            SetTimeToStopView setTime = new SetTimeToStopView();
            setTime.Show();
            setTime.Closed += SetTime_Closed;
        }

        private void SetTime_Closed( object sender, EventArgs e )
        {
            if ( TimeToStop.Instance.minute != 0 )
            {
                this.totalTime = TimeToStop.Instance.minute * 60;
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += Timer_Tick;
                timer.Start();
            }
        }

        private void Timer_Tick( object sender, EventArgs e )
        {
            if ( this.totalTime > 0)
            {
                this.totalTime--;
            }
            else
            {
                btnPlay.Visibility = Visibility.Visible;
                btnPause.Visibility = Visibility.Hidden;
                player.Stop();
                timer.Stop();
            }
        }
    }
}
