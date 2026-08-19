using Application.Interfaces;
using Application.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Console
{
    public sealed class ConsoleApplication
    {
        private readonly ICommandParser commandParser;
        private readonly IFileSystemService fileSystemService;
        private readonly IPathResolver pathResolver;
        private string currentDirectory;

        public ConsoleApplication(ICommandParser commandParser, IFileSystemService fileSystemService, IPathResolver pathResolver)
        {
            this.commandParser = commandParser;
            this.fileSystemService = fileSystemService;
            this.pathResolver = pathResolver;
            currentDirectory = Environment.CurrentDirectory;
        }

        public void Run()
        {
            while (true)
            {
                System.Console.WriteLine();
                System.Console.WriteLine($"Directorio actual: {currentDirectory}> ");
                System.Console.Write("devfile> ");
                var input = System.Console.ReadLine() ?? string.Empty;
                var command = commandParser.Parse(input);

                try
                {
                    if (!Execute(command.Name, command.Arguments))
                    {
                        break;
                    }
                }catch(UnauthorizedAccessException)
                {
                    ShowError("No tienes permisos para realizar esta operacion.");
                }
                catch (DirectoryNotFoundException)
                {
                    ShowError("El directorio indicado no existe.");
                }
                catch (IOException exception)
                {
                    ShowError($"No se pudo completar la operación: {exception.Message}");
                }
                catch(ArgumentException)
                {
                    ShowError($"La ruta o los argumentos proporcionados no son válidos.");
                }
                
            }
        }

        private bool Execute(string commandName, IReadOnlyList<string> arguments)
        {
            switch (commandName)
            {
                case "":
                    return true;
                case "pwd":
                    ShowCurrentDirectory();
                    return true;
                case "list":
                case "ls":
                    ListEntries();
                    return true;
                case "cd":
                    ChangeDirectory(arguments);
                    return true;
                case "mkdir":
                    CreateDirectory(arguments);
                    return true;
                case "touch":
                    CreateFile(arguments);
                    return true;
                case "help":
                    ShowHelp();
                    return true;
                case "exit":
                    return false;
                default:
                    ShowError($"Comando desconocido: {commandName}");
                    return true;
            }
        }
        private void ShowCurrentDirectory()
        {
            System.Console.WriteLine(currentDirectory);
        }

        private void ListEntries()
        {
            var entries = fileSystemService.ListEntries(currentDirectory);

            if(entries.Count == 0)
            {
                System.Console.WriteLine("");
                return;
            }

            foreach (var entry in entries)
            {
                System.Console.WriteLine(entry);
            }
        }

        private void ChangeDirectory(IReadOnlyList<string> arguments)
        {
            if (arguments.Count == 0)
            {
                ShowError("Se requiere un argumento para el comando 'cd'.");
                return;
            }
            var targetPath = pathResolver.Resolve(currentDirectory, arguments[0]);
            if (!fileSystemService.DirectoryExists(targetPath))
            {
                ShowError($"El directorio '{targetPath}' no existe.");
                return;
            }
            currentDirectory = targetPath;
        }

        private void CreateDirectory(IReadOnlyList<string> arguments)
        {
            if (arguments.Count == 0)
            {
                ShowError("Se requiere un argumento para el comando 'mkdir'.");
                return;
            }
            var targetPath = pathResolver.Resolve(currentDirectory, arguments[0]);
            fileSystemService.CreateDirectory(targetPath);

            System.Console.WriteLine($"Directorio '{targetPath}' creado exitosamente.");
        }

        private void CreateFile(IReadOnlyList<string> arguments)
        {
            if (arguments.Count == 0)
            {
                ShowError("Se requiere un argumento para el comando 'touch'.");
                return;
            }
            var targetPath = pathResolver.Resolve(currentDirectory, arguments[0]);
            fileSystemService.CreateFile(targetPath);
            System.Console.WriteLine($"Archivo '{targetPath}' creado exitosamente.");
        }

        private static void ShowHeader()
        {
            System.Console.WriteLine("========================================");
            System.Console.WriteLine("            DEVFILE MANAGER");
            System.Console.WriteLine("========================================");
            System.Console.WriteLine("Escribe 'help' para ver los comandos.");
        }

        private static void ShowHelp()
        {
            System.Console.WriteLine("Comandos disponibles:");
            System.Console.WriteLine("pwd               Muestra el directorio actual.");
            System.Console.WriteLine("list | ls         Lista archivos y carpetas.");
            System.Console.WriteLine("cd <ruta>         Cambia de directorio.");
            System.Console.WriteLine("mkdir <nombre>    Crea un directorio.");
            System.Console.WriteLine("touch <archivo>   Crea un archivo vacío.");
            System.Console.WriteLine("help              Muestra esta ayuda.");
            System.Console.WriteLine("exit              Cierra la aplicación.");
        }

        private static void ShowError(string message)
        {
            System.Console.WriteLine($"Error: {message}");
        }
    }
}
