﻿using ElGasCamion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ElGasCamion.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
        RegisterViewModel ViewModels = new RegisterViewModel( new Models.Cliente());
		public RegisterPage ()
		{
			InitializeComponent ();
            BindingContext = ViewModels;
		}       
    }
}