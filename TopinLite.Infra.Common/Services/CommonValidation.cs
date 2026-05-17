using MediatR;
using Microsoft.Extensions.Logging;
using NatsRpcFoundation.Abstractions;
using Polly;
using System;
using TopinLite.Domain.Commons;
using TopinLite.Domain.HuaweiApiModel.CRMResponses.QuerySubscriber;
using TopinLite.Domain.Messaging;
using TopinLite.Domain.TopinApi;
using TopinLite.Services.Commons;
using static NATS.Client.Core.NatsHeaders;
using DiyPriceRequestModel = TopinLite.Domain.TopinApi.DiyPriceRequestModel;

namespace TopinLite.Infra.Common.Services;

public sealed class CommonValidation : ICommonValidation
{
    
}
