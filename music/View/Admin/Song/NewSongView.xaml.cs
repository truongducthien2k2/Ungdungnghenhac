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

namespace music.View.Admin.Song
{
    /// <summary>
    /// Interaction logic for NewSongView.xaml
    /// </summary>
    public partial class NewSongView : Window
    {
        Frame MainContent;
        SONG song;
        string option;
        string selectedFileName;
        SongViewModel songVM = new SongViewModel();
        public NewSongView()
        {
            InitializeComponent();
        }

        public NewSongView(SONG song, string option, Frame MainContent)
        {
            InitializeComponent();
            this.song = song;
            this.option = option;
            this.MainContent = MainContent;
            LoadAvailableData();
        }

        private void LoadAvailableData()
        {
            if (song == null)
            {
                LoadTopic();
                LoadAlbum();
                LoadSinger();
                LoadArea();
            }
            else
            {
                ImageViewer.Source = new BitmapImage(new Uri(song.songImage));
                tbSongName.Text = song.songName;
                cbAlbumList.Text = song.albumId == null ? "" : songVM.GetAllAlbum().Where(album => album.id == song.albumId).Select(album => album.albumName).First();
                cbTopicList.Text = songVM.GetAllTopic().Where(topic => topic.id == song.topicId).Select(topic => topic.topicName).First();
                cbSingerList.Text = songVM.GetAllSinger().Where(singer => singer.id == song.singerId).Select(singer => singer.singerName).First();
                cbAreaList.Text = songVM.GetAllArea().Where(area => area.id == song.areaId).Select(area => area.areaName).First();
                tbLyrics.Text = song.lyrics;
                tbMusicPath.Text = song.songCode;
            }
        }

        private void LoadTopic()
        {
            var topics = songVM.GetAllTopic();   
            foreach(var topic in topics)
            {
                cbTopicList.Items.Add(topic.topicName);
            }
        }

        private void LoadAlbum()
        {
            var albums = songVM.GetAllAlbum();
            foreach(var album in albums)
            {
                cbAlbumList.Items.Add(album.albumName);
            }
        }

        private void LoadSinger()
        {
            var singers = songVM.GetAllSinger();
            foreach(var singer in singers)
            {
                cbSingerList.Items.Add(singer.singerName);
            }
        }

        private void LoadArea()
        {
            var areas = songVM.GetAllArea();
            foreach(var area in areas)
            {
                cbAreaList.Items.Add(area.areaName);
            }
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

        private void btnAddMusicFile_Click( object sender, RoutedEventArgs e )
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Audio files (*.mp3)|*.mp3|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if ( dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            {
                tbMusicPath.Text = dlg.FileName;
            }
        }

        private void btnSubmit_Click( object sender, RoutedEventArgs e )
        {
            if (String.IsNullOrEmpty(selectedFileName) || String.IsNullOrEmpty(tbSongName.Text) || String.IsNullOrEmpty(cbTopicList.SelectedValue.ToString()) || String.IsNullOrEmpty(cbSingerList.SelectedValue.ToString()) || String.IsNullOrEmpty(cbAreaList.SelectedValue.ToString()) || String.IsNullOrEmpty(tbLyrics.Text) || String.IsNullOrEmpty(tbMusicPath.Text))
            {
                new CustomMessageBox("Vui lòng nhập đầy đủ thông tin", "Warning").Show();
            }
            else
            {
                string songName = tbSongName.Text;
                string topic = cbTopicList.SelectedValue.ToString();
                string album;
                if (cbAlbumList.SelectedIndex == -1)
                {
                    album = "";
                }
                else
                {
                    album = cbAlbumList.SelectedValue.ToString();
                }
                string singer = cbSingerList.SelectedValue.ToString();
                string area = cbAreaList.SelectedValue.ToString();
                string lyrics = tbLyrics.Text;
                string songPath = tbMusicPath.Text;

                switch ( this.option )
                {
                    case "adjust":
                        if ( songVM.UpdateSong(song.id, songName, topic, album, singer, area, lyrics, songPath, selectedFileName) == 1 )
                        {
                            MessageBox.Show("Cập nhật thông tin bài hát thành công!!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                        }
                        break;
                    case "add":
                        if ( songVM.InsertSong(songName, topic, album, singer, area, lyrics, songPath, selectedFileName) == 1 )
                        {
                            MessageBox.Show("Thêm mới bài hát thành công!!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                        }
                        break;
                    default:
                        break;
                }
            } 
                
        }
    }
}
