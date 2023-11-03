using Modelo;
using MySql.Data.MySqlClient;
using System.Data;

namespace Controle
{
    public class ControleProduto
    {
        Conexao con = new Conexao();
        public bool CadastrarCart(int produto)
        {
            bool resultado = false;
            DataTable cart = con.ObterDados("select *from carrinho");
            if (cart.Rows.Count < 7)
            {
                string sqlComando = "INSERT INTO carrinho (cod_prod) VALUES ('" + produto + "')";

                MySqlConnection sqlConexao = con.GetConexao();
                sqlConexao.Open();
                MySqlCommand cmd = new MySqlCommand(sqlComando, sqlConexao);

                if (cmd.ExecuteNonQuery() >= 1)
                    resultado = true;

                sqlConexao.Close();
            }

            return resultado;
        }
        public bool RemoverCart(int produto)
        {
            bool resultado = false;
            string sqlComando = "DELETE FROM `carrinho` WHERE `carrinho`.`cod_prod` = " + produto;

            MySqlConnection sqlConexao = con.GetConexao();
            sqlConexao.Open();
            MySqlCommand cmd = new MySqlCommand(sqlComando, sqlConexao);

            //
            if (cmd.ExecuteNonQuery() >= 1)
                resultado = true;

            sqlConexao.Close();


            return resultado;
        }
        public bool CadastrarPedido(PedidoModelo pedido)
        {
            bool resultado = false;

            string sqlComando = $"insert into pedidos (Nome, idusuario, cep, bairro, endereco, num, tel, status, pedido, obs) values ('" +
                $"{pedido.Nome}','{pedido.idusuario}','{pedido.cep}','{pedido.bairro}','{pedido.end}','{pedido.num}','{pedido.tel}','{pedido.status}','{pedido.json}', '{pedido.obs}')";

            MySqlConnection sqlConexao = con.GetConexao();
            sqlConexao.Open();
            MySqlCommand cmd = new MySqlCommand(sqlComando, sqlConexao);

            if (cmd.ExecuteNonQuery() >= 1)
                resultado = true;

            sqlConexao.Close();

            return resultado;
        }

        public string EditarStatus(int cod, int op)
        {
            string msg = null;

            switch (op)
            {
                case 1:
                    msg = "Pedido Aceito, Estamos Preparando seu Pedido!";
                    break;
                case 2:
                    msg = "Pedido Recusado";
                    break;
                case 3:
                    msg = "Pedido Finalizado";
                    break;
            }

            string sqlComando = $"Update pedidos set status = '{msg}' where id_pedido = {cod}";

            MySqlConnection sqlConexao = con.GetConexao();
            sqlConexao.Open();
            MySqlCommand cmd = new MySqlCommand(sqlComando, sqlConexao);

            cmd.ExecuteNonQuery();

            sqlConexao.Close();

            return msg;
        }

    }
}
