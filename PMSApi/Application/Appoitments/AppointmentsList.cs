using Application.Accountants;
using Application.Appoitments;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Appointments
{
    /// <summary>
    /// Provides functionality to list appointments.
    /// </summary>
    public class AppointmentsList
    {
        /// <summary>
        /// Query to retrieve a list of appointments.
        /// </summary>
        public class Query : IRequest<Result<List<AppointmentDto>>> {}

        /// <summary>
        /// Handler to process the query and return a list of appointments.
        /// </summary>
        public class Handler : IRequestHandler<Query, Result<List<AppointmentDto>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            /// <summary>
            /// Handles the query to retrieve a list of appointments.
            /// </summary>
            /// <param name="request">The query request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result containing a list of appointment DTOs.</returns>
            public async Task<Result<List<AppointmentDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var appointments = await _context.Appointments
                    .Include(a => a.Patient)
                    .Include(a => a.Doctor)
                    .Where(a => !a.IsCancelled)
                    .ProjectTo<AppointmentDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                    return Result<List<AppointmentDto>>.Success(appointments);
                }
                catch (Exception ex)
                {
                    return Result<List<AppointmentDto>>.Failure(ex.Message);
                }
            }
        }
    }
    }
