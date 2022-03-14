
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOsPokemon.DTOs;

namespace DominioPokemon
{

    public class DominioImagenes
    {
        public string nombre { get; set; }
        public string archivoImagen { get; set; }
        public DominioImagenes(string nombre,string archivoImagen )
        {
            this.nombre = nombre;
            this.archivoImagen = archivoImagen;
        }


        private void Validar() {
            if (string.IsNullOrEmpty(nombre)) {
                throw new Exception("El nombre para la imagen no no es valido");
            }
        }
    }
}
