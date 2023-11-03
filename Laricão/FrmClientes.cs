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
using Org.BouncyCastle.Asn1.Pkcs;
using Modelo;
using Controle;
using Google.Protobuf.WellKnownTypes;

namespace Laricão
{
    public partial class FrmClientes : Form
    {
        PrivateFontCollection pfc = new PrivateFontCollection();
        UsuarioModelo modelo = new UsuarioModelo();
        UsuarioControle controle = new UsuarioControle();
        Conexao con = new Conexao();
        int idPerfil;
        int id;
        int perfiluser;
        string sql = "select usuario.idusuario,usuario.nome, usuario.sobrenome, usuario.cpf, usuario.telefone, usuario.id_perfil,usuario.email, usuario.senha from usuario inner join perfil on usuario.id_perfil = perfil.id_perfil;";
        public FrmClientes(int idusuario, int perfil_user)
        {
            InitializeComponent();
            perfiluser = perfil_user;
            id = idusuario;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo voltar ao menu?", "Laricão", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FrmMenu Menu = new FrmMenu(id, perfiluser);

                this.Hide();
                Menu.ShowDialog();
            }
        }

       

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            dtUsuario.DataSource = con.ObterDados(sql);
            cboPerfil.DataSource = con.ObterDados("select * from perfil");
            cboPerfil.DisplayMember = "perfil";
            cboPerfil.ValueMember = "id_perfil";
            pfc.AddFontFile("E:\\laricao\\img\\Gluten.ttf");
            label1.Font = new Font(pfc.Families[0], 16, FontStyle.Regular);
            dtUsuario.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            DtCliente.DataSource = con.ObterDados("select * from usuario");


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo sair", "Laricão", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            modelo.nome = txtNome.Text;
            modelo.sobrenome = txtsobre.Text;
            modelo.cpf = txtcpf.Text;
            modelo.tel = txttel.Text;
            modelo.senha = txtSenha.Text;
            modelo.idperfil = idPerfil;
            modelo.email = txtEmail.Text;
            if (controle.ValidarCPF(modelo.cpf) && controle.Cadastrar(modelo))
                MessageBox.Show("Cadastrado com sucesso");
            else
                MessageBox.Show("Erro no Cadastro");
            dtUsuario.DataSource = con.ObterDados(sql);
            dtUsuario.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void cboPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            idPerfil = Convert.ToInt32(((DataRowView)cboPerfil.SelectedItem)["id_perfil"]);
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            modelo.id =id;
            modelo.nome = txtNome.Text;
            modelo.sobrenome = txtsobre.Text;
            modelo.cpf = txtcpf.Text;
            modelo.tel = txttel.Text;
            modelo.senha = txtSenha.Text;
            modelo.idperfil = idPerfil;
            modelo.email = txtEmail.Text;
            if (controle.Editar(modelo))
                MessageBox.Show("Alterado com sucesso");
            else
                MessageBox.Show("Erro ao Editar");
            dtUsuario.DataSource = con.ObterDados(sql);
            dtUsuario.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

        }

        private void dtUsuario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32(dtUsuario.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            MessageBox.Show("Usuário selecionado: " + id.ToString());
            txtNome.Text = dtUsuario.Rows[e.RowIndex].Cells["nome"].Value.ToString();
            txtsobre.Text = dtUsuario.Rows[e.RowIndex].Cells["sobrenome"].Value.ToString();
            txtcpf.Text = dtUsuario.Rows[e.RowIndex].Cells["cpf"].Value.ToString();
            txttel.Text = dtUsuario.Rows[e.RowIndex].Cells["telefone"].Value.ToString();
            txtEmail.Text = dtUsuario.Rows[e.RowIndex].Cells["email"].Value.ToString();
            txtSenha.Text = dtUsuario.Rows[e.RowIndex].Cells["senha"].Value.ToString();
            cboPerfil.Text = dtUsuario.Rows[e.RowIndex].Cells["id_perfil"].Value.ToString();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (controle.Excluir(id) == true)
            {
                MessageBox.Show("Usuário excluído");
                dtUsuario.DataSource = con.ObterDados(sql);
            }
            else
            {
                MessageBox.Show("Erro ao excluir usuário");
            }
        }

        private void DtCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32(DtCliente.Rows[e.RowIndex].Cells[0].Value);
            MessageBox.Show("Usuário selecionado: " + id.ToString());
            txtCodigo.Text = modelo.id.ToString();
            txtNome.Text = DtCliente.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtsobre.Text = DtCliente.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtcpf.Text = DtCliente.Rows[e.RowIndex].Cells[6].Value.ToString();
            txttel.Text = DtCliente.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtSenha.Text = DtCliente.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtEmail.Text = DtCliente.Rows[e.RowIndex].Cells[4].Value.ToString();
            cboPerfil.Text = DtCliente.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string sql = null;
            int valor;
            if (string.IsNullOrEmpty(txtPesquisar.Text))
            {
                sql = "select * from usuario";
            }
            else
            {
                bool teste = int.TryParse(txtPesquisar.Text, out valor);
                if (teste)
                {
                    sql = "select *from usuario where idusuario = " + valor;
                }
                else
                {
                    sql = "select *from usuario where nome like '%" + txtPesquisar.Text + "%'";
                }
            }
            DtCliente.DataSource = con.ObterDados(sql);
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

    }
    
}