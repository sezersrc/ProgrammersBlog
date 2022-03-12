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
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Text).IsRequired();
            builder.Property(c => c.Text).HasMaxLength(1000);
            builder.HasOne<Article>(c => c.Article).WithMany(a => a.Comments).HasForeignKey(c => c.ArticleId);
            builder.Property(c => c.CreatedByName).IsRequired();
            builder.Property(c => c.CreatedByName).HasMaxLength(50);
            builder.Property(c => c.ModifiedByName).IsRequired();
            builder.Property(c => c.ModifiedByName).HasMaxLength(50);
            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.ModifiedDate).IsRequired();
            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired();
            builder.Property(c => c.Note).HasMaxLength(500);
            builder.ToTable("Comments");
            /// Fluent API
            builder.HasData(
                new Comment
                {
                    Id = 1,
                    ArticleId = 1,
                    Text =
                    "Suspendisse malesuada posuere ligula ac tincidunt. Donec dictum sagittis pulvinar. Donec ornare nec dui eget sagittis. Duis finibus eleifend lacus. Nulla tristique viverra suscipit. Ut lobortis lectus quis quam venenatis, nec sodales nunc laoreet. Maecenas hendrerit laoreet mi at fringilla. Nullam risus nulla, blandit sit amet vehicula vel, ullamcorper a elit. Aliquam aliquet vel elit vel imperdiet. Phasellus at nisi sed sapien volutpat commodo id a purus. Pellentesque rhoncus malesuada placerat. Ut metus felis, iaculis sed tristique sed, ultrices quis diam. Duis sit amet consequat ante. Curabitur nec magna rutrum, semper nisi at, tincidunt magna. Fusce sit amet neque bibendum, tempus urna et, feugiat diam.",

                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "C# Makale Yorumu"
                },
                new Comment
                {
                    Id = 2,
                    ArticleId = 2,
                    Text =
                        "Suspendisse malesuada posuere ligula ac tincidunt. Donec dictum sagittis pulvinar. Donec ornare nec dui eget sagittis. Duis finibus eleifend lacus. Nulla tristique viverra suscipit. Ut lobortis lectus quis quam venenatis, nec sodales nunc laoreet. Maecenas hendrerit laoreet mi at fringilla. Nullam risus nulla, blandit sit amet vehicula vel, ullamcorper a elit. Aliquam aliquet vel elit vel imperdiet. Phasellus at nisi sed sapien volutpat commodo id a purus. Pellentesque rhoncus malesuada placerat. Ut metus felis, iaculis sed tristique sed, ultrices quis diam. Duis sit amet consequat ante. Curabitur nec magna rutrum, semper nisi at, tincidunt magna. Fusce sit amet neque bibendum, tempus urna et, feugiat diam.",

                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "C++ Makale Yorumu"
                },
                new Comment
                {
                    Id = 3,
                    ArticleId = 3,
                    Text =
                        "Suspendisse malesuada posuere ligula ac tincidunt. Donec dictum sagittis pulvinar. Donec ornare nec dui eget sagittis. Duis finibus eleifend lacus. Nulla tristique viverra suscipit. Ut lobortis lectus quis quam venenatis, nec sodales nunc laoreet. Maecenas hendrerit laoreet mi at fringilla. Nullam risus nulla, blandit sit amet vehicula vel, ullamcorper a elit. Aliquam aliquet vel elit vel imperdiet. Phasellus at nisi sed sapien volutpat commodo id a purus. Pellentesque rhoncus malesuada placerat. Ut metus felis, iaculis sed tristique sed, ultrices quis diam. Duis sit amet consequat ante. Curabitur nec magna rutrum, semper nisi at, tincidunt magna. Fusce sit amet neque bibendum, tempus urna et, feugiat diam.",

                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Java Script Makale Yorumu"
                }
            );
        }
    }
}
