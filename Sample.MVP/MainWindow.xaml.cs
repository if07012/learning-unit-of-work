using Sample.Presenters.Views;
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
using Sample.Presenters;
using Sample.Domains;

namespace Sample.MVP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IUserView
    {
        public MainWindow()
        {

            InitializeComponent();
            messagePanel.Visibility = Visibility.Collapsed;
            button.Click += LoginClick;
            btnAddMessage.Click += ButtonAddMessage;
        }

        private void ButtonAddMessage(object sender, RoutedEventArgs e)
        {
            prenseter.AddMessage();
            txtContentMessage.Text = "";
            User = prenseter.Login();
            IsLogin = User != null;
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            User = prenseter.Login();
            IsLogin = User != null;
        }

        public void AttachPresenter(UserPresenter presenter)
        {
            this.prenseter = presenter;
        }

        private bool isLogin;

        public bool IsLogin
        {
            get { return isLogin; }
            set
            {
                isLogin = value;
                if (isLogin)
                {
                    messagePanel.Visibility = Visibility.Visible;
                    listMessage.ItemsSource = prenseter.GetMessage();
                    lblMessage.Text = "Message > Last write Message " + User.LastWriteMessage.ToString("dd-MM-yyyy HH:mm:ss");
                }
            }
        }


        public string Password
        {
            get
            {
                return txtPassword.Text;
            }
        }

        public string UserName
        {
            get
            {
                return txtUserName.Text;
            }
        }
        UserPresenter prenseter;
        public User User { get; set; }

        public UserPresenter Prenseter
        {
            get
            {
                return prenseter;
            }
        }

        public string Message
        {
            get
            {
                return txtContentMessage.Text;
            }
        }
    }
}
