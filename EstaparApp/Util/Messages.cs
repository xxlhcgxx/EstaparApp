using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstaparApp.Util
{
    public class Messages
    {
        public const string MARCAEXISTENTE = "Marca já existe !";
        public const string MARCAVINCULADA = "Marca vinculada há modelo(s), portanto não pode ser excluída !";
        public const string CPFEXISTENTE = "CPF já existe no cadastro de Manobrista !";
        public const string CPFINVALIDO = "CPF inválido !";
        public const string MODELOVINCULADO = "Modelo vinculado há registros, portanto não pode ser excluído !";
        public const string MANOBRISTAVINCULADO = "Manobrista vinculado há registros, portanto não pode ser excluído !";
    }
}
