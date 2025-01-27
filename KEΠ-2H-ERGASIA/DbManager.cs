using System;
using System.Data.SQLite;
using System.Threading.Tasks;
using Dapper;
namespace KEΠ_2H_ERGASIA.Db
{

    public static class DbManager
    {
        private const string ConnString = "Data Source=kep_db.sqlite";

        private static readonly SQLiteConnection Conn = new SQLiteConnection(ConnString);

        private static bool _initialized;

        public static async Task InitDb()
        {
            // I'd rather have an actual migration system but this works.
            const string initDbQuery = @"
                                   CREATE TABLE IF NOT EXISTS Requests (
                                               FormId TEXT PRIMARY KEY,
                                               Name TEXT NOT NULL,
                                               Email Text NOT NULL,
                                               PhoneNumber BIGINT Unique,
                                               Birthday TEXT NOT NULL,
                                               RequestType TEXT NOT NULL,
                                               Address TEXT NOT NULL,
                                               SubmissionTime TEXT NOT NULL
                                               );
                                   ";

            await Conn.ExecuteAsync(initDbQuery);
            _initialized = true;
        }
        public static async Task InsertRequest(Request request)
        {
            if (!_initialized)
            {
                await InitDb();
            }

            const string insertQuery = @"
                INSERT INTO Requests(FormId, Name, Email, PhoneNumber, Birthday, RequestType, Address, SubmissionTime)
                VALUES(@FormId, @Name, @Email, @PhoneNumber, @Birthday, @RequestType, @Address, @SubmissionTime)";
            await Conn.ExecuteAsync(insertQuery, new { FormId = Guid.NewGuid(), request.Name, request.Email, request.PhoneNumber, request.Birthday,
                request.RequestType, request.Address, request.SubmissionTime });
        }

        public static async Task<bool> DeleteRequest(long phoneNumber)
        {
            if (!_initialized)
            {
                await InitDb();
            }
            const string deleteQuery = @"DELETE FROM Requests WHERE PhoneNumber = @PhoneNumber";
            return (await Conn.ExecuteAsync(deleteQuery, new { PhoneNumber = phoneNumber })) != 0;
        }

        public sealed class Request
        {
            public string Name;
            public string Email;
            public long PhoneNumber;
            public string Birthday;
            public string RequestType;
            public string Address;
            public string SubmissionTime;

            public Request(string name,string email, long phoneNumber, string birthday, string requestType, string address, string submissionTime)
            {
                Name = name;
                Email = email;
                PhoneNumber = phoneNumber;
                Birthday = birthday;
                RequestType = requestType;
                Address = address;
                SubmissionTime = submissionTime;
            }
            
                
        }
    }
}