using AutoMapper;

using PlcBase.Features.Payment.Entities;

namespace PlcBase.Features.Payment.DTOs;

public class PaymentMapping : Profile
{
    public PaymentMapping()
    {
        CreateMap<VNPHistory, PaymentEntity>();
    }
}
