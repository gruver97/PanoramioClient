using System.Collections.Generic;
using System.Threading.Tasks;

namespace PanoramioClient.Services
{
    public interface IPanoramioService
    {
        Task<IEnumerable<string>> GetImagesUrlAsync(double minX, double maxX, double minY, double maxY);
    }
}