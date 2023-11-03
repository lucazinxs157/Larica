using Controle;
using LaricãoHamburgueria;
using Modelo;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Laricão
{
    public partial class Cadastro : Form
    {
        PrivateFontCollection pfc = new PrivateFontCollection();
        UsuarioModelo usuario = new UsuarioModelo();
        Conexao con = new Conexao();
        UsuarioControle controle = new UsuarioControle();
        public Cadastro()
        {
            InitializeComponent();
        }

        private void Cadastro_Load(object sender, EventArgs e)
        {
            pfc.AddFontFile("E:\\Laricao\\img\\Gluten.ttf");
            Font padrao = new Font(pfc.Families[0], 16, FontStyle.Regular);
            label6.Font = padrao;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (TextBoxVazias())
                MessageBox.Show("Preencha Todos os Campos");

            else if (txtsenha.Text == txtsenha1.Text)
            {
                usuario.nome = txtnome.Text;
                usuario.sobrenome = txtsobrenome.Text;
                usuario.cpf = txtcpf.Text;
                usuario.email = txtemail.Text;
                usuario.tel = txttel.Text;
                usuario.senha = txtsenha.Text;
                usuario.idperfil = 1;

                string sql = $"select *from usuario where cpf = {usuario.cpf}";
                string sqlmail = $"select *from usuario where cpf = {usuario.email}";

                if (!controle.ValidarCPF(usuario.cpf))
                    MessageBox.Show("CPF inválido");
                else
                {

                    if (con.ValidaCadastro(usuario.email, usuario.cpf))
                    {
                        MessageBox.Show("Usuário já possúi Cadastro");
                    }
                    else
                    {
                        controle.Cadastrar(usuario);
                        MessageBox.Show("Usuário Cadastrado");
                        Login login = new Login();
                        this.Hide();
                        login.Show();

                    }
                }

            }
            else
            {
                MessageBox.Show("As senhas devem ser iguais");
            }
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

        private void label9_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            usuario.cpf = txtcpf.Text;
            if (controle.ValidarCPF(usuario.cpf))
                MessageBox.Show("sla foi mané");
            else
                MessageBox.Show("sla n foi mané");


        }
        private bool senhaVisivel = false;

        private void button3_Click(object sender, EventArgs e)
        {
            if (senhaVisivel)
            {
                txtsenha1.PasswordChar = '*';
                senhaVisivel = false;
            }
            else
            {
                txtsenha1.PasswordChar = '\0';
                senhaVisivel = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(senhaVisivel)
            {
                txtsenha.PasswordChar = '*';
                senhaVisivel = false;
            }
            else
            {
                txtsenha.PasswordChar = '\0';
                senhaVisivel = true;
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
