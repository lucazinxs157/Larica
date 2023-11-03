using LaricãoHamburgueria;
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
    public partial class FrmMenu : Form
    {
        PrivateFontCollection pfc = new PrivateFontCollection();
        int perfilusuario;
        int id;
        public FrmMenu(int idusuario, int perfil)
        {
            InitializeComponent();

            perfilusuario = perfil;
            if (perfilusuario == 1)
            {
                btn_clientes.Visible = false;
            }
            id = idusuario;

        }

        private void button2_Click(object sender, EventArgs e)
        {
               FrmCardapio Cardapio = new FrmCardapio(id, perfilusuario);
               this.Hide();
               Cardapio.ShowDialog();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
                pfc.AddFontFile("E:\\Laricao\\img\\Gluten.ttf");
                Font padrao = new Font(pfc.Families[0], 16, FontStyle.Regular);
                label1.Font = padrao;
                btn_cardapio.Font = padrao;
                btn_clientes.Font = padrao;
                button2.Font = padrao;
                button3.Font = padrao;
                button5.Font = padrao;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Deseja mesmo sair", "Laricão", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btn_clientes_Click(object sender, EventArgs e)
        {
            FrmClientes clientes = new FrmClientes(id, perfilusuario);
            this.Hide();
            clientes.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            FrmMenu Menu = new FrmMenu(id, perfilusuario);
            this.Hide();
            Menu.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmCarrinho carrinho = new FrmCarrinho(id, perfilusuario);
            this.Hide();
            carrinho.ShowDialog();  
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            FrmPedidos pedidos = new FrmPedidos(id, perfilusuario);
            this.Hide();
            pedidos.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();

            // Exibir o formulário de login
            loginForm.Show();

            // Fechar o formulário atual (menu ou qualquer outra página)
            this.Close();
        }
    }
}
