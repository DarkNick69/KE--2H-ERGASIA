using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using static KEΠ_2H_ERGASIA.DbManager;

namespace KEΠ_2H_ERGASIA
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
                                               PhoneNumber BIGINT NOT NULL,
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
                await InitDb();
            
            if (!_initialized)
            {
                await InitDb();
            }
            
            const string insertQuery = @"
                INSERT INTO Requests(FormId, Name, Email, PhoneNumber, Birthday, RequestType, Address, SubmissionTime)
                VALUES(@FormId, @Name, @Email, @PhoneNumber, @Birthday, @RequestType, @Address, @SubmissionTime)";
            await Conn.ExecuteAsync(insertQuery, new { FormId = request.FormId.ToString(), request.Name, request.Email, request.PhoneNumber, request.Birthday,
                request.RequestType, request.Address, request.SubmissionTime });
        }

        public static async Task<Request> GetRequest(Guid requestId)
        {
            if (!_initialized)
                await InitDb();
            
            const string getQuery = @"SELECT * FROM Requests WHERE FormId = @FormId";
            
            return await Conn.QuerySingleOrDefaultAsync<Request>(getQuery, new { FormId = requestId.ToString() });
        }

        public static async Task<bool> DeleteRequest(Guid id)
        {
            if (!_initialized)
                await InitDb();
            
            const string deleteQuery = @"DELETE FROM Requests WHERE FormId = @FormId";
            return await Conn.ExecuteAsync(deleteQuery, new { FormId = id.ToString()}) != 0;
        }

        public static async Task UpdateRequest(Request request)
        {
            if (!_initialized)
                await InitDb();
            
            const string updateQuery = @"UPDATE Requests SET Name = @Name, Email = @Email, PhoneNumber = @PhoneNumber, Birthday = @Birthday, RequestType = @RequestType, Address = @Address WHERE FormId = @FormId";

            await Conn.ExecuteAsync(updateQuery,
                new
                {
                    FormId = request.FormId.ToString(), request.Name, request.Email, request.PhoneNumber, request.Birthday, request.RequestType,
                    request.Address
                });
        }

        public static async Task<IEnumerable<Request>> GetAllRequests()
        {
            if (!_initialized)
                await InitDb();

            const string query = @"SELECT * FROM Requests";

            return await Conn.QueryAsync<Request>(query);
        }

        public static async Task<IEnumerable<Request>> GetRequestsByName(string name)
        {
            if (!_initialized)
                await InitDb();

            if (name == string.Empty)
                return new List<Request> { };

            const string query = @"SELECT * FROM Requests WHERE Name = @Name";

            return await Conn.QueryAsync<Request>(query,new { Name = name });
        }

        public sealed class Request
        {
            public  Guid FormId;
            public  string Name;
            public  string Email;
            public  long PhoneNumber;
            public  string Birthday;
            public  string RequestType;
            public  string Address;
            public  string SubmissionTime;

            
            internal Request(string formId, string name,string email, long phoneNumber, string birthday, string requestType, string address, string submissionTime)
            {
                FormId = Guid.Parse(formId);
                Name = name;
                Email = email;
                PhoneNumber = phoneNumber;
                Birthday = birthday;
                RequestType = requestType;
                Address = address;
                SubmissionTime = submissionTime;
            }
            public Request(Guid formId, string name,string email, long phoneNumber, string birthday, string requestType, string address, string submissionTime)
            {
                FormId = formId;
                Name = name;
                Email = email;
                PhoneNumber = phoneNumber;
                Birthday = birthday;
                RequestType = requestType;
                Address = address;
                SubmissionTime = submissionTime;
            }
            public override string ToString()
            {
                return $"ID: {FormId}\nΌνομα: {Name}\n" +
                $"Email: {Email}\nΤηλέφωνο: {PhoneNumber}\n" +
                $"Ημερομηνία γέννησης: {Birthday}\nΕίδος: {RequestType}\nΔιεύθυνση: {Address}\n" +
                $"Ώρα δήλωσης: {SubmissionTime:dd/MM/yy/HH:MM}";
            }
        }
    }
}