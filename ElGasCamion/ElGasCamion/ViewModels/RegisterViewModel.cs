﻿using ElGasCamion.Helpers;
using ElGasCamion.Models;
using ElGasCamion.Pages;
using ElGasCamion.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ElGasCamion.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        #region Services
        private readonly ApiServices _apiServices = new ApiServices();
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string CalleUno { get; set; }
        public string Numero { get; set; }

        public string CalleDos { get; set; }
        public string Sector { get; set; }

        public string message = "";
        public string Message
        {
            set
            {
                if (message != value)
                {
                    message = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Message"));
                }
            }
            get
            {
                return message;
            }
        }
        public Cliente Cliente { get; set; }

        private bool isBusy = false;
        public bool IsBusy
        {
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsBusy"));
                }
            }
            get
            {
                return isBusy;
            }
        }

        private bool isAcceptPolicy = false;
        public bool IsAcceptPolicy
        {
            set
            {
                if (isAcceptPolicy != value)
                {
                    isAcceptPolicy = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsAcceptPolicy"));
                }
            }
            get
            {
                return isAcceptPolicy;
            }
        }

        private bool isError = false;
        public bool IsError
        {
            set
            {
                if (isError != value)
                {
                    isError = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsError"));
                }
            }
            get
            {
                return isError;
            }
        }

        private Ciudad _ciudadSeleccionada { get; set; }
        public Ciudad CiudadSeleccionada
        {
            get
            {
                return _ciudadSeleccionada;
            }

            set
            {


                if (_ciudadSeleccionada != value)
                {
                    _ciudadSeleccionada = value;
                    LoadSectors(_ciudadSeleccionada.IdCiudad);

                }
            }
        }

        public Sector SectorSeleccionado
        {
            get; set;
        }

        List<Ciudad> ciudades = new List<Ciudad>();
        public List<Ciudad> Ciudades
        {
            set
            {
                if (ciudades != value)
                {
                    ciudades = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Ciudades"));
                }
            }
            get
            {
                return ciudades;
            }
        }

        public List<Sector> Sectores
        {
            get;
            set;
        }

        private List<Sector> _sectoresPorCiudad;
        public List<Sector> SectoresPorCiudad
        {
            get
            {
                return _sectoresPorCiudad;
            }
            set

            {
                if (_sectoresPorCiudad != value)
                {
                    _sectoresPorCiudad = value;
                    OnPropertyChanged();
                }
            }
        }



        #endregion

        #region Cosntructor
        public RegisterViewModel(Cliente cliente)
        {
            //Ciudades = new List<Ciudad>
            //{
            //    new Ciudad("1", "Quito"),
            //    new Ciudad("2", "Guayaquil"),
            //    new Ciudad("3", "Cuenca")
            //};

            //Sectores = new List<Sector>
            //{
            //    new Sector("1","1" ,"Norte"),
            //    new Sector("2","1", "Sur"),
            //    new Sector("3","1", "Centro"),
            //    new Sector("4","1","Los Chillos"),
            //    new Sector("5","2" ,"Norte"),
            //    new Sector("6","2", "Sur"),
            //    new Sector("7","2", "Centro")

            //};

            LoadCities();
            isError = false;
            IsError = false;

            Cliente = cliente;
            Username = cliente.Correo;
            Password = cliente.Identificacion;
            ConfirmPassword = cliente.Identificacion;

            cliente.Identificacion = "";



        }
        #endregion

        #region Commands
        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;
                    if (Password == ConfirmPassword)
                    {
                        Cliente.Direccion = String.Format("{0}, {1}, {2}, {3}", CalleUno, Numero, CalleDos, Sector);
                        Cliente.Habilitado = true;
                        Cliente.IdSector = SectorSeleccionado.IdSector;

                        var isRegistered = await _apiServices.RegisterUserAsync
                        (Username, Password, ConfirmPassword, Cliente);


                        IsBusy = false;

                        if (isRegistered)
                        {
                            Settings.Username = Username;
                            Settings.Password = Password;

                            Message = "Se registró con éxito";
                            await App.Current.MainPage.DisplayAlert("El Gas", Message, "Aceptar");
                            App.Current.MainPage = new NavigationPage(new LoginPage());

                        }
                    }
                    else
                    {
                        IsBusy = false;
                        Message = "Las contraseñas no coincide";
                        await App.Current.MainPage.DisplayAlert("El Gas", Message, "Aceptar");
                    }
                });
            }
        }

        public ICommand PolicyCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new TermsAndPolicyPage());
                });
            }
        }


        public ICommand NextCommand
        {
            get
            {
                return new Command(async () =>
                {
                    //App.Current.MainPage = new NavigationPage(new RegisterPage2(Cliente));                     Application.Current.MainPage.Navigation.PushAsync(new RegisterPage2(Cliente));

                    if (IsAcceptPolicy)
                    {
                        if (Password != null && Password != "")
                        {
                            if (ConfirmPassword == Password)
                            {
                                if (Password.Length > 3)
                                {
                                    Cliente.Correo = Username;
                                    Cliente.Identificacion = Password;
                                    App.Current.MainPage = new NavigationPage(new RegisterPage2(Cliente));
                                }
                                else
                                {
                                    await App.Current.MainPage.DisplayAlert("Error", "Las contraseña debe tener al menos 4 caracteres", "Aceptar");

                                }



                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Error", "Las contraseñas no coinciden", "Aceptar");
                            }

                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Error", "Todos los campos son obligatorios", "Aceptar");

                        }

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Debe haber leído y estar de acuerdo con los Términos y condiciones y las Políticas y tratamiento de datos personales", "Aceptar");
                    }

                });
            }
        }


        #endregion

        #region Methods    
        public async void LoadCities()
        {
            Ciudades = await _apiServices.GetCiudades();
        }

        public async void LoadSectors(int idCities)
        {
            SectoresPorCiudad = await _apiServices.GetSectors(idCities);
        }
        #endregion



        #region PropertyChanged

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        #endregion
    }
}