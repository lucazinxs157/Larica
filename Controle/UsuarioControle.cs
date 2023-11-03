using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using PdfSharp;//biblioteca gerar PDf
using PdfSharp.Drawing;//para desenho
using PdfSharp.Pdf;// conversao

namespace Controle
{
    public class UsuarioControle
    {
        Conexao con = new Conexao();
        UsuarioModelo modelo = new UsuarioModelo();
        PedidoModelo pedido = new PedidoModelo();

        public DataTable logar(UsuarioModelo login)
        {
            DataTable registro = new DataTable();
            string sql = "select idusuario,id_perfil from usuario where nome = @usuario and senha = @senha";
            MySqlConnection sqlConexao = con.GetConexao();
            sqlConexao.Open();
            MySqlCommand cmd = new MySqlCommand(sql, sqlConexao);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@usuario", login.nome);
            cmd.Parameters.AddWithValue("@senha", con.getMD5Hash(login.senha));
            MySqlDataAdapter dados = new MySqlDataAdapter(cmd);
            dados.Fill(registro);
            sqlConexao.Close();
            return registro;
        }
        public bool teste()
        {
            if (con.GetConexao() == null)
                return false;
            else
                return true;
        }

        public bool Cadastrar(UsuarioModelo usuario)
        {
            bool resultado = false;
            string sqlComando = $"INSERT INTO usuario (nome, sobrenome, cpf, telefone, senha, id_perfil, email) VALUES ('{usuario.nome}','{usuario.sobrenome}','{usuario.cpf}','{usuario.tel}','{con.getMD5Hash(usuario.senha)}','{ usuario.idperfil}','{usuario.email}')";

            MySqlConnection sqlConexao = con.GetConexao();
            sqlConexao.Open();
            MySqlCommand cmd = new MySqlCommand(sqlComando, sqlConexao);

            //
            if (con.ValidaCadastro(usuario.email, usuario.cpf))
            {
            }
            else
            {
                resultado = true;
                cmd.ExecuteNonQuery();
            }

            sqlConexao.Close();


            return resultado;
        }
        public bool ValidarCPF(string cpf)
        {
            cpf = cpf.Replace(",", "").Replace("-", "");

            if (cpf.Length != 11 || TodosDigitosIguais(cpf))
            {
                return false;
            }

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
            {
                if (!int.TryParse(cpf[i].ToString(), out numeros[i]))
                {
                    return false;
                }
            }

            int soma1 = 0;
            for (int i = 0; i < 11; i++)
            {
                soma1 += numeros[i];
            }

            int resto1 = soma1 % 11;

            if (resto1 == 0)
                return true;
            else
                return false;
        }
        static bool TodosDigitosIguais(string cpf)
        {
            bool result = false;
            for (int i = 1; i < cpf.Length; i++)
            {
                if (cpf[i] != cpf[0])
                {
                    result = false;
                }
                else
                    result = true;
            }
            return result;
        }
        public bool Editar(UsuarioModelo us)
        {
            bool resultado = false;
            string sql = "update usuario set nome = @nome, sobrenome = @sobrenome, cpf = @cpf, telefone = @telefone, senha = @senha, id_perfil = @perfil, email = @email where idusuario = @id";
            MySqlConnection sqlConexao = con.GetConexao();
            sqlConexao.Open();
            MySqlCommand command = new MySqlCommand(sql, sqlConexao);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sql;
            //substituindo a variavel @___ pelo conteúdo do objeto
            command.Parameters.AddWithValue("@nome", us.nome);
            command.Parameters.AddWithValue("@sobrenome", us.sobrenome);
            command.Parameters.AddWithValue("@cpf", us.cpf);
            command.Parameters.AddWithValue("@telefone", us.tel);
            command.Parameters.AddWithValue("@senha", con.getMD5Hash(us.senha));
            command.Parameters.AddWithValue("@id", us.id);
            command.Parameters.AddWithValue("@perfil", us.idperfil);
            command.Parameters.AddWithValue("@email", us.email);
            if (command.ExecuteNonQuery() >= 1)
                resultado = true;
            sqlConexao.Close();
            return resultado;
        }
        public bool Excluir(int id)
        {
            bool resultado = false;
            string sql = "delete from usuario where idusuario =" + id;
            MySqlConnection sqlConexao = con.GetConexao();
            sqlConexao.Open();
            MySqlCommand Command = new MySqlCommand(sql, sqlConexao);
            Command.CommandType = System.Data.CommandType.Text;
            Command.CommandText = sql;
            if (Command.ExecuteNonQuery() >= 1)
                resultado = true;
            sqlConexao.Close();
            return resultado;
        }

        public void gerarPDF(PedidoModelo modelo, List<int> qtd)

        {
            string sql = "select carrinho.cod_prod, amburge.nome, amburge.preco from carrinho inner join amburge on carrinho.cod_prod = amburge.id";

            //preparo o comando sql
            MySqlConnection sqlConexao = con.GetConexao();
            UsuarioModelo us = new UsuarioModelo();
            ModeloProduto mp = new ModeloProduto();
            MySqlCommand cmd = new MySqlCommand(sql, sqlConexao);
            XUnit pageWidth = XUnit.FromMillimeter(148);
            XUnit pageHeight = XUnit.FromMillimeter(210);
            PdfDocument pdfDocument = new PdfDocument();
            XImage image = XImage.FromFile("E:\\Laricao\\img\\frame.png");
            XRect imageRect = new XRect(30, 30, 30, 30);

            MySqlDataAdapter dados;//preparar os dados
            DataSet ds = new DataSet();
            XFont font = new XFont("Arial", 12);
            double lineHeight = font.GetHeight();

            try //teste de consulta
            {
                int i = 0;//registro
                int ypoint = 0;//espaço do conteudo
                sqlConexao.Open();//abro a conexao
                dados = new MySqlDataAdapter(cmd);//recuperando as informações
                dados.Fill(ds);//carrega as informções geradas
                switch (ds.Tables[0].Rows.Count)
                {
                    case 1:
                        pageHeight = XUnit.FromMillimeter(200); // Altura da página para 1 pedido
                        break;
                    case 2:
                        pageHeight = XUnit.FromMillimeter(210); // Altura da página para 2 pedidos
                        break;
                    case 3:
                        pageHeight = XUnit.FromMillimeter(220); // Altura da página para 3 pedidos
                        break;
                    case 4:
                        pageHeight = XUnit.FromMillimeter(220); // Altura da página para 4 pedidos
                        break;
                    case 5:
                        pageHeight = XUnit.FromMillimeter(230); // Altura da página para 5 pedidos
                        break;
                    case 6:
                        pageHeight = XUnit.FromMillimeter(240); // Altura da página para 6 pedidos
                        break;
                    case 7:
                        pageHeight = XUnit.FromMillimeter(250); // Altura da página para 7 pedidos
                        break;
                    case 8:
                        pageHeight = XUnit.FromMillimeter(260); // Altura da página para 8 pedidos
                        break;
                    case 9:
                        pageHeight = XUnit.FromMillimeter(270); // Altura da página para 9 pedidos
                        break;
                    case 10:
                        pageHeight = XUnit.FromMillimeter(280); // Altura da página para 10 pedidos
                        break;
                    default:
                        // Lidere com outros casos ou erros aqui, se necessário
                        break;



                }
                PdfDocument pdf = new PdfDocument();
                //chamo a instancia do PDf
                pdf.Info.Title = "Nota Fiscal";
                PdfPage page = pdf.AddPage();//gera uma nova pagina
                XGraphics grafic = XGraphics.FromPdfPage(page);
                page.Width = pageWidth;
                page.Height = pageHeight;
                //defino a font e o tamanho
                XPoint startPoint = new XPoint(160, 40);
                double tableStartY = startPoint.Y + lineHeight * 7;
                double tableY = tableStartY + lineHeight;
                grafic.DrawString("--------------------------------------------------------------------------------", font, XBrushes.Black, new XPoint(40, lineHeight * 4));
                grafic.DrawString("--------------------------------------------------------------------------------", font, XBrushes.Black, new XPoint(40, lineHeight * 8));
                grafic.DrawString("Nota Fiscal", font, XBrushes.Black, startPoint);
                grafic.DrawString($"Nome do Cliente: {modelo.Nome}", font, XBrushes.Black, new XPoint(40, lineHeight * 5));
                grafic.DrawString($"Rua: {modelo.end}, {modelo.num} - {modelo.bairro}", font, XBrushes.Black, new XPoint(40, lineHeight * 6));

                grafic.DrawString("Quantidade", font, XBrushes.Black, new XPoint(40, tableStartY));
                grafic.DrawString("Data: " + DateTime.Now.ToString(), font, XBrushes.Black, new XPoint(40, lineHeight * 7));
                grafic.DrawString("Descrição", font, XBrushes.Black, new XPoint(160, tableStartY));
                tableY = tableY + 30;
                grafic.DrawString("Preço", font, XBrushes.Black, new XPoint(315, tableStartY));
                ypoint = ypoint + 75;//nova posição
                decimal total = 0;
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //guarde no objeto nome p resultado da coluna
                    us.id = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0].ToString());
                    mp.preco = Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[2].ToString());
                    us.nome = Convert.ToString(ds.Tables[0].Rows[i].ItemArray[1].ToString());

                    total += mp.preco;
                    grafic.DrawString(qtd[i].ToString(), font, XBrushes.Black, new XPoint(43, tableY));
                    ypoint = ypoint + 30;
                    us.nome = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                    grafic.DrawString(us.nome.ToString(), font, XBrushes.Black, new XPoint(160, tableY));
                    ypoint = ypoint + 30;
                    grafic.DrawString(mp.preco.ToString(), font, XBrushes.Black, new XPoint(315, tableY));
                    tableY = tableY + 30;
                }//defino o nome do arquivo pdf


                // ...

                grafic.DrawImage(image, new XPoint(89, tableY + 5));
                double alturaDaImagem = 70; // Defina a altura real da sua imagem

                // Ajuste a coordenada Y das linhas de texto com base na altura da página
                double linhaY = pageHeight.Point - alturaDaImagem - font.Height * 1; // Posição das linhas de texto

                grafic.DrawString("Total: " + total, font, XBrushes.Black, new XPoint(160, linhaY));
                linhaY += font.Height;

                grafic.DrawString("Obrigado pela preferência", font, XBrushes.Black, new XPoint(130, linhaY));

                // ...


                string pdffilename = "Nota.pdf";
                pdf.Save(pdffilename);//salvo o arquivo em pdf
                Process.Start(pdffilename);//abro o arquivo salvo
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }


        }
    }
}
