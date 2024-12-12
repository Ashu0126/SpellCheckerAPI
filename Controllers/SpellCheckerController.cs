using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using NHunspell;
using SpellCheckerAPI.Models;
using SpellCheckerAPI.Services;

namespace SpellCheckerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpellCheckerController : ControllerBase
    {
        SpellCheckerService spellCheckerService;

        public SpellCheckerController()
        {
            spellCheckerService = new SpellCheckerService();
        }

        [HttpPost("CheckSpelling")]
        public IActionResult CheckSpelling([FromBody] string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
                return BadRequest("Invalid folder path.");

            var result = new List<SpellCheckResult>();
            var spellChecker = new Hunspell("en_US.aff", "en_US.dic");

            foreach (var file in Directory.GetFiles(folderPath, "*.doc*"))
            {
                var errors = spellCheckerService.ProcessWordDocument(file, spellChecker);
                result.Add(new SpellCheckResult
                {
                    FileName = Path.GetFileName(file),
                    Errors = errors
                });
            }

            return Ok(result);
        }

    }
}