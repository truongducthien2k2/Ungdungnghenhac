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

namespace music.View.Admin.Topic
{
    /// <summary>
    /// Interaction logic for NewTopicView.xaml
    /// </summary>
    public partial class NewTopicView : Window
    {
        TopicViewModel topicVM = new TopicViewModel();
        string selectedFileName;
        TOPIC topic;
        string option;
        Frame MainContent;
        public NewTopicView()
        {
            InitializeComponent();
        }

        public NewTopicView(TOPIC topic, string option, Frame MainContent )
        {
            InitializeComponent();
            this.topic = topic;
            this.option = option;
            this.MainContent = MainContent;
            LoadData();
        }

        protected override void OnSourceInitialized( EventArgs e )
        {
            IconHelper.RemoveIcon(this);
        }

        private void LoadData()
        {
            if ( topic != null )
            {
                selectedFileName = topic.topicImage;
                ImageViewer.Source = new BitmapImage(new Uri(selectedFileName));
                tbTopicName.Text = topic.topicName;
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

        private void btnSubmit_Click( object sender, RoutedEventArgs e )
        {
            string topicName = tbTopicName.Text;
            if ( String.IsNullOrEmpty(selectedFileName) || String.IsNullOrEmpty(topicName) )
            {
                new CustomMessageBox("Vui lòng nhập đầy đủ thông tin", "Warning").Show();
            } 
            else
            {
                switch ( this.option )
                {
                    case "adjust":
                        if ( topicVM.UpdateTopic(topic.id, topicName, selectedFileName) == 1 )
                        {
                            MessageBox.Show("Cập nhật chủ đề thành công!!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            MainContent.Navigate(new TopicAdminView(MainContent));
                        }
                        break;
                    case "add":
                        if ( topicVM.InsertNewTopic(topicName, selectedFileName) == 1 )
                        {
                            MessageBox.Show("Thêm mới chủ đề thành công!!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            MainContent.Navigate(new TopicAdminView(MainContent));
                        }
                        break;
                    default:
                        break;
                }
            }    
        }
    }
}
