namespace Project1;

public partial class HistoryExps : ContentPage
{
	public HistoryExps()
	{
		InitializeComponent();
		BindingContext = App.viewModel;
	}

	private async void Button_Clicked(object sender, EventArgs e)
	{
		await App.viewModel.clearHistory();
	}
}