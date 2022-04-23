using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using MusalaTest.Application.Dtos.PeripheralDevices;
using MusalaTest.Domain.Shared;

namespace MusalaTest.Domain.Entities
{
    public class Gateways : BaseEntity
    {
        public Gateways()
        {
        }

        private Gateways(Guid id, string name, string ip, List<PeripheralDevice> devices)
        {
            Id = id;
            Name = name;
            Ip = ip;
            PeripheralDevices = devices;

            Created = DateTime.UtcNow;
            Updated = DateTime.UtcNow;
        }


        public static GatewayWrapper Create(string name, string ip, List<PeripheralDevice> devices)
        {
            // add extra validations
            var isValidIp = IPAddress.TryParse(ip, out var _);

            if (!isValidIp || (devices != null && devices.Count > 10))
                return new GatewayWrapper
                {
                    Value = null,
                    ErrorMessage = !isValidIp ? "Invalid IP address" : "Maximum number of devices is 10",
                    IsSuccess = false
                };

            var gateway = new Gateways(Guid.NewGuid(), name, ip, devices);
            return new GatewayWrapper
            {
                Value = gateway,
                ErrorMessage = null,
                IsSuccess = true
            };
        }

        public void Update(string name, string ip)
        {
            Name = name ?? Name;
            Ip = ip ?? Ip;

            Updated = DateTime.UtcNow;
        }

        public string Name { get; private set; }

        public string Ip { get; private set; }

        public virtual IEnumerable<PeripheralDevice> PeripheralDevices { get; private set; }
    }
}