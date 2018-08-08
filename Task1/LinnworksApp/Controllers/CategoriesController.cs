using Api_App.Servises;
using API_TestApp2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace API_TestApp2.Controllers
{
    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiController
    {
        public CategoriesController() :base() {

        }

        [Route("GetCategories")]
        public async Task<List<Category>> GetCategoriesWithProductCount()
        {
            List<Category> output = await GetCategories();
            string query = "SELECT p.CategoryId, Count(*) as ProductsCount FROM StockItem AS S INNER JOIN ProductCategories AS P ON P.CategoryId = S.CategoryId GROUP BY P.CategoryId";
            List<Category> withProductsCount = await ExecuteCustomQuery(query);
            withProductsCount.ForEach(c => output.Find(cat => cat.CategoryId == c.CategoryId).ProductsCount = c.ProductsCount);

            return output;
        }

        [Route("")]
        public async Task<List<Category>> GetCategories()
        {
            string url = ConfigurationManager.AppSettings["LinnworksApi"] + "Inventory/GetCategories";

            var output = JsonConvert.DeserializeObject<List<Category>>(
            await HttpClientService.Instance.GetStringAsync(url)
            );

            return output;
        }
        
        [NonAction]
        public async Task<List<Category>> ExecuteCustomQuery(string query)
        {
            string url = ConfigurationManager.AppSettings["LinnworksApi"] + "Dashboards/ExecuteCustomScriptQuery";
            
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("script", query),
            });

            HttpResponseMessage response = await HttpClientService.Instance.PostAsync(url, formContent);
            response.EnsureSuccessStatusCode();
            string resultAsString = response.Content.ReadAsStringAsync().Result;
            var resoponseResult = JsonConvert.DeserializeObject<CustomQueryResponse<Category>>(resultAsString);

            return resoponseResult.Results;
        }

        [Route("CreateCategory/{categoryName}")]
        [AcceptVerbs("POST","GET")]
        public async Task<Category> CreateCategory(string categoryName)
        {
            string url = ConfigurationManager.AppSettings["LinnworksApi"] + "Inventory/CreateCategory";

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("categoryName", categoryName)
            });

            HttpResponseMessage response = await HttpClientService.Instance.PostAsync(url, formContent);
            response.EnsureSuccessStatusCode();
            string resultAsString = response.Content.ReadAsStringAsync().Result;
            var resoponseResult = JsonConvert.DeserializeObject<Category>(resultAsString);

            return resoponseResult;
        }

        [Route("UpdateCategory/{categoryId}/{categoryName}")]
        [AcceptVerbs("POST", "GET")]
        public async Task UpdateCategory(Guid categoryId, string categoryName)
        {
            string url = ConfigurationManager.AppSettings["LinnworksApi"] + "Inventory/UpdateCategory";

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("category", JsonConvert.SerializeObject(new Category() { CategoryId = categoryId, CategoryName = categoryName }))
            });

            HttpResponseMessage response = await HttpClientService.Instance.PostAsync(url, formContent);
            response.EnsureSuccessStatusCode();
        }

        [Route("DeleteCategory/{categoryId}")]
        [AcceptVerbs("POST", "GET")]
        public async Task DeleteCategory(Guid categoryId)
        {
            string url = ConfigurationManager.AppSettings["LinnworksApi"] + "Inventory/DeleteCategoryById";

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("categoryId", categoryId.ToString())
            });

            HttpResponseMessage response = await HttpClientService.Instance.PostAsync(url, formContent);
            response.EnsureSuccessStatusCode();
        }
    }
}
