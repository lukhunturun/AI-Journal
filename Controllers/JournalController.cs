using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalAPI.DTOs;
using JournalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using JournalAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace JournalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    public class JournalController : ControllerBase
    {
        private readonly IJournalService _journalService;

        public JournalController(IJournalService journalService)
        {
            _journalService = journalService;
        }

        [HttpPost("create")]
        public ActionResult<ResponseDto<JournalEntryDto>> Create([FromBody] JournalEntryDto dto)
        {
            var entry = _journalService.CreateEntry(dto);
            return Ok(new ResponseDto<JournalEntryDto> { Success = true, Data = entry });
        }

        [HttpGet("me")]
        public ActionResult<ResponseDto<PaginatedList<JournalEntryDto>>> GetMyEntries([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var entries = _journalService.GetEntries(pageIndex, pageSize);
            return Ok(new ResponseDto<PaginatedList<JournalEntryDto>> { Success = true, Data = entries });
        }
    }
}