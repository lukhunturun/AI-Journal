using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalAPI.DTOs;
using JournalAPI.Models;

namespace JournalAPI.Services
{
    public interface IJournalService
    {
        JournalEntryDto CreateEntry(JournalEntryDto dto);
        PaginatedList<JournalEntryDto> GetEntries(int pageIndex = 1, int pageSize = 10);
    }
}