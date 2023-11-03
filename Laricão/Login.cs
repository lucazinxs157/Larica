using Controle;
using Laricão;
using Modelo;
using System;
using System.Data;
using System.Windows.Forms;

namespace LaricãoHamburgueria
{
    public partial class Login : Form
    {
        UsuarioControle controle = new UsuarioControle();
        UsuarioModelo modelo = new UsuarioModelo();
        Conexao con = new Conexao();
        public Login()
        {
            InitializeComponent();
        }

        private void btn_logar_Click(object sender, EventArgs e)
        {
            DataTable usuario = new DataTable();
            modelo.nome = txtLogin.Text;
            modelo.senha = txtSenha.Text;
            if (TextBoxVazias())
                MessageBox.Show("Preencha todos os Campos");
            else
                usuario = controle.logar(modelo);
            try
            {
                int codigoUsuario = Convert.ToInt32(usuario.Rows[0][0]);
                int perfilUsuario = Convert.ToInt32(usuario.Rows[0][1]);
                if (codigoUsuario >= 1)
                {
                    this.Hide();
                    FrmMenu Menu = new FrmMenu(codigoUsuario, perfilUsuario);

                    Menu.Show();
                }
            }
            catch
            {
                MessageBox.Show("Usuário ou senha inválidos");
            }

        }

        private void lblSenha_Click(object sender, EventArgs e)
        {
            MessageBox.Show(con.recuperaremail(txtLogin.Text));
        }

        private void lblCadastro_Click(object sender, EventArgs e)
        {
            Cadastro cadastro = new Cadastro();
            this.Hide();
            cadastro.Show();
        }
        private bool TextBoxVazias()
        {
            foreach (Control c in this.Controls)
                if (c is TextBox)
                {
                    TextBox textBox = c as TextBox;
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                        return true;
                }
            return false;
        }

        private void txtLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btn_logar_Click(sender, e);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSair_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

}
