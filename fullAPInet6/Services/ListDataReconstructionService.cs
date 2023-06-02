using fullAPInet6.Models;

namespace fullAPInet6.Services
{
    public class ListDataReconstructionService
    {
        public List<ReturnData> RebuildData(ListParsingModels model)
        {
            List<ReturnData> returnDataList = new List<ReturnData>();
            returnDataList.Add(new ReturnData()
            {
                nameData = model.NameData,
                descData = model.DescData,
            });
            return returnDataList;
        }
    }
}