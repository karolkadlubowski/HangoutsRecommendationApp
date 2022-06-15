using System;
using Library.Shared.Exceptions;
using Library.Shared.Models;

namespace VenueReview.API.Domain.ValueObjects
{
    public record ReviewContent : ValueObject<String>
    {
        public ReviewContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ValidationException($"{nameof(content)} cannot be null or empty");
            
            Value = content;
        }
    }
}