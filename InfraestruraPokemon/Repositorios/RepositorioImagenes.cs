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
        void GuardarImagen(DominioImagenes imagenes,int idPokemonGuardado);

    }
    public class RepositorioImagenes : IRepositorioImagenes
    {
        private readonly ContextoPokemon contextoPokemon;
        public RepositorioImagenes(ContextoPokemon contextoPokemon)
        {
            this.contextoPokemon = contextoPokemon;
        }

        

        //TODO: Se realizan dos acciones es necesario modificarlo ok finalizado pendiente revicion

        private byte[] ConvertirDeBase64Aimagen(string base64String) {
            int inicioBase64 = base64String.IndexOf(",", 0)+1;
            string imagenBase64 =base64String.Substring(inicioBase64);
           
            return Convert.FromBase64String(imagenBase64);
        }
        private string GuardarImagenEnRuta(byte[] imagenBytes,string nombre)
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

        private Imagenes ConvertirDominioAPersistencia(DominioImagenes imagenes, int idPokemonGuardado) {

            return new Imagenes
            {
                IdPokemon = idPokemonGuardado,
                Nombre = imagenes.nombre,
                ArchivoImagen = imagenes.archivoImagen,
                RutaImagen = GuardarImagenEnRuta(ConvertirDeBase64Aimagen(imagenes.archivoImagen),imagenes.nombre),                
            };
        }
        

        public void GuardarImagen(DominioImagenes imagenes, int idPokemonGuardado)
        {
            contextoPokemon.Imagenes.Add(ConvertirDominioAPersistencia(imagenes, idPokemonGuardado));
            contextoPokemon.SaveChanges();
        }
    }
}
