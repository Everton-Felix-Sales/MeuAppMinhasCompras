using SQLite;
using System.ComponentModel.DataAnnotations;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        string _descricao;
        double _quantidade;
        double _preco;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string  Descricao {
            get => _descricao;
            set
            {
                if (value == null) 
                {
                    throw new Exception("Por favor, Preencha o Campo Descrição");
                }
                _descricao = value;
            }
        
        }

        public double Quantidade
        {
            get => _quantidade;
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Por favor, O Campo quantidade deve ser maior que zero");
                }
                _quantidade = value;
            }

        }
        public double Preco
        {
            get => _preco;
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Por favor, O Campo preço deve ser maior que zero");
                }
                _preco = value;
            }

        }
        public double Total { get => Quantidade * Preco; }
    }
}
