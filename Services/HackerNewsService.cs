using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HackerNewsFeed.Models;
using HackerNewsFeed.Utility;
using Newtonsoft.Json;

namespace HackerNewsFeed.Services
{
    public interface INewsFeed
    {
        public Task<List<StoryItemModel>> GetLatestNews();
        public List<FeedItemModel> SearchFeed(string searchTerm);
    }

    public class NewsFeed : INewsFeed
    {
        public async Task<List<StoryItemModel>> GetLatestNews()
        {
            try
            {

           
            List<FeedItemModel> feedList = new List<FeedItemModel>();
            using (var httpClient = new HttpClient())
            {
                using var feedItemResponse =
                    await httpClient.GetAsync("https://hacker-news.firebaseio.com/v0/newstories.json");
                string apiFeedResponse = await feedItemResponse.Content.ReadAsStringAsync();
                var articleList = JsonConvert.DeserializeObject<List<int>>(apiFeedResponse);
                Console.WriteLine("Response  ==>", articleList);

                var response = new List<StoryItemModel>();
                foreach (var articleId in articleList)
                {
                    using var storyItemResponse =
                        await httpClient.GetAsync(
                            $"https://hacker-news.firebaseio.com/v0/item/{articleId}.json?print=pretty");
                    string apiStoryResponse = await storyItemResponse.Content.ReadAsStringAsync();
                    var articleDetails = JsonConvert.DeserializeObject<StoryResponseModel>(apiStoryResponse);
                    Console.WriteLine("article ===>", articleDetails);
                    response.Add(new StoryItemModel()
                    {
                        Author = articleDetails.by,
                        Published = DateTime.Today.EpochToDateTime(articleDetails.time),
                        CommentCount = articleDetails.kids?.Count??0,
                        Title = articleDetails.title,
                        Url = articleDetails.url
                        //TODO: NATE => Get BaseURl and Add it!
                    });
                }

                return response;
            }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<FeedItemModel> SearchFeed(string searchTerm)
        {
            return null;
        }
    }
}