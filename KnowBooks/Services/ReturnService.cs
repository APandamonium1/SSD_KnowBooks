using KnowBooks.Data;

namespace KnowBooks.Services
{
    // Step 1: Create a BackgroundService class
    public class ReturnService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ReturnService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Step 2: Perform the operation at a regular interval (e.g., daily)
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

                // Step 3: Query and delete the expired rows
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<KnowBooksContext>();
                    TimeZoneInfo singaporeTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");

                    // Get the current UTC time
                    DateTime utcNow = DateTime.UtcNow;

                    // Convert the UTC time to Singapore time
                    DateTime currentDate = TimeZoneInfo.ConvertTimeFromUtc(utcNow, singaporeTimeZone);
                    //var expiredRows = context.Book.Where(row => row.ReturnDate <= currentDate);
                    var rows = context.Book.Where(row => row.ReturnDate <= currentDate);

                    foreach (var row in rows)
                    {
                        // Modify the specific value as needed
                        row.Borrower = "";
                        row.ReturnDate = null;
                        row.AvailabilityStatus = "Available";
                    }

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
