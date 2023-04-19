﻿using CosmosDBExample.Data;
using CosmosDBExample.Models;
using Microsoft.EntityFrameworkCore;

using (var cosmosDataDbContext = new CosmosDataDBContext())
{

    #region Inserting Employees

    var employee1 = new Employee()
    {
        Id = Guid.NewGuid().ToString(),
        LastName = "Davolio",
        FirstName = "Nancy",
        Title = "Sales Representative",
        BirthDate = new DateTime(1948, 12, 08),
        HireDate = new DateTime(1992, 05, 01),
        Address = "507 - 20th Ave. E. Apt. 2A",
        City = "Seattle",
        PostalCode = "98122",
        Country = "USA",
        HomePhone = "(206) 555-9857"
    };

    var employee2 = new Employee()
    {
        Id = Guid.NewGuid().ToString(),
        LastName = "Smith",
        FirstName = "John",
        Title = "Human Resource",
        BirthDate = new DateTime(1948, 12, 08),
        HireDate = new DateTime(2001, 12, 01),
        Address = "507 - 20th Ave. E. Apt. 2A",
        City = "Columbia",
        PostalCode = "98122",
        Country = "USA",
        HomePhone = "(204) 551-9857"
    };

    cosmosDataDbContext.Employees?.Add(employee1);
    cosmosDataDbContext.Employees?.Add(employee2);

    await cosmosDataDbContext.SaveChangesAsync();

    Console.WriteLine("Employee records inserted successfully...");

    #endregion

    #region Inserting Customer

    Customer customer = new Customer()
    {
        Id = Guid.NewGuid().ToString(),
        CompanyName = "Alfreds Futterkiste",
        ContactName = "Maria Anders",
        ContactTitle = "Sales Representative",
        Address = "Obere Str. 57",
        City = "Berlin",
        Region = null,
        PostalCode = "12209",
        Country = "Germany",
        Phone = "030-0074321",
        Orders = new List<Order>()
        {
            new Order()
            {
                Id = Guid.NewGuid().ToString(),
                OrderDate = new DateTime(1997,08,25),
                RequiredDate = new DateTime(1997,09,22),
                ShippedDate = new DateTime(1997,09,02),
                ShipVia = 1,
                Freight = 29.46,
                ShipName = "Alfreds Futterkiste",
                ShipAddress = "Obere Str. 57",
                ShipCity = "Berlin",
                ShipRegion = null,
                ShipPostalCode = "12209",
                ShipCountry = "Germany"
            },
            new Order()
            {

                Id = Guid.NewGuid().ToString(),
                OrderDate = new DateTime(1997,10,03),
                RequiredDate = new DateTime(1997,10,31),
                ShippedDate = new DateTime(1997,10,13),
                ShipVia = 2,
                Freight = 61.02,
                ShipName = "Alfred's Futterkiste",
                ShipAddress = "Obere Str. 57",
                ShipCity = "Berlin",
                ShipRegion = null,
                ShipPostalCode = "12209",
                ShipCountry = "Germany"
            },
            new Order()
            {
                Id= Guid.NewGuid().ToString(),
                OrderDate = new DateTime(1997,10,13),
                RequiredDate = new DateTime(1997,11,24),
                ShippedDate = new DateTime(1997,10,21),
                ShipVia =  1,
                Freight = 23.94,
                ShipName = "Alfred's Futterkiste",
                ShipAddress = "Obere Str. 57",
                ShipCity = "Berlin",
                ShipRegion = null,
                ShipPostalCode = "12209",
                ShipCountry = "Germany",
            }
        }
    };

    cosmosDataDbContext.Customers?.Add(customer);
    await cosmosDataDbContext.SaveChangesAsync();

    Console.WriteLine("Customer record inserted successfully...");

    #endregion

    #region Get Employees

    if (cosmosDataDbContext.Employees != null)
    {
        var employees = await cosmosDataDbContext.Employees.ToListAsync();
        Console.WriteLine("");

        foreach (var employee in employees)
        {
            Console.WriteLine("First Name : " + employee.FirstName);
            Console.WriteLine("Last Name : " + employee.LastName);
            Console.WriteLine("Hire Date : " + employee.HireDate);
            Console.WriteLine("--------------------------------\n");
        }
    }

    #endregion

    #region Get an Employee

    if (cosmosDataDbContext.Employees != null)
    {
        var employee = await cosmosDataDbContext.Employees
            .Where(e => e.FirstName == "John")
            .FirstOrDefaultAsync();

        Console.WriteLine("");

        Console.WriteLine("First Name : " + employee?.FirstName);
        Console.WriteLine("Last Name : " + employee?.LastName);
        Console.WriteLine("Hire Date : " + employee?.HireDate);
        Console.WriteLine("--------------------------------\n");
    }

    #endregion

    #region Update an Employee

    if (cosmosDataDbContext.Employees != null)
    {
        var employee = await cosmosDataDbContext.Employees
            .Where(e => e.FirstName == "John")
            .FirstOrDefaultAsync();

        if (employee != null)
        {
            employee.LastName = "Doe";
            employee.HireDate = new DateTime(2002, 12, 01);

            await cosmosDataDbContext.SaveChangesAsync();

            Console.WriteLine("\nRecord has been updated.\n");
        }
    }

    #endregion

    #region Delete an Employee

    if (cosmosDataDbContext.Employees != null)
    {
        var employee = await cosmosDataDbContext.Employees
            .Where(e => e.FirstName == "John")
            .FirstOrDefaultAsync();

        if (employee != null)
        {
            cosmosDataDbContext.Employees.Remove(employee);
            await cosmosDataDbContext.SaveChangesAsync();

            Console.WriteLine("\nRecord has been deleted.\n");
        }
    }

    #endregion

}