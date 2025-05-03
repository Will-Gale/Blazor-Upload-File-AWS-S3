using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharedlayer.Models.Documents
{
    public class DocumentsDTO
    {

        public string Id { get; set; }

        public IBrowserFile File1 { get; set; }
        public byte[] File1Bytes { get; set; }

        public IBrowserFile File2 { get; set; }
        public byte[] File2Bytes { get; set; }

        public IBrowserFile File3 { get; set; }
        public byte[] File3Bytes { get; set; }

        public IBrowserFile File4 { get; set; }
        public byte[] File4Bytes { get; set; }

    }
}
