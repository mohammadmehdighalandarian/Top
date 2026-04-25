

using TopinLite.Domain.HuawiMicroGateway;

namespace TopinLite.Infra.ApiClient.SOAPApi.HuaweiEndpoint.Statics
{
    public static class RequestStrings
    {
        public static string LoginSystemCode = "682";
        public static string Password = "LPzZ8pyvPVguerHR44oet/yf6xwQ6DvNbqpJsgWMO6A=";

        public static string QueryBalanceSoapRequest(string PrimaryIdentity, string Mss)
        {
            return @$"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ars=""http://www.huawei.com/bme/cbsinterface/arservices"" xmlns:cbs=""http://www.huawei.com/bme/cbsinterface/cbscommon"" xmlns:arc=""http://cbs.huawei.com/ar/wsservice/arcommon"">
   <soapenv:Header/>
   <soapenv:Body>
      <ars:QueryBalanceRequestMsg>
         <RequestHeader>
            <cbs:Version>5.5</cbs:Version>
            <cbs:BusinessCode>QueryBalance</cbs:BusinessCode>
            <cbs:MessageSeq>{Mss}</cbs:MessageSeq>
            <cbs:OwnershipInfo>
               <cbs:BEID>10101</cbs:BEID>
            </cbs:OwnershipInfo>
            <cbs:AccessSecurity>
               <cbs:LoginSystemCode>{LoginSystemCode}</cbs:LoginSystemCode>
               <cbs:Password>{Password}</cbs:Password>
            </cbs:AccessSecurity>
            <!--Optional:-->
            <cbs:OperatorInfo>
               <cbs:OperatorID>{LoginSystemCode}</cbs:OperatorID>
               <!--Optional:-->
               <cbs:ChannelID>{LoginSystemCode}</cbs:ChannelID>
            </cbs:OperatorInfo>
         </RequestHeader>
         <QueryBalanceRequest>
            <ars:QueryObj>
               <ars:AcctAccessCode>
                  <arc:PrimaryIdentity>{PrimaryIdentity}</arc:PrimaryIdentity>
               </ars:AcctAccessCode>
            </ars:QueryObj>
         </QueryBalanceRequest>
      </ars:QueryBalanceRequestMsg>
   </soapenv:Body>
</soapenv:Envelope>";
        }

        public static string IntegrationEnquirySoapRequest(string SubscriberNo, string QueryType, string Mss)
        {
            return @$"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:bus=""http://www.huawei.com/bme/cbsinterface/cbs/businessmgrmsg"" xmlns:com=""http://www.huawei.com/bme/cbsinterface/common"" xmlns:bus1=""http://www.huawei.com/bme/cbsinterface/cbs/businessmgr"">
    <soapenv:Header/>
    <soapenv:Body>
        <bus:IntegrationEnquiryRequestMsg>
            <RequestHeader>
                <com:CommandId>IntegrationEnquiry</com:CommandId>
                <com:Version>5.5</com:Version>
                <com:TransactionId>1</com:TransactionId>
                <com:SequenceId>{Mss}</com:SequenceId>
                <com:RequestType>Event</com:RequestType>
                <!--Optional:-->
                <com:SessionEntity>
                    <com:Name>665</com:Name>
                    <com:Password>i3qsntAtJug0YxeTIr+7Ij0gR9Dgz02gTwWd3E9uhfI=</com:Password>
                    <com:RemoteAddress>1.1.1.1</com:RemoteAddress>
                </com:SessionEntity>
                <com:OperatorID>665</com:OperatorID>
                <com:SerialNo>test1425</com:SerialNo>
                <com:Channel>665</com:Channel>
            </RequestHeader>
            <IntegrationEnquiryRequest>
                <bus1:SubscriberNo>{SubscriberNo}</bus1:SubscriberNo>
                <bus1:QueryType>{QueryType}</bus1:QueryType>
            </IntegrationEnquiryRequest>
        </bus:IntegrationEnquiryRequestMsg>
    </soapenv:Body>
</soapenv:Envelope>";
        }

        public static string RechargeByBrokerSoapRequest(string PrimaryIdentity, string Amount, string Mss, string BrokerId, string RechargeChannelId, string TradeType, string BeId)
        {
            //This string was made using the AR services wsdl file. DO NOT CHANGE
            return @$"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ars=""http://www.huawei.com/bme/cbsinterface/arservices"" xmlns:cbs=""http://www.huawei.com/bme/cbsinterface/cbscommon"" xmlns:arc=""http://cbs.huawei.com/ar/wsservice/arcommon"">
   <soapenv:Header/>
   <soapenv:Body>
      <ars:RechargeRequestMsg>
         <RequestHeader>
            <cbs:Version>5.5</cbs:Version>
            <cbs:BusinessCode>Recharge</cbs:BusinessCode>
            <cbs:MessageSeq>{Mss}</cbs:MessageSeq>
            <cbs:OwnershipInfo>
               <cbs:BEID>{BeId}</cbs:BEID>
            </cbs:OwnershipInfo>
            <cbs:AccessSecurity>
               <cbs:LoginSystemCode>{LoginSystemCode}</cbs:LoginSystemCode>
               <cbs:Password>{Password}</cbs:Password>
            </cbs:AccessSecurity>
            <cbs:OperatorInfo>
               <cbs:OperatorID>{LoginSystemCode}</cbs:OperatorID>
               <cbs:ChannelID>{LoginSystemCode}</cbs:ChannelID>
            </cbs:OperatorInfo>
         </RequestHeader>
         <RechargeRequest>
            <ars:RechargeType>{TradeType}</ars:RechargeType>
            <ars:RechargeChannelID>{RechargeChannelId}</ars:RechargeChannelID>
            <ars:RechargeObj>
               <ars:AcctAccessCode>
                  <arc:PrimaryIdentity>{PrimaryIdentity}</arc:PrimaryIdentity>
               </ars:AcctAccessCode>
            </ars:RechargeObj>
            <ars:RechargeInfo>
               <ars:CashPayment>
                  <ars:Amount>{Amount}</ars:Amount>
               </ars:CashPayment>
            </ars:RechargeInfo>
            <ars:AdditionalProperty>
               <arc:Code>AgentName</arc:Code>
               <arc:Value>{BrokerId}</arc:Value>
            </ars:AdditionalProperty>
         </RechargeRequest>
      </ars:RechargeRequestMsg>
   </soapenv:Body>
</soapenv:Envelope>";
        }

        public static string ChangeSubscriberOfferingRequest(ChangeSubscribersOfferingAddTcpRequest model)
        {
            string diy = string.Empty;
            string sponserNumber = string.Empty;

            if (!string.IsNullOrEmpty(model.Data) || !string.IsNullOrEmpty(model.Voice) || !string.IsNullOrEmpty(model.SMS))
            {
                diy = $@"
                  <cmt:offeringProps>
                     <cmt:propCode>C_DIY_DATA_RANK</cmt:propCode>
                     <cmt:complexFlag>N</cmt:complexFlag>
                     <cmt:value>{model.Data ?? "0"}</cmt:value>
                  </cmt:offeringProps>
                  <cmt:offeringProps>
                     <cmt:propCode>C_DIY_VOICE_RANK</cmt:propCode>
                     <cmt:complexFlag>N</cmt:complexFlag>
                     <cmt:value>{model.Voice ?? "0"}</cmt:value>
                  </cmt:offeringProps>
                  <cmt:offeringProps>
                     <cmt:propCode>C_DIY_SMS_RANK</cmt:propCode>
                     <cmt:complexFlag>N</cmt:complexFlag>
                     <cmt:value>{model.SMS ?? "0"}</cmt:value>
                  </cmt:offeringProps>
               ";
            }

            if (!string.IsNullOrEmpty(model.SponserMsisdn)
                && model.SponserMsisdn != model.PrimaryIdentity)
            {
                sponserNumber = $@"
                  <cmt:offeringProps>
                     <cmt:propCode>C_BPFO_NUMBER</cmt:propCode>
                     <cmt:propId>202503050341000</cmt:propId>
                     <cmt:complexFlag>N</cmt:complexFlag>
                     <cmt:value>{model.SponserMsisdn}</cmt:value>
                  </cmt:offeringProps>
               ";
            }

            return $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:cms=""http://www.huawei.com/bes/crminterface/cmservices"" xmlns:crm=""http://www.huawei.com/bes/crminterface/crmheader"" xmlns:cmt=""http://www.huawei.com/bes/crminterface/cmtype"" xmlns:ord=""http://bes.huawei.com/om/order/fundation/intf/orderbasetype/"" xmlns:pay=""http://bes.huawei.com/om/payment/foundation/intf/paymenttype/"">
   <soapenv:Header/>
   <soapenv:Body>
      <cms:changeSubscriberOfferingReqMsg>
         <requestHeader>
            <crm:version>5.5</crm:version>
            <crm:businessCode>ChangeSubscriberOffering</crm:businessCode>
            <crm:messageSeq>{model.Mss}</crm:messageSeq>
            <crm:ownershipInfo>
               <crm:beId>10101</crm:beId>
            </crm:ownershipInfo>
            <crm:accessSecurity>
               <crm:loginSystemCode>{LoginSystemCode}</crm:loginSystemCode>
               <crm:password>{Password}</crm:password>
            </crm:accessSecurity>
            <crm:operatorInfo>
               <crm:operatorId>{LoginSystemCode}</crm:operatorId>
            </crm:operatorInfo>
            <crm:channelType>{LoginSystemCode}</crm:channelType>
            <crm:msgLanguageCode>fa_IR</crm:msgLanguageCode>
         </requestHeader>
         <changeSubscriberOfferingRequest>
            <cms:subAccessCode>
               <cms:primaryIdentity>{model.PrimaryIdentity}</cms:primaryIdentity>
            </cms:subAccessCode>
            <cms:supplementaryOffering>
               <cms:addOffering>
                  <cmt:offeringId>{model.OfferingId}</cmt:offeringId>
                  <cmt:offeringProps>
                     <cmt:propCode>C_PAID_SUBSCRIBER_NO</cmt:propCode>
                     <cmt:propId>20200913110200</cmt:propId>
                     <cmt:complexFlag>N</cmt:complexFlag>
                     <cmt:value>{model.BrokerId}</cmt:value>
                  </cmt:offeringProps>{sponserNumber ?? ""}
                  <cmt:offeringProps>
                     <cmt:propCode>C_FIRST_CYCLE_DISCOUNT</cmt:propCode>
                     <cmt:propId>20200913110201</cmt:propId>
                     <cmt:complexFlag>N</cmt:complexFlag>
                     <cmt:value>{model.CycleDiscount}</cmt:value>
                  </cmt:offeringProps>{diy ?? ""}
               </cms:addOffering>
            </cms:supplementaryOffering>
         </changeSubscriberOfferingRequest>
      </cms:changeSubscriberOfferingReqMsg>
   </soapenv:Body>
</soapenv:Envelope>";
        }

        public static string DeleteOfferingRequest(string PrimaryIdentity, string OfferingId, string Mss)
        {
            return @$"<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/""
    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
    xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
    <soap:Body>
        <changeSubscriberOfferingReqMsg xmlns=""http://www.huawei.com/bes/crminterface/cmservices"">
            <requestHeader xmlns="""">
                <version xmlns=""http://www.huawei.com/bes/crminterface/crmheader"">5.5</version>
                <businessCode xmlns=""http://www.huawei.com/bes/crminterface/crmheader"">ChangeSubscriberOffering</businessCode>
                <messageSeq xmlns=""http://www.huawei.com/bes/crminterface/crmheader"">{Mss}</messageSeq>
                <ownershipInfo xmlns=""http://www.huawei.com/bes/crminterface/crmheader"">
                    <beId>10101</beId>
                </ownershipInfo>
                <accessSecurity xmlns=""http://www.huawei.com/bes/crminterface/crmheader"">
                    <loginSystemCode>{LoginSystemCode}</loginSystemCode>
                    <password>{Password}</password>
                </accessSecurity>
                <operatorInfo xmlns=""http://www.huawei.com/bes/crminterface/crmheader"">
                    <operatorId>{LoginSystemCode}</operatorId>
                </operatorInfo>
                <channelType xmlns=""http://www.huawei.com/bes/crminterface/crmheader"">{LoginSystemCode}</channelType>
            </requestHeader>
            <changeSubscriberOfferingRequest xmlns="""">
                <subAccessCode xmlns=""http://www.huawei.com/bes/crminterface/cmservices"">
                    <primaryIdentity>{PrimaryIdentity}</primaryIdentity>
                </subAccessCode>
                <supplementaryOffering xmlns=""http://www.huawei.com/bes/crminterface/cmservices"">
                     <delOffering>{OfferingId}</delOffering>
                </supplementaryOffering>
            </changeSubscriberOfferingRequest>
        </changeSubscriberOfferingReqMsg>
    </soap:Body>
</soap:Envelope>";
        }

        public static string QuerySubscriber(string PrimaryIdentity, string Mss, string IncludeOfferFlag, string IncludeHistoryFlag, string IncludeProdFlag, string IncludeContractFlag)
        {
            return @$"<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
    <soap:Body>
        <querySubscriberReqMsg xmlns=""http://www.huawei.com/bes/crminterface/cmservices"">
            <requestHeader xmlns="""">
                <version xmlns=""http://www.huawei.com/bes/crminterface/crmheader"">5.5</version>
                <businessCode xmlns=""http://www.huawei.com/bes/crminterface/crmheader"">QuerySubscriber</businessCode>
                <messageSeq xmlns=""http://www.huawei.com/bes/crminterface/crmheader"">{Mss}</messageSeq>
                <ownershipInfo xmlns=""http://www.huawei.com/bes/crminterface/crmheader"">
                    <beId>10101</beId>
                </ownershipInfo>
                <accessSecurity xmlns=""http://www.huawei.com/bes/crminterface/crmheader"">
                    <loginSystemCode>{LoginSystemCode}</loginSystemCode>
                    <password>{Password}</password>
                </accessSecurity>
                <operatorInfo xmlns=""http://www.huawei.com/bes/crminterface/crmheader"">
                    <operatorId>{LoginSystemCode}</operatorId>
                </operatorInfo>
                <channelType xmlns=""http://www.huawei.com/bes/crminterface/crmheader"">{LoginSystemCode}</channelType>
            </requestHeader>
            <querySubscriberRequest xmlns="""">
                <queryObj xmlns=""http://www.huawei.com/bes/crminterface/cmservices"">
                    <subAccessCode>
                        <primaryIdentity>{PrimaryIdentity}</primaryIdentity>
                    </subAccessCode>
                </queryObj>
                <includeObj xmlns=""http://www.huawei.com/bes/crminterface/cmservices"">
                    <includeOfferFlag>{IncludeOfferFlag}</includeOfferFlag>
                    <includeHistoryFlag>{IncludeHistoryFlag}</includeHistoryFlag>
                    <includeProdFlag>{IncludeProdFlag}</includeProdFlag>
                    <includeContractFlag>{IncludeContractFlag}</includeContractFlag>
                </includeObj>
            </querySubscriberRequest>
        </querySubscriberReqMsg>
    </soap:Body>
</soap:Envelope>";
        }

        public static string QueryCustomerInfo(string primaryIdentity, string messageSeq)
        {
            return @$"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:cms=""http://www.huawei.com/bes/crminterface/cmservices"" xmlns:crm=""http://www.huawei.com/bes/crminterface/crmheader"">
   <soapenv:Header/>
   <soapenv:Body>
      <cms:queryCustomerInfoReqMsg>
         <requestHeader>
            <crm:version>5.5</crm:version>
            <crm:businessCode>QueryCustomerInfo</crm:businessCode>
            <crm:messageSeq>{messageSeq}</crm:messageSeq>
            <crm:ownershipInfo>
               <crm:beId>10101</crm:beId>
               <crm:regionId>10101</crm:regionId>
            </crm:ownershipInfo>
            <crm:accessSecurity>
               <crm:loginSystemCode>{LoginSystemCode}</crm:loginSystemCode>
               <crm:password>{Password}</crm:password>
               <crm:remoteIp></crm:remoteIp>
            </crm:accessSecurity>
            <crm:operatorInfo>
               <crm:operatorId>{LoginSystemCode}</crm:operatorId>
               <crm:orgId>10101</crm:orgId>
            </crm:operatorInfo>
            <crm:channelType>{LoginSystemCode}</crm:channelType>
         </requestHeader>
        <queryCustomerInfoRequest>
        <cms:queryObj>
          <cms:subAccessCode>
            <cms:primaryIdentity>{primaryIdentity}</cms:primaryIdentity>
          </cms:subAccessCode>
        </cms:queryObj>
        <cms:includeObj>
          <cms:includeDeactivationFlag>Y</cms:includeDeactivationFlag>
          <cms:includeDefaultAcctFlag>Y</cms:includeDefaultAcctFlag>
          <cms:includeAddrFlag>Y</cms:includeAddrFlag>
          <cms:includePartyCertFlag>Y</cms:includePartyCertFlag>
        </cms:includeObj>
      </queryCustomerInfoRequest>
      </cms:queryCustomerInfoReqMsg>
   </soapenv:Body>
</soapenv:Envelope>";
        }

        public static string QuerySubscriberCZ2Request(string PrimaryIdentity, string Mss, string OttFlag = "N", string ContractFlag = "Y", string HistoryFlag = "Y", string OfferFlag = "Y", string ProdFlag = "Y", string divertFlag = "N")
        {


            string ottOffer = @"
            <cms:AdditionalProperty>
               <com:code>OTT_OFFER_DESCRIPTION</com:code>
               <com:value>1</com:value>
            </cms:AdditionalProperty>";

            string callDivert = @"
            <cms:AdditionalProperty>
               <com:code>CALL_DIVERT</com:code>
               <com:value>Y</com:value>
            </cms:AdditionalProperty>";


            return $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:cms=""http://www.huawei.com/bes/crminterface/cmservices"" xmlns:crm=""http://www.huawei.com/bes/crminterface/crmheader"" xmlns:com=""http://www.huawei.com/bes/crminterface/common"">
   <soapenv:Header/>
   <soapenv:Body>
      <cms:querySubscriberCZ2ReqMsg>
         <requestHeader>
            <crm:version>5.5</crm:version>
            <crm:businessCode>QuerySubscriberCZ2</crm:businessCode>
            <crm:messageSeq>{Mss}</crm:messageSeq>
            <crm:ownershipInfo>
               <crm:beId>10101</crm:beId>
               <crm:regionId>10101</crm:regionId>
            </crm:ownershipInfo>
            <crm:accessSecurity>
               <crm:loginSystemCode>{LoginSystemCode}</crm:loginSystemCode>
               <crm:password>{Password}</crm:password>
            </crm:accessSecurity>
            <crm:operatorInfo>
               <crm:operatorId>{LoginSystemCode}</crm:operatorId>
               <crm:orgId>{LoginSystemCode}</crm:orgId>
            </crm:operatorInfo>
            <crm:channelType>{LoginSystemCode}</crm:channelType>
            <crm:msgLanguageCode>fa_IR</crm:msgLanguageCode>
         </requestHeader>
         <querySubscriberCZ2Request>
            <cms:queryObj>
               <cms:subAccessCode>
                  <cms:primaryIdentity>{PrimaryIdentity}</cms:primaryIdentity>
               </cms:subAccessCode>
            </cms:queryObj>{(OttFlag.ToUpper().Equals("Y") ? ottOffer : "")}{(divertFlag.ToUpper().Equals("Y") ? callDivert : "")}
            <cms:includeObj>
               <cms:includeOfferFlag>{OfferFlag}</cms:includeOfferFlag>
               <cms:includeHistoryFlag>{HistoryFlag}</cms:includeHistoryFlag>
               <cms:includeProdFlag>{ProdFlag}</cms:includeProdFlag>
               <cms:includeContractFlag>{ContractFlag}</cms:includeContractFlag>
            </cms:includeObj>
         </querySubscriberCZ2Request>
      </cms:querySubscriberCZ2ReqMsg>
   </soapenv:Body>
</soapenv:Envelope>";
        }


        public static string CheckOfferEligibilityRequest(string PrimaryIdentity, string Mss, string OfferId)
        {
            return $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:cms=""http://www.huawei.com/bes/crminterface/cmservices"" xmlns:crm=""http://www.huawei.com/bes/crminterface/crmheader"" xmlns:com=""http://www.huawei.com/bes/crminterface/common"">
   <soapenv:Header/>
   <soapenv:Body>
      <cms:CheckOfferingEligibilityReqMsg>
         <requestHeader>
            <crm:version>5.5</crm:version>
            <crm:businessCode>CheckOfferEligibility</crm:businessCode>
            <crm:messageSeq>{Mss}</crm:messageSeq>
            <crm:ownershipInfo>
               <crm:beId>10101</crm:beId>
               <crm:regionId>10101</crm:regionId>
            </crm:ownershipInfo>
            <crm:accessSecurity>
               <crm:loginSystemCode>{LoginSystemCode}</crm:loginSystemCode>
               <crm:password>{Password}</crm:password>
            </crm:accessSecurity>
            <crm:operatorInfo>
               <crm:operatorId>{LoginSystemCode}</crm:operatorId>
               <crm:orgId>{LoginSystemCode}</crm:orgId>
            </crm:operatorInfo>
            <crm:channelType>{LoginSystemCode}</crm:channelType>
            <crm:msgLanguageCode>fa_IR</crm:msgLanguageCode>
         </requestHeader>
         <CheckOfferingEligibilityRequest>
            <cms:queryObj>
               <cms:subAccessCode>
                  <cms:primaryIdentity>{PrimaryIdentity}</cms:primaryIdentity>
               </cms:subAccessCode>
            </cms:queryObj>
            <cms:OfferID>{OfferId}</cms:OfferID>
         </CheckOfferingEligibilityRequest>
      </cms:CheckOfferingEligibilityReqMsg>
    </soapenv:Body>
</soapenv:Envelope>";
        }

        public static string QueryRelationOfferingRequest(string PrimaryIdentity, string Mss, string RelationType, string OfferingId)
        {
            return $@"<ns0:Envelope xmlns:ns0=""http://schemas.xmlsoap.org/soap/envelope/"">
   <ns0:Header/>
   <ns0:Body xmlns:cms=""http://www.huawei.com/bes/crminterface/cmservices"">
		<cms:QueryRelationOfferingReqMsg>
         <requestHeader xmlns:crm=""http://www.huawei.com/bes/crminterface/crmheader"">
				<crm:version>5.5</crm:version>
				<crm:businessCode>QueryRelationOffering</crm:businessCode>
				<crm:messageSeq>{Mss}</crm:messageSeq>
				<crm:ownershipInfo>
					<crm:beId>10101</crm:beId>
					<crm:regionId>10101</crm:regionId>
				</crm:ownershipInfo>
				<crm:accessSecurity>
					<crm:loginSystemCode>{LoginSystemCode}</crm:loginSystemCode>
					<crm:password>{Password}</crm:password>
               <crm:remoteIp>10.10.10.1</crm:remoteIp>
				</crm:accessSecurity>
				<crm:operatorInfo>
					<crm:operatorId>{LoginSystemCode}</crm:operatorId>
					<crm:orgId>{LoginSystemCode}</crm:orgId>
				</crm:operatorInfo>
				<crm:channelType>{LoginSystemCode}</crm:channelType>
				<crm:msgLanguageCode>fa_IR</crm:msgLanguageCode>
            <crm:additionalProperty>
               <crm:code>Code1</crm:code>
               <crm:value>Value1</crm:value>
            </crm:additionalProperty>
			</requestHeader>
			<QueryRelationOfferingRequest>
				<cms:OfferingID>{OfferingId}</cms:OfferingID>
				<cms:RelationType>{RelationType}</cms:RelationType>
				<cms:MSISDN>{PrimaryIdentity}</cms:MSISDN>
			</QueryRelationOfferingRequest>
		</cms:QueryRelationOfferingReqMsg>
   </ns0:Body>
</ns0:Envelope>";
        }
    }
}