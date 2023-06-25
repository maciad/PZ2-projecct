﻿// <auto-generated />
using System;
using Filmy.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Filmy.Migrations
{
    [DbContext(typeof(FilmyContext))]
    [Migration("20230625182813_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.8");

            modelBuilder.Entity("Filmy.Models.Aktor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataUrodzenia")
                        .HasColumnType("TEXT");

                    b.Property<string>("Imie")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("KrajPochodzenia")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nazwisko")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Aktor");
                });

            modelBuilder.Entity("Filmy.Models.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AktorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Gatunek")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Ocena")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Rezyser")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RokProdukcji")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tytul")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AktorId");

                    b.ToTable("Film");
                });

            modelBuilder.Entity("Filmy.Models.Ocena", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("FilmId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OcenaWartosc")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UzytkownikId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FilmId");

                    b.HasIndex("UzytkownikId");

                    b.ToTable("Ocena");
                });

            modelBuilder.Entity("Filmy.Models.Uzytkownik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Haslo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Imie")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nazwisko")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Uzytkownik");
                });

            modelBuilder.Entity("Filmy.Models.Film", b =>
                {
                    b.HasOne("Filmy.Models.Aktor", "Aktor")
                        .WithMany("Films")
                        .HasForeignKey("AktorId");

                    b.Navigation("Aktor");
                });

            modelBuilder.Entity("Filmy.Models.Ocena", b =>
                {
                    b.HasOne("Filmy.Models.Film", "Film")
                        .WithMany()
                        .HasForeignKey("FilmId");

                    b.HasOne("Filmy.Models.Uzytkownik", "Uzytkownik")
                        .WithMany("Oceny")
                        .HasForeignKey("UzytkownikId");

                    b.Navigation("Film");

                    b.Navigation("Uzytkownik");
                });

            modelBuilder.Entity("Filmy.Models.Aktor", b =>
                {
                    b.Navigation("Films");
                });

            modelBuilder.Entity("Filmy.Models.Uzytkownik", b =>
                {
                    b.Navigation("Oceny");
                });
#pragma warning restore 612, 618
        }
    }
}
