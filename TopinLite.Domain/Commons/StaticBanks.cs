using TopinLite.Domain.TopinDatabaseModels;

namespace TopinLite.Domain.Commons;

public static class StaticBanks
{
    public static readonly IReadOnlyList<BankModel> Items = new List<BankModel>
    {
        new() { BankId = 13, BankTitle = "بانک استان", BankStatus = true },
        new() { BankId = 14, BankTitle = "بانک کارآفرین", BankStatus = true },
        new() { BankId = 0, BankTitle = "بانک مرکزی جمهوری اسلامی ایران", BankStatus = true },
        new() { BankId = 1, BankTitle = "بانک ملی ایران", BankStatus = true },
        new() { BankId = 2, BankTitle = "بانک سپه", BankStatus = true },
        new() { BankId = 3, BankTitle = "بانک تجارت", BankStatus = true },
        new() { BankId = 4, BankTitle = "بانک ملت", BankStatus = true },
        new() { BankId = 5, BankTitle = "بانک صادرات ایران", BankStatus = true },
        new() { BankId = 6, BankTitle = "بانک مسکن", BankStatus = true },
        new() { BankId = 7, BankTitle = "بانک کشاورزی", BankStatus = true },
        new() { BankId = 8, BankTitle = "بانک رفاه کارگران", BankStatus = true },
        new() { BankId = 9, BankTitle = "بانک توسعه صادرات ایران", BankStatus = true },
        new() { BankId = 10, BankTitle = "بانک اقتصاد", BankStatus = true },
        new() { BankId = 11, BankTitle = "بانک صنعت و معدن", BankStatus = true },
        new() { BankId = 12, BankTitle = "نامشخص", BankStatus = true },
        new() { BankId = 16, BankTitle = "بانک پارسیان", BankStatus = true },
        new() { BankId = 17, BankTitle = "بانک سامان", BankStatus = true },
        new() { BankId = 18, BankTitle = "ملت", BankStatus = true },
        new() { BankId = 15, BankTitle = "پست بانک", BankStatus = true },
        new() { BankId = 19, BankTitle = "بانک اقتصاد نوین", BankStatus = true },
        new() { BankId = 20, BankTitle = "بانک پاسارگاد", BankStatus = true },
        new() { BankId = 21, BankTitle = "بانک سرمایه", BankStatus = true },
        new() { BankId = 22, BankTitle = "بانک سینا", BankStatus = true },
        new() { BankId = 26, BankTitle = "بانک شهر", BankStatus = true },
        new() { BankId = 27, BankTitle = "بانک حکمت ایرانیان", BankStatus = true },
        new() { BankId = 25, BankTitle = "بانک تات", BankStatus = true },
        new() { BankId = 23, BankTitle = "جیرینگ", BankStatus = true },
        new() { BankId = 24, BankTitle = "بانک انصار", BankStatus = true },
        new() { BankId = 28, BankTitle = "بانک دی", BankStatus = true },
        new() { BankId = 29, BankTitle = "بانک خاورمیانه", BankStatus = true },
    };
}