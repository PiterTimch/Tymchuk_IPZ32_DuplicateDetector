using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Tymchuk_Petro_IPZ_32_Duplocate_Detector.Utilities
{
    public static class FileHashCalculatorUtility
    {
        public static async Task<string> ComputePartialHashAsync(string pathText)
        {
            try
            {
                byte[] bufferArray = new byte[4096];
                await using FileStream streamItem = new FileStream(pathText, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
                int bytesReadCount = await streamItem.ReadAsync(bufferArray, 0, bufferArray.Length);
                
                using SHA256 shaAlgorithm = SHA256.Create();
                byte[] hashArray = shaAlgorithm.ComputeHash(bufferArray, 0, bytesReadCount);
                return Convert.ToHexString(hashArray);
            }
            catch (UnauthorizedAccessException)
            {
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static async Task<string> ComputeFullHashAsync(string pathText)
        {
            try
            {
                await using FileStream streamItem = new FileStream(pathText, FileMode.Open, FileAccess.Read, FileShare.Read, 8192, true);
                using SHA256 shaAlgorithm = SHA256.Create();
                byte[] hashArray = await shaAlgorithm.ComputeHashAsync(streamItem);
                return Convert.ToHexString(hashArray);
            }
            catch (UnauthorizedAccessException)
            {
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
