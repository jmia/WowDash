## About WowDash

created by [jmia](https://github.com/jmia) as a college capstone project

WowDash is a web dashboard for World of Warcraft players to track static or recurring in-game goals and tasks. It provides character assignment, one-click lockout refreshes, and up-to-date and dynamic game data provided by [Blizzard](https://develop.battle.net/documentation/world-of-warcraft/game-data-apis) APIs and [Wowhead](https://www.wowhead.com/). It was built for players with large character rosters to help automate away some of the work of keeping a spreadsheet of raid lockouts and transmog wish-lists, while providing cross-referenced filters across multiple fields to maximize those attempts when play time is limited. The more data you give it, the harder it works.

### This application is built with:
- [.Net Core 3.1](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1)
- Entity Framework Core 3.1.5
- [Vue 2.6](https://vuejs.org/v2/guide/) and [Vuex](https://vuex.vuejs.org/)
- [TailwindCSS](https://tailwindcss.com/) :sparkles:

### This application is tested with:
- [NUnit](https://docs.nunit.org/)

### This application uses the following NuGet packages:
#### For testing
- [Moq](https://github.com/Moq/moq4/wiki/Quickstart) for mocking dependencies for tests.
- [Respawn](https://github.com/jbogard/Respawn) for resetting the test database after integration tests run.
- [FluentAssertions](https://fluentassertions.com/) to make test assertions more human-readable.
#### For development
- [Swashbuckle](https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio) 5.5.1 to help display my documentation and schemas for my API.

### This application leverages the following `npm` packages:
- [axios](https://www.axios.com/)
- [vue-formulate](https://vueformulate.com/)
- [font-awesome](https://fontawesome.com/)

-----

### Screenshots

![Screenshot 2020-11-12 135236](https://user-images.githubusercontent.com/14793878/112339856-6c023880-8c96-11eb-83cf-d266ce129a7d.png)

![Screenshot 2020-11-12 140530](https://user-images.githubusercontent.com/14793878/112339935-7b818180-8c96-11eb-921f-03226d29c8a3.png)

-----

### References
Some code snippets and concepts were lifted from the following sources.

#### Back-end
- [.NET Template for SPA with Vue](https://medium.com/software-ateliers/asp-net-core-vue-template-with-custom-configuration-using-cli-3-0-8288e18ae80b)
- [Clean Architecture](https://jasontaylor.dev/clean-architecture-getting-started/) (and accompanying [video](https://www.youtube.com/watch?v=5OtUm1BLmG0))

#### Front-end
- [Basic HTTP Authentication in VueJS](https://jasonwatmore.com/post/2018/09/21/vuejs-basic-http-authentication-tutorial-example)

#### Design
- [Admin Template Night](https://github.com/tailwindtoolbox/Admin-Template-Night)
- [Tailwind Starter Kit](https://www.creative-tim.com/learning-lab/tailwind-starter-kit/documentation/vue/alerts)

#### Testing
- [Kent Beck Style TDD with Ian Cooper](https://www.youtube.com/watch?v=EZ05e7EMOLM&ab_channel=DevTernity)
