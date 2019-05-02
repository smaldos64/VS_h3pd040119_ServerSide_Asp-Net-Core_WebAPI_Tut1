﻿// <auto-generated />
using Asp_Net_Core_WebAPI_Tut1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Asp_Net_Core_WebAPI_Tut1.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Asp_Net_Core_WebAPI_Tut1.Models.Author", b =>
                {
                    b.Property<long>("AuthorID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("AuthorID");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Asp_Net_Core_WebAPI_Tut1.Models.AuthorContact", b =>
                {
                    b.Property<long>("AuthorID");

                    b.Property<string>("Address");

                    b.Property<string>("ContactNumber");

                    b.HasKey("AuthorID");

                    b.ToTable("AuthorContacts");
                });

            modelBuilder.Entity("Asp_Net_Core_WebAPI_Tut1.Models.Book", b =>
                {
                    b.Property<long>("BookID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("BookCategoryID");

                    b.Property<long>("PublisherID");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("BookID");

                    b.HasIndex("BookCategoryID");

                    b.HasIndex("PublisherID");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Asp_Net_Core_WebAPI_Tut1.Models.BookAuthor", b =>
                {
                    b.Property<long>("AuthorID");

                    b.Property<long>("BookID");

                    b.HasKey("AuthorID", "BookID");

                    b.HasIndex("BookID");

                    b.ToTable("BookAuthors");
                });

            modelBuilder.Entity("Asp_Net_Core_WebAPI_Tut1.Models.BookCategory", b =>
                {
                    b.Property<long>("BookCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BookCategoryName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("BookCategoryID");

                    b.ToTable("BookCategories");
                });

            modelBuilder.Entity("Asp_Net_Core_WebAPI_Tut1.Models.Publisher", b =>
                {
                    b.Property<long>("PublisherID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PublisherName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("PublisherID");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("Asp_Net_Core_WebAPI_Tut1.Models.AuthorContact", b =>
                {
                    b.HasOne("Asp_Net_Core_WebAPI_Tut1.Models.Author", "Author")
                        .WithOne("AuthorContact")
                        .HasForeignKey("Asp_Net_Core_WebAPI_Tut1.Models.AuthorContact", "AuthorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Asp_Net_Core_WebAPI_Tut1.Models.Book", b =>
                {
                    b.HasOne("Asp_Net_Core_WebAPI_Tut1.Models.BookCategory", "BookCategory")
                        .WithMany("Books")
                        .HasForeignKey("BookCategoryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Asp_Net_Core_WebAPI_Tut1.Models.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("PublisherID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Asp_Net_Core_WebAPI_Tut1.Models.BookAuthor", b =>
                {
                    b.HasOne("Asp_Net_Core_WebAPI_Tut1.Models.Author", "Author")
                        .WithMany("BookAuthors")
                        .HasForeignKey("AuthorID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Asp_Net_Core_WebAPI_Tut1.Models.Book", "Book")
                        .WithMany("BookAuthors")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
