using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{

	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
	public ListaProduto()
	{
		InitializeComponent();

		lst_produtos.ItemsSource = lista;
	}

    protected async override void OnAppearing()
    {
		List<Produto> tmp = await App.Db.GetAll();

		tmp.ForEach(x => lista.Add(x));
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Navigation.PushAsync(new Views.NovoProduto());
		}
		catch ( Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "OK");
		}
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
		string q = e.NewTextValue;

		lista.Clear();

        List<Produto> tmp = await App.Db.Search(q);

        tmp.ForEach(x => lista.Add(x));

    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
		double soma = lista.Sum(x => x.Total);

		string msg = $"O Total é {soma:C}";

		DisplayAlert("Total dos Produtos", msg, "OK");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        if (menuItem != null && menuItem.BindingContext is Produto produto)
        {
            var confirm = await DisplayAlert("Confirmar exclusão",
                $"Você tem certeza que deseja deletar o produto \"{produto.Descricao}\"?", "Sim", "Não");

            if (confirm)
            {
                try
                {
                    // Removendo do banco de dados
                    await App.Db.Delete(produto.Id);

                    // Removendo da lista observável
                    lista.Remove(produto);

                    await DisplayAlert("Sucesso", "Produto deletado com sucesso.", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro", $"Erro ao deletar produto: {ex.Message}", "OK");
                }
            }
        }
    }
}