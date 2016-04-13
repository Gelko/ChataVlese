namespace chataVlese.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        FeedbackID = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Rating = c.Int(nullable: false),
                        Is_Valid = c.Int(nullable: false),
                        Date_In = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FeedbackID);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationID = c.Int(nullable: false, identity: true),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                        Persons = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        PhoneNr = c.Int(nullable: false),
                        Email = c.String(nullable: false),
                        Comment = c.String(),
                        Date_In = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Reservations");
            DropTable("dbo.Feedbacks");
        }
    }
}
