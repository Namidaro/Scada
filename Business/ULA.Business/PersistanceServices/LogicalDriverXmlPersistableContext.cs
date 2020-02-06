using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using ULA.AddinsHost.Business;
using ULA.AddinsHost.Business.Driver;
using ULA.AddinsHost.Business.Driver.Context;
using ULA.AddinsHost.Business.Driver.DataTables;
using ULA.Business.Exceptions;
using ULA.Business.Infrastructure.PersistanceServices;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Business.PersistanceServices
{
    /// <summary>
    ///     Represents xml-based driver persisting context functionality
    /// То же, что и <see cref="LogicalDeviceXmlPersistableContext"/> только для драйвера
    /// </summary>
    internal class LogicalDriverXmlPersistableContext : Disposable, IDriverPersistableContext
    {
        #region [Const]

        private const string XELEMENT_NAME = "state";

        #endregion

        #region [Private fields]

        private XElement _root;
        private Func<Task> _saveRootAsync;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="LogicalDriverXmlPersistableContext" />
        /// </summary>
        /// <param name="root">
        ///     An instance of <see cref="XElement" /> that represents current context root
        /// </param>
        /// <param name="saveRootAsync">
        ///     An instance of <see cref="Func{TResult}" /> that represents asynchronous save template method
        /// </param>
        public LogicalDriverXmlPersistableContext(XElement root, Func<Task> saveRootAsync)
        {
            this._root = root;
            this._saveRootAsync = saveRootAsync;
        }

        #endregion [Ctor's]

        #region [IDriverPersistableContext members]

        /// <summary>
        ///     Saves an instance of <see cref="IDriverMomento" /> that represents drivers state asynchronously
        /// </summary>
        /// <param name="driverMomento">
        ///     An instance of <see cref="IDriverMomento" /> to save
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        public async Task SaveDriverMomentoAsync(IDriverMomento driverMomento)
        {
            this.ThrowIfDisposed();
            await this.SaveContentAsync(driverMomento);
            await this._saveRootAsync();
        }

        /// <summary>
        ///     Restores an instance of <see cref="IDriverMomento" /> that reporesents drivber's state
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        public Task<IDriverMomento> GetDriverMomentoAsync()
        {
            return Task.Factory.StartNew(() => this.GetDriverMomentoInternal(), TaskCreationOptions.LongRunning);
        }

        #endregion [IDriverPersistableContext members]

        #region [Override members]

        /// <summary>
        ///     Does actual explicit disposal of available managed resources
        /// </summary>
        protected override void OnDisposing()
        {
            this._root = null;
            this._saveRootAsync = null;
            base.OnDisposing();
        }

        #endregion [Override members]

        #region [Help members]

        private Task SaveContentAsync(IDriverMomento deviceMomento)
        {
            return Task.Factory.StartNew(() => SaveContent(deviceMomento), TaskCreationOptions.LongRunning);
        }

        private void SaveContent(IDriverMomento deviceMomento)
        {
            this.ThrowIfDisposed();
            var type = deviceMomento.GetType();
            var serializer = new DataContractSerializer(type, this.GetKnownTypesInternal());
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, (object)deviceMomento);
                stream.Position = 0;
                XElement element;
                using (var reader = XmlReader.Create(stream))
                {
                    element = XElement.Load(reader);
                }
                var stateElement = this._root.Element(XELEMENT_NAME);
                if (stateElement == null)
                {
                    this._root.Add(stateElement = new XElement(XELEMENT_NAME));
                }
                stateElement.RemoveAttributes();
                stateElement.RemoveAll();
                // ReSharper disable AssignNullToNotNullAttribute
                stateElement.Add(new XAttribute("type", type.AssemblyQualifiedName));
                // ReSharper restore AssignNullToNotNullAttribute
                stateElement.Add(element);
            }
        }

        private IDriverMomento GetDriverMomentoInternal()
        {
            this.ThrowIfDisposed();

            var contentElement = this._root.Element(XELEMENT_NAME);
            if (contentElement == null) throw new PersistanceServiceFileFormatException();
            var typeAttribute = contentElement.Attribute("type");
            if (typeAttribute == null) throw new PersistanceServiceFileFormatException();
            var type = Type.GetType(typeAttribute.Value);
            if (type == null) throw new PersistanceServiceFileFormatException();


            var serializer = new DataContractSerializer(type, this.GetKnownTypesInternal());
            using (var reader = contentElement.FirstNode.CreateReader())
            {
                var result = serializer.ReadObject(reader);
                return result as IDriverMomento;
            }
        }

        private IEnumerable<Type> GetKnownTypesInternal()
        {
            /*TODO: quick work around. Should be refactored in the future!*/
            return new List<Type> { typeof(AomTcpDriverContextEntity), typeof(DataContentContainer), typeof(DriverDataTable) };
        }

        #endregion [Help members]
    }
}