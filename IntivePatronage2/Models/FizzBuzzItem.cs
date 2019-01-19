using System;
using System.ComponentModel.DataAnnotations;

namespace IntivePatronage2.Model
{
    public class FizzBuzzItem
    {
        public int Id { set; get; }

        [Required]
        public int Value { set; get; }

        public string Result { set; get; } = String.Empty;
    }
}
