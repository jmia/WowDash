https://github.com/jmia/wow-dashboard

http://localhost:8080

#### Powershell Scripts

```powershell
dotnet run
```

Start the application. Must be `cd`ed into the directory where the application lives.

##### Execution Policies

[Why should I care?](https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.core/about/about_execution_policies?view=powershell-7) Because the `vue-cli` doesn't want to run in Powershell unless you've handled this, my friend.

```powershell
Get-ExecutionPolicy -List
```

Brings up the configuration for running scripts on Powershell for this machine.

```powershell
Set-ExecutionPolicy -ExecutionPolicy Unrestricted -Scope Process
```

Sets Powershell's policy for the current session to unrestricted.

#### Helpful Links

##### EF Core

- [Owned Entity Types](https://docs.microsoft.com/en-us/ef/core/modeling/owned-entities)
- [Enumeration Abstract Class](https://lostechies.com/jimmybogard/2008/08/12/enumeration-classes/)

##### Vue + .NET Core

- [Starter Template](https://medium.com/software-ateliers/asp-net-core-vue-template-with-custom-configuration-using-cli-3-0-8288e18ae80b)
- [Clean Architecture](https://jasontaylor.dev/clean-architecture-getting-started/) (and accompanying [video](https://www.youtube.com/watch?v=5OtUm1BLmG0))
- [Okta's Opinion](https://developer.okta.com/blog/2018/08/27/build-crud-app-vuejs-netcore)

##### OAuth2.0

- [Fiddle for Google Sign-In with Vue](https://jsfiddle.net/phanan/a4qyysrh/)

##### Git

- [Git Command Line Cheatsheet](https://github.com/joshnh/Git-Commands)

#### Version Numbers

##### C# Stuff

- `.NetCoreApp` 3.1
- `EntityFrameworkCore.Sqlite` 3.1.4
- `VueCliMiddleware` 3.3.1

##### npm

- `axios` 0.19.2
- `babel-eslint` 10.1.0
- `core-js` 3.6.5

- `eslint` 6.7.2
- `eslint-plugin-vue` 6.2.2
- `vue` 2.6.11
- `vue-cli` (including `plugin-babel` and `plugin-eslint`) 4.4.1
- `vue-router` 3.3.2