using System;
using System.Reflection;

namespace ULA.Presentation.Infrastructure.Services
{
    /// <summary>
    ///     Exposes global resources service
    /// </summary>
    public interface IResourcesService
    {
        /// <summary>
        ///     Injects a resource into a system
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sourceAssembly"></param>
        void AddResourceAsGlobal(string filePath, Assembly sourceAssembly);
    }
}