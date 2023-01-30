using music.Model;
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

namespace music.View.Admin.Artist
{
    /// <summary>
    /// Interaction logic for ArtistItemView.xaml
    /// </summary>
    public partial class ArtistItemView : UserControl
    {
        Frame MainContent;
        SINGER artist;
        public ArtistItemView()
        {
            InitializeComponent();
        }

        public ArtistItemView(SINGER artist, Frame MainContent)
        {
            InitializeComponent();
            artistId.Text = artist.id.ToString();
            artistName.Text = artist.singerName;
            artistImage.Source = new BitmapImage(new Uri(artist.singerImage));
            this.MainContent = MainContent;
            this.artist = artist;
        }

        private void btnAdjust_Click( object sender, RoutedEventArgs e )
        {
            NewArtistView newArtist = new NewArtistView(artist, "adjust", MainContent);
            newArtist.Show();
            newArtist.Closed += NewArtist_Closed;
        }

        private void NewArtist_Closed( object sender, EventArgs e )
        {
            MainContent.Navigate(new ArtistAdminView(MainContent));
        }
    }
}
