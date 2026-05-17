using TopinLite.Domain.TopinApi;

namespace TopinLite.Domain.Commons;

public static class StaticChannels
{
    public static readonly IReadOnlyList<ChannelIdResponseModel> Items = new List<ChannelIdResponseModel>
    {
        new() { PkChannelId = 1, ChannelId = 11, ChannelDesc = "USSD" },
        new() { PkChannelId = 2, ChannelId = 12, ChannelDesc = "PORTAL" },
        new() { PkChannelId = 3, ChannelId = 13, ChannelDesc = "IVR" },
        new() { PkChannelId = 4, ChannelId = 14, ChannelDesc = "ATM" },
        new() { PkChannelId = 5, ChannelId = 15, ChannelDesc = "POS" },
        new() { PkChannelId = 6, ChannelId = 16, ChannelDesc = "APP" },
        new() { PkChannelId = 7, ChannelId = 17, ChannelDesc = "CRM" },
        new() { PkChannelId = 8, ChannelId = 781, ChannelDesc = "Dealer" },
        new() { PkChannelId = 9, ChannelId = 18, ChannelDesc = "AUTO_RENEWAL" }
    };
}