//using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.Services;
using WebStore.Domain.Entities;
using WebStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebStore.Domain;
using System.Data;
using System.Data.SqlClient;
using WebStore.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace WebStore.Services.InSQL;

public class SQLEmployeesData : IEmployeeData
 {
    private readonly WebStoreDB _db;
    private readonly ILogger<SQLEmployeesData> _Logger;
    
    //public static SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebStoreFedyanina.db;");
    public SQLEmployeesData(WebStoreDB db) => _db = db;

   
    public IEnumerable<Employee> GetEmployees() => _db.Employees;
    private int? employeeIdMax => _db.Employees.DefaultIfEmpty().Max(e => e.Id);
    private int _MaxFreeId => employeeIdMax == null ? 0 : (int)employeeIdMax + 1;
    public Employee? GetById(int id) => _db.Employees.FirstOrDefault(employee => employee.Id == id);

    //private async Task InitializeEmployeeAsync(CancellationToken Cancel)
    //{
       
    //    _Logger.LogInformation("Добавление сотрудника в БД ...");
    //    await using (await _db.Database.BeginTransactionAsync(Cancel))
    //    {
    //        //await _db.Employees.AddRangeAsync(TestData.Employees, Cancel);
    //        await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Employees] ON", Cancel);
    //        await _db.SaveChangesAsync(Cancel);
    //        await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Employees] OFF", Cancel);
    //        await _db.Database.CommitTransactionAsync(Cancel);
    //    }
    //    _Logger.LogInformation("Добавлен сотрудник в БД ...");
    //}
        public int Add(Employee employee)
    {
        if (employee is null)
            throw new ArgumentNullException(nameof(employee));

        if (_db.Employees.Contains(employee))
            return employee.Id;
        employee.Id = _MaxFreeId;
        _db.Employees.Add(employee);

        
        //if (conn.State == ConnectionState.Closed)
        //{
        //    conn.Open();
        //}
        //SqlCommand command = new SqlCommand("SET IDENTITY_INSERT [dbo].[Employees] ON", conn);
        //command.ExecuteReader();
        //conn.Close();

        //if (conn.State == ConnectionState.Closed)
        //{
        //    conn.Open();
        //}

        ////_Logger.LogWarning("Попытка создания сотрудника с Id:{0}", employee.Id);
        //SqlCommand command2 = new SqlCommand("INSERT INTO Employees (Id, LastName, FirstName, Patronymic, Age, EmploymentDate, Name) VALUES ('"+ _MaxFreeId+"', '"+ employee.LastName + "', '" + employee.FirstName + "', '" + employee.Patronymic + "', '" + employee.Age + "', '" + employee.EmploymentDate + "', '" + employee.LastName +" "+ employee.FirstName +" "+ employee.Patronymic + "' )", conn);
        //command2.ExecuteReader();

        //conn.Close();

        //if (conn.State == ConnectionState.Closed)
        //{
        //    conn.Open();
        //}


        //SqlCommand command3 = new SqlCommand("SET IDENTITY_INSERT [dbo].[Employees] OFF", conn);
        //command3.ExecuteReader();
        //conn.Close();


        return employee.Id;
    }

    public bool Edit(Employee employee)
    {
        if (employee == null)
            throw new ArgumentNullException(nameof(employee));
        if (_db.Employees.Contains(employee))
            return true;
        var db_employee = GetById(employee.Id);
        if (db_employee == null)
        {
            _Logger.LogWarning("Попытка редактирования отсутствующего сотрудника с Id:{0}", employee.Id);
            return false;
        }

        db_employee.FirstName = employee.FirstName;
        db_employee.LastName = employee.LastName;
        db_employee.Age = employee.Age;
        db_employee.Patronymic = employee.Patronymic;
        db_employee.EmploymentDate = employee.EmploymentDate;

        _Logger.LogInformation("Информация о сотруднике с Id:{0} была изменена", employee.Id);
        


        return true;
    }


    public bool Delete(int id)
    {
        var employee = GetById(id);
        if (employee == null)
        {
            _Logger.LogWarning("Попытка удаления отсутствующего сотрудника с Id:{0}", id);
            return false;
        }

        _db.Employees.Remove(employee);
        //_Logger.LogInformation("Cотрудник с Id:{0} был удален", id);
        return true;
    }
}

