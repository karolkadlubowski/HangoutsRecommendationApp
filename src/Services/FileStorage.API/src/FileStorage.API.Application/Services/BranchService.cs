using System.Threading.Tasks;
using AutoMapper;
using FileStorage.API.Application.Abstractions;
using FileStorage.API.Application.Database.Repositories;
using FileStorage.API.Application.Features.GetBranchByName;
using FileStorage.API.Domain.Entities;
using FileStorage.API.Domain.ValueObjects;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Library.Shared.Models.FileStorage.Dtos;

namespace FileStorage.API.Application.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public BranchService(IBranchRepository branchRepository,
            IMapper mapper,
            ILogger logger)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BranchDto> GetBranchByNameAsync(GetBranchByNameQuery query)
        {
            var branchName = new BranchName(query.Name);

            var branchPersistenceModel = await _branchRepository.GetBranchAsync(branchName)
                                         ?? throw new EntityNotFoundException($"Branch with name: '{query.Name}' not found in the database");

            var branch = _mapper.Map<Branch>(branchPersistenceModel);

            _logger.Info($"Branch with name: '{branch.Name}' found in the database");

            return _mapper.Map<BranchDto>(branch);
        }
    }
}