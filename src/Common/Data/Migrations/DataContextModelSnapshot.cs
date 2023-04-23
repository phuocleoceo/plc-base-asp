﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlcBase.Common.Data.Context;

#nullable disable

namespace plcbase.Common.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PlcBase.Features.AccessControl.Entities.PermissionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<string>("Key")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("key");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("Key")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("permission");
                });

            modelBuilder.Entity("PlcBase.Features.AccessControl.Entities.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("role");
                });

            modelBuilder.Entity("PlcBase.Features.Address.Entities.AddressDistrictEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("AddressProvinceId")
                        .HasColumnType("int")
                        .HasColumnName("province_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("AddressProvinceId");

                    b.ToTable("address_district");
                });

            modelBuilder.Entity("PlcBase.Features.Address.Entities.AddressProvinceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.ToTable("address_province");
                });

            modelBuilder.Entity("PlcBase.Features.Address.Entities.AddressWardEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("AddressDistrictId")
                        .HasColumnType("int")
                        .HasColumnName("district_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("AddressDistrictId");

                    b.ToTable("address_ward");
                });

            modelBuilder.Entity("PlcBase.Features.ConfigSetting.Entities.ConfigSettingEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<string>("Key")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("key");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.Property<double>("Value")
                        .HasColumnType("double")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.HasIndex("Key")
                        .IsUnique();

                    b.ToTable("config_setting");
                });

            modelBuilder.Entity("PlcBase.Features.Media.Entities.MediaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("ContentType")
                        .HasColumnType("longtext")
                        .HasColumnName("content_type");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<int>("EntityId")
                        .HasColumnType("int")
                        .HasColumnName("entity_id");

                    b.Property<string>("EntityType")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("entity_type");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.Property<string>("Url")
                        .HasColumnType("longtext")
                        .HasColumnName("url");

                    b.HasKey("Id");

                    b.HasIndex("EntityId", "EntityType");

                    b.ToTable("media");
                });

            modelBuilder.Entity("PlcBase.Features.Project.Entities.ProjectEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int")
                        .HasColumnName("creator_id");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Image")
                        .HasColumnType("longtext")
                        .HasColumnName("image");

                    b.Property<string>("Key")
                        .HasColumnType("longtext")
                        .HasColumnName("key");

                    b.Property<string>("Name")
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("project");
                });

            modelBuilder.Entity("PlcBase.Features.ProjectMember.Entities.ProjectMemberEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Image")
                        .HasColumnType("longtext")
                        .HasColumnName("image");

                    b.Property<string>("Name")
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("project_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId", "ProjectId");

                    b.ToTable("project_member");
                });

            modelBuilder.Entity("PlcBase.Features.ProjectStatus.Entities.ProjectStatusEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Name")
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("project_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("project_status");
                });

            modelBuilder.Entity("PlcBase.Features.Sprint.Entities.SprintEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("end_time");

                    b.Property<string>("Goal")
                        .HasColumnType("longtext")
                        .HasColumnName("goal");

                    b.Property<bool>("IsInProgress")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_in_progress");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("project_id");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("start_time");

                    b.Property<string>("Title")
                        .HasColumnType("longtext")
                        .HasColumnName("title");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("sprint");
                });

            modelBuilder.Entity("PlcBase.Features.User.Entities.UserAccountEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("email");

                    b.Property<bool>("IsActived")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_actived");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_verified");

                    b.Property<byte[]>("PasswordHashed")
                        .HasColumnType("longblob")
                        .HasColumnName("password_hashed");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("longblob")
                        .HasColumnName("password_salt");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("longtext")
                        .HasColumnName("refresh_token");

                    b.Property<DateTime?>("RefreshTokenExpiredAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("refresh_token_expired_at");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<string>("SecurityCode")
                        .HasColumnType("longtext")
                        .HasColumnName("security_code");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("user_account");
                });

            modelBuilder.Entity("PlcBase.Features.User.Entities.UserProfileEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasColumnType("longtext")
                        .HasColumnName("address");

                    b.Property<int>("AddressWardId")
                        .HasColumnType("int")
                        .HasColumnName("address_ward_id");

                    b.Property<string>("Avatar")
                        .HasColumnType("longtext")
                        .HasColumnName("avatar");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<double>("CurrentCredit")
                        .HasColumnType("double")
                        .HasColumnName("current_credit");

                    b.Property<string>("DisplayName")
                        .HasColumnType("longtext")
                        .HasColumnName("display_name");

                    b.Property<string>("IdentityNumber")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("identity_number");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("phone_number");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.Property<int>("UserAccountId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("AddressWardId");

                    b.HasIndex("UserAccountId")
                        .IsUnique();

                    b.HasIndex("UserAccountId", "IdentityNumber", "PhoneNumber")
                        .IsUnique();

                    b.ToTable("user_profile");
                });

            modelBuilder.Entity("PlcBase.Features.AccessControl.Entities.PermissionEntity", b =>
                {
                    b.HasOne("PlcBase.Features.AccessControl.Entities.RoleEntity", "Role")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PlcBase.Features.Address.Entities.AddressDistrictEntity", b =>
                {
                    b.HasOne("PlcBase.Features.Address.Entities.AddressProvinceEntity", "AddressProvince")
                        .WithMany("AddressDistricts")
                        .HasForeignKey("AddressProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddressProvince");
                });

            modelBuilder.Entity("PlcBase.Features.Address.Entities.AddressWardEntity", b =>
                {
                    b.HasOne("PlcBase.Features.Address.Entities.AddressDistrictEntity", "AddressDistrict")
                        .WithMany("AddressWards")
                        .HasForeignKey("AddressDistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddressDistrict");
                });

            modelBuilder.Entity("PlcBase.Features.Project.Entities.ProjectEntity", b =>
                {
                    b.HasOne("PlcBase.Features.User.Entities.UserAccountEntity", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("PlcBase.Features.ProjectMember.Entities.ProjectMemberEntity", b =>
                {
                    b.HasOne("PlcBase.Features.Project.Entities.ProjectEntity", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlcBase.Features.User.Entities.UserAccountEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlcBase.Features.ProjectStatus.Entities.ProjectStatusEntity", b =>
                {
                    b.HasOne("PlcBase.Features.Project.Entities.ProjectEntity", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("PlcBase.Features.Sprint.Entities.SprintEntity", b =>
                {
                    b.HasOne("PlcBase.Features.Project.Entities.ProjectEntity", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("PlcBase.Features.User.Entities.UserAccountEntity", b =>
                {
                    b.HasOne("PlcBase.Features.AccessControl.Entities.RoleEntity", "Role")
                        .WithMany("UserAccounts")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PlcBase.Features.User.Entities.UserProfileEntity", b =>
                {
                    b.HasOne("PlcBase.Features.Address.Entities.AddressWardEntity", "AddressWard")
                        .WithMany()
                        .HasForeignKey("AddressWardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlcBase.Features.User.Entities.UserAccountEntity", "UserAccount")
                        .WithOne("UserProfile")
                        .HasForeignKey("PlcBase.Features.User.Entities.UserProfileEntity", "UserAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddressWard");

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("PlcBase.Features.AccessControl.Entities.RoleEntity", b =>
                {
                    b.Navigation("Permissions");

                    b.Navigation("UserAccounts");
                });

            modelBuilder.Entity("PlcBase.Features.Address.Entities.AddressDistrictEntity", b =>
                {
                    b.Navigation("AddressWards");
                });

            modelBuilder.Entity("PlcBase.Features.Address.Entities.AddressProvinceEntity", b =>
                {
                    b.Navigation("AddressDistricts");
                });

            modelBuilder.Entity("PlcBase.Features.User.Entities.UserAccountEntity", b =>
                {
                    b.Navigation("UserProfile");
                });
#pragma warning restore 612, 618
        }
    }
}
