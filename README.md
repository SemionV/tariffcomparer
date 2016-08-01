# tariffcomparer
Frameworks and libraries used:

.NET 4.5, C# 6.0

Autofac(DI and IoC patterns)

NLog(logging)

Newtonsoft JSON(parsing of JSON)

xUnit(unit testing)

Moq(mocking for unit tests)


Visual Studio solution:

TariffComparer - domain: Model, Business Logic and Data Layer

xUnitTest - unit tests

CostCalculator - console application with simple input and processing of electricity tariffs comparison


Model:

Tariff - base class

BasicTariff - Product A

PackagedTariff - Product B

ComparisonItem - POCO of result of tariff calculation to show to a user


Business Logic:

TariffCalculator - base class for any electricity tariff calculation approach

BasicTariffCalculator - calculator for BasicTarif

PackagedTariffCalculator - calculator for PackagedTariff

TariffComparer - comparison of existing tariffs


AppContext - configuration and container of application components.

In ./deploy folder you can find already compiled application which you simply can run via tc.bat file.
