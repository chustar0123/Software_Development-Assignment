using System;
using System.Collections.Generic;

namespace Software_Development_Assignment.Models;

public partial class FavoriteFood
{
    public int Id { get; set; }

    public int RespondentId { get; set; }

    public string FoodName { get; set; }

    public virtual Respondent Respondent { get; set; }
}
