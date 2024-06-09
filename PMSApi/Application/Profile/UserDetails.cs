using Application.Core;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profile
{
    public class UserDetails
    {
        public class Query : IRequest<Result<ProfileDto>>
        {
            public string DisplayName { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ProfileDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<ProfileDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.DisplayName == request.DisplayName, cancellationToken);

                if (user is null) return Result<ProfileDto>.Failure("User not found");

                var userDto = _mapper.Map<AppUser, ProfileDto>(user);

                return Result<ProfileDto>.Success(userDto);
            }
        }
    }
}
