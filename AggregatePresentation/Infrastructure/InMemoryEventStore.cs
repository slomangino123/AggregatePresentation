using CQRSlite.Domain;
using CQRSlite.Domain.Exception;
using CQRSlite.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AggregatePresentation.Infrastructure
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly IDictionary<Guid, ICollection<IEvent>> eventStore = new Dictionary<Guid, ICollection<IEvent>>();

        public Task<IEnumerable<IEvent>> Get(Guid aggregateId, int fromVersion, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (!eventStore.TryGetValue(aggregateId, out var events)){
                return Task.FromResult((IEnumerable<IEvent>)new List<IEvent>());
            }

            return Task.FromResult((IEnumerable<IEvent>)events.Where(x => x.Version > fromVersion).OrderBy(x => x.Version).ToArray());
        }

        public Task Save(IEnumerable<IEvent> events, CancellationToken cancellationToken = default(CancellationToken))
        {
            var aggId = events.First().Id;

            if (!eventStore.TryGetValue(aggId, out var aggregateEvents))
            {
                eventStore[aggId] = new List<IEvent>();
            }
            
            foreach(var @event in events)
            {
                eventStore[aggId].Add(@event);
            }

            return Task.CompletedTask;
        }
    }
}
