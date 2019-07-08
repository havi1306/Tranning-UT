using System;

namespace AudioBook.Core.DTO.Request
{
    public class AuthorCreateRequest
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}