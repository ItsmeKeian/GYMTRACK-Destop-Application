using System;
using Microsoft.Data.SqlClient;

namespace GymTrack
{
    public class DatabaseHelper
    {
        private static string masterConnection =
            @"Server=(localdb)\MSSQLLocalDB;Integrated Security=True;";

        private static string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=GymTrackDB;Integrated Security=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static void InitializeDatabase()
        {
            // Step 1: Create database if not exists
            using (var conn = new SqlConnection(masterConnection))
            {
                conn.Open();
                string createDb = @"
                    IF NOT EXISTS (SELECT name FROM sys.databases 
                                   WHERE name = 'GymTrackDB')
                    CREATE DATABASE GymTrackDB";
                new SqlCommand(createDb, conn).ExecuteNonQuery();
            }

            // Step 2: Create tables
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string createTables = @"
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

                new SqlCommand(createTables, conn).ExecuteNonQuery();
            }
        }
    }
}