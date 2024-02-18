using MvcCineAdoNet.Models;

namespace MvcCineAdoNet.Repositories
{
    public interface IRepositoryCine
    {
        List<Medio> GetPeliculas();
        List<Medio> GetSeries();
        Medio FindMedio(int idMedio);
        void DeleteMedio(int idMedio);
        void InsertMedio(int idTipoMedio, string titulo, 
            string director, int anioEstreno, string clasificacionEdad,
            string sinopsis, int duracionmins, int puntuacionmedia, 
            string estado, string imagen, int idGenero);
        //void UpdateMedio();
        List<Genero> GetGeneros();
        void DeleteGenero(int idGenero);
        //void InsertGenero(); Tengo que poner aqui todos los parametros
        //void UpdateGenero();
        List<Actor> GetActores();
        void DeleteActor(int idActor);
        //void InsertActor(); Tengo que poner aqui todos los parametros
        //void UpdateActor();
        List<Personaje> GetPersonajes();
        void DeletePersonaje(int idPersonaje);
        //void InsertPersonaje(); Tengo que poner aqui todos los parametros
        //void UpdatePersonaje();
        List<TipoMedio> GetTiposMedio();
    }
}
