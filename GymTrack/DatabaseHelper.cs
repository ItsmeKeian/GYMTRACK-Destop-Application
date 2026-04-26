using System;
using Microsoft.Data.SqlClient;

namespace GymTrack
{
    public class DatabaseHelper
    {
        private static string connectionString =
         @"Server=(localdb)\MSSQLLocalDB;
          Integrated Security=True;
          AttachDbFilename=|DataDirectory|\GymTrackDB.mdf;
          User Instance=False;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static void InitializeDatabase()
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                string sql = @"
                IF NOT EXISTS (SELECT * FROM sysobjects 
                               WHERE name='Members' AND xtype='U')
                BEGIN
                    CREATE TABLE Members (
                        MemberID   INT PRIMARY KEY IDENTITY(1,1),
                        FirstName  NVARCHAR(50),
                        LastName   NVARCHAR(50),
                        Phone      NVARCHAR(20),
                        Email      NVARCHAR(100),
                        Address    NVARCHAR(200),
                        DateJoined DATE DEFAULT GETDATE()
                    );

                    CREATE TABLE MembershipPlans (
                        PlanID         INT PRIMARY KEY IDENTITY(1,1),
                        PlanName       NVARCHAR(50),
                        DurationMonths INT,
                        Price          DECIMAL(10,2)
                    );

                    CREATE TABLE Subscriptions (
                        SubID     INT PRIMARY KEY IDENTITY(1,1),
                        MemberID  INT REFERENCES Members(MemberID),
                        PlanID    INT REFERENCES MembershipPlans(PlanID),
                        StartDate DATE,
                        EndDate   DATE,
                        Status    NVARCHAR(20) DEFAULT 'Active'
                    );

                    INSERT INTO MembershipPlans VALUES ('Monthly', 1, 500);
                    INSERT INTO MembershipPlans VALUES ('Quarterly', 3, 1200);
                    INSERT INTO MembershipPlans VALUES ('Annual', 12, 4000);
                END";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}