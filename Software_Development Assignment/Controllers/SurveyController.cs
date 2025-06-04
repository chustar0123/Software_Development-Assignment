using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
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
                    LastName = dto.LastName,
                    Email = dto.Email,
                    ContactNumber = dto.ContactNumber,
                    DateOfBirth = dto.DateOfBirth ?? DateTime.MinValue

                };

                _context.Respondents.Add(respondent);
                await _context.SaveChangesAsync();

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

                if (dto.Ratings != null && dto.Ratings.Any())
                {
                    foreach (var rating in dto.Ratings)
                    {
                        _context.Ratings.Add(new Rating
                        {
                            RespondentId = respondent.Id,
                            Question = rating.Question,
                            Rating1 = rating.Rating.Value
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



        [HttpGet]
        public async Task<IActionResult> SurveyResults()
        {
            var respondents = await _context.Respondents.ToListAsync();
            var totalSurveys = respondents.Count;

            if (totalSurveys == 0)
            {
                var emptyResults = new SurveyResultsDTO
                {
                    TotalSurveys = 0,
                    AverageAge = 0,
                    OldestAge = 0,
                    YoungestAge = 0,
                    PercentagePizza = 0,
                    PercentagePasta = 0,
                    PercentagePapWors = 0,
                    AverageWatchMovies = 0,
                    AverageListenRadio = 0,
                    AverageEatOut = 0,
                    AverageWatchTV = 0
                };
                return View(emptyResults);
            }

            var today = DateTime.Today;
            var ages = respondents.Select(r =>
                today.Year - r.DateOfBirth.Year -
                (r.DateOfBirth.Date > today.AddYears(-(today.Year - r.DateOfBirth.Year)) ? 1 : 0)).ToList();

            var favoriteFoods = await _context.FavoriteFoods.ToListAsync();
            var ratings = await _context.Ratings.ToListAsync();

            var percentagePizza = favoriteFoods.Count(f => f.FoodName == "Pizza") * 100.0 / totalSurveys;
            var percentagePasta = favoriteFoods.Count(f => f.FoodName == "Pasta") * 100.0 / totalSurveys;
            var percentagePapWors = favoriteFoods.Count(f => f.FoodName == "Pap and Wors") * 100.0 / totalSurveys;

            double GetAverageRating(string question) =>
                ratings.Where(r => r.Question == question).Select(r => r.Rating1).DefaultIfEmpty(0).Average();

            var surveyResultsDTO = new SurveyResultsDTO
            {
                TotalSurveys = totalSurveys,
                AverageAge = Math.Round(ages.Average(), 1),
                OldestAge = ages.Max(),
                YoungestAge = ages.Min(),

                PercentagePizza = Math.Round(percentagePizza, 1),
                PercentagePasta = Math.Round(percentagePasta, 1),
                PercentagePapWors = Math.Round(percentagePapWors, 1),

                AverageWatchMovies = Math.Round(GetAverageRating("I like to watch Movies"), 1),
                AverageListenRadio = Math.Round(GetAverageRating("I like to listen to radio"), 1),
                AverageEatOut = Math.Round(GetAverageRating("I like to eat out"), 1),
                AverageWatchTV = Math.Round(GetAverageRating("I like to watch TV"), 1)
            };

            return View(surveyResultsDTO);
        }


    }
}
