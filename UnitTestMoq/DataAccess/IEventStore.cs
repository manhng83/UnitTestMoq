using System;
using System.Collections.Generic;
using UnitTestMoq.Domain.Base;

namespace UnitTestMoq.DataAccess
{
    public interface IEventStore
    {
        void SaveEvents(Guid aggregateId, IEnumerable<Event> events, int expectedVersion);

        List<Event> GetEventsForAggregate(Guid aggregateId);
    }
}