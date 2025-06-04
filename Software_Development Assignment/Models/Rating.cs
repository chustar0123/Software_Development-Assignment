using System;
using System.Collections.Generic;

namespace Software_Development_Assignment.Models;

public partial class Rating
{
    public int Id { get; set; }

    public int RespondentId { get; set; }

    public string Question { get; set; }

    public int Rating1 { get; set; }

    public virtual Respondent Respondent { get; set; }
}
