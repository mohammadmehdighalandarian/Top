namespace TopinLite.Domain.Commons
{
    public readonly record struct ResultCode(decimal Code, string Message)
    {
        public static implicit operator decimal(ResultCode rc) => rc.Code;

        public static implicit operator string(ResultCode rc) => rc.Message;

        public override string ToString() => $"{Code}: {Message}";
    }

    public static class ResultCodes
    {
        public static readonly ResultCode Success = new(0, "عملیات با موفقیت انجام شد.");
        public static readonly ResultCode SystemError = new(7, "خطاي سيستمی.");

        public static readonly ResultCode AlreadySucceeded = new(1, "عمليات قبلا با موفقيت انجام شده است.");
        public static readonly ResultCode BadInput = new(19, "پارامتر ورودی اشتباه است.");
        public static readonly ResultCode TaliyaError = new(-1053, "امکان شارژ مشترکين تاليا از اين درگاه وجود ندارد.");
        public static readonly ResultCode TelNumEmpty = new(-1114, "شماره تلفن درخواست دهنده خالی می باشد");
        public static readonly ResultCode ChargeDynamicConditions = new(-1166, "");
        public static readonly ResultCode ChannelNotFound = new(-1149, "");

        public static readonly ResultCode BrokerNotFound = new(-1001, "کارگزار تعريف نشده است.");
        public static readonly ResultCode BrokerInactive = new(-1002, "کارگزار فعال نمی باشد.");
        public static readonly ResultCode BrokerHasNoChargeAccess = new(-1018, "کارگزار مجوز فروش اين نوع شارژ را ندارد.");
        public static readonly ResultCode BrokerHasNoPackageAccess = new(-1032, "کارگزار مجوز فروش اين نوع بسته را ندارد.");
        public static readonly ResultCode BrokerHasNoProductAccess = new(-1020, "کارگزار مجوز فروش اين محصول را ندارد.");
        public static readonly ResultCode BrokerHasNoPackagePermission = new(-1116, "کارگزار مجاز به ارايه بسته نمی باشد.");

        public static readonly ResultCode OfferInactive = new(-1021, "نوع شارژ صحيح نيست.");
        public static readonly ResultCode OfferProductMismatch = new(-1095, "متد فراخوانی شده با محصول يکی نيست.");
        //TODO what is offer Not found code
        public static readonly ResultCode OfferNotFound = new(1234, "آفر تعریف نشده است.");


        public static readonly ResultCode MinAmountCharge = new(-1112, "حداقل مبلغ خريد شارژ 2100 تومان می باشد.");
        public static readonly ResultCode InvalidChargeAmount = new(-1003, "مبلغ کارت شارژ ارسالي صحيح نمي باشد.");
        public static readonly ResultCode OfferAmountMismatch = new(-1078, "مبلغ شارژ با نوع شارژ درخواستی همخوانی ندارد.");
        public static readonly ResultCode WllRestriction = new(-1079, "امکان خريد اين نوع شارژ برای مشترکين روستايی نمی باشد.");
        public static readonly ResultCode ProcessHaltedDueToSpecificCut = new(-1042, "امکان ادامه فرآيند به دليل قطع خاص وجود ندارد.");
        public static readonly ResultCode PhoneNumberIsBlacklisted = new(-1012, "تلفن در ليست سياه می باشد.");
        public static readonly ResultCode ProcessHaltedDueToMissingStatus = new(-1043, "امکان ادامه فرآيند به دليل وضعيت مفقودی وجود ندارد.");
        public static readonly ResultCode ProcessHaltedDueToPortability = new(-1070, "ادامه فرآيند به دليل ترابرد امکان پذير نمی باشد.");
        public static readonly ResultCode SimCardNotEligibleForPurchase = new(-1100, "سيم کارت مشمول خريد نمی باشد.");
        public static readonly ResultCode NoChargeForPermanentSim = new(-1035, "اعمال شارژ برای مشترکين دائمی امکان پذير نمی باشد.");
        public static readonly ResultCode ProcessHaltDueToSubscriberInactivity = new(-1047, "امکان ادامه فرآيند به دليل فعال نبودن وضعيت مشترک وجود ندارد.");
        public static readonly ResultCode ProcessHaltDueToSubscriberEvacuation = new(-1048, "امکان ادامه فرآيند به دليل تخليه بودن وضعيت مشترک وجود ندارد.");
        public static readonly ResultCode SubscriberOverCredit = new(-1014, "شارژ كنونی مشترک بيش از سقف تعيين شده است.");
        public static readonly ResultCode YouthChargeAgeLimit = new(-1023, "شارژ جوانان مختص مشترکين زير 25 سال می باشد.");
        public static readonly ResultCode WomanChargeRestriction = new(-1027, "مشترک مشمول طرح هديه شارژ بانوان نمی باشد.");
        public static readonly ResultCode LoyaltyChargeRestriction = new(-1028, "مشترک مشمول شارژ وفاداری نمی باشد.");
        public static readonly ResultCode UniqueOrderNotFound = new(-1085, "کد يکتای سيستم خدمات مشترکين يافت نشد.");
        public static readonly ResultCode OngoingChargeOrder = new(-1017, "با اين شماره تراکنش عمليات شارژ در حال اجرا است.");
        public static readonly ResultCode CardTypeCardNoRestriction = new(-1086, "پارامتر ورودی اشتباه است.مقدار شماره کارت يا نوع کارت نمی تواند خالی باشد.");
        public static readonly ResultCode BankInactive = new(-1089, "بانک فعال نيست.");
        public static readonly ResultCode WrongBankCode = new(-1051, "کد بانک اشتباه است.");
        public static readonly ResultCode BrokerIdMismatch = new(-1084, "کد يکتای سيستم خدمات مشترکين متعلق به اين کارگزار نيست.");
        public static readonly ResultCode MaxTryExceeded = new(-1082, "تعداد تلاش بيش از حد مجاز است.");
        public static readonly ResultCode TimeExceeded = new(-1083, "مدت زمان شناسه سفارش منقضی شده است.");
        public static readonly ResultCode RulesUnavailable = new(-9001, "سرویس قوانین در دسترس نیست.");
        public static readonly ResultCode TypeValidatorUnavailable = new(-9002, "سرویس اعتبارسنجی نوع شارژ در دسترس نیست.");
        public static readonly ResultCode OrderNotFound = new(-9006, "سفارش یافت نشد یا منقضی شده است.");
        public static readonly ResultCode RechargeUnavailable = new(-9007, "سرویس شارژ در دسترس نیست.");
        public static readonly ResultCode PackageActivationInProgress = new(-1096, "با اين شماره تراکنش عمليات فعالسازی بسته در حال اجرا است.");
        public static readonly ResultCode ActivePackageFail = new(-1036, "خطا در فعالسازي بسته: متن خطا");
    }
}