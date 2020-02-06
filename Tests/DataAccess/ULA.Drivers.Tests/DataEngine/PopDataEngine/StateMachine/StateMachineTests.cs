using NUnit.Framework;
using ULA.Drivers.DataEngines;
using ULA.Drivers.DataEngines.PopDataEngine;
using ULA.Drivers.DataEngines.PopDataEngine.Processor;
using ULA.Drivers.StateMachine.DataEngineStateMachine;
using ULA.Drivers.StateMachine.DataEngineStateMachine.DataEngineState.DataEngineStateImplementation;
using YP.Toolkit.System.Tools.Patterns.EventAggregator;

namespace ULA.Drivers.Tests.DataEngine.PopDataEngine.StateMachine
{
    [TestFixture]
    public class StateMachineTests
    {
        [Test]
        public void CreateStateMachineHost()
        {
            IDataEngine dataEngine = new PopDataEngineBase(new DriverDataAccessor(new ModbusDataDriver("10.12.89.117",4444)),"ip",4444 );
            DataEngineHost host = new DataEngineHost(dataEngine,
                new DefaultDataEngineStateMachine(new DataEngineStarting(dataEngine)), new EventAggregator());
            host.ChangeState(new DataEngineStarting(dataEngine));
        }
    }
}
