using Application.Core;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Photos
{
    public class ListUsersPhoto
    {
        public class Query: IRequest<Result<List<PatientPhoto>>>
        {
            public string UserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<PatientPhoto>>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Result<List<PatientPhoto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var patientPhoto = await _context.PatientPhotos
                        .Where(up => up.AppUserId == request.UserId)
                        .ToListAsync();

                    if (patientPhoto is null) return Result<List<PatientPhoto>>.Failure("No photos found for the user");

                    return Result<List<PatientPhoto>>.Success(patientPhoto);

                }
                catch(Exception ex)
                {
                    throw new Exception("Error while fetching users photo", ex);
                }
            }
        }
    }
}
