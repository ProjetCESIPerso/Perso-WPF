using AnnuaireEntrepriseCESI.DTOs;
using AnnuaireEntrepriseCESI.Interfaces;
using AnnuaireEntrepriseCESI.Services;
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

namespace AnnuaireEntrepriseCESI.Pages.GestionUser
{
    /// <summary>
    /// Logique d'interaction pour UpdateUserWindow.xaml
    /// </summary>
    public partial class UpdateUserWindow : Window
    {
        private readonly IUserService _userService;

        int UserId { get; set; }

        int ServiceId { get; set; }

        int SiteId { get; set; }

        public UpdateUserWindow(UserDTO userDTO)
        {
            InitializeComponent();

            _userService = new UserService();

            NameUser.DataContext = userDTO;
            SurnameUser.DataContext = userDTO;
            MailUser.DataContext = userDTO;
            PhoneNumber.DataContext = userDTO;
            MobilePhone.DataContext = userDTO;

            ServiceId = userDTO.ServiceId;
            SiteId = userDTO.SiteId;
        }

       

        private void BtnValiderClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnRetourClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
