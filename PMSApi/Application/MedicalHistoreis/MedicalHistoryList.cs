using Application.Core;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.MedicalHistories
{
    /// <summary>
    /// Represents a query to retrieve a list of medical histories.
    /// </summary>
    public class MedicalHistoryList
    {
        /// <summary>
        /// Represents the query to retrieve a list of medical histories.
        /// </summary>
        public class Query : IRequest<Result<List<MedicalHistory>>> { }

        /// <summary>
        /// Represents the handler for the <see cref="Query"/> to retrieve a list of medical histories.
        /// </summary>
        public class Handler : IRequestHandler<Query, Result<List<MedicalHistory>>>
        {
            private readonly ApplicationDbContext _context;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler"/> class with the specified application database context.
            /// </summary>
            /// <param name="context">The application database context.</param>
            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            /// <summary>
            /// Handles the query to retrieve a list of medical histories.
            /// </summary>
            /// <param name="request">The query representing the request to retrieve the list of medical histories.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A task representing the asynchronous operation. The task result is the list of <see cref="MedicalHistory"/> objects.</returns>
            public async Task<Result<List<MedicalHistory>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var medicalHistories = await _context.MedicalHistories
                    .Include(mh => mh.Patient)
                    .ThenInclude(p => p.User)
                    .Select(mh => new MedicalHistory
                    {
                        MedicalHistoryId = mh.MedicalHistoryId,
                        Height = mh.Height,
                        Weight = mh.Weight,
                        MedicalProblems = mh.MedicalProblems,
                        MentalHealthProblems = mh.MentalHealthProblems,
                        Medicines = mh.Medicines,
                        Allergics = mh.Allergics,
                        SugreriesHistory = mh.SugreriesHistory,
                        Vaccines = mh.Vaccines,
                        Diagnosis = mh.Diagnosis,
                        TestsPerformed = mh.TestsPerformed,
                        TreatmenPlans = mh.TreatmenPlans,
                        FamilyMedicalHistory = mh.FamilyMedicalHistory,
                        PatientId = mh.PatientId,
                        PatientName = mh.Patient.User.DisplayName
                    })
                    .ToListAsync(cancellationToken);

                return Result<List<MedicalHistory>>.Success(medicalHistories);


                //return Result<List<MedicalHistory>>.Success(await _context.MedicalHistories.ToListAsync(cancellationToken: cancellationToken));
            }
        }
    }

}
