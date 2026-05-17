namespace TopinLite.Domain.HuaweiApiModel.BC
{
    public class FeeDeductionRequestMsgRequest
    {
        public string TelNum { get; set; }
        public string MessageSequence { get; set; }
        public string ChargeCode { get; set; }
        public string ChargeAmt { get; set; }
        public string CurrencyId { get; set; }

        public string VatTaxChargeCode => "C_VAT_TAX_CHARGE_CODE";

        public string VatTaxChargeAmount { get; set; }

        public string DutyTaxChargeCode => "C_DUTY_TAX_CHARGE_CODE";

        public string DutyTaxChargeAmount { get; set; }

        //public List<Tax> Taxes { get; set; }
    }
}