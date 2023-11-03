using Modelo;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;
using System.Text;


namespace Controle
{
    public class Conexao
    {
        static private string servidor = "localhost";
        static private string db = "laricao";
        static private string usuario = "root";
        static private string senha = "";
        public MySqlConnection con = null;
        Random aleatorio = new Random();

        static private string StrCon = "server=" + servidor + ";database=" + db + ";user=" + usuario + ";password=" + senha;

        public MySqlConnection GetConexao()
        {
            MySqlConnection conexao = new MySqlConnection(StrCon);
            return conexao;
        }
        public DataTable ObterDados(string sql)
        {
            //crio uma nova tabela de dados
            DataTable dt = new DataTable();
            //conn é a conexão com o banco de dados
            MySqlConnection conn = GetConexao();
            conn.Open();//abre o banco de dados
            //preparo o comando sql
            MySqlCommand sqlCon = new MySqlCommand(sql, conn);
            //tipo de instrução texto
            sqlCon.CommandType = System.Data.CommandType.Text;
            sqlCon.CommandText = sql;
            //irá montar as informações da consulta
            MySqlDataAdapter dados = new MySqlDataAdapter(sqlCon);
            dados.Fill(dt);
            conn.Close();
            return dt;
        }
        public string getMD5Hash(string senha)
        {
            System.Security.Cryptography.MD5 mD5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(senha);
            byte[] hash = mD5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
        public string recuperaremail(string login)
        { //testar a recuperacao
            try
            {
                DataTable dt = new DataTable();
                string msg = null;//validação da informação
                string senhanova;//guarda a senha gerada
                bool confirmar;//guarda o resultado do editar
                if (login == null)
                {//valido o preenchimento
                    msg = "login está vazio";
                }
                else
                {

                    //abrir o BD
                    con = GetConexao();
                    con.Open();
                    dt = ObterDados("select * from usuario where nome='" + login + "'");

                    if (dt.Rows.Count > 0)
                    {
                        string email = "eduardo-sampaio@outlook.com";
                        string senha = "coxinha123";

                        SmtpClient cliente = new SmtpClient();
                        cliente.Host = "smtp.office365.com";
                        cliente.Port = 587;
                        cliente.EnableSsl = true;
                        cliente.UseDefaultCredentials = false;
                        cliente.Credentials = new System.Net.NetworkCredential(email, senha);
                        cliente.DeliveryMethod = SmtpDeliveryMethod.Network;


                        MailMessage mail = new MailMessage();
                        mail.Sender = new MailAddress(email, "Laricão Hamburgueria");
                        mail.From = new MailAddress(email, "Recuperar senha");
                        string emailusuario = dt.Rows[0][7].ToString();
                        mail.To.Add(new MailAddress(emailusuario, dt.Rows[0][1].ToString()));
                        mail.Subject = "Recuperar Senha";
                        senhanova = aleatorio.Next(2000).ToString();

                        UsuarioModelo modelo = new UsuarioModelo();
                        UsuarioControle controle = new UsuarioControle();
                        modelo.senha = senhanova;
                        modelo.id = Convert.ToInt32(dt.Rows[0][0].ToString());
                        modelo.nome = dt.Rows[0][1].ToString();
                        modelo.sobrenome = dt.Rows[0][2].ToString();
                        modelo.cpf = dt.Rows[0][3].ToString();
                        modelo.tel = dt.Rows[0][4].ToString();
                        modelo.idperfil = Convert.ToInt32(dt.Rows[0][6].ToString());
                        modelo.email = dt.Rows[0][7].ToString();
                        confirmar = controle.Editar(modelo);
                        mail.Body = "Olá " + dt.Rows[0][1].ToString() + " sua senha é: " + senhanova;
                        mail.IsBodyHtml = true;//cria um arquivo html
                        try
                        {
                            if (confirmar)
                            {
                                cliente.SendMailAsync(mail);
                                msg = "E-mail enviado com a nova senha";
                            }
                            else
                            {
                                msg = "não foi possivel atualizar senha";
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Erro ao enviar o email:" + ex.Message);
                        }
                    }
                    else
                    {
                        msg = "Usuário não localizado";
                    }
                }
                return msg;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro:" + ex.Message);
            }

        }
        public void AttStatus(int status, int id)
        { //testar a recuperacao
            try
            {
                string msg = null;
                string user = null;
                string name;

                switch (status)
                {
                    case 1:
                        msg = "Seu Pedido foi aceito e está sendo Preparado!";
                        break;
                    case 2:
                        msg = "Seu Pedido foi Recusado!";
                        break;
                    case 3:
                        msg = "Pedido Finalizado, sendo enviado para Entrega!!";
                        break;
                }

                MySqlConnection con = GetConexao();
                con.Open();//abre o banco de dados
                string command = $"select email, nome from usuario where idusuario= {id}";
                MySqlCommand sqlCon = new MySqlCommand(command, con);
                sqlCon.CommandType = CommandType.Text;
                sqlCon.CommandText = command;

                MySqlDataReader query = sqlCon.ExecuteReader();
                if (query.Read())
                {
                    query.Read();
                    user = query["email"].ToString();
                    name = query["nome"].ToString();
                }
                con.Close();

                string email = "eduardo-sampaio@outlook.com";
                string senha = "coxinha123";

                SmtpClient cliente = new SmtpClient();
                cliente.Host = "smtp.office365.com";
                cliente.Port = 587;
                cliente.EnableSsl = true;
                cliente.UseDefaultCredentials = false;
                cliente.Credentials = new System.Net.NetworkCredential(email, senha);
                cliente.DeliveryMethod = SmtpDeliveryMethod.Network;


                MailMessage mail = new MailMessage();
                mail.Sender = new MailAddress(email, "Laricão Hamburgueria");
                mail.From = new MailAddress(email, "Laricão hamburgueria");
                mail.To.Add(new MailAddress(user));
                mail.Subject = "Status Pedido";
                mail.Body = msg;
                mail.IsBodyHtml = true;//cria um arquivo html
                try
                {
                    cliente.SendMailAsync(mail);

                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao enviar o email:" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro:" + ex.Message);
            }

        }
        public List<int> Validacao(string sql)
        {
            //conn é a conexão com o banco de dados

            MySqlConnection conn = GetConexao();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            List<int> valores = new List<int>();
            while (reader.Read())
            {
                int valor = Convert.ToInt32(reader["cod_prod"]);
                valores.Add(valor);
            }

            reader.Close();
            conn.Close();
            return valores;
        }
        public bool ValidaCadastro(string email, string cpf)
        {
            bool resultado = false;
            string sqlComando = $"select Count(*) from usuario where cpf = '{cpf}' or email = '{email}'";

            MySqlConnection sqlConexao = GetConexao();
            sqlConexao.Open();
            MySqlCommand cmd = new MySqlCommand(sqlComando, sqlConexao);

            //
            if (Convert.ToInt32(cmd.ExecuteScalar()) >= 1)
                resultado = true;

            sqlConexao.Close();

            return resultado;
        }
        public bool Cls()
        {
            bool resultado = false;
            string command = "truncate table carrinho";

            MySqlConnection con = GetConexao();
            con.Open();
            MySqlCommand cmd = new MySqlCommand(command, con);

            if (cmd.ExecuteNonQuery() >= 1)
                resultado = true;

            con.Close();


            return resultado;
        }
    }
}
