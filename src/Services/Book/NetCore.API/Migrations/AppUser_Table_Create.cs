using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;

namespace NetCore.API.Migrations
{
    [Migration(20180430121801)]
    public class AppUser_Table_Create : Migration
    {
        public override void Up()
        {
            Create.Table("AppUser")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Username").AsString()
                .WithColumn("Email").AsString()
                .WithColumn("FirstName").AsString()
                .WithColumn("LastName").AsString()
                .WithColumn("DateOfBirth").AsDate().Nullable()
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsInt64()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsInt64().Nullable();
        }

        public override void Down()
        {
            Delete.Table("AppUser");
        }
    }
}
