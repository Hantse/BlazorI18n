# BlazorI18n
Blazor library for I18n translations

Demo : https://blazori18n.azurewebsites.net/

## Packages
| Name        | Version           | Build status  |
| ------------- |:-------------:| -----|
| BlazorI18n.Core     | 0.0.2-preview01 | |
| BlazorI18n.Provider.Json      | 0.0.2-preview01 | |


## Core package
Core package contains simple component with interface for provider custom value.

Just implement II18n and inject at startup to provide your own services to retrives values.

### Install with 
#### Package manager
> Install-Package BlazorI18n.Core -Version 0.0.2-preview01
#### Dotnet
> dotnet add package BlazorI18n.Core --version 0.0.2-preview01

## How to use 
Inject your own value provider in services
```csharp
ConfigureServices(IServiceCollection services){
	services.AddSingleton<IValueProvider, MyValueProvider>();
	services.AddI18n((config) => ...);
}
```

Usage in views 
```html
	<I18nElement Key="HomePage.Description" />
```
## Json package
Json package contains a service can be configure to retrive values from json.

### Install with 
#### Package manager
> Install-Package BlazorI18n.Provider.Json -Version 0.0.1-preview05
#### Dotnet
> dotnet add package BlazorI18n.Provider.Json --version 0.0.1-preview05

## How to use 
Add to startup in ConfigureServices(IServiceCollection services)
```csharp
services.AddI18nJsonProvider((config) =>
{
	config.DefaultLocal = "en";
	config.LocalsUri = new System.Collections.Generic.Dictionary<string, string>
	{
		{ "en", "i18n/en.json" },
		{ "fr", "i18n/fr.json" }
	};
	config.CurrentLocal = "en";
});

```

