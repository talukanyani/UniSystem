using UniSystem.Core.Interfaces;
using UniSystem.Core.Models;
using UniSystem.Core.Services;
using UniSystem.Infrastructure.Repositories;

Console.Title = "UniSystem";

IRepository<Student> studentRepository =
    new InMemoryRepository<Student>();

IRepository<Department> departmentRepository =
    new InMemoryRepository<Department>();

IRepository<Lecturer> lecturerRepository =
    new InMemoryRepository<Lecturer>();

var studentService = new StudentService(studentRepository);

var departmentService = new DepartmentService(departmentRepository);

var lecturerService = new LecturerService(
    lecturerRepository,
    departmentService
);

var studentMenu = new StudentMenu(studentService);

var lecturerMenu = new LecturerMenu(
    lecturerService,
    departmentService
);

var mainMenu = new MainMenu(
    studentMenu,
    lecturerMenu
);

mainMenu.Run();

Console.Clear();
Console.WriteLine("Thank you for using UniSystem.");
