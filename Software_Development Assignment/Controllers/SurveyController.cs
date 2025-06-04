using Microsoft.AspNetCore.Mvc;
using Software_Development_Assignment.Models;
using Software_Development_Assignment.Models.DTOs;
using System.Diagnostics;

namespace Software_Development_Assignment.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SurveyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SubmitSurvey()
        {
            var dto = new SurveyDTO
            {
                Ratings = new List<RatingDTO>
        {
            new RatingDTO { Question = "I like to watch Movies" },
            new RatingDTO { Question = "I like to listen to radio" },
            new RatingDTO { Question = "I like to eat out" },
            new RatingDTO { Question = "I like to watch TV" }
        }
            };

            return View(dto);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitSurvey(SurveyDTO dto)
        {
            if (!ModelState.IsValid)
            {
                if (dto.Ratings == null || dto.Ratings.Count == 0)
                {
                    dto.Ratings = new List<RatingDTO>
                    {
                        new RatingDTO { Question = "I like to watch Movies" },
                        new RatingDTO { Question = "I like to listen to radio" },
                        new RatingDTO { Question = "I like to eat out" },
                        new RatingDTO { Question = "I like to watch TV" }
                    };
                }
                dto.FavoriteFoods ??= new List<string>();
                return View(dto);
            }

            try
            {
                var respondent = new Respondent
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName ?? string.Empty,
                    Email = dto.Email,
                    ContactNumber = dto.ContactNumber,
                    DateOfBirth = dto.DateOfBirth
                };

                _context.Respondents.Add(respondent);
                await _context.SaveChangesAsync();

                // Save favorite foods if any are selected
                if (dto.FavoriteFoods != null && dto.FavoriteFoods.Any())
                {
                    foreach (var food in dto.FavoriteFoods)
                    {
                        var favorite = new FavoriteFood
                        {
                            RespondentId = respondent.Id,
                            FoodName = food
                        };
                        _context.FavoriteFoods.Add(favorite);
                        await _context.SaveChangesAsync();
                    }
                }

                // Save ratings
                if (dto.Ratings != null && dto.Ratings.Any())
                {
                    foreach (var rating in dto.Ratings)
                    {
                        _context.Ratings.Add(new Rating
                        {
                            RespondentId = respondent.Id,
                            Question = rating.Question,
                            Rating1 = rating.Rating
                        });
                    }
                }

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Survey submitted successfully!";
                return RedirectToAction(nameof(SubmitSurvey));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while saving the survey. Please try again.");
                return View(dto);
            }
        }

    }
}
