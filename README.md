https://github.com/jmia/WowDash

http://localhost:8080

### This application is built with:
- [.Net Core 3.1](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1)
- Entity Framework
- [Vue 2.6](https://vuejs.org/v2/guide/)
- [Microsoft SQL Server](https://docs.microsoft.com/en-us/sql/sql-server/?view=sql-server-ver15)
- JavaScript

### This application is tested with:
- [NUnit](https://docs.nunit.org/)
- [EF Core In Memory Database](https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/testing-sample)
- [Moq](https://github.com/Moq/moq4/wiki/Quickstart)
- [FluentAssertions](https://fluentassertions.com/)

-----

### Stuff For Me

#### Some Business Logic

```
If the task is daily and `IsActive = false`, if it's >= 11AM EST (8AM PST, 15:00 UTC), mark `IsActive = true`.
If the task is daily and `IsActive = false`, if it's >= 11AM EST (8AM PST, 15:00 UTC) on _Tuesday_, mark `IsActive = true`.
If `IsActive = true`, don't mess with it.
```

#### Scripts

```
Add-Migration [FakeMigration] -OutputDir ./Infrastructure/Migrations
```

#### Helpful Links

##### Vue + .NET Core

- [Starter Template](https://medium.com/software-ateliers/asp-net-core-vue-template-with-custom-configuration-using-cli-3-0-8288e18ae80b)
- [Clean Architecture](https://jasontaylor.dev/clean-architecture-getting-started/) (and accompanying [video](https://www.youtube.com/watch?v=5OtUm1BLmG0))
- [Okta's Opinion](https://developer.okta.com/blog/2018/08/27/build-crud-app-vuejs-netcore)

##### OAuth2.0

- [Fiddle for Google Sign-In with Vue](https://jsfiddle.net/phanan/a4qyysrh/)

##### Git

- [Git Command Line Cheatsheet](https://github.com/joshnh/Git-Commands)

##### API Stuff

- [curl to C# Converter](https://curl.olsh.me/)
- [Querying Blizzard APIs](https://www.reddit.com/r/wowgoblins/comments/bz9zth/c_tutorial_how_to_query_information_from_blizzard/)

#### Version Numbers

##### C# Stuff

- `.NetCoreApp` 3.1
- `EntityFrameworkCore.SqlServer` & `Tools` 3.1.5
- `EntityFrameworkCore.InMemory` 3.1.6
- `VueCliMiddleware` 3.3.1
- `NUnit` 3.12.0
- `Moq` 4.14.5

##### npm

- `axios` 0.19.2
- `babel-eslint` 10.1.0
- `core-js` 3.6.5

- `eslint` 6.7.2
- `eslint-plugin-vue` 6.2.2
- `vue` 2.6.11
- `vue-cli` (including `plugin-babel` and `plugin-eslint`) 4.4.1
- `vue-router` 3.3.2
