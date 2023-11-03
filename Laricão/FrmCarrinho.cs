using Controle;
using Correios;
using Microsoft.Win32;
using Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Laricão
{
    public partial class FrmCarrinho : Form
    {
        PrivateFontCollection pfc = new PrivateFontCollection();
        int id, perfiluser;
        List<int> quantidade = new List<int>{1};
        decimal valor, valorProd;
        string table = "select carrinho.cod_prod, amburge.nome, amburge.preco, amburge.foto, amburge.categoria from carrinho inner join amburge on carrinho.cod_prod = amburge.id";
        Conexao con = new Conexao();
        ControleProduto controle = new ControleProduto();
        PedidoModelo modelo = new PedidoModelo();
        UsuarioControle user = new UsuarioControle();
        DataTable dt = new DataTable();
        List<ModeloProduto> produtos = new List<ModeloProduto>();

        public FrmCarrinho(int idusuario, int perfil)
        {
            perfiluser = perfil;
            id = idusuario;
            InitializeComponent();
        }

        private void FrmCarrinho_Load(object sender, EventArgs e)
        {
            pfc.AddFontFile("E:\\Laricao\\img\\Gluten.ttf");
            Font padrao = new Font(pfc.Families[0], 16, FontStyle.Regular);
            Titulo.Font = padrao;
            rs.Font = padrao;
            lblValor.Font = padrao;
            lbl1.Font = padrao;
            ListarProduto(table);
        }

        private void ListarProduto(string ctg)
        {
            pfc.AddFontFile("E:\\laricao\\img\\Gluten.ttf");
            dt = con.ObterDados(ctg);
            int x = 10;
            int y = 0;
            cartPanel.Controls.Clear();
            produtos.Clear();
            if (dt.Rows.Count > 0)
            {
                for (int registros = 0; registros < dt.Rows.Count; registros++)
                {
                    Label id = new Label();
                    id.Name = "id";
                    id.Text = registros.ToString();

                    Label cod = new Label();
                    cod.Name = "cod";
                    cod.Text = dt.Rows[registros][0].ToString();

                    Panel produto = new Panel();
                    produto.Controls.Clear();
                    produto.BorderStyle = BorderStyle.FixedSingle;
                    produto.Location = new Point(x, y);
                    produto.Height = 125;
                    produto.Width = 620;

                    PictureBox foto = new PictureBox();
                    foto.Location = new Point(5, 5);
                    foto.SizeMode = PictureBoxSizeMode.StretchImage;
                    foto.Name = "foto";
                    foto.Image = Image.FromFile(dt.Rows[registros][3].ToString());
                    foto.Height = 115;
                    foto.Width = 115;

                    Label rs = new Label();
                    rs.Name = "rs";
                    rs.Text = "R$";
                    rs.Width = 30;
                    rs.Location = new Point(335, 60);
                    rs.Font = new Font(pfc.Families[0], 11, FontStyle.Bold);

                    Label preco = new Label();
                    preco.Name = "preco";
                    preco.Text = dt.Rows[registros][2].ToString();
                    preco.Location = new Point(360, 60);
                    preco.Font = new Font(pfc.Families[0], 11, FontStyle.Bold);

                    Label descproduto = new Label();
                    descproduto.Width = 200;
                    descproduto.Name = "nome";
                    descproduto.Text = dt.Rows[registros][1].ToString();
                    descproduto.Location = new Point(150, 40);
                    descproduto.Font = new Font(pfc.Families[0], 13, FontStyle.Bold);

                    Label categoria = new Label();
                    categoria.Name = "categoria";
                    categoria.Text = dt.Rows[registros][4].ToString();
                    categoria.Location = new Point(150, 65);
                    categoria.Font = new Font(pfc.Families[0], 9, FontStyle.Regular);

                    TextBox qtd = new TextBox();
                    qtd.Width = 40;
                    quantidade.Add(1);
                    qtd.Text = quantidade[registros].ToString();
                    qtd.TextAlign = HorizontalAlignment.Center;
                    qtd.Name = "Quantidade";
                    qtd.Leave += new EventHandler((sender1, e1) => QuantLeave(sender1, e1, qtd.Text, Convert.ToInt32(id.Text)));
                    qtd.Location = new Point(530, 40);

                    Button remover = new Button();
                    remover.Name = "Remover";
                    remover.Text = "Remover";
                    remover.Size = new Size(100, 30);
                    remover.Click += new EventHandler((sender1, e1) => removerClick(sender1, e1, Convert.ToInt32(cod.Text)));
                    remover.Location = new Point(500, 80);

                    produto.Controls.Add(descproduto);
                    produto.Controls.Add(rs);
                    produto.Controls.Add(preco);
                    produto.Controls.Add(foto);
                    produto.Controls.Add(remover);
                    produto.Controls.Add(categoria);
                    produto.Controls.Add(qtd);

                    //adiciono o painel produto ao painel
                    cartPanel.Controls.Add(produto);
                    y += 135;

                    //somo o valor total do carrinho
                    valorProd = quantidade[registros] * Convert.ToDecimal(dt.Rows[registros][2]);
                    valor += valorProd;

                    //
                    produtos.Add(new ModeloProduto()
                    {
                        Id = Convert.ToInt32(cod.Text),
                        Nome = descproduto.Text,
                        qtd = Convert.ToInt32(qtd.Text),
                        preco = Convert.ToDecimal(preco.Text)
                    });

                }

                lblValor.Text = valor.ToString();
                valorProd = 0;
                valor = 0;
            }
            else
            {
                Label msg = new Label();
                msg.Name = "Carrinho Vazio";
                msg.Text = "Carrinho Vazio";
                msg.Width = Width;
                msg.Font = new Font(pfc.Families[0], 13, FontStyle.Bold);
                msg.Location = new Point(230, 205);
                cartPanel.Controls.Add(msg);

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo sair", "Laricão", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo voltar ao menu?", "Laricão", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FrmMenu Menu = new FrmMenu(id, perfiluser);

                this.Hide();
                Menu.ShowDialog();
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
        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count != 0)
            {
                if (TextBoxVazias())
                    MessageBox.Show("Preencha todos os Campos");
                else
                {
                    modelo.Nome = txt_nome.Text;
                    modelo.idusuario = id;
                    modelo.cep = txtCEP.Text;
                    modelo.bairro = txtBairro.Text;
                    modelo.end = txtEnd.Text;
                    modelo.num = txtNum.Text;
                    modelo.tel = txtTelefone.Text;
                    modelo.status = "Pedido Registrado - Aguardando Confirmação";
                    string Json = JsonConvert.SerializeObject(produtos);
                    modelo.json = Json;
                    modelo.obs = txtobs.Text;
                    if (controle.CadastrarPedido(modelo))
                    {
                        MessageBox.Show("Pedido Enviado - Aguardando Confirmação");
                        user.gerarPDF(modelo, quantidade);
                        con.Cls();
                        ListarProduto(table);
                    }
                    else
                        MessageBox.Show("Erro ao Enviar Pedido");
                }
            }
            else
                MessageBox.Show("Carrinho Vazio");
        }

        private void txtCEP_Leave(object sender, EventArgs e)
        {
            var correio = new CorreiosApi();
            if (String.IsNullOrEmpty(txtCEP.Text))
            {
                txtCEP.BackColor = Color.LightCoral;
            }
            else
            {
                try
                {
                    correio.consultaCEP(txtCEP.Text);
                    var dados = correio.consultaCEP(txtCEP.Text);
                    txtEnd.Text = dados.end;
                    txtBairro.Text = dados.bairro;
                    txtCEP.BackColor = Color.White;
                    txtNum.Focus();
                }
                catch
                {
                    txtCEP.BackColor = Color.LightCoral;
                }
            }
        }

        private void QuantLeave(object sender, EventArgs e, string quant, int id)
        {
            quantidade[id] = Convert.ToInt32(quant);
            ListarProduto(table);
        }
        private void removerClick(object sender, EventArgs e, int id)
        {
            controle.RemoverCart(id);
            ListarProduto(table);
        }
    }
}
