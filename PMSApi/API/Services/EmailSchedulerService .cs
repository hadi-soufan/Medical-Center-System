using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Services
{
    public class EmailSchedulerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<EmailSchedulerService> _logger;

        public EmailSchedulerService(IServiceProvider serviceProvider, ILogger<EmailSchedulerService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await SendReminderEmails(stoppingToken);
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }

        private async Task SendReminderEmails(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                var tomorrow = DateTime.Now.AddDays(1).Date;

                var appointments = await context.Appointments
             .Include(a => a.Patient)
                 .ThenInclude(p => p.User) 
             .Include(a => a.Doctor)
                 .ThenInclude(d => d.User) 
             .Where(a => a.AppointmentDateStart.Date == tomorrow && !a.IsCancelled)
             .ToListAsync(stoppingToken);

                foreach (var appointment in appointments)
                {
                    var emailSubject = "Appointment Reminder";
                    var appointmentDate = appointment.AppointmentDateStart.ToString("MM/dd/yyyy 'at' hh:mm:ss tt");
                    var confirmationLink = $"http://localhost:5000/api/appointments/confirm/{appointment.AppointmentId}";
                    var emailBody = $@"
                Dear {appointment.Patient.User?.DisplayName ?? "Patient"},<br/><br/>
                This is a reminder for your appointment with Dr. {appointment.Doctor.User?.DisplayName ?? "Doctor"} on {appointmentDate}.<br/><br/>
                <a href='{confirmationLink}' style='display: inline-block; padding: 10px 20px; font-size: 16px; color: #ffffff; background-color: #007bff; text-decoration: none; border-radius: 5px;'>
                Confirm your appointment
            </a>.<br/><br/>
                Thank you.";

                    await emailService.SendEmailAsync(appointment.Patient.User?.Email, emailSubject, emailBody);
                }
            }
        }
    }
}
