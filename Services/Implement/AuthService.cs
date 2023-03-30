using PlcBase.Repositories.Interface;
using PlcBase.Services.Interface;
using PlcBase.Base.DomainModel;
using PlcBase.Models.Entities;
using PlcBase.Models.DTO;
using AutoMapper;

namespace PlcBase.Services.Implement;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _uof;

    public AuthService(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
    }
}