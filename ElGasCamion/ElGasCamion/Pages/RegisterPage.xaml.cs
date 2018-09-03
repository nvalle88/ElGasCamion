using ElGasCamion.Models;
using ElGasCamion.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ElGasCamion.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
        RegisterViewModel ViewModels = new RegisterViewModel( new Distribuidor());
		public RegisterPage ()
		{
			InitializeComponent ();
            BindingContext = ViewModels;
		}       
    }
}