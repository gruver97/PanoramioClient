using System.Threading.Tasks;

namespace PanoramioClient.Services
{
    public interface IPanoramioService
    {
        Task<string> GetImagesUrlAsync(double minX, double maxX, double minY, double maxY);
    }
}