using UniSystem.Console.Menus;
using UniSystem.Core.Interfaces;
using UniSystem.Core.Models;
using UniSystem.Core.Services;
using UniSystem.Infrastructure.Repositories;

Console.Title = "UniSystem";

IRepository<Student> studentRepository =
    new InMemoryRepository<Student>();

var studentService =
    new StudentService(studentRepository);

var studentMenu =
    new StudentMenu(studentService);

var mainMenu =
    new MainMenu(studentMenu);

mainMenu.Run();

Console.Clear();
Console.WriteLine("Thank you for using UniSystem.");
