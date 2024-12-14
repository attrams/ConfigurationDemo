using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigurationDemo.Controllers;

[Route("[controller]")]
[ApiController]
public class ConfigurationController(IConfiguration configuration) : ControllerBase
{
    [HttpGet]
    [Route("my-key")]
    public ActionResult GetMyKey()
    {
        var myKey = configuration["MyKey"];

        return Ok(myKey);
    }

    [HttpGet]
    [Route("database-configuration")]
    public ActionResult GetDatabaseConfiguration()
    {
        var type = configuration["Database:Type"];
        var connectionString = configuration["Database:ConnectionString"];

        return Ok(new { Type = type, ConnectionString = connectionString });
    }

    [HttpGet]
    [Route("database-configuration-with-bind")]
    public ActionResult GetDatabaseConfigurationWithBind()
    {
        var databaseOption = new DatabaseOption();
        configuration.GetSection($"{DatabaseOption.SectionName}").Bind(databaseOption);

        return Ok(new { databaseOption.Type, databaseOption.ConnectionString });
    }

    [HttpGet]
    [Route("database-configuration-with-generic-type")]
    public ActionResult GetDatabaseConfigurationWithGenericType()
    {
        var databaseOption = configuration.GetSection($"{DatabaseOption.SectionName}:{DatabaseOption.SystemDatabaseSectionName}").Get<DatabaseOption>();

        return Ok(new { databaseOption!.Type, databaseOption.ConnectionString });
    }

    [HttpGet]
    [Route("database-configuration-with-ioptions")]
    public ActionResult GetDatabaseConfigurationWithIOptions([FromServices] IOptions<DatabaseOption> options)
    {
        var databaseOption = options.Value;

        return Ok(new { databaseOption.Type, databaseOption.ConnectionString });
    }

    [HttpGet]
    [Route("database-configuration-with-ioptions-snapshot")]
    public ActionResult GetDatabaseConfigurationWithIOptionsSnapshot([FromServices] IOptionsSnapshot<DatabaseOption> options)
    {
        var databaseOption = options.Value;

        return Ok(new { databaseOption.Type, databaseOption.ConnectionString });
    }

    [HttpGet]
    [Route("database-configuration-with-ioptions-monitor")]
    public ActionResult GetDatabaseConfigurationWithIOptionsMonitor([FromServices] IOptionsMonitor<DatabaseOption> options)
    {
        var databaseOptions = options.CurrentValue;

        return Ok(new { databaseOptions.Type, databaseOptions.ConnectionString });
    }

    [HttpGet]
    [Route("database-configuration-with-named-options")]
    public ActionResult GetDatabaseConfigurationWithNamedOption([FromServices] IOptionsSnapshot<DatabaseOption> options)
    {
        var systemDatabaseOption = options.Get(DatabaseOption.SystemDatabaseSectionName);
        var businessDatabaseOption = options.Get(DatabaseOption.BusinessDatabaseSectionName);

        return Ok(new { SystemDatabaseOption = systemDatabaseOption, BusinessDatabaseOption = businessDatabaseOption });
    }
}