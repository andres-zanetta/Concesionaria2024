﻿using Concesionaria.DB.Data.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Concesionaria.Server.Repositorio.FacundoRepositorios
{
    public interface IAdjudicacionRepositorio
    {
        Task<PlanVendido> SelectByCodigo(string codigo);
        Task<List<Adjudicacion>> SelectEntidadConVehiculo();
        Task<Adjudicacion> SelectEntidadConVehiculoById(int id);
    }
}

