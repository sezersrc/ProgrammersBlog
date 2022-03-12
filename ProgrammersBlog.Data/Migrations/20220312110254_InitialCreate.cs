using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProgrammersBlog.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "VARBINARY(500)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ViewsCount = table.Column<int>(type: "int", nullable: false),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    SeoAuthor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SeoDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SeoTags = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Name", "Note" },
                values: new object[,]
                {
                    { 1, "InitialCreate", new DateTime(2022, 3, 12, 14, 2, 53, 545, DateTimeKind.Local).AddTicks(201), "C# Programlama dili ile ilgili en güncel bilgiler ", true, false, "InitialCreate", new DateTime(2022, 3, 12, 14, 2, 53, 545, DateTimeKind.Local).AddTicks(254), "C#", "C# Blog Kategorisi" },
                    { 2, "InitialCreate", new DateTime(2022, 3, 12, 14, 2, 53, 545, DateTimeKind.Local).AddTicks(266), "C++ Programlama dili ile ilgili en güncel bilgiler ", true, false, "InitialCreate", new DateTime(2022, 3, 12, 14, 2, 53, 545, DateTimeKind.Local).AddTicks(268), "C++", "C++ Blog Kategorisi" },
                    { 3, "InitialCreate", new DateTime(2022, 3, 12, 14, 2, 53, 545, DateTimeKind.Local).AddTicks(272), "Java Script Programlama dili ile ilgili en güncel bilgiler ", true, false, "InitialCreate", new DateTime(2022, 3, 12, 14, 2, 53, 545, DateTimeKind.Local).AddTicks(273), "Java Script", "Java Script Blog Kategorisi" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Name", "Note" },
                values: new object[] { 1, "InitialCreate", new DateTime(2022, 3, 12, 14, 2, 53, 548, DateTimeKind.Local).AddTicks(4225), "Admin Rolü , Tüm Haklara Sahiptir.", true, false, "InitialCreate", new DateTime(2022, 3, 12, 14, 2, 53, 548, DateTimeKind.Local).AddTicks(4241), "Admin", "Admin Rolüdür." });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "Description", "Email", "FirstName", "IsActive", "IsDeleted", "LastName", "ModifiedByName", "ModifiedDate", "Note", "PasswordHash", "Picture", "RoleId", "UserName" },
                values: new object[] { 1, "InitialCreate", new DateTime(2022, 3, 12, 14, 2, 53, 560, DateTimeKind.Local).AddTicks(4748), "İlk Admin Kullanıcısı", "sezer@softekbilisim.com.tr", "Sezer", true, false, "Sürücü", "InitialCreate", new DateTime(2022, 3, 12, 14, 2, 53, 560, DateTimeKind.Local).AddTicks(4771), "Admin Kullanıcısı", new byte[] { 48, 49, 57, 50, 48, 50, 51, 97, 55, 98, 98, 100, 55, 51, 50, 53, 48, 53, 49, 54, 102, 48, 54, 57, 100, 102, 49, 56, 98, 53, 48, 48 }, "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSX4wVGjMQ37PaO4PdUVEAliSLi8-c2gJ1zvQ&usqp=CAU", 1, "sezersurucu" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreatedByName", "CreatedDate", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "SeoAuthor", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewsCount" },
                values: new object[] { 1, 1, 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed orci mauris, luctus sit amet interdum ac, eleifend commodo tellus. Sed a justo et enim congue aliquet in eu ipsum. Donec efficitur purus ante, tempor ornare quam euismod id. Maecenas a felis sed mauris luctus accumsan. Sed dolor elit, pulvinar id nulla in, elementum mattis odio. Vestibulum lorem est, pharetra nec purus vel, sodales rhoncus odio. Nunc varius eleifend tortor, at consequat lectus placerat vitae. Vivamus quis porttitor felis. Morbi non iaculis massa, eget pharetra dui. Nam faucibus quam at elit vestibulum hendrerit. Aenean elementum augue sed porttitor accumsan.", "InitialCreate", new DateTime(2022, 3, 12, 14, 2, 53, 542, DateTimeKind.Local).AddTicks(1681), new DateTime(2022, 3, 12, 14, 2, 53, 542, DateTimeKind.Local).AddTicks(779), true, false, "InitialCreate", new DateTime(2022, 3, 12, 14, 2, 53, 542, DateTimeKind.Local).AddTicks(2151), "C# 9.0 ce .Net 5 Yenilikleri", "Sezer Sürücü", "C# 9.0 ce .Net 5 Yenilikleri ", "C#, C# 9 ,.Net5 , .Net Framework , .Net Core", "Default.jpg", "C# 9.0 ce .Net 5 Yenilikleri", 1, 100 });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreatedByName", "CreatedDate", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "SeoAuthor", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewsCount" },
                values: new object[] { 2, 2, 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed orci mauris, luctus sit amet interdum ac, eleifend commodo tellus. Sed a justo et enim congue aliquet in eu ipsum. Donec efficitur purus ante, tempor ornare quam euismod id. Maecenas a felis sed mauris luctus accumsan. Sed dolor elit, pulvinar id nulla in, elementum mattis odio. Vestibulum lorem est, pharetra nec purus vel, sodales rhoncus odio. Nunc varius eleifend tortor, at consequat lectus placerat vitae. Vivamus quis porttitor felis. Morbi non iaculis massa, eget pharetra dui. Nam faucibus quam at elit vestibulum hendrerit. Aenean elementum augue sed porttitor accumsan.", "InitialCreate", new DateTime(2022, 3, 12, 14, 2, 53, 542, DateTimeKind.Local).AddTicks(3191), new DateTime(2022, 3, 12, 14, 2, 53, 542, DateTimeKind.Local).AddTicks(3189), true, false, "InitialCreate", new DateTime(2022, 3, 12, 14, 2, 53, 542, DateTimeKind.Local).AddTicks(3193), "C++ 11 ve 19 Yenilikleri", "Sezer Sürücü", "C++ 11 ve 19 Yenilikleri ", "C++ 11 ve 19 Yenilikleri", "Default.jpg", "C++ 11 ve 19 Yenilikleri", 1, 255 });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreatedByName", "CreatedDate", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "SeoAuthor", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewsCount" },
                values: new object[] { 3, 3, 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed orci mauris, luctus sit amet interdum ac, eleifend commodo tellus. Sed a justo et enim congue aliquet in eu ipsum. Donec efficitur purus ante, tempor ornare quam euismod id. Maecenas a felis sed mauris luctus accumsan. Sed dolor elit, pulvinar id nulla in, elementum mattis odio. Vestibulum lorem est, pharetra nec purus vel, sodales rhoncus odio. Nunc varius eleifend tortor, at consequat lectus placerat vitae. Vivamus quis porttitor felis. Morbi non iaculis massa, eget pharetra dui. Nam faucibus quam at elit vestibulum hendrerit. Aenean elementum augue sed porttitor accumsan. Suspendisse malesuada posuere ligula ac tincidunt.Donec dictum sagittis pulvinar.Donec ornare nec dui eget sagittis.Duis finibus eleifend lacus.Nulla tristique viverra suscipit.Ut lobortis lectus quis quam venenatis,nec sodales nunc laoreet.Maecenas hendrerit laoreet mi at fringilla.Nullam risus nulla,blandit sit amet vehicula vel", "InitialCreate", new DateTime(2022, 3, 12, 14, 2, 53, 542, DateTimeKind.Local).AddTicks(3199), new DateTime(2022, 3, 12, 14, 2, 53, 542, DateTimeKind.Local).AddTicks(3197), true, false, "InitialCreate", new DateTime(2022, 3, 12, 14, 2, 53, 542, DateTimeKind.Local).AddTicks(3200), "C++ 11 ve 19 Yenilikleri", "Sezer Sürücü", "Java Script S2019 ve S2020 Yenilikleri", "Java Script S2019 ve S2020 Yenilikleri", "Default.jpg", " Java Script S2019 ve S2020 Yenilikleri", 1, 10 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "Text" },
                values: new object[] { 1, 1, "InitialCreate", new DateTime(2022, 3, 12, 14, 2, 53, 546, DateTimeKind.Local).AddTicks(8953), true, false, "InitialCreate", new DateTime(2022, 3, 12, 14, 2, 53, 546, DateTimeKind.Local).AddTicks(8965), "C# Makale Yorumu", "Suspendisse malesuada posuere ligula ac tincidunt. Donec dictum sagittis pulvinar. Donec ornare nec dui eget sagittis. Duis finibus eleifend lacus. Nulla tristique viverra suscipit. Ut lobortis lectus quis quam venenatis, nec sodales nunc laoreet. Maecenas hendrerit laoreet mi at fringilla. Nullam risus nulla, blandit sit amet vehicula vel, ullamcorper a elit. Aliquam aliquet vel elit vel imperdiet. Phasellus at nisi sed sapien volutpat commodo id a purus. Pellentesque rhoncus malesuada placerat. Ut metus felis, iaculis sed tristique sed, ultrices quis diam. Duis sit amet consequat ante. Curabitur nec magna rutrum, semper nisi at, tincidunt magna. Fusce sit amet neque bibendum, tempus urna et, feugiat diam." });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "Text" },
                values: new object[] { 2, 2, "InitialCreate", new DateTime(2022, 3, 12, 14, 2, 53, 546, DateTimeKind.Local).AddTicks(8977), true, false, "InitialCreate", new DateTime(2022, 3, 12, 14, 2, 53, 546, DateTimeKind.Local).AddTicks(8978), "C++ Makale Yorumu", "Suspendisse malesuada posuere ligula ac tincidunt. Donec dictum sagittis pulvinar. Donec ornare nec dui eget sagittis. Duis finibus eleifend lacus. Nulla tristique viverra suscipit. Ut lobortis lectus quis quam venenatis, nec sodales nunc laoreet. Maecenas hendrerit laoreet mi at fringilla. Nullam risus nulla, blandit sit amet vehicula vel, ullamcorper a elit. Aliquam aliquet vel elit vel imperdiet. Phasellus at nisi sed sapien volutpat commodo id a purus. Pellentesque rhoncus malesuada placerat. Ut metus felis, iaculis sed tristique sed, ultrices quis diam. Duis sit amet consequat ante. Curabitur nec magna rutrum, semper nisi at, tincidunt magna. Fusce sit amet neque bibendum, tempus urna et, feugiat diam." });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "Text" },
                values: new object[] { 3, 3, "InitialCreate", new DateTime(2022, 3, 12, 14, 2, 53, 546, DateTimeKind.Local).AddTicks(8982), true, false, "InitialCreate", new DateTime(2022, 3, 12, 14, 2, 53, 546, DateTimeKind.Local).AddTicks(8983), "Java Script Makale Yorumu", "Suspendisse malesuada posuere ligula ac tincidunt. Donec dictum sagittis pulvinar. Donec ornare nec dui eget sagittis. Duis finibus eleifend lacus. Nulla tristique viverra suscipit. Ut lobortis lectus quis quam venenatis, nec sodales nunc laoreet. Maecenas hendrerit laoreet mi at fringilla. Nullam risus nulla, blandit sit amet vehicula vel, ullamcorper a elit. Aliquam aliquet vel elit vel imperdiet. Phasellus at nisi sed sapien volutpat commodo id a purus. Pellentesque rhoncus malesuada placerat. Ut metus felis, iaculis sed tristique sed, ultrices quis diam. Duis sit amet consequat ante. Curabitur nec magna rutrum, semper nisi at, tincidunt magna. Fusce sit amet neque bibendum, tempus urna et, feugiat diam." });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UserId",
                table: "Articles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleId",
                table: "Comments",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
