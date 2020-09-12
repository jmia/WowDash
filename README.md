https://github.com/jmia/WowDash

http://localhost:8080

### This application is built with:
- [.Net Core 3.1](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1)
- Entity Framework Core 3.1.5
- [Vue 2.6](https://vuejs.org/v2/guide/)
- [Microsoft SQL Server](https://docs.microsoft.com/en-us/sql/sql-server/?view=sql-server-ver15)
- JavaScript

### This application is tested with:
- [NUnit](https://docs.nunit.org/)
- [EF Core In Memory Database](https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/testing-sample) 3.1.7
- [SQL Server Express LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15)

### This application uses the following NuGet packages:
#### For testing
- [Moq](https://github.com/Moq/moq4/wiki/Quickstart) for mocking dependencies for tests.
- [Respawn](https://github.com/jbogard/Respawn) for resetting the test database after integration tests run.
- [FluentAssertions](https://fluentassertions.com/) to make test assertions more human-readable.
#### For development
- VueCliMiddleware 3.1.1 to allow the application to run CLI inputs while building and running the application.
- [Swashbuckle](https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio) 5.5.1 to provide documentation for the API project
- [NodaTime](https://nodatime.org/3.0.x/userguide/index) 3.0.0 for clean `DateTime` and `DateTimeOffset` conversions.

-----

### Stuff For Me

#### Some Business Logic Thing I Don't Want To Misplace

- If the task is daily and `IsActive = false`, if it's >= 11AM EST (8AM PST, 15:00 UTC), mark `IsActive = true`.
- If the task is daily and `IsActive = false`, if it's >= 11AM EST (8AM PST, 15:00 UTC) on _Tuesday_, mark `IsActive = true`.
- If `IsActive = true`, don't mess with it.

#### Scripts

```
Add-Migration [FakeMigration] -OutputDir ./Infrastructure/Migrations
```

#### References

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
