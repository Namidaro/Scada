

using Prism.Events;

namespace ULA.Presentation.Behaviors.Interactions
{
    /// <summary>
    ///     Represents an interaction request event functionality that will be served by an event aggreation implementation
    ///     ѕредставл€ет событие интерактивного запросв, которое будет ссылатьс€ на реализацию Event Aggregation(јгрегатора событий)
    /// </summary>
    public class InteractionRequestEvent : PubSubEvent<InteractionRequestEventArgs>
    {
    }
}