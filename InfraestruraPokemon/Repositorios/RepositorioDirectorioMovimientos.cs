using System;
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
    }
}
