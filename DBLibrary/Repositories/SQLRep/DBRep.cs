﻿using DBLibrary.Interfaces;
using MySqlConnector;

namespace DBLibrary.Repositories.SQLRep;

public class DBRep : IDBRepository
{
    private readonly MySqlConnection _connection;

    public DBRep(DbConnect context)
    {
        _connection = context.GetConnection();
    }

    public int CountOfTables()
    {
        var commandStr = "SELECT count(*) FROM INFORMATION_SCHEMA.TABLES" +
            $" WHERE TABLE_SCHEMA = '{DbConnect.DbName}'";
        var command = new MySqlCommand(commandStr, _connection);
        var count = command.ExecuteScalar()?.ToString() ?? "0";
        return int.Parse(count);
    }

    public void Create()
    {
        var commsndStr = $"CREATE DATABASE IF NOT EXISTS {DbConnect.DbName}";
        var command = new MySqlCommand(commsndStr, _connection);
        command.ExecuteNonQuery();
        command.CommandText = "CREATE TABLE IF NOT EXISTS employees" +
            "(" +
            "ID INT(10) AUTO_INCREMENT NOT NULL," +
            "Name VARCHAR(30)," +
            "Phone VARCHAR(30)," +
            "Position INT(10) NOT NULL," +
            "Login VARCHAR(30)," +
            "Password VARCHAR(30)," +
            "IsDeleted TINYINT(1) DEFAULT '0'," +
            "PRIMARY KEY (ID)" +
            ")" +
            "ENGINE = 'InnoDB'" +
            "DEFAULT CHARACTER SET = 'utf8'" +
            "COLLATE = 'utf8_bin'";
        command.ExecuteNonQuery();
        command.CommandText = "CREATE TABLE IF NOT EXISTS computers" +
            "(" +
            "ID INT(10) AUTO_INCREMENT NOT NULL," +
            "Name VARCHAR(30)," +
            "RegistrationDate DATETIME NOT NULL," +
            "Price DECIMAL(10,2) NOT NULL," +
            "Status INT(10) NOT NULL," +
            "EmployeeID INT(10) NOT NULL," +
            "ExploitationStart DATETIME NOT NULL," +
            "IsDeleted TINYINT(1) DEFAULT '0'," +
            "PRIMARY KEY (ID)" +
            ")" +
            "ENGINE = 'InnoDB'" +
            "DEFAULT CHARACTER SET = 'utf8'" +
            "COLLATE = 'utf8_bin'";
        command.ExecuteNonQuery();
        command.CommandText = "CREATE TABLE IF NOT EXISTS properties" +
            "(" +
            "ID INT(10) AUTO_INCREMENT NOT NULL," +
            "IsDeleted TINYINT(1) DEFAULT '0'," +
            "ComputerID INT(10) NOT NULL," +
            "TypeID INT(10) NOT NULL," +
            "Value VARCHAR(30)," +
            "PRIMARY KEY (ID)" +
            ")" +
            "ENGINE = 'InnoDB'" +
            "DEFAULT CHARACTER SET = 'utf8'" +
            "COLLATE = 'utf8_bin'";
        command.ExecuteNonQuery();
        command.CommandText = "CREATE TABLE IF NOT EXISTS visit_history" +
            "(" +
            "ID INT(10) AUTO_INCREMENT NOT NULL," +
            "EmployeeID INT(10) NOT NULL," +
            "IsDeleted TINYINT(1) DEFAULT '0'," +
            "VisiitTime DATETIME NOT NULL," +
            "PRIMARY KEY (ID)" +
            ")" +
            "ENGINE = 'InnoDB'" +
            "DEFAULT CHARACTER SET = 'utf8'" +
            "COLLATE = 'utf8_bin'";
        command.ExecuteNonQuery();
    }
}
