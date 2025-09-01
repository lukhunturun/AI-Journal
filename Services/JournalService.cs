using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalAPI.Data;
using JournalAPI.DTOs;
using JournalAPI.Models;

namespace JournalAPI.Services
{
    public class JournalService : IJournalService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public JournalService(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }
        public JournalEntryDto CreateEntry(JournalEntryDto dto)
        {
            var entry = new JournalEntry
            {
                UserId = _currentUserService.GetUserId(),
                Content = dto.Content
            };
            _context.JournalEntries.Add(entry);
            _context.SaveChanges();
            return new JournalEntryDto
            {
                Content = entry.Content
            };
        }

        public PaginatedList<JournalEntryDto> GetEntries(int pageIndex = 1, int pageSize = 10)
        {
            var userId = _currentUserService.GetUserId();
            var query = _context.JournalEntries.Where(e => e.UserId == userId);
            var count = query.Count();
            var items = query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new JournalEntryDto
                {
                    Content = e.Content
                })
                .ToList();
            return new PaginatedList<JournalEntryDto>(items, count, pageIndex, pageSize);
        }
    }
}