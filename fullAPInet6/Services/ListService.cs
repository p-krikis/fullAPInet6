
using Amazon.Util;
using fullAPInet6.Models;
using MongoDB.Driver;

namespace fullAPInet6.Services
{
    public class ListService
    {
        private readonly IMongoCollection<ListModels> _lists;

        public ListService()
        {
            MongoClient client = new("mongodb+srv://<USERNAME_HERE>:<PASSWORD_HERE>@fullstackclustertest.qdnrxbu.mongodb.net/?retryWrites=true&w=majority");
            IMongoDatabase db = client.GetDatabase("FullstackDB");
            _lists = db.GetCollection<ListModels>("Lists");
        }
        public async Task<string> ListSave(ListModels list)
        {
            if (list.MainData.Length != list.DescData.Length)
            {
                return null;
            }
            else
            {
                await _lists.InsertOneAsync(list);
                return "ok";
            }
            
        }
        public async  Task<List<ListModels>> GetAllLists (string userID)
        {
            var dataList = await _lists.Find(lu => lu.UserID == userID).ToListAsync();
            if (dataList != null)
            {
                return dataList;
            }
            else
            {
                return null;
            }
        }
        public async Task<ListModels> LoadList (string userID, string listName)
        {
            var listFilter = Builders<ListModels>.Filter.And(
                Builders<ListModels>.Filter.Eq(u => u.UserID, userID),
                Builders<ListModels>.Filter.Eq(ln => ln.ListName, listName));
            var query = await _lists.Find(listFilter).FirstOrDefaultAsync();
            return query;
        }
        public async Task UpdateList (string listID, ListModels listTBU)
        {
            await _lists.ReplaceOneAsync(l => l.ListID == listID, listTBU);
        }
        public async Task DeleteList (string listID)
        {
            await _lists.DeleteOneAsync(l => l.ListID == listID);
        }
    }
}