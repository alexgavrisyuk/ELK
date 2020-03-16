using System.Collections.Generic;

namespace DocumentService.Supervisor.Models.Response
{
    public class DocumentResponseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public ICollection<CommentResponseModel> Items { get; set; }
    }

    public class CommentResponseModel
    {
        public string Description { get; set; }
        
        public string Content { get; set; }
    }
}