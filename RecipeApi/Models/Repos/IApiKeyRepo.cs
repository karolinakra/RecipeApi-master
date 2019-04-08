using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApi.Models.Repos
{
    public class ApiKeyRepo : Interfaces.IApiKeyRepo

    {

        private readonly RecipeContext context;



        public ApiKeyRepo(RecipeContext context)

        {

            this.context = context;

        }

        public async Task<bool> CheckValidUserKey(string key)

        {

            var keyList = await GetKeys();

            return keyList.Contains(key);

        }

        public async Task<List<string>> GetKeys()

        {

            return await context.Keys.Where(a => a.ExpirationDate >= DateTime.Now).Select(a => a.Name).ToListAsync();

        }

        public Dictionary<string, string> GetDictionary()

        {

            var list = context.Keys.Where(a => a.ExpirationDate >= DateTime.Now).ToList();

            var names = list.Select(a => a.Name).Distinct().ToList();

            var dictionary = new Dictionary<string, string>();

            names.ForEach(n =>

            {

                dictionary.Add(n, list.First(k => k.Name == n).Role);

            });

            return dictionary;

        }

        public bool CheckApiKey(string key)
        {
            throw new NotImplementedException();
        }
    }
}
