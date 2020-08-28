# Name Conventions

## Table of Content

0. [GitHub](#0-github)
1. [Solution structure](#1-solution-structure)
    1. [Common](#11-common)
    2. [Services](#12-services)
    3. [Tests](#13-tests)
2. [Code](#2-code)
    1. [Code formatting](#21-code-formatting)
        1. [Indentation](#211-indentation)
        2. [Spaces](#212-spaces)
        3. [Regions](#213-regions)
    2. [Libraries](#22-libraries)
    3. [Patterns](#23-patterns)
    4. [Interfaces](#24-interfaces)
    5. [Variables, properties, constants](#25-variables-properties-constants)
    6. [Methods](#26-methods)
    7. [Tests](#27-tests)
    8. [Comments](#28-comments)
        1. [Common](#281-common)
        2. [XML](#282-xml)
3. [Postman](#3-postman)
4. [SwaggerHub](#4-swaggerhub)
5. [Docker](#5-docker)

## 0. GitHub

- The default branches are created from develop, named via feature in PascalCase with underscores

```For example, branch name: feature/ServiceName\_Task```

- Branches based on a given refinement through an underscore

```For example, branch name: feature/ServiceName\_Task1\_Task2```

- Pull Request name is the name of the branch without &quot;feature/&quot;

```
For example, branch name: feature/ServiceName\_Task
Corresponding pull request name: ServiceName\_Task
```

- Pull request commits and comments are written in English

```For example, comment: Make sure in test that the repository has not been called.```

## 1. Solution structure

### 1.1 Common

- Files and folders are named in PascalCase

```
For example, file name: UserRepository.cs
For example, folder name: Models
```

- Folders are plural

```For example, folder name: Models, Validators, Commands```

- Service projects are stored in the Services folder, test projects in the Tests folder

### 1.2 Services

- Service projects are named with the Service postscript

```For example, service names: UserService, CompanyService```

### 1.3 Tests

- To test the project, create the same test project with the UnitTests postscript

```
For example, project name: UserService
Corresponding test project name: UserServiceUnitTests
```

- Duplicate the structure of the test project, name the classes test with the Tests postscript

```
For example, we want to test the ServiceName/FolderName/ClassName
Corresponding path: ServiceNameUnitTests/FolderName/ClassNameTests
```

- Testing all the key entities added

```For example, AddUserCommand, AddUserValidator, UserMapper, UserRepository```

## 2. Code

### 2.1 Code formatting

- String length must be 120 characters maximum

```For tracking, you can use the character counter in the lower right corner of Visual Studio or set the vertical bar to 120 characters, for example, using the Editor Guidlines extension```

- The file name must match the name of the class, structure or enum
- Variables and methods are declared as available, starting with public: public fields -\&gt; private fields -\&gt; public methods -\&gt; private methods
- Always specify access modifiers in addition to interface methods

```By default, most C# entities are private, specify this explicitly```

- Use curly braces for every block of code, even small

```For example:```
```c#
if (i == 1)
{
    i = i + 1;
}
```

- Use double characters &quot;&amp;&amp;&quot; and &quot;||&quot;

#### 2.1.1 Indentation

- Use indentation and hyphenation as suggested by Visual Studio
- Write LINQ queries one under the other, one Tab indent (standard, like Visual Studio)
- Names of variables and methods should briefly and succinctly show their essence. Expand the name as needed
- Related blocks / lines of code must be side by side
- Separate unrelated blocks of code with an empty line.

#### 2.1.2 Spaces

- Put a space before a condition:

```For example:```
```c#
while (i == 1)
if (i == 1)
```

- Separate arguments between each other with spaces, parentheses are not separated

```For example:```
```c#
Console.WriteLine(character1, character2, character3);
```

#### 2.1.3 Regions

- Use if it helps you navigate your code.

```For example, if several methods of the same class are tested in a test class, it makes sense to separate the tests of different methods by regions```

### 2.2 Libraries

- Trying not to add third-party libraries
- Using the FluentValidation library for validation
- We use NUnit, Mock, InMemoryDB in tests
- If there is a need to use a dynamic expression tree, use LINQKit library

### 2.3 Patterns

- Use the Command pattern, its name corresponds to the controller method

```
For example, controller method: AddUser
Corresponding command: AddUserCommand
```

- Use the validator to check the correctness of the transmitted data, its name matches the model name

```
For example, model name: AddUserRequest
Corresponding validator: AddUserRequestValidator
```

- Use the Mapper pattern, its name corresponds to the entity with which it works, it can implement several interfaces

```
For example, mapper does the following: AddUserRequest -> DbUser, DbUser -> User
Coresponding mapper name: UserMapper
```

- Use the Repository pattern to isolate your database, its name corresponds to the repository method

```For example, a repository has the following methods: AddUser, UpdateUser```

- Use dependency injection

```For example, we need to pass a command to the controller, we have a command interface, we do not use the controller's constructor to pass the command, we add an interface in the controller method and specify the [FromServices] attribute, and in the Startup configuration we indicate that we need to substitute appropriate class```

### 2.4 Interfaces

- Interface name starts with I

```For example, IUserService```

### 2.5 Variables, properties, constants

- When initializing a variable to a local variable with the same name, use the this keyword instead of changing one of the names

```For example, if you need to pass userRepository to the constructor, and the same variable is declared in the class, then you need to use the word this```

- Public variables are named PascalCase, private variables - camelCase, constants - PascalCase
- Variables should be declared as close to the point of use as possible.
- If the object type is specified explicitly, use var to initialize the variable. In other cases, strictly denote the type
- Names of collections, lists in plural

```For example:```
```c#
List<User> users;
```

### 2.6 Methods

- Named in PascalCase
- Method names are verbs or verb combinations:

```For example: GetUserById, SetUserFirstname```

- The method is responsible for performing one operation: Single Responsibility
- It is advisable to avoid a large number of arguments in the method - no more than 3.
- Methods related to each other are placed in descending order: first the main method, then the called method, then another called method, etc.
- If the parameters of the method during the declaration or call do not fit into the character limit, move them to the next line. Try to declare parameters from general to specific
- Try to reduce nesting

```For example:```
```c#
If (something)
{
    throw new Exception();
}

DoSomething();
```
```Not:```
```c#
if (!something)
{
    DoSomething();
}
else
{
    throw new Exception();
}
```

- For asynchronous methods, add Async at the end

```For example, GetUsersAsync()```

### 2.7 Tests

- TestNameTemplate:

```ShouldThrowValidationExceptionWhenValidatorReturnsFalse```

### 2.8 Comments

#### 2.8.1 Common

- Comments are written with a capital letter and end with a period
- One space between slashes and text
- Comments should reflect the essence, and not explain how it works. Inside the method, leave comments only if necessary, you do not need to comment on everything.
- Mark flaws and crutches with the keywords TODO and HACK

```For example:```
```c#
// TODO: A feature is missing. Should be supported when requested by users.
// HACK: Some problem is solved by a temporary workaround. Implementation of
// a permanent fix is to be considered
```

#### 2.8.2 XML

- Comment via ///
- Comment on public interfaces. Note: the classes that implement them will keep the description methods in Visual Studio
- When making changes to existing code, comment on the update. Append UPD to existing method comment


## 3. Postman

- Collections, queries are named PascalCase, arguments - camelCase.

## 4. SwaggerHub

- Methods are named camelCase.

## 5. Docker
