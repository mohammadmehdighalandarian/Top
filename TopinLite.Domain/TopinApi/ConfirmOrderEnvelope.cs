using System;
using System.Collections.Generic;
using System.Text;

namespace TopinLite.Domain.TopinApi
{
    public class ConfirmOrderEnvelope
    {
        public ConfirmOrderRequestModel Request { get; set; }
        public string ClientIp { get; set; }
        public string TraceId { get; set; }
        public DateTime RequestedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
