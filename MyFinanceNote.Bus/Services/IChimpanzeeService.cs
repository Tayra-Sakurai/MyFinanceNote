using MyFinanceNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinanceNote.Services
{
    public interface IChimpanzeeService
    {
        /// <summary>
        /// Gets the list of content.
        /// </summary>
        /// <returns>The list of contents.</returns>
        public Task<IReadOnlyList<Tayra>> GetTayrasAsync();

    }
}
