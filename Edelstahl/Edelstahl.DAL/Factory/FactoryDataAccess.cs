using Edelstahl.DAL.Implementations.Memory;
using Edelstahl.DAL.Interfaces;

namespace Edelstahl.DAL.Factory
{
    /// <summary>
    /// Centraliza la creación de los repositorios utilizados
    /// por la aplicación Edelstahl.
    /// </summary>
    public static class FactoryDataAccess
    {
        public static IClienteRepository ClienteRepository { get; }

        static FactoryDataAccess()
        {
            ClienteRepository = new ClienteRepositoryMemory();
        }
    }
}