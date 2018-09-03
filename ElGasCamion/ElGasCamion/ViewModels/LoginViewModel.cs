﻿using ElGasCamion.Helpers;
using ElGasCamion.Models;
using ElGasCamion.Pages;
using ElGasCamion.Services;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ElGasCamion.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        #region Constructor

        ICommand tapCommand;

        public LoginViewModel()
        {
            Username = Settings.Username;
            Password = Settings.Password;
            tapCommand = new Command(OnTapped);
        }

        #endregion
      
        #region Services

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly ApiServices _apiServices = new ApiServices();

        #endregion

        #region Propieties

        private bool _isRemember = false;
        public bool isRemember
        {
            set
            {
                if (_isRemember != value)
                {
                    _isRemember = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("isRemember"));
                }
            }
            get
            {
                return _isRemember;
            }
        }

        public string Username { get; set; }
        public string Password { get; set; }

        #endregion

        #region Commands

        public ICommand RegisterCommand { get { return new RelayCommand(Register); } }
        private async void Register()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

       
        public ICommand TapCommand
        {
            get { return tapCommand; }
        }

        private async void OnTapped(object s)
        {

            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }


        public ICommand RecoveryCommand { get { return new RelayCommand(Recovery); } }
        private async void Recovery()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RecoveryPassPage());
        }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var accesstoken = await _apiServices.LoginAsync(Username, Password);

                    if (accesstoken != null)
                    {
                        Settings.AccessToken = accesstoken;


                        var d = new Distribuidor { Correo = Username, DeviceID = Settings.DeviceID };
                        var response = await ApiServices.InsertarAsync<Distribuidor>(d, new System.Uri(Constants.BaseApiAddress), "/api/Distribuidors/GetDistribuidorData");
                        var cliente = JsonConvert.DeserializeObject<Distribuidor>(response.Result.ToString());
                        Settings.IdDistribuidor = cliente.IdDistribuidor;
                        Settings.IdFireBase = cliente.FirebaseID;

                        App.Current.MainPage = new NavigationPage(new MasterPage());

                    }

                });
            }
        }
        
        #endregion


      
    }
}
