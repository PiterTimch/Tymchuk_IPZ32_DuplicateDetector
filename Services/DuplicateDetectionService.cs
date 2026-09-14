using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Abstractions;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Models;
using Tymchuk_Petro_IPZ_32_Duplocate_Detector.Utilities;

namespace Tymchuk_Petro_IPZ_32_Duplocate_Detector.Services
{
    public class DuplicateDetectionService : IDuplicateDetectionService
    {
        public async Task<List<DuplicateGroupResult>> FindDuplicatesAsync(IEnumerable<FileInfoModel> files)
        {
            List<DuplicateGroupResult> resultsList = new List<DuplicateGroupResult>();

            IEnumerable<IGrouping<long, FileInfoModel>> sizeGroupsList = files
                .GroupBy(fileItem => fileItem.FileSize)
                .Where(groupItem => groupItem.Count() > 1);

            foreach (IGrouping<long, FileInfoModel> sizeGroupItem in sizeGroupsList)
            {
                List<FileInfoModel> candidateFilesList = sizeGroupItem.ToList();

                ConcurrentDictionary<string, ConcurrentBag<FileInfoModel>> partialHashGroupsMap = 
                    new ConcurrentDictionary<string, ConcurrentBag<FileInfoModel>>();

                await Parallel.ForEachAsync(candidateFilesList, async (candidateItem, cancelToken) =>
                {
                    string hashText = await FileHashCalculatorUtility.ComputePartialHashAsync(candidateItem.FilePath);
                    if (!string.IsNullOrEmpty(hashText))
                    {
                        ConcurrentBag<FileInfoModel> bagItem = partialHashGroupsMap.GetOrAdd(
                            hashText, 
                            keyItem => new ConcurrentBag<FileInfoModel>()
                        );
                        bagItem.Add(candidateItem);
                    }
                });

                IEnumerable<KeyValuePair<string, ConcurrentBag<FileInfoModel>>> potentialDuplicatesList = 
                    partialHashGroupsMap.Where(pairItem => pairItem.Value.Count > 1);

                foreach (KeyValuePair<string, ConcurrentBag<FileInfoModel>> potentialPairItem in potentialDuplicatesList)
                {
                    List<FileInfoModel> partialMatchesList = potentialPairItem.Value.ToList();

                    ConcurrentDictionary<string, ConcurrentBag<FileInfoModel>> fullHashGroupsMap = 
                        new ConcurrentDictionary<string, ConcurrentBag<FileInfoModel>>();

                    await Parallel.ForEachAsync(partialMatchesList, async (partialItem, cancelToken) =>
                    {
                        string fullHashText = await FileHashCalculatorUtility.ComputeFullHashAsync(partialItem.FilePath);
                        if (!string.IsNullOrEmpty(fullHashText))
                        {
                            ConcurrentBag<FileInfoModel> bagItem = fullHashGroupsMap.GetOrAdd(
                                fullHashText, 
                                keyItem => new ConcurrentBag<FileInfoModel>()
                            );
                            bagItem.Add(partialItem);
                        }
                    });

                    IEnumerable<KeyValuePair<string, ConcurrentBag<FileInfoModel>>> verifiedDuplicatesList = 
                        fullHashGroupsMap.Where(pairItem => pairItem.Value.Count > 1);

                    foreach (KeyValuePair<string, ConcurrentBag<FileInfoModel>> verifiedPairItem in verifiedDuplicatesList)
                    {
                        List<string> pathList = verifiedPairItem.Value
                            .Select(fileItem => fileItem.FilePath)
                            .ToList();

                        DuplicateGroupResult groupResultItem = new DuplicateGroupResult
                        {
                            FileSize = sizeGroupItem.Key,
                            FilePathsList = pathList
                        };

                        resultsList.Add(groupResultItem);
                    }
                }
            }

            return resultsList;
        }
    }
}
