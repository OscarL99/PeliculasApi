using PeliculasApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace PeliculasApi.Services
{
    public class MoviesService
    {
        private readonly IMongoCollection<Movie> _movieCollection;

        public MoviesService(IOptions<MovieStoreDatabaseSettings> movieStoreDatabaseSettings)
        {
            var mongoClient=new MongoClient(movieStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(movieStoreDatabaseSettings.Value.DatabaseName);
            _movieCollection = mongoDatabase.GetCollection<Movie>(movieStoreDatabaseSettings.Value.MoviesCollectionName);
        }
        public async Task<List<Movie>> GetAsync() =>
            await _movieCollection.Find(_ => true).ToListAsync();

        public async Task<Movie?>GetAsync(string id) => await  _movieCollection.Find(x=>x.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(Movie newMovie) => await _movieCollection.InsertOneAsync(newMovie);
        public async Task UpdateAsync(string id, Movie updateMovie) => await _movieCollection.ReplaceOneAsync(x => x.Id == id, updateMovie);
        public async Task RemoveAsync(string id) => await _movieCollection.DeleteOneAsync(x => x.Id == id);

    }
}
