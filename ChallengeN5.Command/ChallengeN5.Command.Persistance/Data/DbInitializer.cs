namespace ChallengeN5.Command.Persistance.Data;

using ChallengeN5.Command.Domain.Application.Model;

public static class DbInitializer
{
    public static void Initialize(N5Context context)
    {
        context.Database.EnsureCreated();

        if (context.Set<Employee>().Any())
        {
            return;
        }

        var employees = new Employee[]
        {
            new Employee { FirstName = "John", LastName = "Doe", Position = "Software Developer" },
            new Employee { FirstName = "Jane", LastName = "Doe", Position = "Software Developer" },
            new Employee { FirstName = "Alice", LastName = "Doe", Position = "Software Developer" },
            new Employee { FirstName = "Bob", LastName = "Doe", Position = "Software Developer" },
            new Employee { FirstName = "Charlie", LastName = "Doe", Position = "Software Developer" },
            new Employee { FirstName = "David", LastName = "Doe", Position = "Software Developer" },
            new Employee { FirstName = "Eve", LastName = "Doe", Position = "Software Developer" },
            new Employee { FirstName = "Frank", LastName = "Doe", Position = "Software Developer" }
        };

        var permissionTypes = new PermissionType[]
        {
            new PermissionType { Name = "Security" },
            new PermissionType { Name = "Developer" },
            new PermissionType { Name = "Holder" }
        };

        IList<Permission> permission = [];

        foreach (var employee in employees)
        {
            foreach (PermissionType permissionType in permissionTypes)
            {
                permission.Add(new Permission
                {
                    Employee = employee,
                    PermissionType = permissionType,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddYears(1)
                });
            }
        }

        context.AddRange(permission);
        context.SaveChanges();

        var newPermissionTypes = new PermissionType[]
        {
            new PermissionType { Name = "QA" },
            new PermissionType { Name = "DevOps" }
        };

        context.AddRange(newPermissionTypes);
        context.SaveChanges();

    }
}
