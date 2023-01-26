using music.Component;
using music.Model;
using music.View;
using music.ViewModel;
using System;
using System.Linq;
using System.Windows;
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
        public MainWindow()
        {
            InitializeComponent();
            song = songVM.GetAllSong().First();
            HandleLogin();
            LoadSong();
        }


        private void LoadSong()
        {
            ImageViewer.Source = new BitmapImage(new Uri(song.songImage));
            tbSongName.Text = song.songName;
            tbSingerName.Text = songVM.GetAllSinger().Where(singer => singer.id == song.singerId).Select(singer => singer.singerName).First();
            player.Open(new Uri(song.songCode));
        }

        protected override void OnSourceInitialized( EventArgs e )
        {
            IconHelper.RemoveIcon(this);
        }

        private void Border_MouseDown( object sender, MouseButtonEventArgs e )
        {
            if ( e.ChangedButton == MouseButton.Left )
                {
                this.DragMove();
                }
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
            Properties.Settings.Default ["user"] = account.userName;
            Properties.Settings.Default ["isAdmin"] = account.isAdmin;
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
            navFrame.Navigate(new HistoryView());

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
            navFrame.Navigate(new SongView());

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
            if ( Properties.Settings.Default ["user"].ToString() != "" )
            {
                navFrame.Navigate(new AccountView());
            } else
            {
                navFrame.Navigate(new LoginView());
            }
        }

        private void btnPlay_Click( object sender, RoutedEventArgs e )
        {
            btnPlay.Visibility = Visibility.Hidden;
            btnPause.Visibility = Visibility.Visible;
            player.Play();
        }

        private void btnPause_Click( object sender, RoutedEventArgs e )
        {
            btnPlay.Visibility = Visibility.Visible;
            btnPause.Visibility = Visibility.Hidden;
            player.Pause();
        }
    }
}
