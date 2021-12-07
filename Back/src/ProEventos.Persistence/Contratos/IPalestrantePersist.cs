using System;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface IPalestrantePersist
        {
        //PALESTRANTES
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string Nome, bool includeEventos = false);
        Task<Palestrante[]> GetAllPalestrantesAsync( bool includeEventos = false);
        Task<Palestrante> GetAllPalestranteByIdAsync(int PalestranteId, bool includeEventos = false);

    }
}