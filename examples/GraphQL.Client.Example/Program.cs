using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace GraphQL.Client.Example;

public static class Program
{
    public static async Task Main()
    {
        using var graphQLClient = new GraphQLHttpClient("http://localhost:5000/AElfIndexer_DApp/testschema/graphql", new NewtonsoftJsonSerializer());

        var testBlockRequest = new GraphQLRequest
        {
            Query = @"
			    query ($id: String!) {
                    blockTest(id: $id){
                        chainId
                        blockHash,
                        previousBlockHash,
                        blockHeight,
                        blockTime,
                        signature,
                        confirmed
                    }
                }",
            // OperationName = "PersonAndFilms",
            Variables = new
            {
                id = "99a7c5f26b104fc3c3d7ab9decd30cb813c43f8113063c1b7f3a3b849f3487eb"
            }
        };

        var graphQLResponse = await graphQLClient.SendQueryAsync<TestBlockResponse>(testBlockRequest);
        Console.WriteLine("raw response:");
        Console.WriteLine(JsonSerializer.Serialize(graphQLResponse, new JsonSerializerOptions { WriteIndented = true }));

        Console.WriteLine();
        Console.WriteLine($"Name: {graphQLResponse.Data.BlockTest.Id}");

        Console.WriteLine();
        Console.WriteLine("Press any key to quit...");
        Console.ReadKey();
    }
}
