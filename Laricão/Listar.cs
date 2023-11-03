using Controle;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laricão
{
    public partial class Listar : Form
    {
        PrivateFontCollection pfc = new PrivateFontCollection();
        int id, perfiluser;
        List<int> quantidade = new List<int> { 1 };
        decimal valor, valorProd;
        string table = "select carrinho.cod_prod, amburge.nome, amburge.preco, amburge.foto, amburge.categoria from carrinho inner join amburge on carrinho.cod_prod = amburge.id";
        Conexao con = new Conexao();
        ControleProduto controle = new ControleProduto();
        ModeloProduto pmodelo = new ModeloProduto();
        DataSet ds = new DataSet();
        int i = 0;//registro
        int codigoProduto;//identificar qual codigo
        public Listar()
        {
            InitializeComponent();
        }

        private void txtProduto_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            pmodelo.Id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            id = Convert.ToInt32(Text = pmodelo.Id.ToString());
            pmodelo.Nome = Convert.ToString(Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            pmodelo.preco = Convert.ToDecimal(Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
            pmodelo.qtd = Convert.ToInt32(Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
      
      
        }
    }
}
