using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

public static class DbContext
{
    // Sự kiện để logging, profiling, v.v.
    public static event Action<string>? OnBeforeExecute;
    public static event Action<string, TimeSpan>? OnAfterExecute;

    // Static field lưu chuỗi kết nối sau khi đọc từ appsettings.json
    private static readonly string ConnectionString;

    // Static constructor để load cấu hình đúng 1 lần duy nhất
    static DbContext()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // hoặc AppContext.BaseDirectory tùy cách chạy
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        ConnectionString = config.GetConnectionString("MyDb")
            ?? throw new Exception("Không tìm thấy connection string 'MyDb' trong appsettings.json.");
    }

    // Thực thi truy vấn trả về danh sách T
    public static List<T> Query<T>(
        string sql,
        Func<SqlDataReader, T> map,
        params SqlParameter[] parameters)
    {
        OnBeforeExecute?.Invoke(sql);
        var sw = Stopwatch.StartNew();

        var results = new List<T>();
        using var conn = new SqlConnection(ConnectionString);
        using var cmd = new SqlCommand(sql, conn);
        if (parameters?.Any() == true)
            cmd.Parameters.AddRange(parameters);

        conn.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            results.Add(map(reader));
        }

        sw.Stop();
        OnAfterExecute?.Invoke(sql, sw.Elapsed);
        return results;
    }

    // Thực thi câu lệnh non-query (INSERT/UPDATE/DELETE)
    public static int Execute(
        string sql,
        params SqlParameter[] parameters)
    {
        OnBeforeExecute?.Invoke(sql);
        var sw = Stopwatch.StartNew();

        int affected;
        using var conn = new SqlConnection(ConnectionString);
        using var cmd = new SqlCommand(sql, conn);
        if (parameters?.Any() == true)
            cmd.Parameters.AddRange(parameters);

        conn.Open();
        affected = cmd.ExecuteNonQuery();

        sw.Stop();
        OnAfterExecute?.Invoke(sql, sw.Elapsed);
        return affected;
    }
}
