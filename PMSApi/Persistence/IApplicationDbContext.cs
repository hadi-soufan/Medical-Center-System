using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<AppUser> Users { get; }
        DbSet<Appointment> Appointments { get; }
        DbSet<Doctor> Doctors { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
