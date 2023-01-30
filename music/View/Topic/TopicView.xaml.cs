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

namespace music.View.Topic
{
    /// <summary>
    /// Interaction logic for TopicView.xaml
    /// </summary>
    public partial class TopicView : Page
    {
        SongViewModel topicVM = new SongViewModel();
        Image topicImage;
        TextBlock topicName;
        public TopicView()
        {
            InitializeComponent();
        }
        public TopicView(Image topicimage,TextBlock topicname)
        {
            InitializeComponent();
            this.topicImage = topicimage;
            this.topicName = topicname;
            LoadUI();
        }
        private void LoadUI()
        {
            List<TOPIC> topic = topicVM.GetAllTopic();
            foreach (TOPIC topicDataItem in topic)
            {
                plTOPIC.Children.Add(new TopicItemView(topicDataItem,topicImage,topicName));
            }
        }
    }
}
