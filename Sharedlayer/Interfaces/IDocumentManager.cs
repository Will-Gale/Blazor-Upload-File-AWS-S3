using Datalayer.Data.Models;
using Sharedlayer.Models.Documents;
using Sharedlayer.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharedlayer.Interfaces
{
    public interface IDocumentManager
    {
        Task<Result<DocumentsDTO>> SaveDocuments(DocumentsDTO docs);
        Task<Result<Documents>> RecordDocuments(string key, string documentcategory, string filename);
    }
}
