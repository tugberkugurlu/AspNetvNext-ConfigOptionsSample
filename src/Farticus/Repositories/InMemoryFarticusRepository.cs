using System;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Framework.Logging;

namespace Farticus
{
    public class InMemoryFarticusRepository : IFarticusRepository, IDisposable
    {
        private bool _disposed = false;
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

        private readonly ILogger _logger;

        public InMemoryFarticusRepository(ILoggerFactory loggerFactory)
        {
            if(loggerFactory == null)
            {
                throw new ArgumentNullException("loggerFactory");
            }

            _logger = loggerFactory.Create(typeof(InMemoryFarticusRepository).FullName);

            _logger.Write(
                LogLevel.Verbose,
                0,
                "Instance has been successfully constructed.", null,
                (state, ex) => (string)state);
        }

        public Task<string> GetFartMessageAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            _logger.Write(
                LogLevel.Verbose,
                0,
                "GetFartMessageAsync has been called.", null,
                (state, ex) => (string)state);

            ThrowIfDisposed();
            return Task.FromResult<string>(_messages.OrderBy(_ => Guid.NewGuid()).First());
        }

        public void Dispose()
        {
            _logger.Write(
                LogLevel.Verbose,
                0,
                "Dispose has been called.", null,
                (state, ex) => (string)state);

            if(_disposed == false)
            {
                _disposed = true;
            }
        }

        private void ThrowIfDisposed()
        {
            if(_disposed == true)
            {
                throw new ObjectDisposedException(typeof(InMemoryFarticusRepository).FullName);
            }
        }
    }
}