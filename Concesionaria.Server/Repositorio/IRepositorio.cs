﻿using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio
{
    public interface IRepositorio<E> where E : class, IEntityBase                    
    {       
        Task<bool> Delete(int id);
        Task<bool> Existe(int id);
        Task<int> Insert(E entidad);
        Task<List<E>> Select();
        Task<E> SelectById(int id);
        Task<bool> Update(int id, E entidad);
        Task<E> SelectByCod(string codigo);
		Task<bool> ExisteByCodigo(string codigo);
		Task<string> InsertDevuelveCodigo(E entidad);

    }
}