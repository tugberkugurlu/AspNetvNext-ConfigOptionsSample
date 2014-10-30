using System;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Farticus
{
    public class InMemoryFarticusRepository
    {
        private readonly IEnumerable<string> _messages = new List<string>()
        {
            "Yes! This is my favorite sound in the whole world",
            "Excellent. Don't hold back!",
            "Great job! It smells like roses in here now",
            "Sometimes you just have to let 'em rip",
            "Ahhh, that felt right",
            "Ooops, I'm not sure about that one",
            "Wow, someone has been practicing",
            "You might want to be a little careful",
            "What a stinker. The paint is coming off the walls"
        };

        public Task<string> GetFartMessageAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult<string>(_messages.OrderBy(_ => Guid.NewGuid()).First());
        }
    }
}