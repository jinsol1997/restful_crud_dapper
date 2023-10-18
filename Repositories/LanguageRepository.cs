using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using restful_crud_dapper.Models;

public class LanguageRepository{

    private readonly IConfiguration _configuration;

    public LanguageRepository(IConfiguration configuration){
        _configuration = configuration;
    }

    private IDbConnection getConnection(){
        string connectionString = _configuration.GetConnectionString("DefaultConnection");
        return new MySqlConnection(connectionString);
    }

    public async Task<List<Language>> SelectAll(){

        using IDbConnection dbConnection = getConnection();
        string query = "SELECT * FROM language";

        return (await dbConnection.QueryAsync<Language>(query)).AsList();
    }

    public async Task<Language> SelectById(int id){
        
        using IDbConnection dbConnection = getConnection();
        string query = "SELECT * FROM language WHERE language_id = @id";

        try{
            return await dbConnection.QuerySingleAsync<Language>(query, new {id});
        }catch(InvalidOperationException e){
            return null;
        }
    }

    public async Task<int> Insert(Language language){

        using IDbConnection dbConnection = getConnection();

        string query = @"
        INSERT INTO language (Name) VALUES (@Name);
        SELECT LAST_INSERT_ID();
        ";

        return await dbConnection.QuerySingleAsync<int>(query, language);
    }

    public async Task<int> Update(Language language){

        using IDbConnection dbConnection = getConnection();

        string query = "UPDATE language SET Name = @Name WHERE language_id = @language_id";

        return await dbConnection.ExecuteAsync(query, language);
    }

    public async Task<int> DeleteById(int id){

        using IDbConnection dbConnection = getConnection();

        string query = "DELETE FROM language WHERE language_id = @language_id";

        return await dbConnection.ExecuteAsync(query, new {language_id = id});
    }
}