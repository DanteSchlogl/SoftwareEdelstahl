using Edelstahl.DAL.Implementations.Memory;
using Edelstahl.DAL.Interfaces;

namespace Edelstahl.DAL.Factory
{
    /// <summary>
    /// Centraliza las instancias de los repositorios
    /// utilizados por Edelstahl.
    /// </summary>
    public static class FactoryDataAccess
    {
        public static IClienteRepository ClienteRepository { get; }

        public static IPresupuestoRepository
            PresupuestoRepository
        { get; }

        static FactoryDataAccess()
        {
            ClienteRepository =
                new ClienteRepositoryMemory();

            PresupuestoRepository =
                new PresupuestoRepositoryMemory();
        }
    }
}