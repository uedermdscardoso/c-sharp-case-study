# C#  - Rest API

description: create ASP.NET Web Application  ->  Web API
nomenclature: Corporation.Project.Layer

### Options (for study)
<ul>
  <li>Select Web API</li>
  <li>Check Web API</li>
  <li>Uncheck unit tests</li>
  <li>Uncheck 'host in the cloud'</li>
  <li>No Authentication</li>
</ul>

<hr />

### Commands (after EntityFramework installation)
<ul>
  <li>View -> Other Window< -> Package Manager Console</li>
  <li>Enable-Migrations -ProjectName Empresa.MinhaApi.AcessoDados.Entity (generated Configuration.cs file)</li>
  <li>Add-Migration CriacaoTabelaAlunos -ProjectName Empresa.MinhaApi.AcessoDados.Entity -StartupProjectName Empresa.MinhaApi.AcessoDados.Entity (StartupProjectName - for connection string)</li>
  <li></li>
  <li></li>
</ul>

<hr />

### Version
<ul>
  <li>Visual Studio 2015</studio>
</ul>

<hr />

### Dependencies
<h3><b>ORM</b></h3>
<ul>
  <li>EntityFramework 6.1.2</li>
</ul> 

<h3><b>Security</b></h3>
<ul>
  <li>WebApi.Owin 5.2.3</li>
  <li>Owin.Host.SystemWeb 3.1.0</li>
  <li>Owin.Security.OAuth 3.1.0</li>
  <li>Owin.Cors 3.1.0</li>
</ul
