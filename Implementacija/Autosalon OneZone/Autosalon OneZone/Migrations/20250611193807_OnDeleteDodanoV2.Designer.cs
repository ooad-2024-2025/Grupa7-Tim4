﻿// <auto-generated />
using System;
using Autosalon_OneZone.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Autosalon_OneZone.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250611193807_OnDeleteDodanoV2")]
    partial class OnDeleteDodanoV2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Autosalon_OneZone.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Autosalon_OneZone.Models.Kartica", b =>
                {
                    b.Property<int>("KarticaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KarticaID"));

                    b.Property<string>("BrojKartice")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Cvv")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("DatumIsteka")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("ImeVlasnika")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("KarticaID");

                    b.ToTable("Kartice");
                });

            modelBuilder.Entity("Autosalon_OneZone.Models.Korpa", b =>
                {
                    b.Property<int>("KorpaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KorpaID"));

                    b.Property<string>("KorisnikId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("UkupnaCijena")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("KorpaID");

                    b.HasIndex("KorisnikId")
                        .IsUnique();

                    b.ToTable("Korpe");
                });

            modelBuilder.Entity("Autosalon_OneZone.Models.Kredit", b =>
                {
                    b.Property<int>("KreditID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KreditID"));

                    b.Property<int>("BrojRata")
                        .HasColumnType("int");

                    b.Property<decimal>("Iznos")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("KamatnaStopa")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MjesecnaRata")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("KreditID");

                    b.ToTable("Krediti");
                });

            modelBuilder.Entity("Autosalon_OneZone.Models.Narudzba", b =>
                {
                    b.Property<int>("NarudzbaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NarudzbaID"));

                    b.Property<DateTime>("DatumNarudzbe")
                        .HasColumnType("datetime2");

                    b.Property<string>("KorisnikId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("UkupnaCijena")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("NarudzbaID");

                    b.HasIndex("KorisnikId");

                    b.ToTable("Narudzbe");
                });

            modelBuilder.Entity("Autosalon_OneZone.Models.Placanje", b =>
                {
                    b.Property<int>("NarudzbaID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DatumPlacanja")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Iznos")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("KarticaID")
                        .HasColumnType("int");

                    b.Property<int?>("KreditID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NarudzbaID");

                    b.HasIndex("KarticaID");

                    b.HasIndex("KreditID");

                    b.ToTable("Placanja");
                });

            modelBuilder.Entity("Autosalon_OneZone.Models.Podrska", b =>
                {
                    b.Property<int>("UpitID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UpitID"));

                    b.Property<DateTime>("DatumUpita")
                        .HasColumnType("datetime2");

                    b.Property<string>("KorisnikId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Naslov")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Sadrzaj")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UpitID");

                    b.HasIndex("KorisnikId");

                    b.ToTable("PodrskaUpiti");
                });

            modelBuilder.Entity("Autosalon_OneZone.Models.Recenzija", b =>
                {
                    b.Property<int>("RecenzijaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecenzijaID"));

                    b.Property<DateTime>("DatumRecenzije")
                        .HasColumnType("datetime2");

                    b.Property<string>("Komentar")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("KorisnikId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Ocjena")
                        .HasColumnType("int");

                    b.Property<int>("VoziloID")
                        .HasColumnType("int");

                    b.HasKey("RecenzijaID");

                    b.HasIndex("KorisnikId");

                    b.HasIndex("VoziloID");

                    b.ToTable("Recenzije");
                });

            modelBuilder.Entity("Autosalon_OneZone.Models.StavkaKorpe", b =>
                {
                    b.Property<int>("StavkaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StavkaID"));

                    b.Property<decimal>("CijenaStavke")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Kolicina")
                        .HasColumnType("int");

                    b.Property<int?>("KorpaID")
                        .HasColumnType("int");

                    b.Property<int?>("NarudzbaID")
                        .HasColumnType("int");

                    b.Property<int>("VoziloID")
                        .HasColumnType("int");

                    b.HasKey("StavkaID");

                    b.HasIndex("KorpaID");

                    b.HasIndex("NarudzbaID");

                    b.HasIndex("VoziloID");

                    b.ToTable("StavkeKorpe");
                });

            modelBuilder.Entity("Autosalon_OneZone.Models.Vozilo", b =>
                {
                    b.Property<int>("VoziloID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VoziloID"));

                    b.Property<string>("Boja")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Cijena")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Godiste")
                        .HasColumnType("int");

                    b.Property<string>("Gorivo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Kilometraza")
                        .HasColumnType("float");

                    b.Property<decimal?>("Kubikaza")
                        .HasColumnType("decimal(18,1)");

                    b.Property<string>("Marka")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slika")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VoziloID");

                    b.ToTable("Vozila");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Autosalon_OneZone.Models.Korpa", b =>
                {
                    b.HasOne("Autosalon_OneZone.Models.ApplicationUser", "Korisnik")
                        .WithOne("Korpa")
                        .HasForeignKey("Autosalon_OneZone.Models.Korpa", "KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("Autosalon_OneZone.Models.Narudzba", b =>
                {
                    b.HasOne("Autosalon_OneZone.Models.ApplicationUser", "Korisnik")
                        .WithMany("Narudzbe")
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("Autosalon_OneZone.Models.Placanje", b =>
                {
                    b.HasOne("Autosalon_OneZone.Models.Kartica", "Kartica")
                        .WithMany()
                        .HasForeignKey("KarticaID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Autosalon_OneZone.Models.Kredit", "Kredit")
                        .WithMany()
                        .HasForeignKey("KreditID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Autosalon_OneZone.Models.Narudzba", "Narudzba")
                        .WithOne("Placanje")
                        .HasForeignKey("Autosalon_OneZone.Models.Placanje", "NarudzbaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kartica");

                    b.Navigation("Kredit");

                    b.Navigation("Narudzba");
                });

            modelBuilder.Entity("Autosalon_OneZone.Models.Podrska", b =>
                {
                    b.HasOne("Autosalon_OneZone.Models.ApplicationUser", "Korisnik")
                        .WithMany("PodrskaUpiti")
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("Autosalon_OneZone.Models.Recenzija", b =>
                {
                    b.HasOne("Autosalon_OneZone.Models.ApplicationUser", "Korisnik")
                        .WithMany("Recenzije")
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Autosalon_OneZone.Models.Vozilo", "Vozilo")
                        .WithMany("Recenzije")
                        .HasForeignKey("VoziloID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");

                    b.Navigation("Vozilo");
                });

            modelBuilder.Entity("Autosalon_OneZone.Models.StavkaKorpe", b =>
                {
                    b.HasOne("Autosalon_OneZone.Models.Korpa", "Korpa")
                        .WithMany("StavkeKorpe")
                        .HasForeignKey("KorpaID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Autosalon_OneZone.Models.Narudzba", "Narudzba")
                        .WithMany("StavkeKorpe")
                        .HasForeignKey("NarudzbaID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Autosalon_OneZone.Models.Vozilo", "Vozilo")
                        .WithMany("StavkeKorpe")
                        .HasForeignKey("VoziloID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korpa");

                    b.Navigation("Narudzba");

                    b.Navigation("Vozilo");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Autosalon_OneZone.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Autosalon_OneZone.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Autosalon_OneZone.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Autosalon_OneZone.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Autosalon_OneZone.Models.ApplicationUser", b =>
                {
                    b.Navigation("Korpa")
                        .IsRequired();

                    b.Navigation("Narudzbe");

                    b.Navigation("PodrskaUpiti");

                    b.Navigation("Recenzije");
                });

            modelBuilder.Entity("Autosalon_OneZone.Models.Korpa", b =>
                {
                    b.Navigation("StavkeKorpe");
                });

            modelBuilder.Entity("Autosalon_OneZone.Models.Narudzba", b =>
                {
                    b.Navigation("Placanje")
                        .IsRequired();

                    b.Navigation("StavkeKorpe");
                });

            modelBuilder.Entity("Autosalon_OneZone.Models.Vozilo", b =>
                {
                    b.Navigation("Recenzije");

                    b.Navigation("StavkeKorpe");
                });
#pragma warning restore 612, 618
        }
    }
}
