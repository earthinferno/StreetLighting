# StreetLighting Feedback

## To run locally in debug mode

Create a local SQLite instance using the following EF from the StreetLighting project directory:

```
dotnet ef database update -p ..\StreetLightingDal\StreetLightingDal.csproj -s .\StreetLighting.csproj
```

## Features Added

- A website to collect respondent details and feedback from user.
- Information collected inlcudes name, email address, home address, satisfaction with level of lighting, level of brightness.
- UX applied according to styles, components and patterns contained by Government Digital Services (GDS) Design System. https://design-system.service.gov.uk/
- Solution has applied SOLID where appropiate. For example
  - single responsibility - seperation of concerns of saving data and mapping data to dal data entities.
- Entity Framework with code first migration using SQLite.
- Validation using data annotations and model validation only.
- An object model for the data repository.
- Unit testing.

##### Note: They way the solution has been implemented has not afforded much opportunity to demonstrate solid. A better example can be found at https://github.com/earthinferno/scraper (see eventFinder)

## Issues/Suggested Refactors

- State is maintained between controller calls using TEMPDATA. Refactr this to use Session State.
- Currently the brightness level does not retained the state when the user navigates back to the brightness level question.
- Create a suitable error page, currently redirects to the template supplied error page - /home/error.
- Unit tests against StreetLighiting controller are to completed.
- Create a new integration test project. Using HTLMClient utily, create a set on integration tests to ensure the navigational flow and local (client) state management.
- Index the respondent ef entity by email address to ensure the uniqueness of the response.
