using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DocumentService.Domain.Models
{
    public class Document
    {
        [BsonId]
        public ObjectId Id { get; private set; }
        
        public string Name { get; private set; }
        
        public string Description { get; private set; }
        
        public TopicType Type { get; private set; }
        
        public ICollection<Comment> Comments { get; private set; }

        public Document(string name, string description, int typeId)
        {
            Name = name;
            Description = description;
            Type = (TopicType) typeId;
            Comments = new List<Comment>();
        }
        
        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void AddComment(string description, string content)
        {
            Comments.Add(new Comment {Description = description, Content = content});
        }
    }
}