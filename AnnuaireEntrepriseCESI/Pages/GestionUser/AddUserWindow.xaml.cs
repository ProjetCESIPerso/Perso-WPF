using AnnuaireEntrepriseCESI.Interfaces;
using AnnuaireEntrepriseCESI.Models;
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
    /// Logique d'interaction pour AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        private readonly IUserService _userService;
        private readonly ISiteService _siteService;
        private readonly IServiceService _serviceService;

        public AddUserWindow()
        {
            InitializeComponent();

            _userService = new UserService();
            _siteService = new SiteService();
            _serviceService = new ServiceService();

            RecupListes();
        }

        private async void BtnValiderClick(object sender, RoutedEventArgs e)
        {
            AddUser();
        }

        private void BtnRetourClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void RecupListes()
        {
            List<Site> listSite = _siteService.GetAllSite().Result;

            List<Service> listService = _serviceService.GetAllService().Result;

            foreach (Site item in listSite)
            {
                SiteUser.Items.Add(item.Town);
            }

            foreach (Service item in listService)
            {
                ServiceUser.Items.Add(item.Name);
            }
        }

        private async void AddUser()
        {
            if (VerifDonnees() == true)
            {
                User user = new User();
                Site site = _siteService.GetByName(SiteUser.SelectedValue.ToString()).Result;
                Service service = _serviceService.GetByName(ServiceUser.SelectedValue.ToString()).Result;

                user.Name = NameUser.Text;
                user.Surname = SurnameUser.Text;
                user.Email = MailUser.Text;
                user.PhoneNumber = PhoneNumber.Text;
                user.MobilePhone = MobilePhone.Text;
                user.SiteId = site.Id;
                user.ServiceId = service.Id;

                await _userService.AddUser(user);
                MessageBox.Show("Ajout enregistrée", "Ajout", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Veuillez remplir tout les champs !", "Ajout", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool VerifDonnees()
        {
            if (NameUser.Text == null || NameUser.Text == string.Empty) { return false; }
            if (SurnameUser.Text == null || SurnameUser.Text == string.Empty) { return false; }
            if (MailUser.Text == null || MailUser.Text == string.Empty) { return false; }
            if (PhoneNumber.Text == null || PhoneNumber.Text == string.Empty) { return false; }
            if (MobilePhone.Text == null || MobilePhone.Text == string.Empty) { return false; }
            if (SiteUser.SelectedValue.ToString() == null) { return false; }
            if (ServiceUser.SelectedValue.ToString() == null) { return false; }

            return true;
        }
    }
}
