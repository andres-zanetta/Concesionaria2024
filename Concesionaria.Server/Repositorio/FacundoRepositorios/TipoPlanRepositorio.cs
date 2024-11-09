﻿using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Repositorio.FacundoRepositorios
{
    public class TipoPlanRepositorio : Repositorio<TipoPlan>, ITipoPlanRepositorio
    {
        private readonly Context context;

        public TipoPlanRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<TipoPlan> SelectByNombre(string nombre)
        {
            try
            {
                var tipoPlan = await context.TipoPlanes.FirstOrDefaultAsync(TP => TP.NombrePlan == nombre);
                return tipoPlan;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al traer el registro:  {e.Message}");
                throw e;
            }

        }

        public Task<TipoPlan?> SelectByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
