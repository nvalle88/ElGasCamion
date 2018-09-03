using ElGasCamion.Models;
using ElGasCamion.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ElGasCamion.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage2 : ContentPage
    {
        RegisterViewModel ViewModels;


        public RegisterPage2(Distribuidor distribuidor)
        {
            ViewModels = new RegisterViewModel(distribuidor);
            InitializeComponent();
            BindingContext = ViewModels;
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}