using music.Component;
using music.LocalStore;
using music.Model;
using music.View;
using music.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace music
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AccountViewModel accountVM = new AccountViewModel();
        SongViewModel songVM = new SongViewModel();
        SONG song;
        MediaPlayer player = new MediaPlayer();
        private int indexOfSong;
        private bool isRandom = false;
        private bool isReapeatOnce = false;
        public List<SONG> viewedSongLocal = new List<SONG>();

        public MainWindow()
        {
            InitializeComponent();
            this.song = new SONG();
            HandleLogin();
            LoadSong(-1);
            player.MediaEnded += Player_MediaEnded;
        }

        private void Player_MediaEnded( object sender, EventArgs e )
        {
            btnPlay.Visibility = Visibility.Visible;
            btnPause.Visibility = Visibility.Hidden;
            /// Xử lý khi btnRandom được chọn
            if ( isRandom )
            {

            }
        }

        public void LoadSong(int indexOfSong)
        {
            this.indexOfSong = indexOfSong;
            btnPlay.Visibility = Visibility.Visible;
            btnPause.Visibility = Visibility.Hidden;
            if (indexOfSong != -1)
            {
                if (songVM.GetAllSong().Count > 0)
                {
                    song = songVM.GetAllSong() [this.indexOfSong];
    ;               ImageViewer.Source = new BitmapImage(new Uri(song.songImage));
                    tbSongName.Text = song.songName;
                    tbSingerName.Text = songVM.GetAllSinger().Where(singer => singer.id == song.singerId).Select(singer => singer.singerName).First();
                    player.Open(new Uri(song.songCode));
                }
            }
        }

        protected override void OnSourceInitialized( EventArgs e )
        {
            IconHelper.RemoveIcon(this);
        }

        bool IsMaximize = false;
        private void Border_MouseLeftButtonDown( object sender, MouseButtonEventArgs e )
        {
            if ( e.ClickCount == 2 )
            {
                if ( true )
                {
                    if ( IsMaximize )
                    {
                        this.WindowState = WindowState.Normal;
                        this.Width = 1280;
                        this.Height = 780;

                        IsMaximize = false;
                    }
                    else
                    {
                        this.WindowState = WindowState.Maximized;
                        IsMaximize = true;
                    }
                }
            }
        }

        private void HandleLogin()
        {
            if (accountVM.AccountData() != null)
            {
                if ( (bool) accountVM.AccountData().isAdmin )
                {
                    MainAdmin admin = new MainAdmin();
                    this.Close();
                    admin.Show();
                }
                else
                {
                    StoreData();
                    navFrame.Navigate(new HomeView());
                }
            }
        }

        private void StoreData()
        {
            var account = accountVM.AccountData();
            Properties.Settings.Default.user = account.userName;
            Properties.Settings.Default.isAdmin = (bool) account.isAdmin;
        }

        private void StoreSongIntoHistory(SONG song)
        {
            if ( Properties.Settings.Default.user == "" )
            {
                if ( viewedSongLocal.Count >= 0 && !viewedSongLocal.Contains(song) )
                {
                    viewedSongLocal.Add(song);
                }
            }
            else
            {
                songVM.InsertSongIntoHistory(Properties.Settings.Default.user, song.id);
            }
        }

        private void homeBtn_Click( object sender, RoutedEventArgs e )
        {
            navFrame.Navigate(new HomeView());
        }

        private void rankBtn_Click( object sender, RoutedEventArgs e )
        {
            navFrame.Navigate(new RankView());
        }

        private void historyBtn_Click( object sender, RoutedEventArgs e )
        {
            navFrame.Navigate(new HistoryView(viewedSongLocal, ImageViewer, tbSongName, tbSingerName, player));
        }

        private void topicBtn_Click( object sender, RoutedEventArgs e )
        {
            navFrame.Navigate(new TopicView());
        }

        private void albumBtn_Click( object sender, RoutedEventArgs e )
        {
            navFrame.Navigate(new AlbumView());
        }

        private void artistBtn_Click( object sender, RoutedEventArgs e )
        {
            navFrame.Navigate(new ArtistView());
        }

        private void songBtn_Click( object sender, RoutedEventArgs e )
        {
            navFrame.Navigate(new SongView( ImageViewer, tbSongName, tbSingerName, player));
        }

        private void videoBtn_Click( object sender, RoutedEventArgs e )
        {
            navFrame.Navigate(new VideoView());
        }

        private void songInDeviceBtn_Click( object sender, RoutedEventArgs e )
        {
            navFrame.Navigate(new SongInDeviceView());
        }

        private void favoriteListBtn_Click( object sender, RoutedEventArgs e )
        {
            navFrame.Navigate(new FavoriteListView());
        }

        private void playListBtn_Click( object sender, RoutedEventArgs e )
        {
            navFrame.Navigate(new PlayListView());
        }

        private void accountBtn_Click( object sender, RoutedEventArgs e )
        {
            if ( Properties.Settings.Default.user != "" )
            {
                navFrame.Navigate(new AccountView());
            } else
            {
                navFrame.Navigate(new LoginView());
            }
        }

        private int GetIndexOfSong()
        {
            song = songVM.GetAllSong().Where(song => song.songName == tbSongName.Text).First();
            return songVM.GetAllSong().IndexOf(song);
        }

        private void btnPlay_Click( object sender, RoutedEventArgs e )
        {
            if (player.Source != null)
            {
                song = songVM.GetAllSong().Where(song => song.songName == tbSongName.Text).First();
                StoreSongIntoHistory(song);
                if (this.indexOfSong == -1)
                {
                    this.indexOfSong = GetIndexOfSong();
                }
                btnPlay.Visibility = Visibility.Hidden;
                btnPause.Visibility = Visibility.Visible;
                player.Play();
            }
        }

        private void btnPause_Click( object sender, RoutedEventArgs e )
        {
            if (player.Source != null )
            {
                btnPlay.Visibility = Visibility.Visible;
                btnPause.Visibility = Visibility.Hidden;
                player.Pause();
            }
        }

        private void btnMuteVolume_Click( object sender, RoutedEventArgs e )
        {
            if ( player.Source != null )
            {
                btnMuteVolume.Visibility = Visibility.Hidden;
                btnVolume.Visibility = Visibility.Visible;
                player.IsMuted = false;
            }
        }

        private void btnVolume_Click( object sender, RoutedEventArgs e )
        {
            if ( player.Source != null )
            {
                btnMuteVolume.Visibility = Visibility.Visible;
                btnVolume.Visibility = Visibility.Hidden;
                player.IsMuted = true;
            }
        }

        private void btnViewSong_Click( object sender, MouseButtonEventArgs e )
        {
            if ( !String.IsNullOrEmpty(BasicSong.Instance.name) )
            {
                this.indexOfSong = GetIndexOfSong();
                navFrame.Navigate(new PlaySongView(navFrame));
            }
        }

        private void btnPrevious_Click( object sender, RoutedEventArgs e )
        {
            if (this.indexOfSong != -1)
            {
                if ( this.indexOfSong == 0 )
                {
                    this.indexOfSong = songVM.GetAllSong().Count - 1;
                }
                else
                {
                    this.indexOfSong--;
                }
                LoadSong(this.indexOfSong);
            }
        }

        private void btnNext_Click( object sender, RoutedEventArgs e )
        {
            if (this.indexOfSong != -1)
            {
                if ( this.indexOfSong == songVM.GetAllSong().Count - 1 )
                {
                    this.indexOfSong = 0;
                }
                else
                {
                    this.indexOfSong++;
                }
                LoadSong(this.indexOfSong);
            }
        }

        private void btnRandom_Click( object sender, RoutedEventArgs e )
        {
            /// Chưa đổi màu btnRandom
            if (this.isRandom)
            {
                this.isRandom = false;
                btnRandom.Foreground = new SolidColorBrush(Colors.LightGray);
            }
            else
            {
                this.isRandom = true;
                btnRandom.Foreground = new SolidColorBrush(Colors.Gray);
            } 
        }

        private void btnRepeat_Click( object sender, RoutedEventArgs e )
        {
            this.isReapeatOnce = true;
            btnRepeat.Visibility = Visibility.Hidden;
            btnRepeatOnce.Visibility = Visibility.Visible;
        }

        private void btnRepeatOnce_Click( object sender, RoutedEventArgs e )
        {
            this.isReapeatOnce = false;
            btnRepeat.Visibility = Visibility.Visible;
            btnRepeatOnce.Visibility = Visibility.Hidden;
        }
    }
}
