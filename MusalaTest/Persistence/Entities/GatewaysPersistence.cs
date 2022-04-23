using System;
using System.Collections;
using System.Collections.Generic;

namespace MusalaTest.Persistence.Entities
{
    public class GatewaysPersistence
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Ip { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public virtual IEnumerable<PeripheralDevicePersistence> PeripheralDevices { get; set; }
    }
}