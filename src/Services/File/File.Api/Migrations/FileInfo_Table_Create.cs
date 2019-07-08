using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace File.Api.Migrations
{
    [Migration(20190612090500)]
    public class FileInfo_Table_Create : Migration
    {
        public override void Up()
        {
            Create.Table("FileInfo")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
                .WithColumn("OriginalName").AsString(255).NotNullable()
                .WithColumn("Type").AsString(100).NotNullable()
                .WithColumn("Length").AsInt64().NotNullable()
                .WithColumn("Extension").AsString(10).NotNullable()
                .WithColumn("Path").AsString(2000).NotNullable()
                .WithColumn("IsDeleted").AsBoolean().NotNullable()
                .WithColumn("IsVerified").AsBoolean().NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("CreatedBy").AsString(50).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("FileInfo");
        }
    }
}
