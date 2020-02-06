using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using ULA.AddinsHost.Business;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.Business.Device.Context;
using ULA.AddinsHost.Business.Device.DataTables;
using ULA.AddinsHost.Business.Device.SchemeTable;
using ULA.AddinsHost.Business.Device.SchemeTable.CustomCollections;
using ULA.AddinsHost.Business.Device.SchemeTable.CustomTableItems;
using ULA.Business.Exceptions;
using ULA.Business.Infrastructure.PersistanceServices;
using ULA.Common.AOM;
using ULA.Common.Formatters;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Business.PersistanceServices
{
    /// <summary>
    ///     Represents xml-based deviceViewModel persisting context functionality
    ///  ласс-сервис дл€ работы c xml контекстом устройств
    /// </summary>
    internal class LogicalDeviceXmlPersistableContext : Disposable, IDevicePersistableContext
    {
        #region [Const]

        private const string STATE_ELEMENT_NAME = "state";

        #endregion

        #region [Private fields]

        private XElement _root;
        private Func<Task> _saveRootAsync;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="LogicalDeviceXmlPersistableContext" />
        /// </summary>
        /// <param name="root">
        ///     An instance of <see cref="XElement" /> that represents current deviceViewModel xml node
        /// </param>
        /// <param name="saveRootAsync">
        ///     An instance of <see cref="Func{TResult}" /> that represents asynchronous save template method
        /// </param>
        public LogicalDeviceXmlPersistableContext(XElement root, Func<Task> saveRootAsync)
        {
            this._root = root;
            this._saveRootAsync = saveRootAsync;
        }

        #endregion [Ctor's]

        #region [IDevicePersistableContext members]

        /// <summary>
        ///     Saves deviceViewModel momento asynchronously
        /// ћетод сохран€ет дынные (о) устройство в xml
        /// </summary>
        /// <param name="deviceMomento">
        ///     An instance of <see cref="IDeviceMomento" /> that represents deviceViewModel momento
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current synchronous operation</returns>
        public async void SaveDeviceMomentoAsync(IDeviceMomento deviceMomento)
        {
            this.ThrowIfDisposed();
            await Task.Factory.StartNew(() => this.SaveMomentoInternal(deviceMomento), TaskCreationOptions.LongRunning);
            await this._saveRootAsync();
        }


        /// <summary>
        ///     Gets current deviceViewModel momento asynchronously
        /// ¬ернет данные о устройстве из xml
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current synchronous operation</returns>
        public Task<IDeviceMomento> GetMomentoAsync()
        {
            return Task.Factory.StartNew(this.GetDeviceMomentoInternal, TaskCreationOptions.LongRunning);
        }

        #endregion [IDevicePersistableContext members]

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

        //¬ернет моменто устройства из xml
        private IDeviceMomento GetDeviceMomentoInternal()
        {
            var contentElement = this._root.Element(STATE_ELEMENT_NAME);
            if (contentElement == null) throw new PersistanceServiceFileFormatException();
            var typeAttribute = contentElement.Attribute("type");
            if (typeAttribute == null) throw new PersistanceServiceFileFormatException();
            var type = Type.GetType(typeAttribute.Value);
            if (type == null) throw new PersistanceServiceFileFormatException();


            var serializer = new DataContractSerializer(type, this.GetKnownTypesInternal());
            using (var reader = contentElement.Elements().First().CreateReader())
            {
                var result = serializer.ReadObject(reader);
                return result as IDeviceMomento;
            }
        }

        //—охран€ет состоние(моменто) устройства в xml
        private void SaveMomentoInternal(IDeviceMomento deviceMomento)
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
                var stateElement = this._root.Element(STATE_ELEMENT_NAME);
                if (stateElement == null)
                {
                    this._root.Add(stateElement = new XElement(STATE_ELEMENT_NAME));
                }
                stateElement.RemoveAttributes();
                stateElement.RemoveAll();
                // ReSharper disable AssignNullToNotNullAttribute
                stateElement.Add(new XAttribute("type", type.AssemblyQualifiedName));
                // ReSharper restore AssignNullToNotNullAttribute
                stateElement.Add(element);
            }
        }

        //«десь возвращаютс€ типы которые учавствую в (де)сериализации в(из) xml
        private IEnumerable<Type> GetKnownTypesInternal()
        {
            /*TODO: quick work around. Should be refactored in the future!*/
            return new List<Type>
            {
                typeof (AomEntity),
                typeof (AomDeviceContextEntity),
                typeof (DataContentContainer),
                typeof (DefaultTagNamedObjectCollection<IDeviceDataTableRow>),
                typeof (CustomFider),
                typeof (CustomIndicator),
                typeof (CustomSignal),
                typeof (CascadeIndicatorCollection),
                typeof (CascadeIndicator),

                typeof (CustomFidersCollection),
                typeof (CustomIndicatorsCollections),
                typeof (CustomSignalsCollection),
                typeof (CustomDeviceSchemeTable),
                typeof (DefaultDataRow),
                typeof (DefaultSchemeTable),
                typeof (DefaultResistorRow),
                typeof (DefaultStarterRow),
                typeof (BytesToByteFormatter),
                typeof (BytesToBooleanFormatter),
                typeof (BytesToInt16Formatter),
                typeof (BytesToInt16FormatterForPicon2),
                typeof (BytesToIntVoltageFormatter),
                typeof (BytesToIntCurrentFormatter),
                typeof (BytesToIntEnergyFormatter),
                typeof (BytesToIntEnergyFormatterForPicon2),
                typeof (BytesToIntPowerFormatter),
                typeof (BytesToFailAckFormatter),
                typeof (StringFormatter),
typeof (BytesToStringFormatter),
            };
        }

        #endregion [Help members]
    }
}