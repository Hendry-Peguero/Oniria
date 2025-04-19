namespace Oniria.Services
{
    public class BackgroundService : IBackgroundService
    {
        private readonly IWebHostEnvironment environment;
        private readonly string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        private const string relativeFolder = "images/random-backgrounds";

        public BackgroundService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public string GetRandom()
        {
            var fullPath = Path.Combine(environment.WebRootPath, relativeFolder);

            if (!Directory.Exists(fullPath)) return string.Empty;

            var files = Directory
                .GetFiles(fullPath)
                .Where(f => allowedExtensions.Contains(Path.GetExtension(f).ToLower()))
                .ToList();

            if (files.Count == 0) return string.Empty;

            var random = new Random();
            var selected = Path.GetFileName(files[random.Next(files.Count)]);
            return $"/{relativeFolder}/{selected}".Replace("\\", "/");
        }
    }

    public interface IBackgroundService
    {
        string GetRandom();
    }
}
