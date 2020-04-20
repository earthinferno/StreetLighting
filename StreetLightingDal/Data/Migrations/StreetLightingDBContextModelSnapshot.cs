﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StreetLightingDal.Data;

namespace StreetLightingDal.Data.Migrations
{
    [DbContext(typeof(StreetLightingDBContext))]
    partial class StreetLightingDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("StreetLightingDal.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AddressLine1")
                        .HasColumnType("TEXT");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("PostCode")
                        .HasColumnType("TEXT");

                    b.Property<int>("RespondentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AddressId");

                    b.HasIndex("RespondentId")
                        .IsUnique();

                    b.ToTable("Address");
                });

            modelBuilder.Entity("StreetLightingDal.Models.Respondent", b =>
                {
                    b.Property<int>("RespondentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("RespondentId");

                    b.ToTable("Respondent");
                });

            modelBuilder.Entity("StreetLightingDal.Models.Response", b =>
                {
                    b.Property<int>("ResponseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BrightnessLevel")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RespondentId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Satisfied")
                        .HasColumnType("INTEGER");

                    b.HasKey("ResponseId");

                    b.HasIndex("RespondentId")
                        .IsUnique();

                    b.ToTable("QuestionnaireResponse");
                });

            modelBuilder.Entity("StreetLightingDal.Models.Address", b =>
                {
                    b.HasOne("StreetLightingDal.Models.Respondent", "Respondent")
                        .WithOne("Address")
                        .HasForeignKey("StreetLightingDal.Models.Address", "RespondentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StreetLightingDal.Models.Response", b =>
                {
                    b.HasOne("StreetLightingDal.Models.Respondent", "Respondent")
                        .WithOne("QuestionnaireResponse")
                        .HasForeignKey("StreetLightingDal.Models.Response", "RespondentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
