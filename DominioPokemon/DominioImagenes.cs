
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
        public string Nombre { get; set; }
        public string ArchivoImagen { get; set; }
        public DominioImagenes(string nombre,string archivoImagen )
        {            
            this.Nombre = nombre;
            this.ArchivoImagen = archivoImagen;
            Validar();
        }


        private void Validar() {
            if (string.IsNullOrEmpty(Nombre)) {
                throw new Exception("El nombre para la imagen puede estar vació");
            }
            if (Nombre.Length >= 100)
            {
                throw new Exception($"El nombre de la imagen supera 100 letras");
            }
        }
    }
}
