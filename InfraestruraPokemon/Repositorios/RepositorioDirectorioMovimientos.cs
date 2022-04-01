﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOsPokemon.DTOS;
using InfraestructuraPokemon.Modelos;
using InfraestructuraPokemon;

namespace InfraestructuraPokemon.Repositorios
{
    public interface IRepositorioDirectorioMovimientos
    {
        void GuardarRelacion(List<int> directorioHabilidades, int idPokemonGuardado);
        void ModificacionDirectorioMovimientos(int id, List<int> idsMovimientos);
    }
    public class RepositorioDirectorioMovimientos : IRepositorioDirectorioMovimientos
    {
        private readonly ContextoPokemon contextoPokemon;

        public RepositorioDirectorioMovimientos(ContextoPokemon contextoPokemon)
        {
            this.contextoPokemon = contextoPokemon;
        }       

        private DirectorioMovimientos ConvertirDominioAPersistencia(int idHabilidad, int idPokemonGuardado)
        {
            return new DirectorioMovimientos
            {
                IdPokemon = idPokemonGuardado,
                IdMovimiento = idHabilidad
            };
        }

        private void EliminarRelacionMovimientos(int id)
        {
            var info = contextoPokemon.DirectorioMovimientos.FirstOrDefault(x => x.IdPokemon == id);
            try
            {
                if (info == null)
                {
                    throw new Exception($"No se ha encontrado ninguna relacion de los movimientos asociados con el pokemon ");
                }
                contextoPokemon.DirectorioMovimientos.Remove(info);
                contextoPokemon.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception($"Se ha generado un error al eliminar la relacion de los movimientos asociados con el pokemon : {e}");
            }
        }


        public void GuardarRelacion(List<int> directorioHabilidades, int idPokemonGuardado)
        {

            foreach (var idHabilidad in directorioHabilidades)
            {
                try
                {
                    contextoPokemon.DirectorioMovimientos.Add(ConvertirDominioAPersistencia(idHabilidad,idPokemonGuardado));
                    contextoPokemon.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception($"Error al guardar relacion entre Pokemon y Movimiento", e);
                }
            }
            
        }

        public void ModificacionDirectorioMovimientos(int id, List<int> idsMovimientos)
        {
            var relacionMovimientos = (from x in contextoPokemon.DirectorioMovimientos
                                 where x.IdPokemon == id
                                 select x).ToList();


            //Todo:Necesario mejorar esta relacion 
            if (relacionMovimientos.Count > idsMovimientos.Count || relacionMovimientos.Count < idsMovimientos.Count)
            {
                EliminarRelacionMovimientos(id);
                GuardarRelacion(idsMovimientos, id);
            }

            if (relacionMovimientos.Count == idsMovimientos.Count)
            {
                for (int i = 0; i < idsMovimientos.Count; i++)
                {
                    relacionMovimientos[i].IdMovimiento = idsMovimientos[i];
                }
            }

            contextoPokemon.SaveChanges();
        }
    }
}
