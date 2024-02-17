﻿using MvcCineAdoNet.Models;

namespace MvcCineAdoNet.Repositories
{
    public interface IRepositoryCine
    {
        List<Medio> GetPeliculas();
        List<Medio> GetSeries();
        Medio FindMedio();

        void DeleteMedio(int idMedio);
        //void InsertMedio(); Tengo que poner aqui todos los parametros
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
