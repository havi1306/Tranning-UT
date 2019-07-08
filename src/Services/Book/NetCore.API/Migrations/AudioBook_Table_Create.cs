using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;

namespace NetCore.API.Migrations
{
    public class AudioBook_Table_Create : Migration
    {
        public override void Up()
        {
            Create.Table("Category")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsInt16()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsInt16().Nullable();

            Create.Table("Author")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("DateOfBirth").AsDate().Nullable()
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsInt16()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsInt16().Nullable();

            Create.Table("Reader")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsInt16()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsInt16().Nullable();

            Create.Table("AudioBook")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("ImageBackground").AsString()
                .WithColumn("Views").AsInt32()
                .WithColumn("Rate").AsInt32()
                .WithColumn("Duration").AsInt32()
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsInt16()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsInt16().Nullable();

            Create.Table("AudioBookTrack")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("PathFile").AsString()
                .WithColumn("Duration").AsInt32()
                .WithColumn("AudioBookId").AsInt64().ForeignKey("AudioBook", "Id")
                .WithColumn("ReaderId").AsInt64().ForeignKey("Reader", "Id")
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsInt16()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsInt16().Nullable();

            Create.Table("AudioBook_Categories")
                .WithColumn("CategoryId").AsInt64().ForeignKey("Category", "Id")
                .WithColumn("AudioBookId").AsInt64().ForeignKey("AudioBook", "Id")
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsInt16()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsInt16().Nullable();

            Create.Table("AudioBook_Authors")
                .WithColumn("AudioBookId").AsInt64().ForeignKey("AudioBook", "Id")
                .WithColumn("AuthorId").AsInt64().ForeignKey("Author", "Id")
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsInt16()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsInt16().Nullable();

            Create.Table("AudioBook_Readers")
                .WithColumn("AudioBookId").AsInt64().ForeignKey("AudioBook", "Id")
                .WithColumn("ReaderId").AsInt64().ForeignKey("Reader", "Id")
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsInt16()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsInt16().Nullable();
        }

        public override void Down()
        {
            Delete.Table("AudioBook_Categories");
            Delete.Table("AudioBookTrack");
            Delete.Table("AudioBook");
            Delete.Table("Reader");
            Delete.Table("Author");
            Delete.Table("Category");
        }
    }
}
