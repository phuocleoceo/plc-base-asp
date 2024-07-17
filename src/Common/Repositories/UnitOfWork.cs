using Microsoft.EntityFrameworkCore.Storage;
using AutoMapper;

using PlcBase.Features.AccessControl.Repositories;
using PlcBase.Features.ConfigSetting.Repositories;
using PlcBase.Features.ProjectMember.Repositories;
using PlcBase.Features.ProjectAccess.Repositories;
using PlcBase.Features.ProjectStatus.Repositories;
using PlcBase.Features.Invitation.Repositories;
using PlcBase.Features.Address.Repositories;
using PlcBase.Features.Payment.Repositories;
using PlcBase.Features.Project.Repositories;
using PlcBase.Features.Sprint.Repositories;
using PlcBase.Features.Event.Repositories;
using PlcBase.Features.Issue.Repositories;
using PlcBase.Features.Media.Repositories;
using PlcBase.Features.User.Repositories;
using PlcBase.Common.Data.Context;

namespace PlcBase.Common.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;
    private IDbContextTransaction _transaction;

    public UnitOfWork(DataContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;

        DapperContainer = new DapperContainer(_db, _mapper);
        AddressProvince = new AddressProvinceRepository(_db, _mapper);
        AddressDistrict = new AddressDistrictRepository(_db, _mapper);
        AddressWard = new AddressWardRepository(_db, _mapper);
        UserAccount = new UserAccountRepository(_db, _mapper);
        UserProfile = new UserProfileRepository(_db, _mapper);
        Role = new RoleRepository(_db, _mapper);
        Permission = new PermissionRepository(_db, _mapper);
        ConfigSetting = new ConfigSettingRepository(_db, _mapper);
        Media = new MediaRepository(_db, _mapper);
        Project = new ProjectRepository(_db, _mapper);
        ProjectMember = new ProjectMemberRepository(_db, _mapper);
        ProjectStatus = new ProjectStatusRepository(_db, _mapper);
        Invitation = new InvitationRepository(_db, _mapper);
        Sprint = new SprintRepository(_db, _mapper);
        Issue = new IssueRepository(_db, _mapper);
        IssueComment = new IssueCommentRepository(_db, _mapper);
        ProjectRole = new ProjectRoleRepository(_db, _mapper);
        ProjectPermission = new ProjectPermissionRepository(_db, _mapper);
        MemberRole = new MemberRoleRepository(_db, _mapper);
        Event = new EventRepository(_db, _mapper);
        EventAttendee = new EventAttendeeRepository(_db, _mapper);
        Payment = new PaymentRepository(_db, _mapper);
    }

    public IDapperContainer DapperContainer { get; }
    public IAddressProvinceRepository AddressProvince { get; }
    public IAddressDistrictRepository AddressDistrict { get; }
    public IAddressWardRepository AddressWard { get; }
    public IUserAccountRepository UserAccount { get; }
    public IUserProfileRepository UserProfile { get; }
    public IRoleRepository Role { get; }
    public IPermissionRepository Permission { get; }
    public IConfigSettingRepository ConfigSetting { get; }
    public IMediaRepository Media { get; }
    public IProjectRepository Project { get; }
    public IProjectMemberRepository ProjectMember { get; }
    public IProjectStatusRepository ProjectStatus { get; }
    public IInvitationRepository Invitation { get; }
    public ISprintRepository Sprint { get; }
    public IIssueRepository Issue { get; }
    public IIssueCommentRepository IssueComment { get; }
    public IProjectRoleRepository ProjectRole { get; }
    public IProjectPermissionRepository ProjectPermission { get; }
    public IMemberRoleRepository MemberRole { get; }
    public IEventRepository Event { get; }
    public IEventAttendeeRepository EventAttendee { get; }
    public IPaymentRepository Payment { get; }

    public void Dispose()
    {
        _db.Dispose();
    }

    public async Task<bool> Save()
    {
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task CreateTransaction()
    {
        if (_transaction != null)
            return;

        _transaction = await _db.Database.BeginTransactionAsync();
    }

    public async Task CommitTransaction()
    {
        if (_transaction == null)
            return;

        await _transaction.CommitAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task AbortTransaction()
    {
        if (_transaction == null)
            return;

        await _transaction.RollbackAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }
}
