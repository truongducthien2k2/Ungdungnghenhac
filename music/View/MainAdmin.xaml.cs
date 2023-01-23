using music.Component;
using music.View.Admin;
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
using System.Windows.Shapes;

namespace music.View
{
    /// <summary>
    /// Interaction logic for MainAdmin.xaml
    /// </summary>
    public partial class MainAdmin : Window
    {
        public MainAdmin()
        {
            InitializeComponent();
            navFrame.Navigate(new HomeAdminView());
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
            navFrame.Navigate(new HomeAdminView());
        }

        private void songBtn_Click( object sender, RoutedEventArgs e )
        {
            navFrame.Navigate(new SongAdminView(navFrame));
        }

        private void topicBtn_Click( object sender, RoutedEventArgs e )
        {
            navFrame.Navigate (new TopicAdminView(navFrame));
        }

        private void albumBtn_Click( object sender, RoutedEventArgs e )
        {
            navFrame.Navigate(new AlbumAdminView());
        }

        private void artistBtn_Click( object sender, RoutedEventArgs e )
        {
            navFrame.Navigate(new ArtistAdminView(navFrame));
        }

        private void advertisementBtn_Click( object sender, RoutedEventArgs e )
        {
            navFrame.Navigate(new AdvertisementAdminView());
        }

        private void accountBtn_Click( object sender, RoutedEventArgs e )
        {

        }
    }
}
