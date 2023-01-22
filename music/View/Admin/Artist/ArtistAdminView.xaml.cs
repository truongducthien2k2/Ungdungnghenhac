using Microsoft.Win32;
using music.Model;
using music.View.Admin.Artist;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace music.View.Admin
{
    /// <summary>
    /// Interaction logic for ArtistAdminView.xaml
    /// </summary>
    public partial class ArtistAdminView : Page
    {
        ArtistViewModel artistVM = new ArtistViewModel();
        Frame MainContent;
        public ArtistAdminView()
        {
            InitializeComponent();
            LoadSingerData();
        }

        public ArtistAdminView(Frame MainContent)
        {
            InitializeComponent();
            LoadSingerData();
            this.MainContent = MainContent;
        }

        private void btnNewArtist_Click( object sender, RoutedEventArgs e )
        {
            NewArtistView newArtist = new NewArtistView(null, "add", MainContent);
            newArtist.Show();
        }

        private void LoadSingerData()
        {
            List<SINGER> singerList = artistVM.GetAllArtist();

            foreach ( var item in singerList)
            {
                plArtistList.Children.Add(new ArtistItemView(item, MainContent));
            }
        }
    }
}
