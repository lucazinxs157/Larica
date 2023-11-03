using Controle;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laricão
{
    public partial class FrmInserir : Form
    {
        PrivateFontCollection pfc = new PrivateFontCollection();
        int id, perfiluser;
        List<int> quantidade = new List<int> { 1 };
        decimal valor, valorProd;
        string table = "select carrinho.cod_prod, amburge.nome, amburge.preco, amburge.foto, amburge.categoria from carrinho inner join amburge on carrinho.cod_prod = amburge.id";
        Conexao con = new Conexao();
        ControleProduto controle = new ControleProduto();
        ModeloProduto mp = new ModeloProduto();
        DataSet ds = new DataSet();
        int i = 0;//registro
        int codigoProduto;//identificar qual codigo
 




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {




        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public FrmInserir()
        {
            InitializeComponent();
        }
       

        private void FrmInserir_Load(object sender, EventArgs e)
        {

        }
    }
}
