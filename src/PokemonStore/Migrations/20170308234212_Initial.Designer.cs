using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using PokemonStore.Models;

namespace PokemonStore.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20170308234212_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasAnnotation("Relational:Name", "RoleNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasAnnotation("Relational:TableName", "AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasAnnotation("Relational:TableName", "AspNetUserRoles");
                });

            modelBuilder.Entity("PokemonStore.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address1");

                    b.Property<int>("Age");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Country");

                    b.Property<string>("CreditcardType");

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Firstname");

                    b.Property<string>("Lastname");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Mailcode");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Region");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasAnnotation("Relational:Name", "EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasAnnotation("Relational:Name", "UserNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetUsers");
                });

            modelBuilder.Entity("PokemonStore.Models.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasAnnotation("MaxLength", 150);

                    b.Property<double?>("Distance");

                    b.Property<double?>("Latitude");

                    b.Property<double?>("Longitude");

                    b.Property<string>("Region")
                        .HasAnnotation("MaxLength", 2);

                    b.Property<string>("Street")
                        .HasAnnotation("MaxLength", 150);

                    b.HasKey("Id");

                    b.HasAnnotation("SqlServer:TableName", "Branches");
                });

            modelBuilder.Entity("PokemonStore.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.HasKey("Id");

                    b.HasAnnotation("SqlServer:TableName", "Brands");
                });

            modelBuilder.Entity("PokemonStore.Models.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("QtyBackOrdered");

                    b.Property<int>("QtyOrdered");

                    b.Property<int>("QtySold");

                    b.Property<int>("ShoppingCartId");

                    b.Property<byte[]>("Timer")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasAnnotation("MaxLength", 8)
                        .HasAnnotation("Relational:ColumnType", "timestamp");

                    b.HasKey("Id");

                    b.HasAnnotation("SqlServer:TableName", "CartItems");
                });

            modelBuilder.Entity("PokemonStore.Models.Product", b =>
                {
                    b.Property<string>("Id")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("Attack");

                    b.Property<int>("BrandId");

                    b.Property<decimal>("CostPrice")
                        .HasAnnotation("Relational:ColumnType", "money");

                    b.Property<int>("Defence");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 2000);

                    b.Property<string>("GraphicName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<int>("HP");

                    b.Property<decimal>("MSRP")
                        .HasAnnotation("Relational:ColumnType", "money");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<int>("QtyOnBackOrder");

                    b.Property<int>("QtyOnHand");

                    b.Property<int>("SpecialAttack");

                    b.Property<int>("SpecialDefence");

                    b.Property<int>("Speed");

                    b.Property<byte[]>("Timer")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasAnnotation("MaxLength", 8)
                        .HasAnnotation("Relational:ColumnType", "timestamp");

                    b.HasKey("Id");

                    b.HasAnnotation("SqlServer:TableName", "Products");
                });

            modelBuilder.Entity("PokemonStore.Models.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<decimal>("Subtotal");

                    b.Property<decimal>("Tax");

                    b.Property<byte[]>("Timer")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasAnnotation("MaxLength", 8)
                        .HasAnnotation("Relational:ColumnType", "timestamp");

                    b.Property<decimal>("TotalPrice");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("SqlServer:TableName", "ShoppingCarts");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PokemonStore.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PokemonStore.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("PokemonStore.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("PokemonStore.Models.CartItem", b =>
                {
                    b.HasOne("PokemonStore.Models.Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.HasOne("PokemonStore.Models.ShoppingCart")
                        .WithMany()
                        .HasForeignKey("ShoppingCartId");
                });

            modelBuilder.Entity("PokemonStore.Models.Product", b =>
                {
                    b.HasOne("PokemonStore.Models.Brand")
                        .WithMany()
                        .HasForeignKey("BrandId");
                });
        }
    }
}
