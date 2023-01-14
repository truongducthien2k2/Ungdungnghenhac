using music.Component;
using music.View;
using System;
using System.Windows;
using System.Windows.Input;


namespace music
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
        {
        public MainWindow()
        {
            InitializeComponent();
            navFrame.Navigate(new HomeView());
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
            bool isSuccesLogin = false;
            if ( isSuccesLogin )
            {
                navFrame.Navigate(new AccountView());
            } else
            {
                navFrame.Navigate(new LoginView());
            }

        }
    }
}
