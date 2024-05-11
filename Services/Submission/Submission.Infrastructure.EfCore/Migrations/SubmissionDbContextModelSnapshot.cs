﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Submission.Infrastructure.EfCore;

#nullable disable

namespace Submission.Infrastructure.EfCore.Migrations
{
    [DbContext(typeof(SubmissionDbContext))]
    partial class SubmissionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

                    b.HasKey("Id");

                    b.ToTable("FormSubmission");
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

                    b.HasIndex("SubmissionId");

                    b.ToTable("SubmissionTextValue");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.SubmissionAttachmentField", b =>
                {
                    b.HasOne("Submission.Domain.SubmissionAggregate.Entities.FormSubmission", "Submission")
                        .WithMany("AttachmentFields")
                        .HasForeignKey("SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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
                    b.HasOne("Submission.Domain.SubmissionAggregate.Entities.FormSubmission", "Submission")
                        .WithMany("DateFields")
                        .HasForeignKey("SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Submission");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.SubmissionNumberValue", b =>
                {
                    b.HasOne("Submission.Domain.SubmissionAggregate.Entities.FormSubmission", "Submission")
                        .WithMany("NumberFields")
                        .HasForeignKey("SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Submission");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.SubmissionOptionField", b =>
                {
                    b.HasOne("Submission.Domain.SubmissionAggregate.Entities.FormSubmission", "Submission")
                        .WithMany("OptionFields")
                        .HasForeignKey("SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Submission");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.SubmissionOptionFieldValue", b =>
                {
                    b.HasOne("Submission.Domain.SubmissionAggregate.Entities.SubmissionOptionField", "Field")
                        .WithMany("Values")
                        .HasForeignKey("SubmissionFieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Field");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.SubmissionTextValue", b =>
                {
                    b.HasOne("Submission.Domain.SubmissionAggregate.Entities.FormSubmission", "Submission")
                        .WithMany("TextFields")
                        .HasForeignKey("SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Submission");
                });

            modelBuilder.Entity("Submission.Domain.SubmissionAggregate.Entities.FormSubmission", b =>
                {
                    b.Navigation("AttachmentFields");

                    b.Navigation("DateFields");

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
