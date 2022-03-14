﻿using DTOsPokemon.DTOs;
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
    }
    public class DTOTipo
    {
        public int IdTipo { get; set; }
        public string NombreTipo { get; set; }
    }

    public class DTOStats
    {
        public int IdPokemon { get; set; }
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
        public int IdMovimiento { get; set; }
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

    }


    public class DTOMovimientoBD {
        public string NombreMovimiento { get;set; }
        public int Valor { get; set;}
    }
    public class DTOTiposBD
    {
        public int IdTipo { get; set; }
        public string NombreTipo { get; set; }
    }

    public class DTOStatsdBD
    {        
        public int Ataque { get; set; }
        public int Defensa { get; set; }
        public int EspecialAtaque { get; set; }
        public int EspecialDefensa { get; set; }
        public int Velocidad { get; set; }
        public int Vida { get; set; }
    }

    public class DTOImagenBD
    {
        public string Nombre { get; set; }
        public string ArchivoImagen { get; set; }
        public string RutaImagen { get; set; }
    }

    public class DTODetallePokemon { 
        public DTOPokemon Pokemon { get; set; }        
        public List<DTOMovimientoBD> Movimientos { get; set; }
        public List<DTOTiposBD> Tipos { get; set; }
        public DTOStatsdBD Stats { get; set; }
        public DTOImagenBD Imagen { get; set; }

    }




}
