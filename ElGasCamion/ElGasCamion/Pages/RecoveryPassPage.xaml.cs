using ElGasCamion.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ElGasCamion.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecoveryPassPage : ContentPage
	{
        RecoveryPassViewModel viewModel = new RecoveryPassViewModel("");
		public RecoveryPassPage()
		{
			InitializeComponent();
            BindingContext = viewModel;
		}
	}
}