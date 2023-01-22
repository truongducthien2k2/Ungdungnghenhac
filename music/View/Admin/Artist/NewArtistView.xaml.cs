using music.Component;
using music.Model;
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
using System.Windows.Shapes;

namespace music.View.Admin.Artist
{
    /// <summary>
    /// Interaction logic for NewArtistView.xaml
    /// </summary>
    public partial class NewArtistView : Window
    {
        ArtistViewModel artistVM = new ArtistViewModel();
        string selectedFileName;
        SINGER artist;
        string option;
        Frame MainContent;
        public NewArtistView()
        {
            InitializeComponent();
        }

        public NewArtistView(SINGER artist, string option, Frame MainContent)
        {
            InitializeComponent();
            this.artist = artist;
            this.option = option ?? throw new ArgumentNullException(nameof(option));
            LoadData();
            this.MainContent = MainContent;
        }

        private void LoadData()
        {
            if (artist != null)
            {
                selectedFileName = artist.singerImage;
                ImageViewer.Source = new BitmapImage(new Uri(selectedFileName));
                tbArtistName.Text = artist.singerName;
                tbArtistIntro.Text = artist.intro;
            }
        }

        protected override void OnSourceInitialized( EventArgs e )
        {
            IconHelper.RemoveIcon(this);
        }

        private void btnAddImage_Click( object sender, RoutedEventArgs e )
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if ( dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            {
                selectedFileName = dlg.FileName;
                ImageViewer.Source = new BitmapImage(new Uri(selectedFileName));
            }
        }

        private void btnSubmit_Click( object sender, RoutedEventArgs e )
        {
            string artistName = tbArtistName.Text;
            string artistIntro = tbArtistIntro.Text;
            if ( String.IsNullOrEmpty(selectedFileName) || String.IsNullOrEmpty(artistName) || String.IsNullOrEmpty(artistIntro) )
            {
                new CustomMessageBox("Vui lòng nhập đầy đủ thông tin", "Warning").Show();
            }
            else
            {
                switch ( this.option )
                {
                    case "adjust":
                        if ( artistVM.UpdateArtist(artist.id, artistName, artistIntro, selectedFileName) == 1 )
                        {
                            bool? result = new CustomMessageBox("Cập nhật thông tin nghệ sĩ thành công!!!", "Success").ShowDialog();
                            if ( result.Value )
                            {
                                this.Close();
                                MainContent.Navigate(new ArtistAdminView(MainContent));
                            }
                        }
                        break;
                    case "add":
                        if ( artistVM.InsertNewArtist(artistName, artistIntro, selectedFileName) == 1 )
                        {
                            bool? result = new CustomMessageBox("Thêm mới nghệ sĩ thành công!!!", "Success").ShowDialog();
                            if ( result.Value )
                            {
                                this.Close();
                                MainContent.Navigate(new ArtistAdminView(MainContent));
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
