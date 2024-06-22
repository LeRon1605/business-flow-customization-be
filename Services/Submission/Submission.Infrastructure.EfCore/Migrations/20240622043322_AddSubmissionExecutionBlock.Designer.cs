﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Submission.Infrastructure.EfCore;

#nullable disable

namespace Submission.Infrastructure.EfCore.Migrations
{
    [DbContext(typeof(SubmissionDbContext))]
    [Migration("20240622043322_AddSubmissionExecutionBlock")]
    partial class AddSubmissionExecutionBlock
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Submission.Domain.FormAggregate.Entities.Form", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid?>("BusinessFlowBlockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CoverImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsShared")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublicToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SpaceId")
                        .HasColumnType("int");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Form");
                });

            modelBuilder.Entity("Submission.Domain.FormAggregate.Entities.FormElement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FormVersionId")
                        .HasColumnType("int");

                    b.Property<double>("Index")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FormVersionId");

                    b.ToTable("FormElement");
                });

            modelBuilder.Entity("Submission.Domain.FormAggregate.Entities.FormElementSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FormElementId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FormElementId");

                    b.ToTable("FormElementSetting");
                });

            modelBuilder.Entity("Submission.Domain.FormAggregate.Entities.FormVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FormId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FormId");

                    b.ToTable("FormVersion");
                });

            modelBuilder.Entity("Submission.Domain.FormAggregate.Entities.OptionFormElementSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FormElementId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FormElementId");

                    b.ToTable("OptionFormElementSetting");
                });

            modelBuilder.Entity("Submission.Domain.SpaceBusinessFlowAggregate.Entities.SpaceBusinessFlow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BusinessFlowVersionId")
                        .HasColumnType("int");

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

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SpaceBusinessFlow");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.FormSubmission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BusinessFlowVersionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ExecutionId")
                        .HasColumnType("int");

                    b.Property<int>("FormVersionId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.Property<string>("TrackingEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrackingToken")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FormVersionId");

                    b.ToTable("FormSubmission");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.FormSubmissionExecution", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<Guid>("BusinessFlowBlockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("FormSubmissionId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FormSubmissionId")
                        .IsUnique();

                    b.ToTable("FormSubmissionExecution");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.SubmissionAttachmentField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ElementId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubmissionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ElementId");

                    b.HasIndex("SubmissionId");

                    b.ToTable("SubmissionAttachmentField");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.SubmissionAttachmentValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubmissionFieldId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubmissionFieldId");

                    b.ToTable("SubmissionAttachmentValue");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.SubmissionDateValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ElementId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubmissionId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Value")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ElementId");

                    b.HasIndex("SubmissionId");

                    b.ToTable("SubmissionDateValue");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.SubmissionNumberValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ElementId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubmissionId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ElementId");

                    b.HasIndex("SubmissionId");

                    b.ToTable("SubmissionNumberValue");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.SubmissionOptionField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ElementId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubmissionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ElementId");

                    b.HasIndex("SubmissionId");

                    b.ToTable("SubmissionOptionField");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.SubmissionOptionFieldValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OptionId")
                        .HasColumnType("int");

                    b.Property<int>("SubmissionFieldId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OptionId");

                    b.HasIndex("SubmissionFieldId");

                    b.ToTable("SubmissionOptionFieldValue");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.SubmissionTextValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ElementId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubmissionId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ElementId");

                    b.HasIndex("SubmissionId");

                    b.ToTable("SubmissionTextValue");
                });

            modelBuilder.Entity("Submission.Domain.FormAggregate.Entities.FormElement", b =>
                {
                    b.HasOne("Submission.Domain.FormAggregate.Entities.FormVersion", "FormVersion")
                        .WithMany("Elements")
                        .HasForeignKey("FormVersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FormVersion");
                });

            modelBuilder.Entity("Submission.Domain.FormAggregate.Entities.FormElementSetting", b =>
                {
                    b.HasOne("Submission.Domain.FormAggregate.Entities.FormElement", "FormElement")
                        .WithMany("Settings")
                        .HasForeignKey("FormElementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FormElement");
                });

            modelBuilder.Entity("Submission.Domain.FormAggregate.Entities.FormVersion", b =>
                {
                    b.HasOne("Submission.Domain.FormAggregate.Entities.Form", "Form")
                        .WithMany("Versions")
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Form");
                });

            modelBuilder.Entity("Submission.Domain.FormAggregate.Entities.OptionFormElementSetting", b =>
                {
                    b.HasOne("Submission.Domain.FormAggregate.Entities.FormElement", "FormElement")
                        .WithMany("Options")
                        .HasForeignKey("FormElementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FormElement");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.FormSubmission", b =>
                {
                    b.HasOne("Submission.Domain.FormAggregate.Entities.FormVersion", "FormVersion")
                        .WithMany("Submissions")
                        .HasForeignKey("FormVersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FormVersion");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.FormSubmissionExecution", b =>
                {
                    b.HasOne("Submission.Domain.SubmissionAggregate.Entities.FormSubmission", "FormSubmission")
                        .WithOne("Execution")
                        .HasForeignKey("Submission.Domain.SubmissionAggregate.Entities.FormSubmissionExecution", "FormSubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FormSubmission");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.SubmissionAttachmentField", b =>
                {
                    b.HasOne("Submission.Domain.FormAggregate.Entities.FormElement", "Element")
                        .WithMany()
                        .HasForeignKey("ElementId")
                        .IsRequired();

                    b.HasOne("Submission.Domain.SubmissionAggregate.Entities.FormSubmission", "Submission")
                        .WithMany("AttachmentFields")
                        .HasForeignKey("SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Element");

                    b.Navigation("Submission");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.SubmissionAttachmentValue", b =>
                {
                    b.HasOne("Submission.Domain.SubmissionAggregate.Entities.SubmissionAttachmentField", "Field")
                        .WithMany("Values")
                        .HasForeignKey("SubmissionFieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Field");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.SubmissionDateValue", b =>
                {
                    b.HasOne("Submission.Domain.FormAggregate.Entities.FormElement", "Element")
                        .WithMany()
                        .HasForeignKey("ElementId")
                        .IsRequired();

                    b.HasOne("Submission.Domain.SubmissionAggregate.Entities.FormSubmission", "Submission")
                        .WithMany("DateFields")
                        .HasForeignKey("SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Element");

                    b.Navigation("Submission");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.SubmissionNumberValue", b =>
                {
                    b.HasOne("Submission.Domain.FormAggregate.Entities.FormElement", "Element")
                        .WithMany()
                        .HasForeignKey("ElementId")
                        .IsRequired();

                    b.HasOne("Submission.Domain.SubmissionAggregate.Entities.FormSubmission", "Submission")
                        .WithMany("NumberFields")
                        .HasForeignKey("SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Element");

                    b.Navigation("Submission");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.SubmissionOptionField", b =>
                {
                    b.HasOne("Submission.Domain.FormAggregate.Entities.FormElement", "Element")
                        .WithMany()
                        .HasForeignKey("ElementId")
                        .IsRequired();

                    b.HasOne("Submission.Domain.SubmissionAggregate.Entities.FormSubmission", "Submission")
                        .WithMany("OptionFields")
                        .HasForeignKey("SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Element");

                    b.Navigation("Submission");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.SubmissionOptionFieldValue", b =>
                {
                    b.HasOne("Submission.Domain.FormAggregate.Entities.OptionFormElementSetting", "Option")
                        .WithMany()
                        .HasForeignKey("OptionId")
                        .IsRequired();

                    b.HasOne("Submission.Domain.SubmissionAggregate.Entities.SubmissionOptionField", "Field")
                        .WithMany("Values")
                        .HasForeignKey("SubmissionFieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Field");

                    b.Navigation("Option");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.SubmissionTextValue", b =>
                {
                    b.HasOne("Submission.Domain.FormAggregate.Entities.FormElement", "Element")
                        .WithMany()
                        .HasForeignKey("ElementId")
                        .IsRequired();

                    b.HasOne("Submission.Domain.SubmissionAggregate.Entities.FormSubmission", "Submission")
                        .WithMany("TextFields")
                        .HasForeignKey("SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Element");

                    b.Navigation("Submission");
                });

            modelBuilder.Entity("Submission.Domain.FormAggregate.Entities.Form", b =>
                {
                    b.Navigation("Versions");
                });

            modelBuilder.Entity("Submission.Domain.FormAggregate.Entities.FormElement", b =>
                {
                    b.Navigation("Options");

                    b.Navigation("Settings");
                });

            modelBuilder.Entity("Submission.Domain.FormAggregate.Entities.FormVersion", b =>
                {
                    b.Navigation("Elements");

                    b.Navigation("Submissions");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.FormSubmission", b =>
                {
                    b.Navigation("AttachmentFields");

                    b.Navigation("DateFields");

                    b.Navigation("Execution");

                    b.Navigation("NumberFields");

                    b.Navigation("OptionFields");

                    b.Navigation("TextFields");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.SubmissionAttachmentField", b =>
                {
                    b.Navigation("Values");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.SubmissionOptionField", b =>
                {
                    b.Navigation("Values");
                });
#pragma warning restore 612, 618
        }
    }
}
