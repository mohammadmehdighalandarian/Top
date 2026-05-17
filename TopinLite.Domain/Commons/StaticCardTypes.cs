using TopinLite.Domain.TopinDatabaseModels;

namespace TopinLite.Domain.Commons;

public static class StaticCardTypes
{
    public static readonly IReadOnlyList<CardTypeModel> Items = new List<CardTypeModel>
    {
        new() { CardTypeId = 1, CardTypeTitle = "گذرنامه" },
        new() { CardTypeId = 2, CardTypeTitle = "کارت آمايش" },
        new() { CardTypeId = 3, CardTypeTitle = "کارت پناهندگی" },
        new() { CardTypeId = 4, CardTypeTitle = "کارت هويت" },
        new() { CardTypeId = 5, CardTypeTitle = "شرکت حقوقی ايرانی" },
        new() { CardTypeId = 6, CardTypeTitle = "شرکت حقوقی خارجی" },
        new() { CardTypeId = 7, CardTypeTitle = "اطلاعات حساب بانکی" },
        new() { CardTypeId = 8, CardTypeTitle = "اطلاعات کارت بانکی" },
        new() { CardTypeId = 9, CardTypeTitle = "شماره تلفن" },
        new() { CardTypeId = 0, CardTypeTitle = "کارت ملی" },
        new() { CardTypeId = -1, CardTypeTitle = "خرید اینترنتی" }
    };
}