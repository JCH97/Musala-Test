using System;

namespace MusalaTest.Application.Dtos.Gateways
{
    public class GatewayDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Ip { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}