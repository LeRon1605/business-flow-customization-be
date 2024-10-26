﻿// <auto-generated />
using System;
using BusinessFlow.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessFlow.Infrastructure.EfCore.Migrations
{
    [DbContext(typeof(BusinessFlowDbContext))]
    [Migration("20240519033800_AddSubmissionExecutionCompletedBy")]
    partial class AddSubmissionExecutionCompletedBy
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowBlock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BusinessFlowVersionId")
                        .HasColumnType("int");

                    b.Property<int?>("FormId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BusinessFlowVersionId");

                    b.ToTable("BusinessFlowBlock");
                });

            modelBuilder.Entity("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowBlockPersonInChargeSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("BusinessFlowBlockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessFlowBlockId");

                    b.ToTable("BusinessFlowBlockPersonInChargeSetting");
                });

            modelBuilder.Entity("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowBlockTaskSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("BusinessFlowBlockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Index")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessFlowBlockId");

                    b.ToTable("BusinessFlowBlockTaskSetting");
                });

            modelBuilder.Entity("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowBranch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("FromBlockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OutComeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ToBlockId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FromBlockId");

                    b.HasIndex("OutComeId");

                    b.HasIndex("ToBlockId");

                    b.ToTable("BusinessFlowBranch");
                });

            modelBuilder.Entity("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowOutCome", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BusinessFlowBlockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessFlowBlockId");

                    b.ToTable("BusinessFlowOutCome");
                });

            modelBuilder.Entity("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SpaceId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpaceId");

                    b.ToTable("BusinessFlowVersion");
                });

            modelBuilder.Entity("BusinessFlow.Domain.SpaceAggregate.Entities.Space", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Space");
                });

            modelBuilder.Entity("BusinessFlow.Domain.SpaceAggregate.Entities.SpaceMember", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SpaceId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "SpaceId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("SpaceId");

                    b.ToTable("SpaceMember");
                });

            modelBuilder.Entity("BusinessFlow.Domain.SpaceAggregate.Entities.SpaceRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SpaceRole");
                });

            modelBuilder.Entity("BusinessFlow.Domain.SubmissionExecutionAggregate.Entities.SubmissionExecution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("BusinessFlowBlockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CompletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CompletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OutComeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("SubmissionId")
                        .HasColumnType("int");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BusinessFlowBlockId");

                    b.HasIndex("OutComeId");

                    b.ToTable("SubmissionExecution");
                });

            modelBuilder.Entity("BusinessFlow.Domain.SubmissionExecutionAggregate.Entities.SubmissionExecutionPersonInCharge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExecutionId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExecutionId");

                    b.ToTable("SubmissionExecutionPersonInCharge");
                });

            modelBuilder.Entity("BusinessFlow.Domain.SubmissionExecutionAggregate.Entities.SubmissionExecutionTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExecutionId")
                        .HasColumnType("int");

                    b.Property<double>("Index")
                        .HasColumnType("float");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExecutionId");

                    b.ToTable("SubmissionExecutionTask");
                });

            modelBuilder.Entity("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowBlock", b =>
                {
                    b.HasOne("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowVersion", "BusinessFlowVersion")
                        .WithMany("Blocks")
                        .HasForeignKey("BusinessFlowVersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusinessFlowVersion");
                });

            modelBuilder.Entity("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowBlockPersonInChargeSetting", b =>
                {
                    b.HasOne("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowBlock", "BusinessFlowBlock")
                        .WithMany("PersonInChargeSettings")
                        .HasForeignKey("BusinessFlowBlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusinessFlowBlock");
                });

            modelBuilder.Entity("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowBlockTaskSetting", b =>
                {
                    b.HasOne("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowBlock", "BusinessFlowBlock")
                        .WithMany("TaskSettings")
                        .HasForeignKey("BusinessFlowBlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusinessFlowBlock");
                });

            modelBuilder.Entity("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowBranch", b =>
                {
                    b.HasOne("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowBlock", "FromBlock")
                        .WithMany("FromBranches")
                        .HasForeignKey("FromBlockId")
                        .IsRequired();

                    b.HasOne("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowOutCome", "OutCome")
                        .WithMany("Branches")
                        .HasForeignKey("OutComeId");

                    b.HasOne("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowBlock", "ToBlock")
                        .WithMany("ToBranches")
                        .HasForeignKey("ToBlockId")
                        .IsRequired();

                    b.Navigation("FromBlock");

                    b.Navigation("OutCome");

                    b.Navigation("ToBlock");
                });

            modelBuilder.Entity("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowOutCome", b =>
                {
                    b.HasOne("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowBlock", "BusinessFlowBlock")
                        .WithMany("OutComes")
                        .HasForeignKey("BusinessFlowBlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusinessFlowBlock");
                });

            modelBuilder.Entity("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowVersion", b =>
                {
                    b.HasOne("BusinessFlow.Domain.SpaceAggregate.Entities.Space", "Space")
                        .WithMany("BusinessFlowVersions")
                        .HasForeignKey("SpaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Space");
                });

            modelBuilder.Entity("BusinessFlow.Domain.SpaceAggregate.Entities.SpaceMember", b =>
                {
                    b.HasOne("BusinessFlow.Domain.SpaceAggregate.Entities.SpaceRole", "Role")
                        .WithMany("Members")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessFlow.Domain.SpaceAggregate.Entities.Space", "Space")
                        .WithMany("Members")
                        .HasForeignKey("SpaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Space");
                });

            modelBuilder.Entity("BusinessFlow.Domain.SubmissionExecutionAggregate.Entities.SubmissionExecution", b =>
                {
                    b.HasOne("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowBlock", "BusinessFlowBlock")
                        .WithMany("Executions")
                        .HasForeignKey("BusinessFlowBlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowOutCome", "OutCome")
                        .WithMany("Executions")
                        .HasForeignKey("OutComeId");

                    b.Navigation("BusinessFlowBlock");

                    b.Navigation("OutCome");
                });

            modelBuilder.Entity("BusinessFlow.Domain.SubmissionExecutionAggregate.Entities.SubmissionExecutionPersonInCharge", b =>
                {
                    b.HasOne("BusinessFlow.Domain.SubmissionExecutionAggregate.Entities.SubmissionExecution", "Execution")
                        .WithMany("PersonInCharges")
                        .HasForeignKey("ExecutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Execution");
                });

            modelBuilder.Entity("BusinessFlow.Domain.SubmissionExecutionAggregate.Entities.SubmissionExecutionTask", b =>
                {
                    b.HasOne("BusinessFlow.Domain.SubmissionExecutionAggregate.Entities.SubmissionExecution", "Execution")
                        .WithMany("Tasks")
                        .HasForeignKey("ExecutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Execution");
                });

            modelBuilder.Entity("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowBlock", b =>
                {
                    b.Navigation("Executions");

                    b.Navigation("FromBranches");

                    b.Navigation("OutComes");

                    b.Navigation("PersonInChargeSettings");

                    b.Navigation("TaskSettings");

                    b.Navigation("ToBranches");
                });

            modelBuilder.Entity("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowOutCome", b =>
                {
                    b.Navigation("Branches");

                    b.Navigation("Executions");
                });

            modelBuilder.Entity("BusinessFlow.Domain.BusinessFlowAggregate.Entities.BusinessFlowVersion", b =>
                {
                    b.Navigation("Blocks");
                });

            modelBuilder.Entity("BusinessFlow.Domain.SpaceAggregate.Entities.Space", b =>
                {
                    b.Navigation("BusinessFlowVersions");

                    b.Navigation("Members");
                });

            modelBuilder.Entity("BusinessFlow.Domain.SpaceAggregate.Entities.SpaceRole", b =>
                {
                    b.Navigation("Members");
                });

            modelBuilder.Entity("BusinessFlow.Domain.SubmissionExecutionAggregate.Entities.SubmissionExecution", b =>
                {
                    b.Navigation("PersonInCharges");

                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
