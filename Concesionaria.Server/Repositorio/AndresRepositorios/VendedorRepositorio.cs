﻿using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Repositorio.AndresRepositorios
{
    public class VendedorRepositorio : Repositorio<Vendedor>, IVendedorRepositorio
    {
        private readonly Context context;
        public VendedorRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

		public async Task<Vendedor> SelectByDNI(string numDoc)
		{
			try
			{
				var vendedor = await context.Vendedores.Include(V => V.Persona).FirstOrDefaultAsync(V => V.Persona.NumDoc == numDoc);
				return vendedor;
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error al obtener los registros:  {e.Message}");
				throw e;
			}
		}

		public async Task<Vendedor> SelectByCodConPersona(string codigo)
		{
			try
			{
				var vendedor = await context.Vendedores.Include(V => V.Persona).FirstOrDefaultAsync(V => V.Codigo == codigo);
				return vendedor;
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error al obtener los registros:  {e.Message}");
				throw e;
			}
		}

		public async Task<List<Vendedor>> SelectConPersona()
		{
			try
			{
				var vendedor = await context.Vendedores.Include(V => V.Persona).ToListAsync();
				return vendedor;
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error al obtener los registros:  {e.Message}");
				throw e;
			}
		}




		public async Task<Vendedor?> SelectByFechaInicio(DateTime fechaInicio)
        {
            Vendedor? V = await context.Vendedores.
                      FirstOrDefaultAsync(x => x.FechaInicio== fechaInicio);
           return V;
        }

        public async Task<Vendedor?> SelectByFechaFin(DateTime fechaFin)
        {
            Vendedor? V = await context.Vendedores.
                      FirstOrDefaultAsync(x => x.FechaFin == fechaFin);
            return V;
        }

        //agrego esto.
        public async Task<Vendedor?> SelectByCantPlanesVendidos(int cantPlanesVendidos)
        {
            Vendedor? V = await context.Vendedores.
                        FirstOrDefaultAsync(x => x.CantPlanesVendidos == cantPlanesVendidos);
            return V;
        }
    }
}
