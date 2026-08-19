using Application.Parsing;
using Console;
using Infrastructure.FileSystem;

var commandParser = new CommandParser();
var fileSystemService = new FileSystemService();
var pathResolver = new PathResolver();

var application = new ConsoleApplication(commandParser, fileSystemService, pathResolver);
application.Run();