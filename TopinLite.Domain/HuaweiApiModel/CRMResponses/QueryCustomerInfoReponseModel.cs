


using TopinLite.Domain.HuaweiApiModel.CM;
using TopinLite.Domain.HuaweiApiModel.Mg;

namespace TopinLite.Domain.HuaweiApiModel.CRMResponses.QueryCustomerInfo
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public partial class EnvelopeQueryCustomerInfoReponse
    {

        private EnvelopeBody bodyField;

        /// <remarks/>
        public EnvelopeBody Body
        {
            get
            {
                return bodyField;
            }
            set
            {
                bodyField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public partial class EnvelopeBody
    {

        private queryCustomerInfoRspMsg queryCustomerInfoRspMsgField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
        public queryCustomerInfoRspMsg queryCustomerInfoRspMsg
        {
            get
            {
                return queryCustomerInfoRspMsgField;
            }
            set
            {
                queryCustomerInfoRspMsgField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://www.huawei.com/bes/crminterface/cmservices", IsNullable = false)]
    public partial class queryCustomerInfoRspMsg
    {

        private resultHeader resultHeaderField;

        private queryCustomerInfoResponse queryCustomerInfoResponseField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public resultHeader resultHeader
        {
            get
            {
                return resultHeaderField;
            }
            set
            {
                resultHeaderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public queryCustomerInfoResponse queryCustomerInfoResponse
        {
            get
            {
                return queryCustomerInfoResponseField;
            }
            set
            {
                queryCustomerInfoResponseField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
    public partial class resultHeader
    {

        private string versionField;

        private decimal resultCodeField;

        private string resultDescField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/crmheader")]
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
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/crmheader")]
        public decimal resultCode
        {
            get
            {
                return resultCodeField;
            }
            set
            {
                resultCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/crmheader")]
        public string resultDesc
        {
            get
            {
                return resultDescField;
            }
            set
            {
                resultDescField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
    public partial class queryCustomerInfoResponse
    {

        private customer customerField;

        private long defaultAccountIdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
        public customer customer
        {
            get
            {
                return customerField;
            }
            set
            {
                customerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
        public long defaultAccountId
        {
            get
            {
                return defaultAccountIdField;
            }
            set
            {
                defaultAccountIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class customer
    {

        private decimal customerIdField;

        private bool customerIdFieldSpecified;

        private CustomerBasicType custBasicInfoField;

        private object itemField;

        private AddressType[] addressInfoField;

        private ContactPersonType[] contactPersonInfoField;

        /// <remarks/>
        public decimal customerId
        {
            get
            {
                return customerIdField;
            }
            set
            {
                customerIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool customerIdSpecified
        {
            get
            {
                return customerIdFieldSpecified;
            }
            set
            {
                customerIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public CustomerBasicType custBasicInfo
        {
            get
            {
                return custBasicInfoField;
            }
            set
            {
                custBasicInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("individualInfo", typeof(IndividualDetailType))]
        [System.Xml.Serialization.XmlElement("orgInfo", typeof(OrganizationDetailType))]
        public object Item
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
        [System.Xml.Serialization.XmlElement("addressInfo")]
        public AddressType[] addressInfo
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
        [System.Xml.Serialization.XmlElement("contactPersonInfo")]
        public ContactPersonType[] contactPersonInfo
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
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://www.huawei.com/bes/crminterface/cmtype", IsNullable = false)]
    public partial class custBasicInfo
    {

        private long custCodeField;

        private byte custTypeField;

        private byte custClassField;

        private long partyIdField;

        private ushort regionIdField;

        /// <remarks/>
        public long custCode
        {
            get
            {
                return custCodeField;
            }
            set
            {
                custCodeField = value;
            }
        }

        /// <remarks/>
        public byte custType
        {
            get
            {
                return custTypeField;
            }
            set
            {
                custTypeField = value;
            }
        }

        /// <remarks/>
        public byte custClass
        {
            get
            {
                return custClassField;
            }
            set
            {
                custClassField = value;
            }
        }

        /// <remarks/>
        public long partyId
        {
            get
            {
                return partyIdField;
            }
            set
            {
                partyIdField = value;
            }
        }

        /// <remarks/>
        public ushort regionId
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
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://www.huawei.com/bes/crminterface/cmtype", IsNullable = false)]
    public partial class individualInfo
    {

        private individualInfoIndividualBaseInfo individualBaseInfoField;

        private individualInfoCertificationInfo[] certificationInfoField;

        /// <remarks/>
        public individualInfoIndividualBaseInfo individualBaseInfo
        {
            get
            {
                return individualBaseInfoField;
            }
            set
            {
                individualBaseInfoField = value;
            }
        }

        /// <remarks/>
        public individualInfoCertificationInfo[] certificationInfo
        {
            get
            {
                return certificationInfoField;
            }
            set
            {
                certificationInfoField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class individualInfoIndividualBaseInfo
    {

        private long individualIdField;

        private string firstNameField;

        private string lastNameField;
        private string middleNameField;

        private byte titleField;

        private byte genderField;

        private DateTime birthdayField;

        private string nationalityField;

        private string nativePlaceField;

        private string writtenLangField;

        private byte phoneticLangField;

        private individualInfoIndividualBaseInfoIndividualProperty[] individualPropertyField;

        /// <remarks/>
        public long individualId
        {
            get
            {
                return individualIdField;
            }
            set
            {
                individualIdField = value;
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
        public byte title
        {
            get
            {
                return titleField;
            }
            set
            {
                titleField = value;
            }
        }

        /// <remarks/>
        public byte gender
        {
            get
            {
                return genderField;
            }
            set
            {
                genderField = value;
            }
        }

        /// <remarks/>
        public DateTime birthday
        {
            get
            {
                return birthdayField;
            }
            set
            {
                birthdayField = value;
            }
        }

        /// <remarks/>
        public string nationality
        {
            get
            {
                return nationalityField;
            }
            set
            {
                nationalityField = value;
            }
        }

        /// <remarks/>
        public string nativePlace
        {
            get
            {
                return nativePlaceField;
            }
            set
            {
                nativePlaceField = value;
            }
        }

        /// <remarks/>
        public string writtenLang
        {
            get
            {
                return writtenLangField;
            }
            set
            {
                writtenLangField = value;
            }
        }

        /// <remarks/>
        public byte phoneticLang
        {
            get
            {
                return phoneticLangField;
            }
            set
            {
                phoneticLangField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("individualProperty")]
        public individualInfoIndividualBaseInfoIndividualProperty[] individualProperty
        {
            get
            {
                return individualPropertyField;
            }
            set
            {
                individualPropertyField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class individualInfoIndividualBaseInfoIndividualProperty
    {

        private string codeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "")]
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
        [System.Xml.Serialization.XmlElement(Namespace = "")]
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
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class individualInfoCertificationInfo
    {

        private string idTypeField;

        private int idNumberField;

        /// <remarks/>
        public string idType
        {
            get
            {
                return idTypeField;
            }
            set
            {
                idTypeField = value;
            }
        }

        /// <remarks/>
        public int idNumber
        {
            get
            {
                return idNumberField;
            }
            set
            {
                idNumberField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://www.huawei.com/bes/crminterface/cmtype", IsNullable = false)]
    public partial class addressInfo
    {

        private long addressIdField;

        private string preferenceFlagField;

        private DateTime effDateField;

        private DateTime expDateField;

        private long addrTypeField;

        private addressInfoAddressInfo addressInfo1Field;

        /// <remarks/>
        public long addressId
        {
            get
            {
                return addressIdField;
            }
            set
            {
                addressIdField = value;
            }
        }

        /// <remarks/>
        public string preferenceFlag
        {
            get
            {
                return preferenceFlagField;
            }
            set
            {
                preferenceFlagField = value;
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
        public long addrType
        {
            get
            {
                return addrTypeField;
            }
            set
            {
                addrTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("addressInfo")]
        public addressInfoAddressInfo addressInfo1
        {
            get
            {
                return addressInfo1Field;
            }
            set
            {
                addressInfo1Field = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class addressInfoAddressInfo
    {

        private long addrIdField;

        private string addrClassField;

        private string addr1Field;

        private string addr2Field;

        private string addr3Field;

        private string usAddrField;

        private int postalCodeField;

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
        public int postalCode
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
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class IndividualDetailType
    {

        private IndividualBaseType individualBaseInfoField;

        private PartyCertificationType[] certificationInfoField;

        /// <remarks/>
        public IndividualBaseType individualBaseInfo
        {
            get
            {
                return individualBaseInfoField;
            }
            set
            {
                individualBaseInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("certificationInfo")]
        public PartyCertificationType[] certificationInfo
        {
            get
            {
                return certificationInfoField;
            }
            set
            {
                certificationInfoField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class IndividualBaseType
    {

        private decimal individualIdField;

        private bool individualIdFieldSpecified;

        private string firstNameField;

        private string middleNameField;

        private string lastNameField;

        private string titleField;

        private string genderField;

        private DateTime birthdayField;

        private string emailField;

        private string faxField;

        private string marriedStatusField;

        private string mobilePhoneField;

        private string officePhoneField;

        private string homePhoneField;

        private string nationalityField;

        private string nativePlaceField;

        private string occupationField;

        private string educationField;

        private string positionField;

        private string raceField;

        private string religionField;

        private string salaryField;

        private string writtenLangField;

        private string phoneticLangField;

        private SimpleProperty[] individualPropertyField;

        /// <remarks/>
        public decimal individualId
        {
            get
            {
                return individualIdField;
            }
            set
            {
                individualIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool individualIdSpecified
        {
            get
            {
                return individualIdFieldSpecified;
            }
            set
            {
                individualIdFieldSpecified = value;
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
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public string title
        {
            get
            {
                return titleField;
            }
            set
            {
                titleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public string gender
        {
            get
            {
                return genderField;
            }
            set
            {
                genderField = value;
            }
        }

        /// <remarks/>
        public DateTime birthday
        {
            get
            {
                return birthdayField;
            }
            set
            {
                birthdayField = value;
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public string marriedStatus
        {
            get
            {
                return marriedStatusField;
            }
            set
            {
                marriedStatusField = value;
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
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public string nationality
        {
            get
            {
                return nationalityField;
            }
            set
            {
                nationalityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public string nativePlace
        {
            get
            {
                return nativePlaceField;
            }
            set
            {
                nativePlaceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public string occupation
        {
            get
            {
                return occupationField;
            }
            set
            {
                occupationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public string education
        {
            get
            {
                return educationField;
            }
            set
            {
                educationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public string position
        {
            get
            {
                return positionField;
            }
            set
            {
                positionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public string race
        {
            get
            {
                return raceField;
            }
            set
            {
                raceField = value;
            }
        }

        /// <remarks/>
        public string religion
        {
            get
            {
                return religionField;
            }
            set
            {
                religionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public string salary
        {
            get
            {
                return salaryField;
            }
            set
            {
                salaryField = value;
            }
        }

        /// <remarks/>
        public string writtenLang
        {
            get
            {
                return writtenLangField;
            }
            set
            {
                writtenLangField = value;
            }
        }

        /// <remarks/>
        public string phoneticLang
        {
            get
            {
                return phoneticLangField;
            }
            set
            {
                phoneticLangField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("individualProperty")]
        public SimpleProperty[] individualProperty
        {
            get
            {
                return individualPropertyField;
            }
            set
            {
                individualPropertyField = value;
            }
        }
    }



    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class PartyCertificationType
    {

        private string idTypeField;

        private string idNumberField;

        private string idAddressField;

        private DateTime certEffDateField;

        private bool certEffDateFieldSpecified;

        private DateTime certExpDateField;

        private bool certExpDateFieldSpecified;

        private string issueCountryField;

        private string authorityField;

        private string isDefaultField;

        /// <remarks/>
        public string idType
        {
            get
            {
                return idTypeField;
            }
            set
            {
                idTypeField = value;
            }
        }

        /// <remarks/>
        public string idNumber
        {
            get
            {
                return idNumberField;
            }
            set
            {
                idNumberField = value;
            }
        }

        /// <remarks/>
        public string idAddress
        {
            get
            {
                return idAddressField;
            }
            set
            {
                idAddressField = value;
            }
        }

        /// <remarks/>
        public DateTime certEffDate
        {
            get
            {
                return certEffDateField;
            }
            set
            {
                certEffDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool certEffDateSpecified
        {
            get
            {
                return certEffDateFieldSpecified;
            }
            set
            {
                certEffDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime certExpDate
        {
            get
            {
                return certExpDateField;
            }
            set
            {
                certExpDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool certExpDateSpecified
        {
            get
            {
                return certExpDateFieldSpecified;
            }
            set
            {
                certExpDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string issueCountry
        {
            get
            {
                return issueCountryField;
            }
            set
            {
                issueCountryField = value;
            }
        }

        /// <remarks/>
        public string authority
        {
            get
            {
                return authorityField;
            }
            set
            {
                authorityField = value;
            }
        }

        /// <remarks/>
        public string isDefault
        {
            get
            {
                return isDefaultField;
            }
            set
            {
                isDefaultField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class CustomerBasicType
    {

        private string custCodeField;

        private string custLevelField;

        private string custTypeField;

        private string custClassField;

        private string custSegmentField;

        private string custCreditLevelField;

        private decimal partyIdField;

        private bool partyIdFieldSpecified;

        private decimal parentCustIdField;

        private bool parentCustIdFieldSpecified;

        private decimal regionIdField;

        private bool regionIdFieldSpecified;

        private SimpleProperty[] custPropertyField;

        /// <remarks/>
        public string custCode
        {
            get
            {
                return custCodeField;
            }
            set
            {
                custCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public string custLevel
        {
            get
            {
                return custLevelField;
            }
            set
            {
                custLevelField = value;
            }
        }

        /// <remarks/>
        public string custType
        {
            get
            {
                return custTypeField;
            }
            set
            {
                custTypeField = value;
            }
        }

        /// <remarks/>
        public string custClass
        {
            get
            {
                return custClassField;
            }
            set
            {
                custClassField = value;
            }
        }

        /// <remarks/>
        public string custSegment
        {
            get
            {
                return custSegmentField;
            }
            set
            {
                custSegmentField = value;
            }
        }

        /// <remarks/>
        public string custCreditLevel
        {
            get
            {
                return custCreditLevelField;
            }
            set
            {
                custCreditLevelField = value;
            }
        }

        /// <remarks/>
        public decimal partyId
        {
            get
            {
                return partyIdField;
            }
            set
            {
                partyIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool partyIdSpecified
        {
            get
            {
                return partyIdFieldSpecified;
            }
            set
            {
                partyIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal parentCustId
        {
            get
            {
                return parentCustIdField;
            }
            set
            {
                parentCustIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool parentCustIdSpecified
        {
            get
            {
                return parentCustIdFieldSpecified;
            }
            set
            {
                parentCustIdFieldSpecified = value;
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("custProperty")]
        public SimpleProperty[] custProperty
        {
            get
            {
                return custPropertyField;
            }
            set
            {
                custPropertyField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class AddressType
    {

        private decimal addressIdField;

        private bool addressIdFieldSpecified;

        private string opCodeField;

        private string preferenceFlagField;

        private DateTime effDateField;

        private bool effDateFieldSpecified;

        private DateTime expDateField;

        private bool expDateFieldSpecified;

        private decimal addrTypeField;

        private bool addrTypeFieldSpecified;

        private AddressInfo addressInfoField;

        /// <remarks/>
        public decimal addressId
        {
            get
            {
                return addressIdField;
            }
            set
            {
                addressIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool addressIdSpecified
        {
            get
            {
                return addressIdFieldSpecified;
            }
            set
            {
                addressIdFieldSpecified = value;
            }
        }

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
        public string preferenceFlag
        {
            get
            {
                return preferenceFlagField;
            }
            set
            {
                preferenceFlagField = value;
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
        public decimal addrType
        {
            get
            {
                return addrTypeField;
            }
            set
            {
                addrTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool addrTypeSpecified
        {
            get
            {
                return addrTypeFieldSpecified;
            }
            set
            {
                addrTypeFieldSpecified = value;
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
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class ContactPersonType
    {

        private decimal contactPersonIdField;

        private bool contactPersonIdFieldSpecified;

        private object itemField;

        private string opCodeField;

        private string contactPersonTypeField;

        private string titleField;

        private string firstNameField;

        private string middleNameField;

        private string lastNameField;

        private string mobilePhoneField;

        private string homePhoneField;

        private string officePhoneField;

        private string emailField;

        private string faxField;

        /// <remarks/>
        public decimal contactPersonId
        {
            get
            {
                return contactPersonIdField;
            }
            set
            {
                contactPersonIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool contactPersonIdSpecified
        {
            get
            {
                return contactPersonIdFieldSpecified;
            }
            set
            {
                contactPersonIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("addressId", typeof(decimal))]
        [System.Xml.Serialization.XmlElement("addressInfo", typeof(AddressInfo))]
        public object Item
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
        public string contactPersonType
        {
            get
            {
                return contactPersonTypeField;
            }
            set
            {
                contactPersonTypeField = value;
            }
        }

        /// <remarks/>
        public string title
        {
            get
            {
                return titleField;
            }
            set
            {
                titleField = value;
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
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class OrganizationDetailType
    {

        private OrganizationBaseType organizationBaseInfoField;

        private PartyCertificationType[] certificationInfoField;

        /// <remarks/>
        public OrganizationBaseType organizationBaseInfo
        {
            get
            {
                return organizationBaseInfoField;
            }
            set
            {
                organizationBaseInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("certificationInfo")]
        public PartyCertificationType[] certificationInfo
        {
            get
            {
                return certificationInfoField;
            }
            set
            {
                certificationInfoField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class OrganizationBaseType
    {

        private decimal orgIdField;

        private bool orgIdFieldSpecified;

        private string orgNameField;

        private string orgShortNameField;

        private string orgTypeField;

        private string artificialPersonField;

        private string orgEmailField;

        private string faxField;

        private DateTime foundationDateField;

        private bool foundationDateFieldSpecified;

        private string mobilePhoneField;

        private string orgPhoneField;

        private string industryField;

        private string orgDescField;

        private string orgLayerField;

        private string orgLevelField;

        private string orgWebField;

        private string sizeLevelField;

        private string revenueLevelField;

        private string registerAmountField;

        private string writtenLangField;

        private decimal parentOrgIdField;

        private bool parentOrgIdFieldSpecified;

        private SimpleProperty[] orgPropertyField;

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

        /// <remarks/>
        public string orgName
        {
            get
            {
                return orgNameField;
            }
            set
            {
                orgNameField = value;
            }
        }

        /// <remarks/>
        public string orgShortName
        {
            get
            {
                return orgShortNameField;
            }
            set
            {
                orgShortNameField = value;
            }
        }

        /// <remarks/>
        public string orgType
        {
            get
            {
                return orgTypeField;
            }
            set
            {
                orgTypeField = value;
            }
        }

        /// <remarks/>
        public string artificialPerson
        {
            get
            {
                return artificialPersonField;
            }
            set
            {
                artificialPersonField = value;
            }
        }

        /// <remarks/>
        public string orgEmail
        {
            get
            {
                return orgEmailField;
            }
            set
            {
                orgEmailField = value;
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

        /// <remarks/>
        public DateTime foundationDate
        {
            get
            {
                return foundationDateField;
            }
            set
            {
                foundationDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool foundationDateSpecified
        {
            get
            {
                return foundationDateFieldSpecified;
            }
            set
            {
                foundationDateFieldSpecified = value;
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
        public string orgPhone
        {
            get
            {
                return orgPhoneField;
            }
            set
            {
                orgPhoneField = value;
            }
        }

        /// <remarks/>
        public string industry
        {
            get
            {
                return industryField;
            }
            set
            {
                industryField = value;
            }
        }

        /// <remarks/>
        public string orgDesc
        {
            get
            {
                return orgDescField;
            }
            set
            {
                orgDescField = value;
            }
        }

        /// <remarks/>
        public string orgLayer
        {
            get
            {
                return orgLayerField;
            }
            set
            {
                orgLayerField = value;
            }
        }

        /// <remarks/>
        public string orgLevel
        {
            get
            {
                return orgLevelField;
            }
            set
            {
                orgLevelField = value;
            }
        }

        /// <remarks/>
        public string orgWeb
        {
            get
            {
                return orgWebField;
            }
            set
            {
                orgWebField = value;
            }
        }

        /// <remarks/>
        public string sizeLevel
        {
            get
            {
                return sizeLevelField;
            }
            set
            {
                sizeLevelField = value;
            }
        }

        /// <remarks/>
        public string revenueLevel
        {
            get
            {
                return revenueLevelField;
            }
            set
            {
                revenueLevelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "integer")]
        public string registerAmount
        {
            get
            {
                return registerAmountField;
            }
            set
            {
                registerAmountField = value;
            }
        }

        /// <remarks/>
        public string writtenLang
        {
            get
            {
                return writtenLangField;
            }
            set
            {
                writtenLangField = value;
            }
        }

        /// <remarks/>
        public decimal parentOrgId
        {
            get
            {
                return parentOrgIdField;
            }
            set
            {
                parentOrgIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool parentOrgIdSpecified
        {
            get
            {
                return parentOrgIdFieldSpecified;
            }
            set
            {
                parentOrgIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("orgProperty")]
        public SimpleProperty[] orgProperty
        {
            get
            {
                return orgPropertyField;
            }
            set
            {
                orgPropertyField = value;
            }
        }
    }

}