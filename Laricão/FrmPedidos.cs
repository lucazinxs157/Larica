using Controle;
using Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace Laricão
{
    public partial class FrmPedidos : Form
    {
        PrivateFontCollection pfc = new PrivateFontCollection();
        PedidoModelo pedido = new PedidoModelo();
        ModeloProduto modelo = new ModeloProduto();
        ControleProduto produto = new ControleProduto();
        Conexao con = new Conexao();
        int perfiluser, id;
        int[] y = { 0, 135, 270, 405, 540, 675, 810, 945 };
        bool validacao = false;
        string SqlComando = null;

        public FrmPedidos(int idusuario, int perfil)
        {
            InitializeComponent();
            pfc.AddFontFile("E:\\laricao\\img\\Gluten.ttf");
            label1.Font = new Font(pfc.Families[0], 16, FontStyle.Regular);
            perfiluser = perfil;
            id = idusuario;
        }

        private void FrmPedidos_Load(object sender, EventArgs e)

        {
            if (perfiluser == 2)
                SqlComando = "Select * from pedidos";
            else
                SqlComando = $"Select * from pedidos where idusuario = {id}";

            DataTable dt = new DataTable();

            dt = con.ObterDados(SqlComando);

            List<PedidoModelo> pedidos = new List<PedidoModelo>();

            foreach (DataRow row in dt.Rows)
            {
                PedidoModelo pedido = new PedidoModelo
                {
                    Id = (row["id_pedido"].ToString()),
                    Nome = row["Nome"].ToString(),
                    cep = row["cep"].ToString(),
                    bairro = row["bairro"].ToString(),
                    end = row["endereco"].ToString(),
                    num = row["num"].ToString(),
                    tel = row["tel"].ToString(),
                    status = row["status"].ToString(),
                    json = row["pedido"].ToString(),
                    obs = row["obs"].ToString()
                };
                pedidos.Add(pedido);
            }

            try
            {
                ListarProduto(pedidos);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Erro ao ler o JSON: " + ex.Message);
            }

        }

        private void ListarProduto(List<PedidoModelo> pedidos)
        {
            pfc.AddFontFile("E:\\Laricao\\img\\Gluten.ttf");

            int registros;
            if (pedidos.Count != 0)
            {
                for (registros = 0; registros <= pedidos.Count; registros++)
                {

                    PedidoModelo item = pedidos[registros];
                    List<ModeloProduto> produtos = new List<ModeloProduto>();


                    produtos = JsonConvert.DeserializeObject<List<ModeloProduto>>(item.json);


                    Label Id = new Label();
                    Id.Name = "id";
                    Id.Text = registros.ToString();

                    Panel produto = new Panel();
                    produto.Controls.Clear();
                    produto.BorderStyle = BorderStyle.FixedSingle;
                    produto.Location = new Point(10, y[registros]);
                    produto.Height = 120;
                    produto.Width = 485;
                    produto.Click += new EventHandler((sender1, e1) => panelClick(sender1, e1, produto, Convert.ToInt32(Id.Text)));

                    Label Cod = new Label();
                    Cod.Text = $"#{item.Id.PadLeft(4, '0')}";
                    Cod.Name = "Código do Pedido";
                    Cod.Location = new Point(20, 20);
                    Cod.Font = new Font(pfc.Families[0], 9, FontStyle.Regular);
                    Cod.Click += new EventHandler((sender1, e1) => panelClick(sender1, e1, produto, Convert.ToInt32(Id.Text)));

                    Label Nome = new Label();
                    Nome.Width = Width;
                    Nome.Height = 40;
                    Nome.Name = "nome";
                    Nome.Text = item.Nome.ToString();
                    Nome.Location = new Point(20, 40);
                    Nome.Font = new Font(pfc.Families[0], 14, FontStyle.Bold);
                    Nome.Click += new EventHandler((sender1, e1) => panelClick(sender1, e1, produto, Convert.ToInt32(Id.Text)));

                    Label status = new Label();
                    status.Name = "status";
                    status.Width = Width;
                    status.Text = item.status.ToString();
                    status.Location = new Point(20, 80);
                    status.Font = new Font(pfc.Families[0], 9, FontStyle.Regular);
                    status.Click += new EventHandler((sender1, e1) => panelClick(sender1, e1, produto, Convert.ToInt32(Id.Text)));
                    if(status.Text == "Pedido Finalizado")
                    {
                        PictureBox img = new PictureBox();
                        img.Image = Image.FromFile("E:/Laricao/img/check-mark.png");
                        img.Location = new Point(400, 45);
                        produto.Controls.Add(img);
                    }

                    Label banner1 = new Label();
                    banner1.Name = "Nome";
                    banner1.Text = "Produto";
                    banner1.Font = new Font(banner1.Font, FontStyle.Bold);
                    banner1.Location = new Point(20, 120);

                    Label banner2 = new Label();
                    banner2.Name = "Preço";
                    banner2.Text = "Preço";
                    banner2.Font = new Font(banner2.Font, FontStyle.Bold);
                    banner2.Location = new Point(200, 120);

                    Label banner3 = new Label();
                    banner3.Name = "Quantidade";
                    banner3.Text = "Quantidade";
                    banner3.Font = new Font(banner3.Font, FontStyle.Bold);
                    banner3.Location = new Point(310, 120);

                    int pos = 150;
                    foreach (var modelo in produtos)
                    {
                        Label coisa = new Label();
                        coisa.Name = "Coisa";
                        coisa.Text = modelo.Nome.ToString();
                        coisa.Location = new Point(20, pos);
                        produto.Controls.Add(coisa);

                        Label preco = new Label();
                        preco.Name = "Preço";
                        preco.Text = "R$ " + modelo.preco.ToString();
                        preco.Location = new Point(200, pos);
                        produto.Controls.Add(preco);

                        Label qtd = new Label();
                        qtd.Name = "Quantidade";
                        qtd.Text = modelo.qtd.ToString();
                        qtd.Location = new Point(330, pos);
                        produto.Controls.Add(qtd);
                        pos += 25;
                    }

                    pos += 10;
                    Label observ = new Label();
                    observ.Name = "Observações";
                    observ.Text = "Observações: ";
                    observ.Width = 85;
                    observ.Font = new Font(banner1.Font, FontStyle.Bold);
                    observ.Location = new Point(20, pos);
                    produto.Controls.Add(observ);

                    Label obs = new Label();
                    obs.Name = "Observações";
                    obs.Text = item.obs.ToString();
                    obs.Width = Width;
                    obs.Location = new Point(105, pos);
                    produto.Controls.Add(obs);
                    pos += 25;

                    Label endereco = new Label();
                    endereco.Name = "Endereço";
                    endereco.Text = "Endereço:";
                    endereco.Width = 67;
                    endereco.Font = new Font(banner1.Font, FontStyle.Bold);
                    endereco.Location = new Point(20, pos);
                    produto.Controls.Add(endereco);

                    Label end = new Label();
                    end.Name = "Endereço";
                    end.Text = $"{item.end}, {item.num} - {item.bairro}";
                    end.Width = Width;
                    end.Location = new Point(85, pos);
                    produto.Controls.Add(end);
                    pos += 25;

                    if (perfiluser == 2)
                    {
                        Button aceitar = new Button();
                        aceitar.Name = "Aceitar Pedido";
                        aceitar.Text = "Aceitar Pedido";
                        aceitar.Width = 200;
                        aceitar.Location = new Point(20, pos);
                        aceitar.BackColor = Color.LightGreen;
                        aceitar.FlatStyle = FlatStyle.Flat;
                        aceitar.Click += new EventHandler((sender1, e1) => ButtonClick(sender1, e1, Convert.ToInt32(item.Id), 1));
                        produto.Controls.Add(aceitar);

                        Button recusar = new Button();
                        recusar.Name = "Recusar Pedido";
                        recusar.Text = "Recusar Pedido";
                        recusar.Width = 200;
                        recusar.Location = new Point(240, pos);
                        recusar.BackColor = Color.LightCoral;
                        recusar.FlatStyle = FlatStyle.Flat;
                        recusar.Click += new EventHandler((sender1, e1) => ButtonClick(sender1, e1, Convert.ToInt32(item.Id), 2));
                        produto.Controls.Add(recusar);

                        pos += 30;
                        
                        Button finalizar = new Button();
                        finalizar.Name = "Finalizar Pedido";
                        finalizar.Text = "Finalizar Pedido";
                        finalizar.Width = 200;
                        finalizar.Location = new Point(120, pos);
                        finalizar.BackColor = Color.LightBlue;
                        finalizar.FlatStyle = FlatStyle.Flat;
                        finalizar.Click += new EventHandler((sender1, e1) => ButtonClick(sender1, e1, Convert.ToInt32(item.Id), 3));
                        produto.Controls.Add(finalizar);
                    }

                    produto.Controls.Add(Nome);
                    produto.Controls.Add(status);
                    produto.Controls.Add(Cod);
                    produto.Controls.Add(banner1);
                    produto.Controls.Add(banner2);
                    produto.Controls.Add(banner3);

                    //adiciono o painel produto ao painel
                    PnlPedidos.Controls.Add(produto);
                }
            }
            else
            {
                Label msg = new Label();
                msg.Name = "Pedidos";
                msg.Text = "Não Há Pedidos";
                msg.Width = Width;
                msg.Font = new Font(pfc.Families[0], 13, FontStyle.Bold);
                msg.Location = new Point(400, 225);
                PnlPedidos.Controls.Add(msg);
            }
        }

        private void btn_voltar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo voltar ao menu?", "Laricão", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FrmMenu Menu = new FrmMenu(id, perfiluser);

                this.Hide();
                Menu.ShowDialog();
            }
        }

        private void panelClick(object sender, EventArgs e, Panel produto, int pos)
        {
            if (produto.Height == 120 && validacao == false)
            {
                produto.Location = new Point(510, 0);
                produto.Height = 460;
                validacao = true;
            }
            else if (produto.Height == 460)
            {
                validacao = false;
                produto.Location = new Point(10, y[pos]);
                produto.Height = 120;
            }
        }

        private void ButtonClick(object sender, EventArgs e, int cod, int op)
        {
            MessageBox.Show(produto.EditarStatus(cod, op));
            con.AttStatus(op, id);
        }
    }

}

