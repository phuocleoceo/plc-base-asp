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

        DapperContainer = new DapperContainer(_db);
        AddressProvince = new AddressProvinceRepository(_db, _mapper);
        AddressDistrict = new AddressDistrictRepository(_db, _mapper);
        AddressWard = new AddressWardRepository(_db, _mapper);
        UserAccount = new UserAccountRepository(_db, _mapper);
        UserProfile = new UserProfileRepository(_db, _mapper);
        Role = new RoleRepository(_db, _mapper);
        Permisison = new PermissionRepository(_db, _mapper);
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

    public IDapperContainer DapperContainer { get; private set; }
    public IAddressProvinceRepository AddressProvince { get; private set; }
    public IAddressDistrictRepository AddressDistrict { get; private set; }
    public IAddressWardRepository AddressWard { get; private set; }
    public IUserAccountRepository UserAccount { get; private set; }
    public IUserProfileRepository UserProfile { get; private set; }
    public IRoleRepository Role { get; private set; }
    public IPermisisonRepository Permisison { get; private set; }
    public IConfigSettingRepository ConfigSetting { get; private set; }
    public IMediaRepository Media { get; private set; }
    public IProjectRepository Project { get; private set; }
    public IProjectMemberRepository ProjectMember { get; private set; }
    public IProjectStatusRepository ProjectStatus { get; private set; }
    public IInvitationRepository Invitation { get; private set; }
    public ISprintRepository Sprint { get; private set; }
    public IIssueRepository Issue { get; private set; }
    public IIssueCommentRepository IssueComment { get; private set; }
    public IProjectRoleRepository ProjectRole { get; private set; }
    public IProjectPermissionRepository ProjectPermission { get; private set; }
    public IMemberRoleRepository MemberRole { get; private set; }
    public IEventRepository Event { get; private set; }
    public IEventAttendeeRepository EventAttendee { get; private set; }
    public IPaymentRepository Payment { get; private set; }

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
