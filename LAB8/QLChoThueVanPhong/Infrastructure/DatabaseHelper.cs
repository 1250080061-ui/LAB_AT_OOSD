using System.Data;
using Microsoft.Data.SqlClient;

namespace QLChoThueVanPhong.Infrastructure;

public static class DatabaseHelper
{
    public static DataTable Query(string commandText, params SqlParameter[] parameters)
    {
        DataTable table = new();

        using SqlConnection connection = new(DbSettings.ConnectionString);
        using SqlCommand command = new(commandText, connection);
        using SqlDataAdapter adapter = new(command);

        if (parameters.Length > 0)
        {
            command.Parameters.AddRange(parameters);
        }

        connection.Open();
        adapter.Fill(table);
        return table;
    }

    public static DataTable QueryStoredProcedure(
        string procedureName,
        params SqlParameter[] parameters)
    {
        DataTable table = new();

        using SqlConnection connection = new(DbSettings.ConnectionString);
        using SqlCommand command = new(procedureName, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        using SqlDataAdapter adapter = new(command);

        if (parameters.Length > 0)
        {
            command.Parameters.AddRange(parameters);
        }

        connection.Open();
        adapter.Fill(table);
        return table;
    }

    public static object? Scalar(string commandText, params SqlParameter[] parameters)
    {
        using SqlConnection connection = new(DbSettings.ConnectionString);
        using SqlCommand command = new(commandText, connection);

        if (parameters.Length > 0)
        {
            command.Parameters.AddRange(parameters);
        }

        connection.Open();
        return command.ExecuteScalar();
    }

    public static bool CanConnect(out string errorMessage)
    {
        try
        {
            using SqlConnection connection = new(DbSettings.ConnectionString);
            connection.Open();
            errorMessage = string.Empty;
            return true;
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
            return false;
        }
    }
}
