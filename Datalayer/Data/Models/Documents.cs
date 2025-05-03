using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datalayer.Data.Models
{
    public class Documents
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public bool isS3Bucket { get; set; }
        public bool isLocalStorage { get; set; }
        public string DocumentCategory { get; set; }
        public string FileName { get; set; }
        public string FileURL { get; set; }
        public string CreatedUser { get; set; }
        public DateTime DateUploaded { get; set; }
        public bool IsArchived { get; set; }
        public string WhoArchived { get; set; }
        public DateTime DateArchived { get; set; }

    }
}
