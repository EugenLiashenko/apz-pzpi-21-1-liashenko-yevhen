using BLL.BackUp;
using EcoWattServer.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoWattServer.Controllers;

[AdminAuth]
[ApiController]
[Route("Api/[controller]")]
[Authorize]
public class BackUpController : ControllerBase
{
    private readonly IBackUpService _backupService;

    public BackUpController(IBackUpService backupService)
    {
        _backupService = backupService;
    }

    [HttpPost("Save")]
    public async Task<IActionResult> SaveToCsv()
    {
        string relativeDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "backups");
        Console.WriteLine(relativeDirectoryPath);
        Directory.CreateDirectory(relativeDirectoryPath);

        await _backupService.SaveToCsv(relativeDirectoryPath);

        return Ok($"Database saved to CSV {relativeDirectoryPath}");
    }

    [HttpPost("Restore")]
    public async Task<IActionResult> RestoreFromCsv()
    {
        string relativeDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "backups");
        Console.WriteLine(relativeDirectoryPath);
        
        if (!Directory.Exists(relativeDirectoryPath))
        {
            return BadRequest("Backup directory does not exist.");
        }

        await _backupService.RestoreFromCsv(relativeDirectoryPath);
        return Ok("Database restored from CSV.");
    }

    [HttpPost("Download")]
    public async Task<IActionResult> DownloadAsZip()
    {
        string relativeDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "backups");
        Directory.CreateDirectory(relativeDirectoryPath);

        var zipFileData = await _backupService.DownloadAsZip(relativeDirectoryPath);

        return File(zipFileData, "application/zip", "database_backup.zip");
    }
}