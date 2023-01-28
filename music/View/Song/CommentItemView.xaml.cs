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
        COMMENT comment;
        public CommentItemView()
        {
            InitializeComponent();
        }

        public CommentItemView(COMMENT comment)
        {
            InitializeComponent();
            this.comment = comment;
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
    }
}
