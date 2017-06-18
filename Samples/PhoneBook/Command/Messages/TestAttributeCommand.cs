using System;
using System.ComponentModel.DataAnnotations;
using Chakad.Pipeline.Core.Attributes;
using Chakad.Pipeline.Core.Command;
using Chakad.Pipeline.Core.Message;

namespace Chakad.Samples.PhoneBook.Commands
{
    public class TestAttributeCommand : ChakadRequest<ChakadResult>
    {
        [GuidRequired]
        public Guid Id { get; set; }
        [Required]
        [StringLength(5)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public int MaxValue { get; set; }
        [MinLength(10)]
        public int MinValue { get; set; }
        [Range(10,20)]
        public int Range { get; set; }
    }
    
}