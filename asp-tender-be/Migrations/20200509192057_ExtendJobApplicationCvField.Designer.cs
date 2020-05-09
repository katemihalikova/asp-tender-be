﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using asp_tender_be.Data;

namespace asp_tender_be.Migrations
{
    [DbContext(typeof(TenderContext))]
    [Migration("20200509192057_ExtendJobApplicationCvField")]
    partial class ExtendJobApplicationCvField
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("asp_tender_be.Models.JobApplication", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<byte[]>("CvData")
                        .IsRequired();

                    b.Property<string>("CvFileName")
                        .IsRequired();

                    b.Property<string>("CvMimeType")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int?>("JobApplicationAnswerID");

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<int>("PositionID");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.HasKey("ID");

                    b.HasIndex("JobApplicationAnswerID")
                        .IsUnique()
                        .HasFilter("[JobApplicationAnswerID] IS NOT NULL");

                    b.HasIndex("PositionID");

                    b.ToTable("JobApplications");
                });

            modelBuilder.Entity("asp_tender_be.Models.JobApplicationAnswer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Accepted");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.HasKey("ID");

                    b.ToTable("JobApplicationAnswer");
                });

            modelBuilder.Entity("asp_tender_be.Models.Position", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("WorkplaceID");

                    b.HasKey("ID");

                    b.HasIndex("WorkplaceID");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("asp_tender_be.Models.Workplace", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Workplaces");
                });

            modelBuilder.Entity("asp_tender_be.Models.JobApplication", b =>
                {
                    b.HasOne("asp_tender_be.Models.JobApplicationAnswer", "JobApplicationAnswer")
                        .WithOne("JobApplication")
                        .HasForeignKey("asp_tender_be.Models.JobApplication", "JobApplicationAnswerID");

                    b.HasOne("asp_tender_be.Models.Position", "Position")
                        .WithMany("JobApplications")
                        .HasForeignKey("PositionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("asp_tender_be.Models.Position", b =>
                {
                    b.HasOne("asp_tender_be.Models.Workplace", "Workplace")
                        .WithMany("Positions")
                        .HasForeignKey("WorkplaceID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}