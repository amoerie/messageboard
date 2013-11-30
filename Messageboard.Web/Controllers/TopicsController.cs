using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Messageboard.Domain;
using Messageboard.Domain.Infrastructure.Filtering;
using Messageboard.Domain.Infrastructure.Including;
using Messageboard.Domain.Infrastructure.Sorting;
using Messageboard.Domain.Models;

namespace Messageboard.Web.Controllers
{
    public class TopicsController: ApiController
    {
        private readonly IMessageboardService _messageboard;

        public TopicsController(IMessageboardService messageboard)
        {
            _messageboard = messageboard;
        }

        public async Task<HttpResponseMessage> Get(bool includeReplies = false)
        {
            var includer = includeReplies
                ? EntityIncluder<Topic>.Include(t => t.Replies)
                : EntityIncluder<Topic>.AsQueryable();
            var sorter = EntitySorter<Topic>.OrderByDescending(t => t.CreationDate);
            var topics = _messageboard.Topics
                .Query(sorter: sorter, includer: includer)
                .Take(25);
            await topics.LoadAsync();
            return Request.CreateResponse(HttpStatusCode.OK, topics);
        }

        public async Task<HttpResponseMessage> Get(int id, bool includeReplies = false)
        {
            var filter = EntityFilter<Topic>.Where(t => t.Id == id);
            var includer = includeReplies
                ? EntityIncluder<Topic>.Include(t => t.Replies)
                : EntityIncluder<Topic>.AsQueryable();
            var topic = await _messageboard.Topics.SingleOrDefaultAsync(filter, includer);
            return topic == null 
                ? Request.CreateResponse(HttpStatusCode.NotFound) 
                : Request.CreateResponse(HttpStatusCode.OK, topic);
        }

        public async Task<HttpResponseMessage> Post([FromBody] Topic topic)
        {
            /* Set creation date if necessary */
            if(topic.CreationDate == default( DateTime ))
                topic.CreationDate = DateTime.Now;;

            try
            {
                _messageboard.Topics.Save(topic);
                await _messageboard.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.Created, topic);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}