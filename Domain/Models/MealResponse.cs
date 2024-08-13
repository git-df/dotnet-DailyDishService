
namespace Domain.Models
{
    public record MealResponse(
        IEnumerable<MealDto> Meals);
}
