using music.Model;
using music.View.Admin.Topic;
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
    /// Interaction logic for TopicAdminView.xaml
    /// </summary>
    public partial class TopicAdminView : Page
    {
        TopicViewModel topicVM = new TopicViewModel();
        Frame MainContent;
        public TopicAdminView()
        {
            InitializeComponent();
        }

        public TopicAdminView(Frame MainContent)
        {
            InitializeComponent();
            this.MainContent = MainContent;
            LoadTopics();
        }

        private void btnAddTopic_Click( object sender, RoutedEventArgs e )
        {
            NewTopicView newTopic = new NewTopicView(null, "add", MainContent);
            newTopic.Show();
        }

        private void LoadTopics()
        {
            List<TOPIC> topics = topicVM.GetAllTopic();
            foreach(var topic in topics)
            {
                plTopics.Children.Add(new TopicItemView(topic, MainContent));
            }
        }
    }
}
