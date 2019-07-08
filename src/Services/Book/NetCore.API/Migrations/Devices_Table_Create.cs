using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;

namespace NetCore.API.Migrations
{
    [Migration(20181109224000)]
    public class Devices_Table_Create : Migration
    {
        public override void Up()
        {
            Create.Table("Devices")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("StoreId").AsString().NotNullable()
                .WithColumn("Username").AsString().NotNullable()
                .WithColumn("DeviceToken").AsString().NotNullable()
                .WithColumn("DeviceType").AsInt16() //Android: 0, IOS: 1 
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsString().NotNullable()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsInt64().Nullable();
        }

        public override void Down()
        {
            Delete.Table("Devices");
        }
    }
}
