using InfraestructuraPokemon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOsPokemon.DTOs;
using InfraestructuraPokemon.Modelos;
using DominioPokemon;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.IO;

namespace InfraestructuraPokemon.Repositorios
{

   public interface IRepositorioImagenes {
        //void AgregarImagen(DTOImagen DtoTipo);
        //DTOImagen ConvertirDominioADtoGuardandoImagenEnLocal(DominioImagenes imagenes);

    }
    public class RepositorioImagenes : IRepositorioImagenes
    {
        private readonly ContextoPokemon contextoPokemon;
        public RepositorioImagenes(ContextoPokemon contextoPokemon)
        {
            this.contextoPokemon = contextoPokemon;
        }

        

        //TODO: Se realizan dos acciones es necesario modificarlo ok finalizado pendiente revicion

        
        private string GuardarImagenEnLocal(byte[] imagenBytes,string nombre)
        {
            string directorioDeGuardado = @"C:\nueva";
            string ruta = directorioDeGuardado + @"\" + nombre;            
            if (!Directory.Exists(directorioDeGuardado))
            {
                Directory.CreateDirectory(directorioDeGuardado);
            }            
            using (var imageFile = new FileStream(ruta, FileMode.Create))
            {
                imageFile.Write(imagenBytes, 0, imagenBytes.Length);
                imageFile.Flush();
            }
            return ruta;
        }



        //NO me agrada las acciones internas del nombre guarda ruta o solo hace una convercion
        
    }
}
