﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebsiteShoe.Entities;

namespace WebsiteShoe.Migrations
{
    [DbContext(typeof(ShoeDbContext))]
    [Migration("20200626025419_AddTableAdmin")]
    partial class AddTableAdmin
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebsiteShoe.Entities.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("PK_Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("WebsiteShoe.Entities.Bill", b =>
                {
                    b.Property<int>("BillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("BillId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("BillId")
                        .HasName("PK_Bill");

                    b.HasIndex("CustomerId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("WebsiteShoe.Entities.BillDetail", b =>
                {
                    b.Property<int>("BillId")
                        .HasColumnType("int");

                    b.Property<int>("ShoeId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("BillId", "ShoeId");

                    b.HasIndex("ShoeId");

                    b.ToTable("BillDetails");
                });

            modelBuilder.Entity("WebsiteShoe.Entities.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Sex")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CustomerId")
                        .HasName("PK_Customer");

                    b.HasIndex("UserName")
                        .IsUnique()
                        .HasFilter("[UserName] IS NOT NULL");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("WebsiteShoe.Entities.Producer", b =>
                {
                    b.Property<int>("ProducerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProducerName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProducerId")
                        .HasName("PK_Producer");

                    b.ToTable("Producers");
                });

            modelBuilder.Entity("WebsiteShoe.Entities.Shoe", b =>
                {
                    b.Property<int>("ShoeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ShoeId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image360")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Images")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ShoeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StyleId")
                        .HasColumnType("int");

                    b.HasKey("ShoeId")
                        .HasName("PK_Shoe");

                    b.HasIndex("StyleId");

                    b.ToTable("Shoes");
                });

            modelBuilder.Entity("WebsiteShoe.Entities.ShoeDetail", b =>
                {
                    b.Property<int>("ShoeId")
                        .HasColumnType("int");

                    b.Property<int>("SizeId")
                        .HasColumnType("int");

                    b.Property<long>("Quantity")
                        .HasColumnType("bigint");

                    b.HasKey("ShoeId", "SizeId");

                    b.HasIndex("SizeId");

                    b.ToTable("ShoeDetails");
                });

            modelBuilder.Entity("WebsiteShoe.Entities.ShoeSize", b =>
                {
                    b.Property<int>("SizeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SizeId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("SizeId")
                        .HasName("PK_ShoeSize");

                    b.ToTable("ShoeSizes");
                });

            modelBuilder.Entity("WebsiteShoe.Entities.ShoeStyle", b =>
                {
                    b.Property<int>("StyleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("StyleId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProducerId")
                        .HasColumnType("int");

                    b.Property<string>("StyleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StyleId")
                        .HasName("PK_ShoeStyle");

                    b.HasIndex("ProducerId");

                    b.ToTable("ShoeStyles");
                });

            modelBuilder.Entity("WebsiteShoe.Entities.Bill", b =>
                {
                    b.HasOne("WebsiteShoe.Entities.Customer", "Customer")
                        .WithMany("Bills")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebsiteShoe.Entities.BillDetail", b =>
                {
                    b.HasOne("WebsiteShoe.Entities.Bill", "Bill")
                        .WithMany("BillDetails")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebsiteShoe.Entities.Shoe", "Shoe")
                        .WithMany("BillDetails")
                        .HasForeignKey("ShoeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebsiteShoe.Entities.Shoe", b =>
                {
                    b.HasOne("WebsiteShoe.Entities.ShoeStyle", "ShoeStyle")
                        .WithMany("Shoes")
                        .HasForeignKey("StyleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebsiteShoe.Entities.ShoeDetail", b =>
                {
                    b.HasOne("WebsiteShoe.Entities.Shoe", "Shoe")
                        .WithMany("ShoeDetails")
                        .HasForeignKey("ShoeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebsiteShoe.Entities.ShoeSize", "ShoeSize")
                        .WithMany("ShoeDetails")
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebsiteShoe.Entities.ShoeStyle", b =>
                {
                    b.HasOne("WebsiteShoe.Entities.Producer", "Producer")
                        .WithMany("ShoeStyles")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}