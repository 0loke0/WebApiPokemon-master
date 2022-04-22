using DTOsPokemon.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsPokemon.DTOS
{
    public class DTORelacionPokemonTipo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdTipo { get; set; }
        public string NombreTipo { get; set; }
    }

    
        public class DTOPokemon
    {
        
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Ataque { get; set; }
        public int Defensa { get; set; }
        public int EspecialAtaque { get; set; }
        public int EspecialDefensa { get; set; }
        public int Velocidad { get; set; }
        public int Vida { get; set; }
        public string NombreImagen { get; set; }
        public string RutaImagen { get; set; }
        public string Rareza { get; set; }
        public string Detalle { get; set; }

    }
    public class DTOTipo
    {
        public int? IdTemporalPokemon { get; set; }
        public int IdTipo { get; set; }
        public string NombreTipo { get; set; }
    }

    public class DTOStats
    {
        public int? IdPokemon { get; set; }
        public int Ataque { get; set; }
        public int Defensa { get; set; }
        public int EspecialAtaque { get; set; }
        public int EspecialDefensa { get; set; }
        public int Velocidad { get; set; }
        public int Vida { get; set; }
    }

    public class DTODirectorioMovimientos
    {
        public int IdPokemon { get; set; }
        public int IdMovimiento { get; set; }
    }

    public class DTODirectorioTipos
    {
        public int IdPokemon { get; set; }
        public int IdTipo { get; set; }
    }
    
    public class DTOMovimiento
    {
        //todo: quitar IdTemporalPokemon cuando la consulta en el repositorio pokemon por sp ya este terminada
        public int? IdTemporalPokemon { get; set; }
        public int? IdMovimiento { get; set; }
        public string NombreMovimiento { get; set; }
        public int Valor { get; set; }
    }
    public class DTONuevoPokemonOriginal
    {
        public string NombrePokemon { get; set; }
        public int IdTipo { get; set; }        //lista para el directorio


    }
    public class DTONuevoPokemon
    {
        public string NombrePokemon { get; set; }
        public List<int> IdsTipo { get; set; }        //lista para el directorio
        public List<int> IdsMovimiento { get; set; }
        public DTOIngresoImagen Imagen { get; set; }  
        public string Detalle { get; set; }

    }

    public class DTOModificacionAPokemon
    {
        public int Id { get; set; }
        public string NombrePokemon { get; set; }
        public List<int> IdsTipo { get; set; }        
        public List<int> IdsMovimiento { get; set; }
        public string Detalle { get; set; }

    }



    public class DTODetallePokemon {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Ataque { get; set; }
        public int Defensa { get; set; }
        public int EspecialAtaque { get; set; }
        public int EspecialDefensa { get; set; }
        public int Velocidad { get; set; }
        public int Vida { get; set; }
        public string NombreImagen { get; set; }      
        public string RutaImagen { get; set; }
        public string Rareza { get; set; }
        public List<DTOMovimiento> Movimientos { get; set; }
        public List<DTOTipo> Tipos { get; set; }
        public string Detalle { get; set; }

    }

    public class DTOPokemonSP
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Ataque { get; set; }
        public int Defensa { get; set; }
        public int EspecialAtaque { get; set; }
        public int EspecialDefensa { get; set; }
        public int Velocidad { get; set; }
        public int Vida { get; set; }
        public string NombreImagen { get; set; }
        public string RutaImagen { get; set; }
        public string Rareza { get; set; }       
        public string Detalle { get; set; }

    }

    public class DTOMovimientoSP
    {
        public int IdPokemon { get; set; }
        public string Movimientos { get; set; }
      
    }

    public class DTOTipoSP
    {
        public int IdPokemon { get; set; }
        public string NombreTipo { get; set; }
    }




    public class DTOPaginacion {
        public int Indice { get; set; }
        public int CantidadRegistros { get; set; }
    }




}
