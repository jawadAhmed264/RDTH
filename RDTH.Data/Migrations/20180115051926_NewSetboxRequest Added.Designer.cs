﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using RDTH.Data;
using System;

namespace RDTH.Data.Migrations
{
    [DbContext(typeof(RDTHDbContext))]
    [Migration("20180115051926_NewSetboxRequest Added")]
    partial class NewSetboxRequestAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("RDTH.Data.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TotalItems");

                    b.Property<decimal>("TotalPrice");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("RDTH.Data.Models.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CartId");

                    b.Property<int?>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("Total");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("RDTH.Data.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<int?>("CustomerCardId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("CustomerCardId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("RDTH.Data.Models.CustomerCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("CardNumber")
                        .IsRequired();

                    b.Property<string>("ContactNumber")
                        .IsRequired();

                    b.Property<string>("OwnerName")
                        .IsRequired();

                    b.Property<int?>("PackageId");

                    b.Property<int?>("SetBoxId");

                    b.Property<DateTime>("SubscribeDate");

                    b.HasKey("Id");

                    b.HasIndex("PackageId");

                    b.HasIndex("SetBoxId");

                    b.ToTable("CustomerCards");
                });

            modelBuilder.Entity("RDTH.Data.Models.CustomerPackage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CustomerCardId");

                    b.Property<int?>("CustomerId");

                    b.Property<DateTime>("ExpirationDate");

                    b.Property<int>("NumberOfMonths");

                    b.Property<int?>("PackageId");

                    b.Property<int?>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerCardId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PackageId");

                    b.HasIndex("StatusId");

                    b.ToTable("CustomerPackages");
                });

            modelBuilder.Entity("RDTH.Data.Models.Dealer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("City");

                    b.Property<string>("FirstName");

                    b.Property<DateTime>("JoinDate");

                    b.Property<string>("LastName");

                    b.Property<string>("Telephone");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Dealers");
                });

            modelBuilder.Entity("RDTH.Data.Models.Distributer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("City");

                    b.Property<string>("FirstName");

                    b.Property<DateTime>("JoinDate");

                    b.Property<string>("LastName");

                    b.Property<string>("Telephone");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Distributers");
                });

            modelBuilder.Entity("RDTH.Data.Models.Faq", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Answer");

                    b.Property<string>("Question");

                    b.HasKey("Id");

                    b.ToTable("Faqs");
                });

            modelBuilder.Entity("RDTH.Data.Models.FeedBack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Email");

                    b.Property<string>("Msg");

                    b.Property<string>("Name");

                    b.Property<int?>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("FeedBacks");
                });

            modelBuilder.Entity("RDTH.Data.Models.MovieOnDemand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CustomerId");

                    b.Property<string>("Movie");

                    b.Property<DateTime>("MovieTime");

                    b.Property<int?>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("StatusId");

                    b.ToTable("MoviesOnDemand");
                });

            modelBuilder.Entity("RDTH.Data.Models.NewSetBoxRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CardId");

                    b.Property<int?>("SetboxId");

                    b.Property<int?>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.HasIndex("SetboxId");

                    b.HasIndex("StatusId");

                    b.ToTable("NewSetBoxRequest");
                });

            modelBuilder.Entity("RDTH.Data.Models.NewSubscribe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("ApplyDate");

                    b.Property<string>("ContactNumber");

                    b.Property<string>("OwnerName");

                    b.Property<int?>("PackageId");

                    b.Property<int?>("SetBoxId");

                    b.Property<int?>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("PackageId");

                    b.HasIndex("SetBoxId");

                    b.HasIndex("StatusId");

                    b.ToTable("NewSubscribes");
                });

            modelBuilder.Entity("RDTH.Data.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CartId");

                    b.Property<DateTime>("DatePlaced");

                    b.Property<int?>("DealerId");

                    b.Property<int?>("DistributerId");

                    b.Property<int?>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("DealerId");

                    b.HasIndex("DistributerId");

                    b.HasIndex("StatusId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("RDTH.Data.Models.Package", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Charges");

                    b.Property<int>("DocumentariesChannel");

                    b.Property<int>("EntertainmentChannel");

                    b.Property<string>("ImageUrl");

                    b.Property<int>("NewsChannel");

                    b.Property<int>("NoOfChannels");

                    b.Property<string>("PackageName");

                    b.Property<int>("SportsChannel");

                    b.HasKey("Id");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("RDTH.Data.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CVV");

                    b.Property<DateTime>("CardExpiry");

                    b.Property<decimal>("Cost");

                    b.Property<string>("CreditCardNumber");

                    b.Property<int?>("DealerId");

                    b.Property<int?>("DistributerId");

                    b.Property<int?>("OrderId");

                    b.Property<DateTime>("PaymentDate");

                    b.Property<string>("PaymentType");

                    b.HasKey("Id");

                    b.HasIndex("DealerId");

                    b.HasIndex("DistributerId");

                    b.HasIndex("OrderId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("RDTH.Data.Models.Recharge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CVV");

                    b.Property<DateTime>("CardExpiry");

                    b.Property<decimal>("Cost");

                    b.Property<string>("CreditCardNumber");

                    b.Property<int?>("CustomerCardId");

                    b.Property<int?>("PackageId");

                    b.Property<string>("PaymentType");

                    b.Property<DateTime>("RechargeDate");

                    b.HasKey("Id");

                    b.HasIndex("CustomerCardId");

                    b.HasIndex("PackageId");

                    b.ToTable("RechargeHistory");
                });

            modelBuilder.Entity("RDTH.Data.Models.SetBox", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<string>("Specification");

                    b.HasKey("Id");

                    b.ToTable("SetBoxes");
                });

            modelBuilder.Entity("RDTH.Data.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("RDTH.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("RDTH.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("RDTH.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RDTH.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("RDTH.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RDTH.Data.Models.CartItem", b =>
                {
                    b.HasOne("RDTH.Data.Models.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId");

                    b.HasOne("RDTH.Data.Models.SetBox", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("RDTH.Data.Models.Customer", b =>
                {
                    b.HasOne("RDTH.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("RDTH.Data.Models.CustomerCard", "CustomerCard")
                        .WithMany()
                        .HasForeignKey("CustomerCardId");
                });

            modelBuilder.Entity("RDTH.Data.Models.CustomerCard", b =>
                {
                    b.HasOne("RDTH.Data.Models.Package", "Package")
                        .WithMany()
                        .HasForeignKey("PackageId");

                    b.HasOne("RDTH.Data.Models.SetBox", "SetBox")
                        .WithMany()
                        .HasForeignKey("SetBoxId");
                });

            modelBuilder.Entity("RDTH.Data.Models.CustomerPackage", b =>
                {
                    b.HasOne("RDTH.Data.Models.CustomerCard", "CustomerCard")
                        .WithMany()
                        .HasForeignKey("CustomerCardId");

                    b.HasOne("RDTH.Data.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("RDTH.Data.Models.Package", "Package")
                        .WithMany()
                        .HasForeignKey("PackageId");

                    b.HasOne("RDTH.Data.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("RDTH.Data.Models.Dealer", b =>
                {
                    b.HasOne("RDTH.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("RDTH.Data.Models.Distributer", b =>
                {
                    b.HasOne("RDTH.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("RDTH.Data.Models.FeedBack", b =>
                {
                    b.HasOne("RDTH.Data.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("RDTH.Data.Models.MovieOnDemand", b =>
                {
                    b.HasOne("RDTH.Data.Models.Customer", "Customer")
                        .WithMany("MoviesOnDemand")
                        .HasForeignKey("CustomerId");

                    b.HasOne("RDTH.Data.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("RDTH.Data.Models.NewSetBoxRequest", b =>
                {
                    b.HasOne("RDTH.Data.Models.CustomerCard", "Card")
                        .WithMany()
                        .HasForeignKey("CardId");

                    b.HasOne("RDTH.Data.Models.SetBox", "Setbox")
                        .WithMany()
                        .HasForeignKey("SetboxId");

                    b.HasOne("RDTH.Data.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("RDTH.Data.Models.NewSubscribe", b =>
                {
                    b.HasOne("RDTH.Data.Models.Package", "Package")
                        .WithMany()
                        .HasForeignKey("PackageId");

                    b.HasOne("RDTH.Data.Models.SetBox", "SetBox")
                        .WithMany()
                        .HasForeignKey("SetBoxId");

                    b.HasOne("RDTH.Data.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("RDTH.Data.Models.Order", b =>
                {
                    b.HasOne("RDTH.Data.Models.Cart", "Cart")
                        .WithMany()
                        .HasForeignKey("CartId");

                    b.HasOne("RDTH.Data.Models.Dealer")
                        .WithMany("Orders")
                        .HasForeignKey("DealerId");

                    b.HasOne("RDTH.Data.Models.Distributer")
                        .WithMany("Orders")
                        .HasForeignKey("DistributerId");

                    b.HasOne("RDTH.Data.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("RDTH.Data.Models.Payment", b =>
                {
                    b.HasOne("RDTH.Data.Models.Dealer")
                        .WithMany("Payments")
                        .HasForeignKey("DealerId");

                    b.HasOne("RDTH.Data.Models.Distributer")
                        .WithMany("Payments")
                        .HasForeignKey("DistributerId");

                    b.HasOne("RDTH.Data.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("RDTH.Data.Models.Recharge", b =>
                {
                    b.HasOne("RDTH.Data.Models.CustomerCard", "CustomerCard")
                        .WithMany("RechargeHistory")
                        .HasForeignKey("CustomerCardId");

                    b.HasOne("RDTH.Data.Models.Package", "Package")
                        .WithMany()
                        .HasForeignKey("PackageId");
                });
#pragma warning restore 612, 618
        }
    }
}
