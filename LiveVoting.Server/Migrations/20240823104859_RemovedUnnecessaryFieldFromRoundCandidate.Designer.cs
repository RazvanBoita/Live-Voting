﻿// <auto-generated />
using System;
using LiveVoting.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LiveVoting.Server.Migrations
{
    [DbContext(typeof(VotingDbContext))]
    [Migration("20240823104859_RemovedUnnecessaryFieldFromRoundCandidate")]
    partial class RemovedUnnecessaryFieldFromRoundCandidate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LiveVoting.Server.Models.Candidate", b =>
                {
                    b.Property<int>("CandidateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CandidateId"));

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Party")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CandidateId");

                    b.ToTable("Candidate");
                });

            modelBuilder.Entity("LiveVoting.Server.Models.Election", b =>
                {
                    b.Property<int>("ElectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ElectionId"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("HasFinished")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ElectionId");

                    b.ToTable("Election");
                });

            modelBuilder.Entity("LiveVoting.Server.Models.ElectionRound", b =>
                {
                    b.Property<int>("ElectionRoundId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ElectionRoundId"));

                    b.Property<int>("ElectionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalVotes")
                        .HasColumnType("int");

                    b.HasKey("ElectionRoundId");

                    b.HasIndex("ElectionId");

                    b.ToTable("ElectionRound");
                });

            modelBuilder.Entity("LiveVoting.Server.Models.LoggedUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("LoggedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LoggedUserId")
                        .IsUnique();

                    b.ToTable("LoggedUsers");
                });

            modelBuilder.Entity("LiveVoting.Server.Models.RoundCandidate", b =>
                {
                    b.Property<int>("RoundCandidateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoundCandidateId"));

                    b.Property<int>("CandidateId")
                        .HasColumnType("int");

                    b.Property<int>("ElectionRoundId")
                        .HasColumnType("int");

                    b.Property<int>("Votes")
                        .HasColumnType("int");

                    b.HasKey("RoundCandidateId");

                    b.HasIndex("CandidateId");

                    b.HasIndex("ElectionRoundId");

                    b.ToTable("RoundCandidate");
                });

            modelBuilder.Entity("LiveVoting.Server.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CNP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ConfirmedEmail")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LiveVoting.Server.Models.ElectionRound", b =>
                {
                    b.HasOne("LiveVoting.Server.Models.Election", "Election")
                        .WithMany("ElectionRounds")
                        .HasForeignKey("ElectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Election");
                });

            modelBuilder.Entity("LiveVoting.Server.Models.LoggedUser", b =>
                {
                    b.HasOne("LiveVoting.Server.Models.User", "User")
                        .WithOne("LoggedUser")
                        .HasForeignKey("LiveVoting.Server.Models.LoggedUser", "LoggedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LiveVoting.Server.Models.RoundCandidate", b =>
                {
                    b.HasOne("LiveVoting.Server.Models.Candidate", "Candidate")
                        .WithMany()
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LiveVoting.Server.Models.ElectionRound", "ElectionRound")
                        .WithMany("Candidates")
                        .HasForeignKey("ElectionRoundId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Candidate");

                    b.Navigation("ElectionRound");
                });

            modelBuilder.Entity("LiveVoting.Server.Models.Election", b =>
                {
                    b.Navigation("ElectionRounds");
                });

            modelBuilder.Entity("LiveVoting.Server.Models.ElectionRound", b =>
                {
                    b.Navigation("Candidates");
                });

            modelBuilder.Entity("LiveVoting.Server.Models.User", b =>
                {
                    b.Navigation("LoggedUser");
                });
#pragma warning restore 612, 618
        }
    }
}
