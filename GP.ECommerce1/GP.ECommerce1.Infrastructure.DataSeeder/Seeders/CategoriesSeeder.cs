using GP.ECommerce1.Core.Application.Categories.Commands.CreateCategory;
using GP.ECommerce1.Core.Domain;
using MediatR;

namespace GP.ECommerce1.Infrastructure.DataSeeder.Seeders;

public class CategoriesSeeder
{
    private readonly IMediator _mediator;

    public CategoriesSeeder(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task SeedSql()
    {
        Console.WriteLine("Seeding Categories");
        List<Category> categories = new();
        foreach (var item in _rawData)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = item["cat_name"],
                ParentId = item["cat_parent"] == "0" ? null : categories[int.Parse(item["cat_parent"]) - 1].Id
            };
            categories.Add(category);
        }

        foreach (var category in categories)
        {
            var command = new CreateCategoryCommand
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = category.ParentId
            };
            await _mediator.Send(command);
        }
        
        Console.WriteLine("Seeding Categories Succeeded");
    }

    private List<Dictionary<string, string>> _rawData = new()
    {
        new() {{"cat_id", "1"}, {"cat_name", "Electronics"}, {"cat_parent", "0"}},
        new() {{"cat_id", "2"}, {"cat_name", "Computers"}, {"cat_parent", "0"}},
        new() {{"cat_id", "3"}, {"cat_name", "Smart Home"}, {"cat_parent", "0"}},
        new() {{"cat_id", "4"}, {"cat_name", "Arts &amp; Crafts"}, {"cat_parent", "0"}},
        new() {{"cat_id", "5"}, {"cat_name", "Automotive"}, {"cat_parent", "0"}},
        new() {{"cat_id", "6"}, {"cat_name", "Baby"}, {"cat_parent", "0"}},
        new() {{"cat_id", "7"}, {"cat_name", "Books"}, {"cat_parent", "0"}},
        new() {{"cat_id", "8"}, {"cat_name", "Women&#039;s Fashion"}, {"cat_parent", "0"}},
        new() {{"cat_id", "9"}, {"cat_name", "Pet Supplies"}, {"cat_parent", "0"}},
        new() {{"cat_id", "10"}, {"cat_name", "Sports &amp; Fitness"}, {"cat_parent", "0"}},
        new() {{"cat_id", "11"}, {"cat_name", "Televisions"}, {"cat_parent", "1"}},
        new() {{"cat_id", "12"}, {"cat_name", "Speakers"}, {"cat_parent", "1"}},
        new() {{"cat_id", "13"}, {"cat_name", "Cameras"}, {"cat_parent", "1"}},
        new() {{"cat_id", "14"}, {"cat_name", "GPS &amp; Navigations"}, {"cat_parent", "1"}},
        new() {{"cat_id", "15"}, {"cat_name", "Video Projectors"}, {"cat_parent", "1"}},
        new() {{"cat_id", "16"}, {"cat_name", "Desktops"}, {"cat_parent", "2"}},
        new() {{"cat_id", "17"}, {"cat_name", "Drives &amp; Storage"}, {"cat_parent", "2"}},
        new() {{"cat_id", "18"}, {"cat_name", "Monitors"}, {"cat_parent", "2"}},
        new() {{"cat_id", "19"}, {"cat_name", "Computer Accessories"}, {"cat_parent", "2"}},
        new() {{"cat_id", "20"}, {"cat_name", "Printers &amp; Ink"}, {"cat_parent", "2"}},
        new() {{"cat_id", "23"}, {"cat_name", "Plugs &amp; Outlets"}, {"cat_parent", "3"}},
        new() {{"cat_id", "24"}, {"cat_name", "Security Cameras &amp; System"}, {"cat_parent", "3"}},
        new() {{"cat_id", "25"}, {"cat_name", "Heating &amp; Cooling"}, {"cat_parent", "3"}},
        new() {{"cat_id", "26"}, {"cat_name", "Vaccums &amp; Mops"}, {"cat_parent", "3"}},
        new() {{"cat_id", "27"}, {"cat_name", "Detectors &amp; Sensors"}, {"cat_parent", "3"}},
        new() {{"cat_id", "30"}, {"cat_name", "Crafting"}, {"cat_parent", "4"}},
        new() {{"cat_id", "31"}, {"cat_name", "Party Decoration &amp; Supplies"}, {"cat_parent", "4"}},
        new() {{"cat_id", "32"}, {"cat_name", "Organization, Storage &amp; Transport"}, {"cat_parent", "4"}},
        new() {{"cat_id", "33"}, {"cat_name", "Scrapbooking &amp; Stamping"}, {"cat_parent", "4"}},
        new() {{"cat_id", "34"}, {"cat_name", "Printmaking"}, {"cat_parent", "4"}},
        new() {{"cat_id", "37"}, {"cat_name", "Oil &amp; Fluids"}, {"cat_parent", "5"}},
        new() {{"cat_id", "38"}, {"cat_name", "Lights &amp; Lighting Accessories"}, {"cat_parent", "5"}},
        new() {{"cat_id", "39"}, {"cat_name", "Paint &amp; Paint Supplies"}, {"cat_parent", "5"}},
        new() {{"cat_id", "40"}, {"cat_name", "Motorcycles &amp; Powersports"}, {"cat_parent", "5"}},
        new() {{"cat_id", "41"}, {"cat_name", "Interior Accessories"}, {"cat_parent", "5"}},
        new() {{"cat_id", "44"}, {"cat_name", "Diapering"}, {"cat_parent", "6"}},
        new() {{"cat_id", "45"}, {"cat_name", "Baby bath, Skin &amp; Grooming"}, {"cat_parent", "6"}},
        new() {{"cat_id", "46"}, {"cat_name", "Baby Care"}, {"cat_parent", "6"}},
        new() {{"cat_id", "47"}, {"cat_name", "Strollers &amp; Accessories"}, {"cat_parent", "6"}},
        new() {{"cat_id", "48"}, {"cat_name", "Baby Stationery"}, {"cat_parent", "6"}},
        new() {{"cat_id", "51"}, {"cat_name", "Indian Language Books"}, {"cat_parent", "7"}},
        new() {{"cat_id", "52"}, {"cat_name", "Children&#039;s Books"}, {"cat_parent", "7"}},
        new() {{"cat_id", "53"}, {"cat_name", "Kindle eBooks"}, {"cat_parent", "7"}},
        new() {{"cat_id", "54"}, {"cat_name", "Fiction Books"}, {"cat_parent", "7"}},
        new() {{"cat_id", "55"}, {"cat_name", "School Textbooks"}, {"cat_parent", "7"}},
        new() {{"cat_id", "58"}, {"cat_name", "Nightwear"}, {"cat_parent", "8"}},
        new() {{"cat_id", "59"}, {"cat_name", "Lingerie"}, {"cat_parent", "8"}},
        new() {{"cat_id", "60"}, {"cat_name", "Ethnic Wear"}, {"cat_parent", "8"}},
        new() {{"cat_id", "61"}, {"cat_name", "Clothing"}, {"cat_parent", "8"}},
        new() {{"cat_id", "62"}, {"cat_name", "Western Wear"}, {"cat_parent", "8"}},
        new() {{"cat_id", "65"}, {"cat_name", "Fish &amp; Aquatic Pets"}, {"cat_parent", "9"}},
        new() {{"cat_id", "66"}, {"cat_name", "Birds"}, {"cat_parent", "9"}},
        new() {{"cat_id", "67"}, {"cat_name", "Cats"}, {"cat_parent", "9"}},
        new() {{"cat_id", "68"}, {"cat_name", "Reptiles &amp; Amphibians"}, {"cat_parent", "9"}},
        new() {{"cat_id", "69"}, {"cat_name", "Small Animals"}, {"cat_parent", "9"}},
        new() {{"cat_id", "72"}, {"cat_name", "Yoga"}, {"cat_parent", "10"}},
        new() {{"cat_id", "73"}, {"cat_name", "Cycling"}, {"cat_parent", "10"}},
        new() {{"cat_id", "74"}, {"cat_name", "Cardio Equipment"}, {"cat_parent", "10"}},
        new() {{"cat_id", "75"}, {"cat_name", "Fitness Accessories"}, {"cat_parent", "10"}},
        new() {{"cat_id", "76"}, {"cat_name", "Cricket"}, {"cat_parent", "10"}},
    };
}