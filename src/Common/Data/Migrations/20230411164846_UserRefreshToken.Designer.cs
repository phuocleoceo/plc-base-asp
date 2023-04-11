﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlcBase.Common.Data.Context;

#nullable disable

namespace plcbase.Common.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230411164846_UserRefreshToken")]
    partial class UserRefreshToken
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
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
                        .HasColumnType("longtext")
                        .HasColumnName("identity_number");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext")
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
