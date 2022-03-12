using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings
{
    public class ArticleMap:IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.Id);  // ID alanı tanımladık Primary Key
            builder.Property(a => a.Id).ValueGeneratedOnAdd(); // Otomatik Artan
            builder.Property(a => a.Title).HasMaxLength(100);//Maksimum 100 karakter 
            builder.Property(a => a.Title).IsRequired(true);//zorunlu alan
            builder.Property(a => a.Content).IsRequired();
            builder.Property(a => a.Content).HasColumnType("NVARCHAR(MAX)"); // Content alanında Maximum karakter .
            builder.Property(a => a.Date).IsRequired();
            builder.Property(a => a.SeoAuthor).IsRequired();
            builder.Property(a => a.SeoAuthor).HasMaxLength(50); // Karakter kısıtlaması
            builder.Property(a => a.SeoDescription).IsRequired();
            builder.Property(a => a.SeoDescription).HasMaxLength(150);
            builder.Property(a => a.SeoTags).IsRequired();
            builder.Property(a => a.SeoTags).HasMaxLength(70);
            builder.Property(a => a.ViewsCount).IsRequired();
            builder.Property(a => a.CommentCount).IsRequired();
            builder.Property(a => a.Thumbnail).IsRequired();
            builder.Property(a => a.Thumbnail).HasMaxLength(250);
            builder.Property(a => a.CreatedByName).IsRequired();
            builder.Property(a => a.CreatedByName).HasMaxLength(50);
            builder.Property(a => a.ModifiedByName).IsRequired();
            builder.Property(a => a.ModifiedByName).HasMaxLength(50);
            builder.Property(a => a.CreatedDate).IsRequired();
            builder.Property(a => a.ModifiedDate).IsRequired();
            builder.Property(a => a.IsActive).IsRequired();
            builder.Property(a => a.IsDeleted).IsRequired();
            builder.Property(a => a.Note).HasMaxLength(500);
            builder.HasOne<Category>(a => a.Category).WithMany(c=>c.Articles).HasForeignKey(a=>a.CategoryId); // bir kategorinin birden fazla artike olabilir 
            builder.HasOne<User>(a => a.User).WithMany(u=>u.Articles).HasForeignKey(a=>a.UserId); // bir kategorinin birden fazla artike olabilir 
            // Dışarıdan gelen ID olduğundan ilişki kuruyoruz.
            builder.ToTable("Articles");

            builder.HasData(

                new Article
                {
                    Id = 1,
                    CategoryId = 1,
                    Title = "C# 9.0 ce .Net 5 Yenilikleri",
                    Content =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed orci mauris, luctus sit amet interdum ac, eleifend commodo tellus. Sed a justo et enim congue aliquet in eu ipsum. Donec efficitur purus ante, tempor ornare quam euismod id. Maecenas a felis sed mauris luctus accumsan. Sed dolor elit, pulvinar id nulla in, elementum mattis odio. Vestibulum lorem est, pharetra nec purus vel, sodales rhoncus odio. Nunc varius eleifend tortor, at consequat lectus placerat vitae. Vivamus quis porttitor felis. Morbi non iaculis massa, eget pharetra dui. Nam faucibus quam at elit vestibulum hendrerit. Aenean elementum augue sed porttitor accumsan.",
                    Thumbnail = "Default.jpg",
                    SeoDescription = "C# 9.0 ce .Net 5 Yenilikleri ",
                    SeoTags =  "C#, C# 9 ,.Net5 , .Net Framework , .Net Core",
                    SeoAuthor = "Sezer Sürücü",
                    Date = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "C# 9.0 ce .Net 5 Yenilikleri",
                    UserId = 1,
                    ViewsCount = 100,
                    CommentCount = 1

                },
                new Article
                {
                    Id = 2,
                    CategoryId = 2,
                    Title = "C++ 11 ve 19 Yenilikleri",
                    Content =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed orci mauris, luctus sit amet interdum ac, eleifend commodo tellus. Sed a justo et enim congue aliquet in eu ipsum. Donec efficitur purus ante, tempor ornare quam euismod id. Maecenas a felis sed mauris luctus accumsan. Sed dolor elit, pulvinar id nulla in, elementum mattis odio. Vestibulum lorem est, pharetra nec purus vel, sodales rhoncus odio. Nunc varius eleifend tortor, at consequat lectus placerat vitae. Vivamus quis porttitor felis. Morbi non iaculis massa, eget pharetra dui. Nam faucibus quam at elit vestibulum hendrerit. Aenean elementum augue sed porttitor accumsan.",
                    Thumbnail = "Default.jpg",
                    SeoDescription = "C++ 11 ve 19 Yenilikleri ",
                    SeoTags = "C++ 11 ve 19 Yenilikleri",
                    SeoAuthor = "Sezer Sürücü",
                    Date = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "C++ 11 ve 19 Yenilikleri",
                    UserId = 1,
                    ViewsCount = 255,
                    CommentCount = 1

                },
                new Article
                {
                    Id = 3,
                    CategoryId = 3,
                    Title = " Java Script S2019 ve S2020 Yenilikleri",
                    Content =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed orci mauris, luctus sit amet interdum ac, eleifend commodo tellus. Sed a justo et enim congue aliquet in eu ipsum. Donec efficitur purus ante, tempor ornare quam euismod id. Maecenas a felis sed mauris luctus accumsan. Sed dolor elit, pulvinar id nulla in, elementum mattis odio. Vestibulum lorem est, pharetra nec purus vel, sodales rhoncus odio. Nunc varius eleifend tortor, at consequat lectus placerat vitae. Vivamus quis porttitor felis. Morbi non iaculis massa, eget pharetra dui. Nam faucibus quam at elit vestibulum hendrerit. Aenean elementum augue sed porttitor accumsan. Suspendisse malesuada posuere ligula ac tincidunt.Donec dictum sagittis pulvinar.Donec ornare nec dui eget sagittis.Duis finibus eleifend lacus.Nulla tristique viverra suscipit.Ut lobortis lectus quis quam venenatis,nec sodales nunc laoreet.Maecenas hendrerit laoreet mi at fringilla.Nullam risus nulla,blandit sit amet vehicula vel",
                    
                    Thumbnail = "Default.jpg",
                    SeoDescription = "Java Script S2019 ve S2020 Yenilikleri",
                    SeoTags = "Java Script S2019 ve S2020 Yenilikleri",
                    SeoAuthor = "Sezer Sürücü",
                    Date = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "C++ 11 ve 19 Yenilikleri",
                    UserId = 1,
                    ViewsCount = 10,
                    CommentCount = 1
                }
                
            );
        }
    }
}
