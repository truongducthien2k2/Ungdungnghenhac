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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace music.View.Song
{
    /// <summary>
    /// Interaction logic for CommentItemView.xaml
    /// </summary>
    public partial class CommentItemView : UserControl
    {
        AccountViewModel accountVM = new AccountViewModel();
        SongViewModel songVM = new SongViewModel();
        COMMENT comment;
        Frame MainContent;
        Button btnPlay;
        Button btnPause;
        MediaPlayer player;
        public CommentItemView()
        {
            InitializeComponent();
        }

        public CommentItemView(COMMENT comment, Frame MainContent, Button btnPlay, Button btnPause, MediaPlayer player )
        {
            InitializeComponent();
            this.comment = comment;
            this.MainContent = MainContent;
            this.btnPlay = btnPlay;
            this.btnPause = btnPause;
            this.player = player;
            LoadComment();
        }

        private void LoadComment()
        {
            CLIENT user = accountVM.GetUserInfo((int) comment.clientId);
            tbNameOfUser.Text = user.fullName;
            DateTime commentDate = (DateTime) comment.commentDate;
            tbTimeOfComment.Text = $"{commentDate.Day}/{commentDate.Month}/{commentDate.Year}";
            tbContentOfComment.Text = comment.content;
        }

        private void btnRemove_Click( object sender, RoutedEventArgs e )
        {
            if ( songVM.RemoveComment(comment.id) == 1 )
            {
                MainContent.Navigate(new PlaySongView(MainContent, btnPlay, btnPause, player));
            }
        }
    }
}
