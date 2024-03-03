using Application.Core;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Profile
{
    public class UserList
    {
        public class Query : IRequest<Result<List<ProfileDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<ProfileDto>>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Result<List<ProfileDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var users = await _context.Users
                    .Select(u => new ProfileDto
                    {
                        DisplayName = u.DisplayName,
                        FatherName = u.FatherName,
                        MotherName = u.MotherName,
                        DateOfBirth = u.DateOfBirth,
                        Nationality = u.Nationality,
                        Education = u.Education,
                        Gender = u.Gender,
                        MaritalStatus = u.MaritalStatus,
                        BloodType = u.BloodType,
                        Address = u.Address,
                        City = u.City,
                        ZipCode = u.ZipCode,
                        State = u.State,
                        Occupation = u.Occupation,
                        Role = u.Role,
                        Rank = u.Rank,
                        CreatedAt = u.CreatedAt,
                        IsCancelled = u.IsCancelled,
                    })
                    .ToListAsync(cancellationToken);

                return Result<List<ProfileDto>>.Success(users);
            }
        }
    }
}
