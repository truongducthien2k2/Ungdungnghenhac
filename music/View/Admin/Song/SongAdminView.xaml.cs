using music.View.Admin.Song;
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

namespace music.View.Admin
{
    /// <summary>
    /// Interaction logic for SongAdminView.xaml
    /// </summary>
    public partial class SongAdminView : Page
    {
        Frame MainContent;
        SongViewModel songVM = new SongViewModel();
        public SongAdminView(Frame MainContent)
        {
            InitializeComponent();
            this.MainContent = MainContent;
            LoadUI();
        }

        private void btnAddSong_Click( object sender, RoutedEventArgs e )
        {
            NewSongView newSong = new NewSongView(null, "add", MainContent);
            newSong.Show();
        }

        private void LoadUI()
        {
            var songs = songVM.GetAllSong();
            foreach(var song in songs)
            {
                plSongs.Children.Add(new SongItemView(song, MainContent));
            }
        }
    }
}
