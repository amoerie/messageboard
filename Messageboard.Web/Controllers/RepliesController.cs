using System;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Messageboard.Domain;
using Messageboard.Domain.Infrastructure.Filtering;
using Messageboard.Domain.Infrastructure.Including;
using Messageboard.Domain.Models;

namespace Messageboard.Web.Controllers
{
    public class RepliesController: ApiController
    {
        private readonly IMessageboardService _messageboard;

        public RepliesController(IMessageboardService messageboard)
        {
            _messageboard = messageboard;
        }

        public async Task<HttpResponseMessage> Get(int topicId)
        {
            var filter = EntityFilter<Reply>.Where(r => r.TopicId == topicId);
            var replies = _messageboard.Replies.Query(filter);
            await replies.LoadAsync();
            return Request.CreateResponse(HttpStatusCode.OK, replies);
        }

        public async Task<HttpResponseMessage> Post(int topicId, [FromBody] Reply reply)
        {
            if (reply.CreationDate == default( DateTime ))
                reply.CreationDate = DateTime.Now;

            var topic = await _messageboard.Topics.SingleOrDefaultAsync(EntityFilter<Topic>.Where(t => t.Id == topicId), EntityIncluder<Topic>.Include(t => t.Replies));
            if (topic == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            topic.Replies.Add(reply);
            try
            {
                await _messageboard.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.Created, reply);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}