﻿using DBLibrary.Entities;
using System.Data;

namespace DBLibrary.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>
{
    public IList<Employee> GetEmployees(int take, int skip, 
        string? name = null, string? phone = null, string? login = null);
}