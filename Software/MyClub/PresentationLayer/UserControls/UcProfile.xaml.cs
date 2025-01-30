﻿using BusinessLogicLayer;
using EntitiesLayer.Entities;
using PresentationLayer.Helper;
using System;
using System.Collections.Generic;
using System.IO;
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

//Černjević
namespace PresentationLayer.UserControls
{
    /// <summary>
    /// Interaction logic for UcProfile.xaml
    /// </summary>
    public partial class UcProfile : UserControl
    {
        private UserProfileServices userProfileService = new UserProfileServices();

        public UcProfile()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayUserData();
        }
        //dodati popis zadnjih 10 prijava
        private void DisplayUserData()
        {
            //testni podataka
            var users = userProfileService.GetUserByEmail("admin@gmail.com");
            CurrentUser.User = users.FirstOrDefault();
            if (users != null && users.Count > 0)
            {
                var user = users.First();
                switch (user.RoleID)
                {
                    case 1:
                        lblRoleType.Content = "Admin";
                        break;
                    case 2:
                        lblRoleType.Content = "Coach";
                        break;
                    default:
                        lblRoleType.Content = "User";
                        break;
                }
                lblFirstName.Content = user.FirstName;
                lblLastName.Content = user.LastName;
                lblEmail.Content = user.Email;
                lblBirthDate.Content = user.BirthDate.ToString();
                imgProfilePicture.Source = ConvertToImage(user.ProfilePicture);
            }
        }

        private BitmapImage ConvertToImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;

            BitmapImage image = new BitmapImage();
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                image.BeginInit();
                image.StreamSource = ms;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
            }
            return image;
        }

        private void btnEditProfile_Click(object sender, RoutedEventArgs e)
        {
            GuiManager.OpenContent(new UcEditProfile(CurrentUser.User));
        }
    }
}
