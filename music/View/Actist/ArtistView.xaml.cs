using music.Model;
using music.View.Song;
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

namespace music.View.Actist
{
    /// <summary>
    /// Interaction logic for ArtistView.xaml
    /// </summary>
    public partial class ArtistView : Page
    {
        SongViewModel topicVM = new SongViewModel();
        Image songImage;
        TextBlock songName;
        TextBlock singerName;
        MediaPlayer player;
        SINGER singer;
        Frame MainContent;
        public ArtistView( Image songImage, TextBlock songName, TextBlock singerName, MediaPlayer player, SINGER singer, Frame MainContent )
        {
            InitializeComponent();
            this.songImage = songImage;
            this.songName = songName;
            this.singerName = singerName;
            this.player = player;
            this.singer = singer;
            this.MainContent = MainContent;
            LoadUI();
        }
        private void LoadUI()
        {
            if ( singer == null )
            {
                List<SINGER> singers = topicVM.GetAllSinger();
                foreach ( SINGER singer in singers )
                {
                    plSinger.Children.Add(new ActistItemView(singer, songImage, songName, singerName, player, MainContent));
                }
            }
            else
            {
                List<SONG> songs = topicVM.GetAllSong().Where(song => song.singerId == singer.id).ToList();
                if ( songs.Count == 0 )
                {
                    TextBlock notice = new TextBlock();
                    notice.Text = "Không có bài hát có sẵn của nghệ sĩ này";
                    notice.HorizontalAlignment = HorizontalAlignment.Center;
                    notice.Margin = new Thickness(0, 30, 0, 0);
                    notice.FontSize = 14;
                    plSinger.Children.Add(notice);
                }
                else
                {
                    foreach ( SONG song in songs )
                    {
                        plSinger.Children.Add(new SongItemView(song, songImage, songName, singerName, player));
                    }
                }
            }
        }
    }
}
