﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp.Lib;

#nullable disable

namespace WebApp.Migrations
{
    [DbContext(typeof(WebAppContext))]
    [Migration("20230625182420_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.8");

            modelBuilder.Entity("WebApp.Models.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Characters")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("HiddenAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SeenLock")
                        .HasColumnType("INTEGER");

                    b.HasKey("SubjectId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("WebApp.Models.SubjectMeaning", b =>
                {
                    b.Property<int>("SubjectMeaningId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AcceptedAnswer")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Meaning")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Primary")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SubjectId")
                        .HasColumnType("INTEGER");

                    b.HasKey("SubjectMeaningId");

                    b.HasIndex("SubjectId");

                    b.ToTable("SubjectMeanings");
                });

            modelBuilder.Entity("WebApp.Models.SubjectReading", b =>
                {
                    b.Property<int>("SubjectReadingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AcceptedAnswer")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Primary")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Reading")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SubjectId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("SubjectReadingId");

                    b.HasIndex("SubjectId");

                    b.ToTable("SubjectReadings");
                });

            modelBuilder.Entity("WebApp.Models.SubjectMeaning", b =>
                {
                    b.HasOne("WebApp.Models.Subject", "Subject")
                        .WithMany("Meanings")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("WebApp.Models.SubjectReading", b =>
                {
                    b.HasOne("WebApp.Models.Subject", "Subject")
                        .WithMany("Readings")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("WebApp.Models.Subject", b =>
                {
                    b.Navigation("Meanings");

                    b.Navigation("Readings");
                });
#pragma warning restore 612, 618
        }
    }
}
