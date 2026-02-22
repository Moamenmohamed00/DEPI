Console.WriteLine("Hello, World!");
/*
  <Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
	  //version of app(SDK)
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
//install suitable package version with app
  <ItemGroup>
	  //is orm convert C#,LinQ to sql depend on data provider (xml,json,database,xls)
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="10.0.3" />
	  //to apply any change in C# on migration(understand mapping)
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="10.0.3">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
	  //convert from C# to sql server syntax
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="10.0.3" />
	  //use commands when convert
	  //Add-Migration
	  //Bundle-Migration
	  //Drop-Database
	  //Get-DbContext
	  //Get-Migration
	  //Optimize-DbContext
	  //Remove-Migration
	  //Scaffold-DbContext
	  //Script-Migration
	  //Update-Database
	  <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="10.0.3">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>

</Project>

 */
//Scaffold-DbContext(database first)
//Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Company;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False
//Scaffold-DbContext "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Company;Integrated Security=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models//