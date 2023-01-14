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
        Page parentPage;
        SINGER artist;
        public ArtistItemView()
        {
            InitializeComponent();
        }

        public ArtistItemView(SINGER artist, Page parent)
        {
            InitializeComponent();
            artistId.Text = artist.id.ToString();
            artistName.Text = artist.singerName;
            artistImage.Source = new BitmapImage(new Uri(artist.singerImage));
            parentPage = parent;
            this.artist = artist;
        }

        private void btnAdjust_Click( object sender, RoutedEventArgs e )
        {
            NewArtistView newArtist = new NewArtistView(artist, "adjust");
            newArtist.Show();
        }
    }
}
