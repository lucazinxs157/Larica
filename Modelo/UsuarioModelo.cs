using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class UsuarioModelo
    {
        public int id;
        public string nome;
        public string sobrenome;
        public string cpf;
        public string tel;
        public string senha;
        public string email;
        public int idperfil;

        public UsuarioModelo()
        {
            nome = null;
            sobrenome = null;
            cpf = null;
            tel = null;
            senha = null;
            email = null;
            idperfil = 2;
        }
    }
}
