using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AspNetCorePostgreSQLDockerApp.Repository
{
    public class DockerCommandsRepository : IDockerCommandsRepository
    {
        private readonly DockerCommandsDbContext _context;
        private readonly ILogger _logger;

        public DockerCommandsRepository(DockerCommandsDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("DockerCommandsRepository");
        }

        public async Task<List<DockerCommand>> GetDockerCommandsAsync()
        {
            return await _context.DockerCommands.Include(dc => dc.Examples).ToListAsync();
        }

        public async Task InsertDockerCommandAsync(DockerCommand command)
        {
            _context.DockerCommands.Add(command);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception exp)
            {
                _logger.LogError($"Error in {nameof(InsertDockerCommandAsync)}: " + exp.Message);
            }
        }
    }
}