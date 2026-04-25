using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopinLite.Domain.HuaweiApiModel.CM
{
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
    public partial class changeSubscriberOfferingReqMsg
    {

        private RequestHeader requestHeaderField;

        private ChangeSubscriberOfferingRequest changeSubscriberOfferingRequestField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public RequestHeader requestHeader
        {
            get
            {
                return requestHeaderField;
            }
            set
            {
                requestHeaderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ChangeSubscriberOfferingRequest changeSubscriberOfferingRequest
        {
            get
            {
                return changeSubscriberOfferingRequestField;
            }
            set
            {
                changeSubscriberOfferingRequestField = value;
            }
        }
    }



    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/crmheader")]
    public partial class RequestHeader
    {

        private string versionField;

        private string businessCodeField;

        private string messageSeqField;

        private OwnershipInfo ownershipInfoField;

        private SecurityInfo accessSecurityField;

        private OperatorInfo operatorInfoField;

        private string channelTypeField;

        private string msgLanguageCodeField;

        private RequestHeaderTimeFormat timeFormatField;

        private RequestHeaderAdditionalProperty[] additionalPropertyField;

        /// <remarks/>
        public string version
        {
            get
            {
                return versionField;
            }
            set
            {
                versionField = value;
            }
        }

        /// <remarks/>
        public string businessCode
        {
            get
            {
                return businessCodeField;
            }
            set
            {
                businessCodeField = value;
            }
        }

        /// <remarks/>
        public string messageSeq
        {
            get
            {
                return messageSeqField;
            }
            set
            {
                messageSeqField = value;
            }
        }

        /// <remarks/>
        public OwnershipInfo ownershipInfo
        {
            get
            {
                return ownershipInfoField;
            }
            set
            {
                ownershipInfoField = value;
            }
        }

        /// <remarks/>
        public SecurityInfo accessSecurity
        {
            get
            {
                return accessSecurityField;
            }
            set
            {
                accessSecurityField = value;
            }
        }

        /// <remarks/>
        public OperatorInfo operatorInfo
        {
            get
            {
                return operatorInfoField;
            }
            set
            {
                operatorInfoField = value;
            }
        }

        /// <remarks/>
        public string channelType
        {
            get
            {
                return channelTypeField;
            }
            set
            {
                channelTypeField = value;
            }
        }

        /// <remarks/>
        public string msgLanguageCode
        {
            get
            {
                return msgLanguageCodeField;
            }
            set
            {
                msgLanguageCodeField = value;
            }
        }

        /// <remarks/>
        public RequestHeaderTimeFormat timeFormat
        {
            get
            {
                return timeFormatField;
            }
            set
            {
                timeFormatField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("additionalProperty")]
        public RequestHeaderAdditionalProperty[] additionalProperty
        {
            get
            {
                return additionalPropertyField;
            }
            set
            {
                additionalPropertyField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
    public partial class ChangeSubscriberOfferingRequest
    {

        private ChangeSubscriberOfferingRequestSubAccessCode subAccessCodeField;

        private ChangeSubscriberOfferingRequestPrimaryOffering primaryOfferingField;

        private ChangeSubscriberOfferingRequestSupplementaryOffering supplementaryOfferingField;

        private BusinessOrderInfo orderField;

        /// <remarks/>
        public ChangeSubscriberOfferingRequestSubAccessCode subAccessCode
        {
            get
            {
                return subAccessCodeField;
            }
            set
            {
                subAccessCodeField = value;
            }
        }

        /// <remarks/>
        public ChangeSubscriberOfferingRequestPrimaryOffering primaryOffering
        {
            get
            {
                return primaryOfferingField;
            }
            set
            {
                primaryOfferingField = value;
            }
        }

        /// <remarks/>
        public ChangeSubscriberOfferingRequestSupplementaryOffering supplementaryOffering
        {
            get
            {
                return supplementaryOfferingField;
            }
            set
            {
                supplementaryOfferingField = value;
            }
        }

        /// <remarks/>
        public BusinessOrderInfo order
        {
            get
            {
                return orderField;
            }
            set
            {
                orderField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/crmheader")]
    public partial class OwnershipInfo
    {

        private decimal beIdField;

        private decimal regionIdField;

        private bool regionIdFieldSpecified;

        /// <remarks/>
        public decimal beId
        {
            get
            {
                return beIdField;
            }
            set
            {
                beIdField = value;
            }
        }

        /// <remarks/>
        public decimal regionId
        {
            get
            {
                return regionIdField;
            }
            set
            {
                regionIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool regionIdSpecified
        {
            get
            {
                return regionIdFieldSpecified;
            }
            set
            {
                regionIdFieldSpecified = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/crmheader")]
    public partial class SecurityInfo
    {

        private string loginSystemCodeField;

        private string passwordField;

        private string remoteIpField;

        /// <remarks/>
        public string loginSystemCode
        {
            get
            {
                return loginSystemCodeField;
            }
            set
            {
                loginSystemCodeField = value;
            }
        }

        /// <remarks/>
        public string password
        {
            get
            {
                return passwordField;
            }
            set
            {
                passwordField = value;
            }
        }

        /// <remarks/>
        public string remoteIp
        {
            get
            {
                return remoteIpField;
            }
            set
            {
                remoteIpField = value;
            }
        }
    }



    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/crmheader")]
    public partial class OperatorInfo
    {

        private decimal operatorIdField;

        private decimal orgIdField;

        private bool orgIdFieldSpecified;

        /// <remarks/>
        public decimal operatorId
        {
            get
            {
                return operatorIdField;
            }
            set
            {
                operatorIdField = value;
            }
        }

        /// <remarks/>
        public decimal orgId
        {
            get
            {
                return orgIdField;
            }
            set
            {
                orgIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool orgIdSpecified
        {
            get
            {
                return orgIdFieldSpecified;
            }
            set
            {
                orgIdFieldSpecified = value;
            }
        }
    }


    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bes/crminterface/crmheader")]
    public partial class RequestHeaderTimeFormat
    {

        private string timeTypeField;

        private decimal timeZoneIdField;

        private bool timeZoneIdFieldSpecified;

        /// <remarks/>
        public string timeType
        {
            get
            {
                return timeTypeField;
            }
            set
            {
                timeTypeField = value;
            }
        }

        /// <remarks/>
        public decimal timeZoneId
        {
            get
            {
                return timeZoneIdField;
            }
            set
            {
                timeZoneIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool timeZoneIdSpecified
        {
            get
            {
                return timeZoneIdFieldSpecified;
            }
            set
            {
                timeZoneIdFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bes/crminterface/crmheader")]
    public partial class RequestHeaderAdditionalProperty
    {

        private string codeField;

        private string valueField;

        /// <remarks/>
        public string code
        {
            get
            {
                return codeField;
            }
            set
            {
                codeField = value;
            }
        }

        /// <remarks/>
        public string value
        {
            get
            {
                return valueField;
            }
            set
            {
                valueField = value;
            }
        }
    }







    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
    public partial class ChangeSubscriberOfferingRequestSubAccessCode
    {

        private string itemField;

        private ItemChoiceType4 itemElementNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("primaryIdentity", typeof(string))]
        [System.Xml.Serialization.XmlElement("subscriberId", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifier("ItemElementName")]
        public string Item
        {
            get
            {
                return itemField;
            }
            set
            {
                itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public ItemChoiceType4 ItemElementName
        {
            get
            {
                return itemElementNameField;
            }
            set
            {
                itemElementNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
    public partial class ChangeSubscriberOfferingRequestPrimaryOffering
    {

        private decimal oldOfferingInstIdField;

        private OfferingOrderType newPrimaryOfferingField;

        /// <remarks/>
        public decimal oldOfferingInstId
        {
            get
            {
                return oldOfferingInstIdField;
            }
            set
            {
                oldOfferingInstIdField = value;
            }
        }

        /// <remarks/>
        public OfferingOrderType newPrimaryOffering
        {
            get
            {
                return newPrimaryOfferingField;
            }
            set
            {
                newPrimaryOfferingField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
    public partial class ChangeSubscriberOfferingRequestSupplementaryOffering
    {

        private OfferingOrderType[] addOfferingField;

        private decimal[] delOfferingField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("addOffering")]
        public OfferingOrderType[] addOffering
        {
            get
            {
                return addOfferingField;
            }
            set
            {
                addOfferingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("delOffering")]
        public decimal[] delOffering
        {
            get
            {
                return delOfferingField;
            }
            set
            {
                delOfferingField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class BusinessOrderInfo
    {

        private DateTime wishExecDateField;

        private bool wishExecDateFieldSpecified;

        private DateTime reqApplyTimeField;

        private bool reqApplyTimeFieldSpecified;

        private string bkgCalcFeeFlagField;

        private ShippingInfo[] shippingInfoField;

        private OrderReceiptInfo[] businessReceiptField;

        private OrderInvoiceInfo businessFeeField;

        private decimal salesDepartIdField;

        private bool salesDepartIdFieldSpecified;

        private decimal salesPersonField;

        private bool salesPersonFieldSpecified;

        private decimal beIdField;

        private bool beIdFieldSpecified;

        private string remarkField;

        /// <remarks/>
        public DateTime wishExecDate
        {
            get
            {
                return wishExecDateField;
            }
            set
            {
                wishExecDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool wishExecDateSpecified
        {
            get
            {
                return wishExecDateFieldSpecified;
            }
            set
            {
                wishExecDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime reqApplyTime
        {
            get
            {
                return reqApplyTimeField;
            }
            set
            {
                reqApplyTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool reqApplyTimeSpecified
        {
            get
            {
                return reqApplyTimeFieldSpecified;
            }
            set
            {
                reqApplyTimeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string bkgCalcFeeFlag
        {
            get
            {
                return bkgCalcFeeFlagField;
            }
            set
            {
                bkgCalcFeeFlagField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("shippingInfo")]
        public ShippingInfo[] shippingInfo
        {
            get
            {
                return shippingInfoField;
            }
            set
            {
                shippingInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("businessReceipt")]
        public OrderReceiptInfo[] businessReceipt
        {
            get
            {
                return businessReceiptField;
            }
            set
            {
                businessReceiptField = value;
            }
        }

        /// <remarks/>
        public OrderInvoiceInfo businessFee
        {
            get
            {
                return businessFeeField;
            }
            set
            {
                businessFeeField = value;
            }
        }

        /// <remarks/>
        public decimal salesDepartId
        {
            get
            {
                return salesDepartIdField;
            }
            set
            {
                salesDepartIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool salesDepartIdSpecified
        {
            get
            {
                return salesDepartIdFieldSpecified;
            }
            set
            {
                salesDepartIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal salesPerson
        {
            get
            {
                return salesPersonField;
            }
            set
            {
                salesPersonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool salesPersonSpecified
        {
            get
            {
                return salesPersonFieldSpecified;
            }
            set
            {
                salesPersonFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal beId
        {
            get
            {
                return beIdField;
            }
            set
            {
                beIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool beIdSpecified
        {
            get
            {
                return beIdFieldSpecified;
            }
            set
            {
                beIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string remark
        {
            get
            {
                return remarkField;
            }
            set
            {
                remarkField = value;
            }
        }
    }



    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmservices", IncludeInSchema = false)]
    public enum ItemChoiceType4
    {

        /// <remarks/>
        primaryIdentity,

        /// <remarks/>
        subscriberId,
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class OfferingOrderType
    {

        private decimal offeringIdField;

        private bool offeringIdFieldSpecified;

        private string purchaseSeqField;

        private ContractOrderType offeringContractField;

        private PropertyOrderType[] offeringPropsField;

        private OfferingEffectiveMode offeringEffectiveModeField;

        private OfferingExpiryMode offeringExpiryModeField;

        private ActiveMode offeringActiveModeField;

        private OfferingDelivery offeringDeliveryField;

        private OfferingResource[] offeringResourcesField;

        private ProductOrderType[] productsField;

        /// <remarks/>
        public decimal offeringId
        {
            get
            {
                return offeringIdField;
            }
            set
            {
                offeringIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool offeringIdSpecified
        {
            get
            {
                return offeringIdFieldSpecified;
            }
            set
            {
                offeringIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string purchaseSeq
        {
            get
            {
                return purchaseSeqField;
            }
            set
            {
                purchaseSeqField = value;
            }
        }

        /// <remarks/>
        public ContractOrderType offeringContract
        {
            get
            {
                return offeringContractField;
            }
            set
            {
                offeringContractField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("offeringProps")]
        public PropertyOrderType[] offeringProps
        {
            get
            {
                return offeringPropsField;
            }
            set
            {
                offeringPropsField = value;
            }
        }

        /// <remarks/>
        public OfferingEffectiveMode offeringEffectiveMode
        {
            get
            {
                return offeringEffectiveModeField;
            }
            set
            {
                offeringEffectiveModeField = value;
            }
        }

        /// <remarks/>
        public OfferingExpiryMode offeringExpiryMode
        {
            get
            {
                return offeringExpiryModeField;
            }
            set
            {
                offeringExpiryModeField = value;
            }
        }

        /// <remarks/>
        public ActiveMode offeringActiveMode
        {
            get
            {
                return offeringActiveModeField;
            }
            set
            {
                offeringActiveModeField = value;
            }
        }

        /// <remarks/>
        public OfferingDelivery offeringDelivery
        {
            get
            {
                return offeringDeliveryField;
            }
            set
            {
                offeringDeliveryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("offeringResources")]
        public OfferingResource[] offeringResources
        {
            get
            {
                return offeringResourcesField;
            }
            set
            {
                offeringResourcesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("products")]
        public ProductOrderType[] products
        {
            get
            {
                return productsField;
            }
            set
            {
                productsField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://bes.huawei.com/om/order/fundation/intf/orderbasetype/")]
    public partial class ShippingInfo
    {

        private string opCodeField;

        private decimal shippingIdField;

        private bool shippingIdFieldSpecified;

        private decimal relaShippingIdField;

        private bool relaShippingIdFieldSpecified;

        private decimal shippingCarrierIdField;

        private bool shippingCarrierIdFieldSpecified;

        private decimal shippingModeIdField;

        private string shippingModeIdDescField;

        private string shippingTimeLimitField;

        private DateTime shippingFromDateField;

        private bool shippingFromDateFieldSpecified;

        private DateTime shippingToDateField;

        private bool shippingToDateFieldSpecified;

        private string printInvoiceFlagField;

        private string fulfillmentCenterField;

        private string remarkField;

        private string shippingStatusField;

        private AddressInfo addressInfoField;

        private ContactPersonInfo contactPersonInfoField;

        /// <remarks/>
        public string opCode
        {
            get
            {
                return opCodeField;
            }
            set
            {
                opCodeField = value;
            }
        }

        /// <remarks/>
        public decimal shippingId
        {
            get
            {
                return shippingIdField;
            }
            set
            {
                shippingIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool shippingIdSpecified
        {
            get
            {
                return shippingIdFieldSpecified;
            }
            set
            {
                shippingIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal relaShippingId
        {
            get
            {
                return relaShippingIdField;
            }
            set
            {
                relaShippingIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool relaShippingIdSpecified
        {
            get
            {
                return relaShippingIdFieldSpecified;
            }
            set
            {
                relaShippingIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal shippingCarrierId
        {
            get
            {
                return shippingCarrierIdField;
            }
            set
            {
                shippingCarrierIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool shippingCarrierIdSpecified
        {
            get
            {
                return shippingCarrierIdFieldSpecified;
            }
            set
            {
                shippingCarrierIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal shippingModeId
        {
            get
            {
                return shippingModeIdField;
            }
            set
            {
                shippingModeIdField = value;
            }
        }

        /// <remarks/>
        public string shippingModeIdDesc
        {
            get
            {
                return shippingModeIdDescField;
            }
            set
            {
                shippingModeIdDescField = value;
            }
        }

        /// <remarks/>
        public string shippingTimeLimit
        {
            get
            {
                return shippingTimeLimitField;
            }
            set
            {
                shippingTimeLimitField = value;
            }
        }

        /// <remarks/>
        public DateTime shippingFromDate
        {
            get
            {
                return shippingFromDateField;
            }
            set
            {
                shippingFromDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool shippingFromDateSpecified
        {
            get
            {
                return shippingFromDateFieldSpecified;
            }
            set
            {
                shippingFromDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime shippingToDate
        {
            get
            {
                return shippingToDateField;
            }
            set
            {
                shippingToDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool shippingToDateSpecified
        {
            get
            {
                return shippingToDateFieldSpecified;
            }
            set
            {
                shippingToDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string printInvoiceFlag
        {
            get
            {
                return printInvoiceFlagField;
            }
            set
            {
                printInvoiceFlagField = value;
            }
        }

        /// <remarks/>
        public string fulfillmentCenter
        {
            get
            {
                return fulfillmentCenterField;
            }
            set
            {
                fulfillmentCenterField = value;
            }
        }

        /// <remarks/>
        public string remark
        {
            get
            {
                return remarkField;
            }
            set
            {
                remarkField = value;
            }
        }

        /// <remarks/>
        public string shippingStatus
        {
            get
            {
                return shippingStatusField;
            }
            set
            {
                shippingStatusField = value;
            }
        }

        /// <remarks/>
        public AddressInfo addressInfo
        {
            get
            {
                return addressInfoField;
            }
            set
            {
                addressInfoField = value;
            }
        }

        /// <remarks/>
        public ContactPersonInfo contactPersonInfo
        {
            get
            {
                return contactPersonInfoField;
            }
            set
            {
                contactPersonInfoField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://bes.huawei.com/om/order/fundation/intf/orderbasetype/")]
    public partial class OrderReceiptInfo
    {

        private decimal orderReceiptIdField;

        private decimal shippingIdField;

        private bool shippingIdFieldSpecified;

        private string receiptTypeField;

        private string headTypeField;

        private string headNameField;

        private string serviceNumberField;

        private string contentTypeField;

        private string remarkField;

        private string receiptNoField;

        private decimal receiptAmountField;

        private bool receiptAmountFieldSpecified;

        private string printTypeField;

        /// <remarks/>
        public decimal orderReceiptId
        {
            get
            {
                return orderReceiptIdField;
            }
            set
            {
                orderReceiptIdField = value;
            }
        }

        /// <remarks/>
        public decimal shippingId
        {
            get
            {
                return shippingIdField;
            }
            set
            {
                shippingIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool shippingIdSpecified
        {
            get
            {
                return shippingIdFieldSpecified;
            }
            set
            {
                shippingIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string receiptType
        {
            get
            {
                return receiptTypeField;
            }
            set
            {
                receiptTypeField = value;
            }
        }

        /// <remarks/>
        public string headType
        {
            get
            {
                return headTypeField;
            }
            set
            {
                headTypeField = value;
            }
        }

        /// <remarks/>
        public string headName
        {
            get
            {
                return headNameField;
            }
            set
            {
                headNameField = value;
            }
        }

        /// <remarks/>
        public string serviceNumber
        {
            get
            {
                return serviceNumberField;
            }
            set
            {
                serviceNumberField = value;
            }
        }

        /// <remarks/>
        public string contentType
        {
            get
            {
                return contentTypeField;
            }
            set
            {
                contentTypeField = value;
            }
        }

        /// <remarks/>
        public string remark
        {
            get
            {
                return remarkField;
            }
            set
            {
                remarkField = value;
            }
        }

        /// <remarks/>
        public string receiptNo
        {
            get
            {
                return receiptNoField;
            }
            set
            {
                receiptNoField = value;
            }
        }

        /// <remarks/>
        public decimal receiptAmount
        {
            get
            {
                return receiptAmountField;
            }
            set
            {
                receiptAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool receiptAmountSpecified
        {
            get
            {
                return receiptAmountFieldSpecified;
            }
            set
            {
                receiptAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string printType
        {
            get
            {
                return printTypeField;
            }
            set
            {
                printTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://bes.huawei.com/om/payment/foundation/intf/paymenttype/")]
    public partial class OrderInvoiceInfo
    {

        private DateTime invoiceDateField;

        private bool invoiceDateFieldSpecified;

        private InvoiceItemInfo[] invoiceItemField;

        /// <remarks/>
        public DateTime invoiceDate
        {
            get
            {
                return invoiceDateField;
            }
            set
            {
                invoiceDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool invoiceDateSpecified
        {
            get
            {
                return invoiceDateFieldSpecified;
            }
            set
            {
                invoiceDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("invoiceItem")]
        public InvoiceItemInfo[] invoiceItem
        {
            get
            {
                return invoiceItemField;
            }
            set
            {
                invoiceItemField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class ContractOrderType
    {

        private string itemField;

        private ItemChoiceType5 itemElementNameField;

        private string periodUnitField;

        private string periodField;

        private DateTime effDateField;

        private bool effDateFieldSpecified;

        private DateTime expDateField;

        private bool expDateFieldSpecified;

        private string prolongPeriodUnitField;

        private string prolongPeriodField;

        private string prolongTypeField;

        private string autoProlongFlagField;

        private string reserveActionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("contractCode", typeof(string))]
        [System.Xml.Serialization.XmlElement("contractId", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifier("ItemElementName")]
        public string Item
        {
            get
            {
                return itemField;
            }
            set
            {
                itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public ItemChoiceType5 ItemElementName
        {
            get
            {
                return itemElementNameField;
            }
            set
            {
                itemElementNameField = value;
            }
        }

        /// <remarks/>
        public string periodUnit
        {
            get
            {
                return periodUnitField;
            }
            set
            {
                periodUnitField = value;
            }
        }

        /// <remarks/>
        public string period
        {
            get
            {
                return periodField;
            }
            set
            {
                periodField = value;
            }
        }

        /// <remarks/>
        public DateTime effDate
        {
            get
            {
                return effDateField;
            }
            set
            {
                effDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool effDateSpecified
        {
            get
            {
                return effDateFieldSpecified;
            }
            set
            {
                effDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime expDate
        {
            get
            {
                return expDateField;
            }
            set
            {
                expDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool expDateSpecified
        {
            get
            {
                return expDateFieldSpecified;
            }
            set
            {
                expDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string prolongPeriodUnit
        {
            get
            {
                return prolongPeriodUnitField;
            }
            set
            {
                prolongPeriodUnitField = value;
            }
        }

        /// <remarks/>
        public string prolongPeriod
        {
            get
            {
                return prolongPeriodField;
            }
            set
            {
                prolongPeriodField = value;
            }
        }

        /// <remarks/>
        public string prolongType
        {
            get
            {
                return prolongTypeField;
            }
            set
            {
                prolongTypeField = value;
            }
        }

        /// <remarks/>
        public string autoProlongFlag
        {
            get
            {
                return autoProlongFlagField;
            }
            set
            {
                autoProlongFlagField = value;
            }
        }

        /// <remarks/>
        public string reserveAction
        {
            get
            {
                return reserveActionField;
            }
            set
            {
                reserveActionField = value;
            }
        }
    }



    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype", IncludeInSchema = false)]
    public enum ItemChoiceType5
    {

        /// <remarks/>
        contractCode,

        /// <remarks/>
        contractId,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class PropertyOrderType
    {

        private string propCodeField;

        private string complexFlagField;

        private string valueField;

        private string oldValueField;

        private DateTime effDateField;

        private bool effDateFieldSpecified;

        private DateTime expDateField;

        private bool expDateFieldSpecified;

        private string effectiveModeField;

        private string expiryModeField;

        private PropertyOrderType[] subPropInfoField;

        /// <remarks/>
        public string propCode
        {
            get
            {
                return propCodeField;
            }
            set
            {
                propCodeField = value;
            }
        }

        /// <remarks/>
        public string complexFlag
        {
            get
            {
                return complexFlagField;
            }
            set
            {
                complexFlagField = value;
            }
        }

        /// <remarks/>
        public string value
        {
            get
            {
                return valueField;
            }
            set
            {
                valueField = value;
            }
        }

        /// <remarks/>
        public string oldValue
        {
            get
            {
                return oldValueField;
            }
            set
            {
                oldValueField = value;
            }
        }

        /// <remarks/>
        public DateTime effDate
        {
            get
            {
                return effDateField;
            }
            set
            {
                effDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool effDateSpecified
        {
            get
            {
                return effDateFieldSpecified;
            }
            set
            {
                effDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime expDate
        {
            get
            {
                return expDateField;
            }
            set
            {
                expDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool expDateSpecified
        {
            get
            {
                return expDateFieldSpecified;
            }
            set
            {
                expDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string effectiveMode
        {
            get
            {
                return effectiveModeField;
            }
            set
            {
                effectiveModeField = value;
            }
        }

        /// <remarks/>
        public string expiryMode
        {
            get
            {
                return expiryModeField;
            }
            set
            {
                expiryModeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("subPropInfo")]
        public PropertyOrderType[] subPropInfo
        {
            get
            {
                return subPropInfoField;
            }
            set
            {
                subPropInfoField = value;
            }
        }
    }




    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class OfferingEffectiveMode
    {

        private string effectiveModeField;

        /// <remarks/>
        public string effectiveMode
        {
            get
            {
                return effectiveModeField;
            }
            set
            {
                effectiveModeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class OfferingExpiryMode
    {

        private string expiryModeField;

        /// <remarks/>
        public string expiryMode
        {
            get
            {
                return expiryModeField;
            }
            set
            {
                expiryModeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://bes.huawei.com/om/order/fundation/intf/orderbasetype/")]
    public partial class ActiveMode
    {

        private string activeModeField;

        private string durationUnitField;

        private decimal durationValueField;

        private bool durationValueFieldSpecified;

        private DateTime lastActiveTimeField;

        private bool lastActiveTimeFieldSpecified;

        /// <remarks/>
        public string activeMode
        {
            get
            {
                return activeModeField;
            }
            set
            {
                activeModeField = value;
            }
        }

        /// <remarks/>
        public string durationUnit
        {
            get
            {
                return durationUnitField;
            }
            set
            {
                durationUnitField = value;
            }
        }

        /// <remarks/>
        public decimal durationValue
        {
            get
            {
                return durationValueField;
            }
            set
            {
                durationValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool durationValueSpecified
        {
            get
            {
                return durationValueFieldSpecified;
            }
            set
            {
                durationValueFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime lastActiveTime
        {
            get
            {
                return lastActiveTimeField;
            }
            set
            {
                lastActiveTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool lastActiveTimeSpecified
        {
            get
            {
                return lastActiveTimeFieldSpecified;
            }
            set
            {
                lastActiveTimeFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class OfferingDelivery
    {

        private string deliveryMethodField;

        private decimal shippingIdField;

        private bool shippingIdFieldSpecified;

        private decimal relaShippingIdField;

        private bool relaShippingIdFieldSpecified;

        private decimal shopIdField;

        private bool shopIdFieldSpecified;

        private DateTime deliveryDateField;

        private bool deliveryDateFieldSpecified;

        /// <remarks/>
        public string deliveryMethod
        {
            get
            {
                return deliveryMethodField;
            }
            set
            {
                deliveryMethodField = value;
            }
        }

        /// <remarks/>
        public decimal shippingId
        {
            get
            {
                return shippingIdField;
            }
            set
            {
                shippingIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool shippingIdSpecified
        {
            get
            {
                return shippingIdFieldSpecified;
            }
            set
            {
                shippingIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal relaShippingId
        {
            get
            {
                return relaShippingIdField;
            }
            set
            {
                relaShippingIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool relaShippingIdSpecified
        {
            get
            {
                return relaShippingIdFieldSpecified;
            }
            set
            {
                relaShippingIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal shopId
        {
            get
            {
                return shopIdField;
            }
            set
            {
                shopIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool shopIdSpecified
        {
            get
            {
                return shopIdFieldSpecified;
            }
            set
            {
                shopIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime deliveryDate
        {
            get
            {
                return deliveryDateField;
            }
            set
            {
                deliveryDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool deliveryDateSpecified
        {
            get
            {
                return deliveryDateFieldSpecified;
            }
            set
            {
                deliveryDateFieldSpecified = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class OfferingResource
    {

        private string identityIdField;

        private string endIdentityIdField;

        private string resourceTypeField;

        private decimal skuIdField;

        private bool skuIdFieldSpecified;

        private string skuCodeField;

        private string lockTypeField;

        private string lockCodeField;

        private decimal reserveCodeField;

        private bool reserveCodeFieldSpecified;

        private decimal quantityField;

        private bool quantityFieldSpecified;

        /// <remarks/>
        public string identityId
        {
            get
            {
                return identityIdField;
            }
            set
            {
                identityIdField = value;
            }
        }

        /// <remarks/>
        public string endIdentityId
        {
            get
            {
                return endIdentityIdField;
            }
            set
            {
                endIdentityIdField = value;
            }
        }

        /// <remarks/>
        public string resourceType
        {
            get
            {
                return resourceTypeField;
            }
            set
            {
                resourceTypeField = value;
            }
        }

        /// <remarks/>
        public decimal skuId
        {
            get
            {
                return skuIdField;
            }
            set
            {
                skuIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool skuIdSpecified
        {
            get
            {
                return skuIdFieldSpecified;
            }
            set
            {
                skuIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string skuCode
        {
            get
            {
                return skuCodeField;
            }
            set
            {
                skuCodeField = value;
            }
        }

        /// <remarks/>
        public string lockType
        {
            get
            {
                return lockTypeField;
            }
            set
            {
                lockTypeField = value;
            }
        }

        /// <remarks/>
        public string lockCode
        {
            get
            {
                return lockCodeField;
            }
            set
            {
                lockCodeField = value;
            }
        }

        /// <remarks/>
        public decimal reserveCode
        {
            get
            {
                return reserveCodeField;
            }
            set
            {
                reserveCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool reserveCodeSpecified
        {
            get
            {
                return reserveCodeFieldSpecified;
            }
            set
            {
                reserveCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal quantity
        {
            get
            {
                return quantityField;
            }
            set
            {
                quantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool quantitySpecified
        {
            get
            {
                return quantityFieldSpecified;
            }
            set
            {
                quantityFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class ProductOrderType
    {

        private decimal productIdField;

        private bool productIdFieldSpecified;

        private PropertyOrderType[] productPropsField;

        /// <remarks/>
        public decimal productId
        {
            get
            {
                return productIdField;
            }
            set
            {
                productIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool productIdSpecified
        {
            get
            {
                return productIdFieldSpecified;
            }
            set
            {
                productIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("productProps")]
        public PropertyOrderType[] productProps
        {
            get
            {
                return productPropsField;
            }
            set
            {
                productPropsField = value;
            }
        }
    }



    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class AddressInfo
    {

        private long addrIdField;

        private bool addrIdFieldSpecified;

        private string addrClassField;

        private string addr1Field;

        private string addr2Field;

        private string addr3Field;

        private string addr4Field;

        private string addr5Field;

        private string addr6Field;

        private string addr7Field;

        private string addr8Field;

        private string addr9Field;

        private string addr10Field;

        private string addr11Field;

        private string addr12Field;

        private string addr13Field;

        private string addr14Field;

        private string addr15Field;

        private string usAddrField;

        private string postalCodeField;

        /// <remarks/>
        public long addrId
        {
            get
            {
                return addrIdField;
            }
            set
            {
                addrIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool addrIdSpecified
        {
            get
            {
                return addrIdFieldSpecified;
            }
            set
            {
                addrIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string addrClass
        {
            get
            {
                return addrClassField;
            }
            set
            {
                addrClassField = value;
            }
        }

        /// <remarks/>
        public string addr1
        {
            get
            {
                return addr1Field;
            }
            set
            {
                addr1Field = value;
            }
        }

        /// <remarks/>
        public string addr2
        {
            get
            {
                return addr2Field;
            }
            set
            {
                addr2Field = value;
            }
        }

        /// <remarks/>
        public string addr3
        {
            get
            {
                return addr3Field;
            }
            set
            {
                addr3Field = value;
            }
        }

        /// <remarks/>
        public string addr4
        {
            get
            {
                return addr4Field;
            }
            set
            {
                addr4Field = value;
            }
        }

        /// <remarks/>
        public string addr5
        {
            get
            {
                return addr5Field;
            }
            set
            {
                addr5Field = value;
            }
        }

        /// <remarks/>
        public string addr6
        {
            get
            {
                return addr6Field;
            }
            set
            {
                addr6Field = value;
            }
        }

        /// <remarks/>
        public string addr7
        {
            get
            {
                return addr7Field;
            }
            set
            {
                addr7Field = value;
            }
        }

        /// <remarks/>
        public string addr8
        {
            get
            {
                return addr8Field;
            }
            set
            {
                addr8Field = value;
            }
        }

        /// <remarks/>
        public string addr9
        {
            get
            {
                return addr9Field;
            }
            set
            {
                addr9Field = value;
            }
        }

        /// <remarks/>
        public string addr10
        {
            get
            {
                return addr10Field;
            }
            set
            {
                addr10Field = value;
            }
        }

        /// <remarks/>
        public string addr11
        {
            get
            {
                return addr11Field;
            }
            set
            {
                addr11Field = value;
            }
        }

        /// <remarks/>
        public string addr12
        {
            get
            {
                return addr12Field;
            }
            set
            {
                addr12Field = value;
            }
        }

        /// <remarks/>
        public string addr13
        {
            get
            {
                return addr13Field;
            }
            set
            {
                addr13Field = value;
            }
        }

        /// <remarks/>
        public string addr14
        {
            get
            {
                return addr14Field;
            }
            set
            {
                addr14Field = value;
            }
        }

        /// <remarks/>
        public string addr15
        {
            get
            {
                return addr15Field;
            }
            set
            {
                addr15Field = value;
            }
        }

        /// <remarks/>
        public string usAddr
        {
            get
            {
                return usAddrField;
            }
            set
            {
                usAddrField = value;
            }
        }

        /// <remarks/>
        public string postalCode
        {
            get
            {
                return postalCodeField;
            }
            set
            {
                postalCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://bes.huawei.com/om/order/fundation/intf/orderbasetype/")]
    public partial class ContactPersonInfo
    {

        private object contactInfoIdField;

        private string firstNameField;

        private string middleNameField;

        private string lastNameField;

        private string homePhoneField;

        private string mobilePhoneField;

        private string officePhoneField;

        private string emailField;

        private string faxField;

        /// <remarks/>
        public object contactInfoId
        {
            get
            {
                return contactInfoIdField;
            }
            set
            {
                contactInfoIdField = value;
            }
        }

        /// <remarks/>
        public string firstName
        {
            get
            {
                return firstNameField;
            }
            set
            {
                firstNameField = value;
            }
        }

        /// <remarks/>
        public string middleName
        {
            get
            {
                return middleNameField;
            }
            set
            {
                middleNameField = value;
            }
        }

        /// <remarks/>
        public string lastName
        {
            get
            {
                return lastNameField;
            }
            set
            {
                lastNameField = value;
            }
        }

        /// <remarks/>
        public string homePhone
        {
            get
            {
                return homePhoneField;
            }
            set
            {
                homePhoneField = value;
            }
        }

        /// <remarks/>
        public string mobilePhone
        {
            get
            {
                return mobilePhoneField;
            }
            set
            {
                mobilePhoneField = value;
            }
        }

        /// <remarks/>
        public string officePhone
        {
            get
            {
                return officePhoneField;
            }
            set
            {
                officePhoneField = value;
            }
        }

        /// <remarks/>
        public string email
        {
            get
            {
                return emailField;
            }
            set
            {
                emailField = value;
            }
        }

        /// <remarks/>
        public string fax
        {
            get
            {
                return faxField;
            }
            set
            {
                faxField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://bes.huawei.com/om/payment/foundation/intf/paymenttype/")]
    public partial class InvoiceItemInfo
    {

        private long invoiceItemIdField;

        private bool invoiceItemIdFieldSpecified;

        private string payModeField;

        private string feeTypeField;

        private string chargeCodeField;

        private string chargeNameField;

        private decimal unitPriceField;

        private bool unitPriceFieldSpecified;

        private string quantityField;

        private decimal amountField;

        private bool amountFieldSpecified;

        private decimal openAmountField;

        private bool openAmountFieldSpecified;

        private decimal closeAmountField;

        private bool closeAmountFieldSpecified;

        private string includeTaxField;

        private decimal taxAmountField;

        private bool taxAmountFieldSpecified;

        private decimal discountAmountField;

        private bool discountAmountFieldSpecified;

        private string currencyTypeField;

        private long currencyIdField;

        private bool currencyIdFieldSpecified;

        private InvoiceItemDetailInfo itemDetailField;

        private string remarkField;

        /// <remarks/>
        public long invoiceItemId
        {
            get
            {
                return invoiceItemIdField;
            }
            set
            {
                invoiceItemIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool invoiceItemIdSpecified
        {
            get
            {
                return invoiceItemIdFieldSpecified;
            }
            set
            {
                invoiceItemIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string payMode
        {
            get
            {
                return payModeField;
            }
            set
            {
                payModeField = value;
            }
        }

        /// <remarks/>
        public string feeType
        {
            get
            {
                return feeTypeField;
            }
            set
            {
                feeTypeField = value;
            }
        }

        /// <remarks/>
        public string chargeCode
        {
            get
            {
                return chargeCodeField;
            }
            set
            {
                chargeCodeField = value;
            }
        }

        /// <remarks/>
        public string chargeName
        {
            get
            {
                return chargeNameField;
            }
            set
            {
                chargeNameField = value;
            }
        }

        /// <remarks/>
        public decimal unitPrice
        {
            get
            {
                return unitPriceField;
            }
            set
            {
                unitPriceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool unitPriceSpecified
        {
            get
            {
                return unitPriceFieldSpecified;
            }
            set
            {
                unitPriceFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "integer")]
        public string quantity
        {
            get
            {
                return quantityField;
            }
            set
            {
                quantityField = value;
            }
        }

        /// <remarks/>
        public decimal amount
        {
            get
            {
                return amountField;
            }
            set
            {
                amountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool amountSpecified
        {
            get
            {
                return amountFieldSpecified;
            }
            set
            {
                amountFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal openAmount
        {
            get
            {
                return openAmountField;
            }
            set
            {
                openAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool openAmountSpecified
        {
            get
            {
                return openAmountFieldSpecified;
            }
            set
            {
                openAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal closeAmount
        {
            get
            {
                return closeAmountField;
            }
            set
            {
                closeAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool closeAmountSpecified
        {
            get
            {
                return closeAmountFieldSpecified;
            }
            set
            {
                closeAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string includeTax
        {
            get
            {
                return includeTaxField;
            }
            set
            {
                includeTaxField = value;
            }
        }

        /// <remarks/>
        public decimal taxAmount
        {
            get
            {
                return taxAmountField;
            }
            set
            {
                taxAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool taxAmountSpecified
        {
            get
            {
                return taxAmountFieldSpecified;
            }
            set
            {
                taxAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal discountAmount
        {
            get
            {
                return discountAmountField;
            }
            set
            {
                discountAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool discountAmountSpecified
        {
            get
            {
                return discountAmountFieldSpecified;
            }
            set
            {
                discountAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string currencyType
        {
            get
            {
                return currencyTypeField;
            }
            set
            {
                currencyTypeField = value;
            }
        }

        /// <remarks/>
        public long currencyId
        {
            get
            {
                return currencyIdField;
            }
            set
            {
                currencyIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool currencyIdSpecified
        {
            get
            {
                return currencyIdFieldSpecified;
            }
            set
            {
                currencyIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public InvoiceItemDetailInfo itemDetail
        {
            get
            {
                return itemDetailField;
            }
            set
            {
                itemDetailField = value;
            }
        }

        /// <remarks/>
        public string remark
        {
            get
            {
                return remarkField;
            }
            set
            {
                remarkField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://bes.huawei.com/om/payment/foundation/intf/paymenttype/")]
    public partial class InvoiceItemDetailInfo
    {

        private InvoiceItemTaxInfo[] taxInfoField;

        private InvoiceItemPenaltyInfo[] penaltyInfoField;

        private InvoiceItemWaiveInfo[] waiveInfoField;

        private InvoiceItemDiscountInfo[] discountInfoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("taxInfo")]
        public InvoiceItemTaxInfo[] taxInfo
        {
            get
            {
                return taxInfoField;
            }
            set
            {
                taxInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("penaltyInfo")]
        public InvoiceItemPenaltyInfo[] penaltyInfo
        {
            get
            {
                return penaltyInfoField;
            }
            set
            {
                penaltyInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("waiveInfo")]
        public InvoiceItemWaiveInfo[] waiveInfo
        {
            get
            {
                return waiveInfoField;
            }
            set
            {
                waiveInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("discountInfo")]
        public InvoiceItemDiscountInfo[] discountInfo
        {
            get
            {
                return discountInfoField;
            }
            set
            {
                discountInfoField = value;
            }
        }
    }




    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://bes.huawei.com/om/payment/foundation/intf/paymenttype/")]
    public partial class InvoiceItemTaxInfo
    {

        private string taxTypeField;

        private string chargeCodeField;

        private decimal amountField;

        private bool amountFieldSpecified;

        private string taxRateField;

        private string exemptionFlagField;

        /// <remarks/>
        public string taxType
        {
            get
            {
                return taxTypeField;
            }
            set
            {
                taxTypeField = value;
            }
        }

        /// <remarks/>
        public string chargeCode
        {
            get
            {
                return chargeCodeField;
            }
            set
            {
                chargeCodeField = value;
            }
        }

        /// <remarks/>
        public decimal amount
        {
            get
            {
                return amountField;
            }
            set
            {
                amountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool amountSpecified
        {
            get
            {
                return amountFieldSpecified;
            }
            set
            {
                amountFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string taxRate
        {
            get
            {
                return taxRateField;
            }
            set
            {
                taxRateField = value;
            }
        }

        /// <remarks/>
        public string exemptionFlag
        {
            get
            {
                return exemptionFlagField;
            }
            set
            {
                exemptionFlagField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://bes.huawei.com/om/payment/foundation/intf/paymenttype/")]
    public partial class InvoiceItemPenaltyInfo
    {

        private string penaltyTypeField;

        private long penaltyItemIdField;

        private bool penaltyItemIdFieldSpecified;

        private decimal fixedAmountField;

        private bool fixedAmountFieldSpecified;

        private decimal variableAmountField;

        private bool variableAmountFieldSpecified;

        private decimal cycleNumField;

        private bool cycleNumFieldSpecified;

        private string cycleUnitField;

        private decimal annexCycleNumField;

        private bool annexCycleNumFieldSpecified;

        private string annexCycleUnitField;

        private decimal contractIdField;

        private bool contractIdFieldSpecified;

        /// <remarks/>
        public string penaltyType
        {
            get
            {
                return penaltyTypeField;
            }
            set
            {
                penaltyTypeField = value;
            }
        }

        /// <remarks/>
        public long penaltyItemId
        {
            get
            {
                return penaltyItemIdField;
            }
            set
            {
                penaltyItemIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool penaltyItemIdSpecified
        {
            get
            {
                return penaltyItemIdFieldSpecified;
            }
            set
            {
                penaltyItemIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal fixedAmount
        {
            get
            {
                return fixedAmountField;
            }
            set
            {
                fixedAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool fixedAmountSpecified
        {
            get
            {
                return fixedAmountFieldSpecified;
            }
            set
            {
                fixedAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal variableAmount
        {
            get
            {
                return variableAmountField;
            }
            set
            {
                variableAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool variableAmountSpecified
        {
            get
            {
                return variableAmountFieldSpecified;
            }
            set
            {
                variableAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal cycleNum
        {
            get
            {
                return cycleNumField;
            }
            set
            {
                cycleNumField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool cycleNumSpecified
        {
            get
            {
                return cycleNumFieldSpecified;
            }
            set
            {
                cycleNumFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string cycleUnit
        {
            get
            {
                return cycleUnitField;
            }
            set
            {
                cycleUnitField = value;
            }
        }

        /// <remarks/>
        public decimal annexCycleNum
        {
            get
            {
                return annexCycleNumField;
            }
            set
            {
                annexCycleNumField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool annexCycleNumSpecified
        {
            get
            {
                return annexCycleNumFieldSpecified;
            }
            set
            {
                annexCycleNumFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string annexCycleUnit
        {
            get
            {
                return annexCycleUnitField;
            }
            set
            {
                annexCycleUnitField = value;
            }
        }

        /// <remarks/>
        public decimal contractId
        {
            get
            {
                return contractIdField;
            }
            set
            {
                contractIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool contractIdSpecified
        {
            get
            {
                return contractIdFieldSpecified;
            }
            set
            {
                contractIdFieldSpecified = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://bes.huawei.com/om/payment/foundation/intf/paymenttype/")]
    public partial class InvoiceItemWaiveInfo
    {

        private string reasonCodeField;

        private decimal authOperIdField;

        private bool authOperIdFieldSpecified;

        private decimal authDeptIdField;

        private bool authDeptIdFieldSpecified;

        private string remarkField;

        /// <remarks/>
        public string reasonCode
        {
            get
            {
                return reasonCodeField;
            }
            set
            {
                reasonCodeField = value;
            }
        }

        /// <remarks/>
        public decimal authOperId
        {
            get
            {
                return authOperIdField;
            }
            set
            {
                authOperIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool authOperIdSpecified
        {
            get
            {
                return authOperIdFieldSpecified;
            }
            set
            {
                authOperIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal authDeptId
        {
            get
            {
                return authDeptIdField;
            }
            set
            {
                authDeptIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool authDeptIdSpecified
        {
            get
            {
                return authDeptIdFieldSpecified;
            }
            set
            {
                authDeptIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string remark
        {
            get
            {
                return remarkField;
            }
            set
            {
                remarkField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://bes.huawei.com/om/payment/foundation/intf/paymenttype/")]
    public partial class InvoiceItemDiscountInfo
    {

        private long promotionIdField;

        private bool promotionIdFieldSpecified;

        private string discountMethodField;

        private decimal discountValueField;

        private bool discountValueFieldSpecified;

        private string redeemCodeField;

        /// <remarks/>
        public long promotionId
        {
            get
            {
                return promotionIdField;
            }
            set
            {
                promotionIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool promotionIdSpecified
        {
            get
            {
                return promotionIdFieldSpecified;
            }
            set
            {
                promotionIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string discountMethod
        {
            get
            {
                return discountMethodField;
            }
            set
            {
                discountMethodField = value;
            }
        }

        /// <remarks/>
        public decimal discountValue
        {
            get
            {
                return discountValueField;
            }
            set
            {
                discountValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool discountValueSpecified
        {
            get
            {
                return discountValueFieldSpecified;
            }
            set
            {
                discountValueFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string redeemCode
        {
            get
            {
                return redeemCodeField;
            }
            set
            {
                redeemCodeField = value;
            }
        }
    }










}
