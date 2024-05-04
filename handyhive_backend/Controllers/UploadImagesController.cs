using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace handyhive_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadImagesController : ControllerBase
    {
        private readonly string _uploadFolder = "Uploads"; 
		//private readonly List<Film> _films = new List<Film>(); // Simulation des données en mémoire (vous devrez utiliser une base de données réelle)
		private readonly AppDbContext _db;
		public UploadImagesController(AppDbContext _db)
		{
			this._db = _db;
		}
        [HttpPost("upload")]

        [RequestSizeLimit(2L * 1024 * 1024 * 1024)]
        public async Task<IActionResult> UploadImage(IFormFile ImageFile)
        {
            try
            {
                if (ImageFile == null || ImageFile.Length == 0)
                {
                    return new BadRequestObjectResult("No video file detected.");
                }

                // Create directory if it doesn't exist
                if (!Directory.Exists(_uploadFolder))
                {
                    Directory.CreateDirectory(_uploadFolder);
                }

                // Generate unique file name to prevent naming conflicts
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;

                // Combine the upload folder path with the unique file name
                var filePath = Path.Combine(_uploadFolder, uniqueFileName);

                // Save the video file to the server
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);

                }

                // You may save the file path to a database or return it as response
                var fileUrl = Path.Combine("~", filePath);



                return Ok(new { filePath = fileUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{fileName}")]
        public IActionResult GetVideo(string fileName)
        {
            var filePath = Path.Combine(_uploadFolder, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(); // Si la vidéo n'existe pas, retourne 404 Not Found
            }

            // Lire la vidéo en tant que tableau de bytes
            var videoBytes = System.IO.File.ReadAllBytes(filePath);

            // Retourne le contenu de la vidéo avec le type de contenu approprié
            return File(videoBytes, "video/mp4"); // Vous pouvez ajuster le type de contenu selon le format de votre vidéo
        }
    }
}
