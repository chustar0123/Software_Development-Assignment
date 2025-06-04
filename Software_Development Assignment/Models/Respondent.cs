using System;
using System.Collections.Generic;

namespace Software_Development_Assignment.Models;

public partial class Respondent
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string ContactNumber { get; set; }

    public DateTime DateOfBirth { get; set; }

    public virtual ICollection<FavoriteFood> FavoriteFoods { get; set; } = new List<FavoriteFood>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
